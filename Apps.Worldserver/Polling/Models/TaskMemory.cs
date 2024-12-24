using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Polling.Models
{
    public class TaskMemory
    {
        public DateTime? LastPollingTime { get; set; }

        public bool Triggered { get; set; }

        public List<string> CompletedTaskIds { get; set; } = new();
    }
}
