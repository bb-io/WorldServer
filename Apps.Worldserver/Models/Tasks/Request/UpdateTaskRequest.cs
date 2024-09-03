namespace Apps.Worldserver.Models.Tasks.Request;
public class UpdateTaskRequest
{
    public int? Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public int? ExpectedDuration { get; set; }
}

