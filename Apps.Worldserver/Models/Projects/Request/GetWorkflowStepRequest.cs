using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Projects.Request;

public class GetWorkflowStepRequest
{
    [Display("Workflow step ID")]
    [DataSource(typeof(WorkflowStepsDataHandler))]
    public string WorkflowStepId { get; set; }
}
