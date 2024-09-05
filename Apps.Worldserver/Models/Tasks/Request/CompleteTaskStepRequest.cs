using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Tasks.Request;
public class CompleteTaskStepRequest
{
    [DataSource(typeof(TaskStepTransitionDataHandler))]
    [Display("Transition ID")]
    public string TransitionId { get; set; }

    public string? Comment { get; set; }
}

