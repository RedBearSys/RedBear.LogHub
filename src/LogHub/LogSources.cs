using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using NLog.Config;

namespace LogHub
{
    public class LogSources
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

        private readonly List<Listener> _listeners = new List<Listener>();
        public static readonly LogSources Instance = new LogSources();

        public List<LogSource> Sources { get; private set; } = new List<LogSource>();

        public void Load()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LogHub");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = Path.Combine(folder, "NLog.xml");

            if (!File.Exists(file))
            {
                File.Copy("example.xml", file);
            }
            
            LogManager.Configuration = new XmlLoggingConfiguration(file);

            file = Path.Combine(folder, "LogHub.json");

            if (File.Exists(file))
            {
                Sources = JsonConvert.DeserializeObject<List<LogSource>>(File.ReadAllText(file), _settings);
            }
        }

        public void Save()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LogHub");
            File.WriteAllText(Path.Combine(folder, "LogHub.json"), JsonConvert.SerializeObject(Sources, _settings));
        }

        public void Add(LogSource source)
        {
            Sources.Add(source);
            Save();
            StartListener(source);
        }

        public void Delete(LogSource source)
        {
            var listener = _listeners.FirstOrDefault(x => x.Id == source.Id);

            if (listener != null)
            {
                listener.Stop();
                _listeners.Remove(listener);
            }

            Sources.Remove(source);
            Save();
        }

        public void Update(LogSource source)
        {
            Save();

            var listener = _listeners.FirstOrDefault(x => x.Id == source.Id);

            if (listener != null)
            {
                listener.Stop();
                _listeners.Remove(listener);
            }

            StartListener(source);
        }

        public void StartListeners()
        {
            foreach (var source in Sources)
            {
                StartListener(source);
            }
        }

        public void StopListeners()
        {
            foreach (var listener in _listeners)
            {
                listener.Stop();
            }

            _listeners.Clear();
        }

        private void StartListener(LogSource source)
        {
            if (source.Enabled)
            {
                var listener = new Listener(source);
                listener.Start();
                _listeners.Add(listener);
            }
        }
    }
}
