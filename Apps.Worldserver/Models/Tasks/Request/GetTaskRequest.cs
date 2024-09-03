using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Tasks.Request;

public class GetTaskRequest
{
    [Display("Task ID")]
    [DataSource(typeof(TaskDataHandler))]
    public string TaskId { get; set; }
}
