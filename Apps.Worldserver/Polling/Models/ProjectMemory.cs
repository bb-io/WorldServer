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
    }
}
