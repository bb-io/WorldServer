using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class TaskStepDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Due date")]
    public string DueDate { get; set; }

    [Display("Start date")]
    public DateTime StartDate { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }
    public string Type { get; set; }

    [Display("Workflow transitions")]
    public List<WorkflowTransition> WorkflowTransitions { get; set; }

    [Display("Type name")]
    public string TypeName { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Workflow step")]
    public TaskWorkflowStep WorkflowStep { get; set; }
}

public class TaskWorkflowStep
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }

    [Display("Id in workflow")]
    public int IdInWorkflow { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }
    public bool Updatable { get; set; }
}

public class WorkflowTransition
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }
}

