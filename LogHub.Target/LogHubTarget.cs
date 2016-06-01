using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using NLog;
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

        private void CreateClient()
        {
            if (_client == null)
            {
                var nsm = NamespaceManager.CreateFromConnectionString(ConnectionString);

                if (!nsm.TopicExists(Topic))
                {
                    nsm.CreateTopic(Topic);
                }

                _client = TopicClient.CreateFromConnectionString(ConnectionString, Topic);
            }
        }
    }
}