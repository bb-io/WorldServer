using Apps.Worldserver.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.Models.Tasks.Request;
public class ClaimTaskRequest
{
    [Display("Cost management")]
    [StaticDataSource(typeof(TaskCostManagementDataHandler))]
    public string? CostManagement { get; set; }
}

