namespace Apps.Worldserver.Dto;
public class TaskStepDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string DueDate { get; set; }
    public DateTime StartDate { get; set; }
    public int ExpectedDuration { get; set; }
    public string Type { get; set; }
    public List<WorkflowTransition> WorkflowTransitions { get; set; }
    public string TypeName { get; set; }
    public DateTime CreationDate { get; set; }
    public TaskWorkflowStep WorkflowStep { get; set; }
}

public class TaskWorkflowStep
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public int IdInWorkflow { get; set; }
    public int ExpectedDuration { get; set; }
    public bool Updatable { get; set; }
}

public class WorkflowTransition
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }
}

