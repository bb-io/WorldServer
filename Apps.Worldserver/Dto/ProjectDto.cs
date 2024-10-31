using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;

public class ProjectDto
{
    public int Id { get; set; }

    [Display("Project group ID")]
    public int ProjectGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Target locale")]
    public LocaleDto TargetLocale { get; set; }

    [Display("Source locale")]
    public LocaleDto SourceLocale { get; set; }
    public List<TaskDto> Tasks { get; set; }
    public Workgroup Workgroup { get; set; }
    public Workflow Workflow { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Active tasks")]
    public int ActiveTasks { get; set; }

    [Display("Total tasks")]
    public int TotalTasks { get; set; }

    [Display("Claimed tasks")]
    public int ClaimedTasks { get; set; }

    [Display("Unclaimed tasks")]
    public int UnclaimedTasks { get; set; }

    [Display("Canceled tasks")]
    public int CanceledTasks { get; set; }

    [Display("Completed tasks")]
    public int CompletedTasks { get; set; }

    [Display("Assigned tasks")]
    public int AssignedTasks { get; set; }

    [Display("Unassigned tasks")]
    public int UnassignedTasks { get; set; }

    [Display("Pending tasks")]
    public int PendingTasks { get; set; }

    [Display("Total words")]
    public int TotalWords { get; set; }

    [Display("Translated words")]
    public int TranslatedWords { get; set; }

    [Display("Translated progress")]
    public double TranslatedProgress { get; set; }

    [Display("Pending words")]
    public int PendingWords { get; set; }

    [Display("Finished words")]
    public int FinishedWords { get; set; }

    [Display("Finished progress")]
    public double FinishedProgress { get; set; }
    public Creator Creator { get; set; }

    [Display("Scoping mode")]
    public string ScopingMode { get; set; }
    public int Errors { get; set; }

    [Display("Total auto error tasks")]
    public int TotalAutoErrorTasks { get; set; }
    public string Status { get; set; }
    public Client Client { get; set; }

    [Display("Project type")]
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

    [Display("Display name")]
    public string DisplayName { get; set; }
    public string Description { get; set; }
}

public class Creator
{
    public int Id { get; set; }
    public string Username { get; set; }

    [Display("First name")]
    public string FirstName { get; set; }

    [Display("Last name")]
    public string LastName { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}

public class CurrentTaskStep
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }
    public string Type { get; set; }

    [Display("Type name")]
    public string TypeName { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Workflow transitions")]
    public List<WorkflowTransition> WorkflowTransitions { get; set; }
}

public class DefaultWorkflow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Invalidate asset on completion")]
    public bool InvalidateAssetOnCompletion { get; set; }
}

public class Project
{
    public int Id { get; set; }

    [Display("Project group ID")]
    public int ProjectGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Active tasks")]
    public int ActiveTasks { get; set; }

    [Display("Total tasks")]
    public int TotalTasks { get; set; }

    [Display("Claimed tasks")]
    public int ClaimedTasks { get; set; }

    [Display("Unclaimed tasks")]
    public int UnclaimedTasks { get; set; }

    [Display("Assigned tasks")]
    public int AssignedTasks { get; set; }

    [Display("Unassigned tasks")]
    public int UnassignedTasks { get; set; }

    [Display("Pending tasks")]
    public int PendingTasks { get; set; }

    [Display("Total words")]
    public int TotalWords { get; set; }

    [Display("Translated words")]
    public int TranslatedWords { get; set; }

    [Display("Pending words")]
    public int PendingWords { get; set; }

    [Display("Finished words")]
    public int FinishedWords { get; set; }
    public int Errors { get; set; }
    public string Status { get; set; }
}

public class ScopingConfiguration
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Is default")]
    public bool IsDefault { get; set; }
}

public class StatusDto
{
    public string Status { get; set; }

    [Display("Display text")]
    public string DisplayText { get; set; }
}

public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Invalidate asset on completion")]
    public bool InvalidateAssetOnCompletion { get; set; }
}

public class Workgroup
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}
