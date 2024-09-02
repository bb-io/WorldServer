using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Projects.Request;

public class GetWorkflowStepTransitionRequest
{
    [Display("Transition ID")]
    [DataSource(typeof(WorkflowStepTransitionDataHandler))]
    public string TransitionId { get; set; }
}
