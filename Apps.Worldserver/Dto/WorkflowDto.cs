using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;

public class WorkflowDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public List<Step> Steps { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Invalidate asset on completion")]
    public bool InvalidateAssetOnCompletion { get; set; }

    [Display("Finish step")]
    public FinishStep FinishStep { get; set; }
}

public class Action
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Scope { get; set; }

    [Display("Is deprecated")]
    public bool IsDeprecated { get; set; }
    public List<object> Returns { get; set; }
}

public class FinishStep
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }

    [Display("Outbound transitions")]
    public List<object> OutboundTransitions { get; set; }

    [Display("Inbound transitions")]
    public List<InboundTransition> InboundTransitions { get; set; }

    [Display("Id in workflow")]
    public int IdInWorkflow { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Workflow step attributes")]
    public List<object> WorkflowStepAttributes { get; set; }
    public bool Updatable { get; set; }
}

public class InboundTransition
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }
}

public class OutboundTransition
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }
}

public class Step
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }

    [Display("Outbound transitions")]
    public List<OutboundTransition> OutboundTransitions { get; set; }

    [Display("Inbound transitions")]
    public List<InboundTransition> InboundTransitions { get; set; }

    [Display("Id in workflow")]
    public int IdInWorkflow { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Workflow step attributes")]
    public List<WorkflowStepAttribute> WorkflowStepAttributes { get; set; }
    public bool Updatable { get; set; }
    public string Lock { get; set; }

    [Display("Step type")]
    public string StepType { get; set; }
    public Action Action { get; set; }
}

public class WorkflowStepAttribute
{
    [Display("Workflow step attribute type")]
    public int WorkflowStepAttrType { get; set; }

    [Display("Int value")]
    public int IntValue { get; set; }

    [Display("Boolean value")]
    public bool BooleanValue { get; set; }
}


