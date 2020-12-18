using System.Collections.Generic;

namespace xperters.configurations.Settings
{
    public class ServiceBusSettings
    {
        public string NameSpace { get; set; }
        public int ConcurrentThreads { get; set; }

        public Dictionary<string, Queue> Queues { get; set; }

        public ServiceBusSettings()
        {
            Queues = new Dictionary<string, Queue>();
        }
    }

    public class Queue
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}