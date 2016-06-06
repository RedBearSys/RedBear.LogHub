using System;
using System.Collections.Generic;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace LogHub.Target
{
    [Target("LogHubTarget")]
    public sealed class LogHubTarget : TargetWithLayout
    {
        private TopicClient _client;

        [RequiredParameter]
        public string ConnectionString { get; set; }

        [RequiredParameter]
        public string Topic { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var json = JsonConvert.SerializeObject(logEvent);
            CreateClient();
            _client.Send(new BrokeredMessage(json));
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            var json = JsonConvert.SerializeObject(logEvent.LogEvent);

            try
            {
                CreateClient();
                _client.Send(new BrokeredMessage(json));
                logEvent.Continuation(null);
            }
            catch (Exception ex)
            {
                logEvent.Continuation(ex);
            }
        }

        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            var messages = new List<BrokeredMessage>();
            var pendingContinuations = new List<AsyncContinuation>();
            Exception lastException = null;

            foreach (var logEvent in logEvents)
            {
                var json = JsonConvert.SerializeObject(logEvent.LogEvent);
                messages.Add(new BrokeredMessage(json));
                pendingContinuations.Add(logEvent.Continuation);
            }

            try
            {
                CreateClient();
                _client.SendBatch(messages);
            }
            catch (Exception ex)
            {
                lastException = ex;
            }

            foreach (var cont in pendingContinuations)
            {
                cont(lastException);
            }

            pendingContinuations.Clear();
        }

        private void CreateClient()
        {
            if (_client == null)
            {
                var nsm = NamespaceManager.CreateFromConnectionString(ConnectionString);

                if (!nsm.TopicExists(Topic))
                {
                    var td = new TopicDescription(Topic) {DefaultMessageTimeToLive = TimeSpan.FromMinutes(1)};
                    nsm.CreateTopic(td);
                }

                _client = TopicClient.CreateFromConnectionString(ConnectionString, Topic);
            }
        }
    }
}