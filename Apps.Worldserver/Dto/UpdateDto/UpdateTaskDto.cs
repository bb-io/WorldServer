namespace Apps.Worldserver.Dto.UpdateDto;
public class UpdateTaskDto
{
    public int Id { get; set; }
    public int? Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public int? ExpectedDuration { get; set; }
}

