using Apps.Worldserver.Dto;

namespace Apps.Worldserver.Models.Projects.Response;

public class SearchProjectsResponse
{
    public SearchProjectsResponse(CollectionResponseDto<ProjectDto> collection)
    {
        Projects = collection.Items.ToList();
    }

    public List<ProjectDto> Projects { get; set; }
}
