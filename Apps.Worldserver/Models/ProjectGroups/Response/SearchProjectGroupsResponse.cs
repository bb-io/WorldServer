using Apps.Worldserver.Dto;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Models.ProjectGroups.Response;
public class SearchProjectGroupsResponse
{
    public SearchProjectGroupsResponse(IEnumerable<ProjectGroupDto> collection)
    {
        ProjectGroups = collection.ToList();
    }

    [Display("Project groups")]
    public List<ProjectGroupDto> ProjectGroups { get; set; }
}

