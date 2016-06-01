using System;

namespace LogHub
{
    public class LogSource
    {
        private string _prefix;

        public Guid Id { get; set; }

        public string Topic { get; set; }

        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                if (!value.EndsWith("."))
                {
                    value += ".";
                }

                _prefix = value;
            }
        }

        public string ConnectionString { get; set; }

        public bool Enabled { get; set; }

        public LogSource()
        {
            Id = Guid.NewGuid();
            Enabled = true;
        }
    }
}
