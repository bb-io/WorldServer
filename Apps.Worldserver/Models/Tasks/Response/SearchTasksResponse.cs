using Apps.Worldserver.Dto;

namespace Apps.Worldserver.Models.Tasks.Response;

public class SearchTasksResponse
{
    public SearchTasksResponse(IEnumerable<TaskDto> collection)
    {
        Tasks = collection.ToList();
    }

    public List<TaskDto> Tasks { get; set; }
}
