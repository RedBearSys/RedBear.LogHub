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

                if (
                    !namespaceManager.SubscriptionExists(_source.Topic,
                        $"{Environment.UserDomainName}-{Environment.MachineName}-{Environment.UserName}"))
                {
                    var description = new SubscriptionDescription(_source.Topic,
                        $"{Environment.UserDomainName}-{Environment.MachineName}-{Environment.UserName}")
                    {
                        AutoDeleteOnIdle = TimeSpan.FromMinutes(15)
                    };
                    namespaceManager.CreateSubscription(description);
                }

                _client = SubscriptionClient.CreateFromConnectionString(_source.ConnectionString, _source.Topic,
                    $"{Environment.UserDomainName}-{Environment.MachineName}-{Environment.UserName}");

                var options = new OnMessageOptions();
                options.ExceptionReceived += Options_ExceptionReceived;

                _client.OnMessage(message =>
                {
                    var json = (JObject) JsonConvert.DeserializeObject(message.GetBody<string>());

                    var logEvent = new LogEventInfo
                    {
                        LoggerName =
                            !string.IsNullOrEmpty(_source.Prefix)
                                ? _source.Prefix + json["LoggerName"].Value<string>()
                                : json["LoggerName"].Value<string>(),
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
                }, options);
            }
            catch (TimeoutException ex)
            {
                var message =
                    $@"
There's a problem communicating with Azure Service Bus.

Check your connection string, internet access, firewalls and any anti-virus or anti-malware applications that might be blocking access.

Exception:

{ex
                        .Message}
";
                LogManager.GetLogger("LogHub").Warn(message.Trim());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("LogHub").Error(ex);
            }
        }

        private void Options_ExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (_client != null && !_client.IsClosed)
            {
                LogManager.GetLogger("LogHub").Error(e.Exception);
            }
        }

        public void Stop()
        {
            _client?.Close();
        }
    }
}
