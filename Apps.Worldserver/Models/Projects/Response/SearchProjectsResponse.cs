using Apps.Worldserver.Dto;

namespace Apps.Worldserver.Models.Projects.Response;

public class SearchProjectsResponse
{
    public SearchProjectsResponse(IEnumerable<ProjectDto> collection)
    {
        Projects = collection.ToList();
    }

    public List<ProjectDto> Projects { get; set; }
}
