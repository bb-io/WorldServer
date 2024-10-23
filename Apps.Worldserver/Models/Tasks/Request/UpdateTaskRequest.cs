using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Models.Tasks.Request;
public class UpdateTaskRequest
{
    public int? Priority { get; set; }
    public DateTime? DueDate { get; set; }

    [Display("Expected duration")]
    public int? ExpectedDuration { get; set; }
}

