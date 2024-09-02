namespace Apps.Worldserver.Dto;

public class WorkflowDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public List<Step> Steps { get; set; }
    public int ExpectedDuration { get; set; }
    public bool InvalidateAssetOnCompletion { get; set; }
    public FinishStep FinishStep { get; set; }
}

public class Action
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Scope { get; set; }
    public bool IsDeprecated { get; set; }
    public List<object> Returns { get; set; }
}

public class FinishStep
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public List<object> OutboundTransitions { get; set; }
    public List<InboundTransition> InboundTransitions { get; set; }
    public int IdInWorkflow { get; set; }
    public int ExpectedDuration { get; set; }
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
    public List<OutboundTransition> OutboundTransitions { get; set; }
    public List<InboundTransition> InboundTransitions { get; set; }
    public int IdInWorkflow { get; set; }
    public int ExpectedDuration { get; set; }
    public List<WorkflowStepAttribute> WorkflowStepAttributes { get; set; }
    public bool Updatable { get; set; }
    public string Lock { get; set; }
    public string StepType { get; set; }
    public Action Action { get; set; }
}

public class WorkflowStepAttribute
{
    public int WorkflowStepAttrType { get; set; }
    public int IntValue { get; set; }
    public bool BooleanValue { get; set; }
}


