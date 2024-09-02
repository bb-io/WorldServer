using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Projects.Request;

public class UpdateProjectRequest
{
    [Display("Due date")]
    public DateTime? DueDate { get; set; }

    [Display("Cost model ID")]
    [DataSource(typeof(CostModelDataHandler))]
    public string? CostModelId { get; set; }

    [Display("Quality model ID")]
    [DataSource(typeof(QualityModelDataHandler))]
    public string? QualityModelId { get; set; }
}
