using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Worldserver.Polling.Models
{
    public class ProjectMemory
    {
        public DateTime? LastPollingTime { get; set; }

        public bool Triggered { get; set; }

        public int LastProjectTotal { get; set; }

        public List<ProjectMemoryItem>? LastProjects { get; set; } = new();
    }

    public class ProjectMemoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
