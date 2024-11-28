using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class ProjectGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Source locale")]
    public SourceLocale SourceLocale { get; set; }
    public Workgroup Workgroup { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Due date")]
    public DateTime DueDate { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Active tasks")]
    public int ActiveTasks { get; set; }

    [Display("Total tasks")]
    public int TotalTasks { get; set; }

    [Display("Pending tasks")]
    public int PendingTasks { get; set; }

    [Display("Claimed tasks")]
    public int ClaimedTasks { get; set; }

    [Display("Assigned tasks")]
    public int AssignedTasks { get; set; }

    [Display("Total words")]
    public int TotalWords { get; set; }

    [Display("Translated progress")]
    public double TranslatedProgress { get; set; }

    [Display("Finished progress")]
    public double FinishedProgress { get; set; }

    [Display("Translated words")]
    public int TranslatedWords { get; set; }

    [Display("In progress words")]
    public int InprogressWords { get; set; }

    [Display("Pending words")]
    public int PendingWords { get; set; }

    [Display("Finished words")]
    public int FinishedWords { get; set; }
    public Creator Creator { get; set; }
    public int Errors { get; set; }

    [Display("Total auto error tasks")]
    public int TotalAutoErrorTasks { get; set; }

    [Display("Total overdue tasks")]
    public int TotalOverdueTasks { get; set; }

    [Display("Total task issues")]
    public int TotalTaskIssues { get; set; }

    [Display("Total TCR tasks")]
    public int TotalTcrTasks { get; set; }
    public Client Client { get; set; }

    [Display("Project type")]
    public ProjectTypeDto ProjectType { get; set; }
    public List<ProjectDto> Projects { get; set; }

    [Display("Total projects")]
    public int TotalProjects { get; set; }

    [Display("Canceled tasks")]
    public int CanceledTasks { get; set; }

    [Display("Completed tasks")]
    public int CompletedTasks { get; set; }
    public Cost Cost { get; set; }
    public string Type { get; set; }

    [Display("Last modified date")]
    public DateTime LastModifiedDate { get; set; }
    public string Status { get; set; }
}

public class Cost
{
    [Display("Last modified date")]
    public double CostByPricePerWord { get; set; }
    public Currency Currency { get; set; }

    [Display("Grand total cost")]
    public double GrandTotalCost { get; set; }

    [Display("Manual adjustment")]
    public double ManualAdjustment { get; set; }
}

public class CostModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }

    [Display("Access type")]
    public int AccessType { get; set; }
}

public class Currency
{
    public string Code { get; set; }
    public string Symbol { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
}

public class SourceLocale
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public Language Language { get; set; }

    [Display("Language name")]
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}

public class TargetLocale
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Language name")]
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}

public class Workgroup2
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}



