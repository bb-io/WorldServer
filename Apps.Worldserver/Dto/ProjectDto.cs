namespace Apps.Worldserver.Dto;

public class ProjectDto
{
    public int Id { get; set; }
    public int ProjectGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public LocaleDto TargetLocale { get; set; }
    public LocaleDto SourceLocale { get; set; }
    public List<TaskDto> Tasks { get; set; }
    public Workgroup Workgroup { get; set; }
    public Workflow Workflow { get; set; }
    public DateTime CreationDate { get; set; }
    public int ActiveTasks { get; set; }
    public int TotalTasks { get; set; }
    public int ClaimedTasks { get; set; }
    public int UnclaimedTasks { get; set; }
    public int CanceledTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int AssignedTasks { get; set; }
    public int UnassignedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int TotalWords { get; set; }
    public int TranslatedWords { get; set; }
    public double TranslatedProgress { get; set; }
    public int PendingWords { get; set; }
    public int FinishedWords { get; set; }
    public double FinishedProgress { get; set; }
    public Creator Creator { get; set; }
    public string ScopingMode { get; set; }
    public int Errors { get; set; }
    public int TotalAutoErrorTasks { get; set; }
    public string Status { get; set; }
    public Client Client { get; set; }
    public ProjectTypeDto ProjectType { get; set; }
}

public class Assignee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool Disabled { get; set; }
}

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
}

public class Creator
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}

public class CurrentTaskStep
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int ExpectedDuration { get; set; }
    public string Type { get; set; }
    public string TypeName { get; set; }
    public DateTime CreationDate { get; set; }
    public List<WorkflowTransition> WorkflowTransitions { get; set; }
}

public class DefaultWorkflow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int ExpectedDuration { get; set; }
    public bool InvalidateAssetOnCompletion { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public int ProjectGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public int ActiveTasks { get; set; }
    public int TotalTasks { get; set; }
    public int ClaimedTasks { get; set; }
    public int UnclaimedTasks { get; set; }
    public int AssignedTasks { get; set; }
    public int UnassignedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int TotalWords { get; set; }
    public int TranslatedWords { get; set; }
    public int PendingWords { get; set; }
    public int FinishedWords { get; set; }
    public int Errors { get; set; }
    public string Status { get; set; }
}

public class ScopingConfiguration
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDefault { get; set; }
}

public class StatusDto
{
    public string Status { get; set; }
    public string DisplayText { get; set; }
}

public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int ExpectedDuration { get; set; }
    public bool InvalidateAssetOnCompletion { get; set; }
}

public class Workgroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}
