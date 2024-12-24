using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Polling.Models
{
    public class CompletedTasksResponse
    {
        [Display("Completed tasks")]
        public List<TaskCompletedResponse> Tasks { get; set; } = new();
    }

    public class TaskCompletedResponse
    {
        [Display("Task ID")]
        public int Id { get; set; }

        [Display("Task status")]
        public string? Status { get; set; }

        [Display("Task completion time")]
        public DateTime CompletionDetected { get; set; }
    }
}
