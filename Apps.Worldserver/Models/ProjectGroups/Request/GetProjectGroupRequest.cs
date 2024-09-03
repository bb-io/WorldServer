using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.ProjectGroups.Request;
public class GetProjectGroupRequest
{
    [Display("Project group ID")]
    [DataSource(typeof(ProjectGroupDataHandler))]
    public string ProjectGroupId { get; set; }
}

