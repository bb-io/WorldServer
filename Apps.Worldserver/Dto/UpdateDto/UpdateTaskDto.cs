using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto.UpdateDto;
public class UpdateTaskDto
{
    public int Id { get; set; }
    public int? Priority { get; set; }

    [Display("Due date")]
    public DateTime? DueDate { get; set; }

    [Display("Expected duration")]
    public int? ExpectedDuration { get; set; }
}

