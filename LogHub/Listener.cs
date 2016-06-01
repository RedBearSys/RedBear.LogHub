using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace LogHub
{
    public class Listener
    {
        private readonly LogSource _source;
        private SubscriptionClient _client;

        public Guid Id { get; private set; }

        public Listener(LogSource source)
        {
            Id = source.Id;
            _source = source;
        }

        public void Start()
        {
            try
            {
                var namespaceManager = NamespaceManager.CreateFromConnectionString(_source.ConnectionString);

                if (!namespaceManager.SubscriptionExists(_source.Topic, $"{Environment.UserDomainName}-{Environment.MachineName}-{Environment.UserName}"))
                {
                    namespaceManager.CreateSubscription(_source.Topic, $"{Environment.UserDomainName}-{Environment.MachineName}-{Environment.UserName}");
                }

                _client = SubscriptionClient.CreateFromConnectionString(_source.ConnectionString, _source.Topic, $"{Environment.UserDomainName}-{Environment.MachineName}-{Environment.UserName}");

                _client.OnMessage(message =>
                {
                    var json = (JObject)JsonConvert.DeserializeObject(message.GetBody<string>());

                    var logEvent = new LogEventInfo
                    {
                        LoggerName = !string.IsNullOrEmpty(_source.Prefix) ? _source.Prefix + json["LoggerName"].Value<string>() : json["LoggerName"].Value<string>(),
                        TimeStamp = json["TimeStamp"].Value<DateTime>(),
                        Level = LogLevel.FromString(json["Level"]["Name"].Value<string>()),
                        Message = json["FormattedMessage"].Value<string>()
                    };

                    try
                    {
                        var stack = json["StackTrace"].Value<StackTrace>();
                        if (stack != null) logEvent.SetStackTrace(stack, json["UserStrackFrame"].Value<int>());
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        var ex = json["Exception"].Value<Exception>();
                        if (ex != null) logEvent.Exception = ex;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        var parameters = json["Parameters"].Values<object>();
                        if (parameters != null) logEvent.Parameters = parameters.ToArray();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    LogManager.GetLogger(logEvent.LoggerName).Log(logEvent);
                });
            }
            catch (MessagingException ex)
            {
                LogManager.GetLogger("LogHub").Error(ex);
            }
        }

        public void Stop()
        {
            _client.Close();
        }
    }
}
