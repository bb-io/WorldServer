using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;

public class TaskDto
{
    public int Id { get; set; }
    public Project Project { get; set; }
    public List<string> Assets { get; set; }

    [Display("Task num in project")]
    public string TaskNumInProject { get; set; }

    [Display("Oldest ancestor ID")]
    public int OldestAncestorId{ get; set; }
    public Workflow Workflow { get; set; }
    public Creator Creator { get; set; }
    public int Priority { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Total words")]
    public int TotalWords { get; set; }

    [Display("Due date")]
    public string DueDate { get; set; }

    [Display("Expected duration")]
    public int ExpectedDuration { get; set; }

    [Display("Update pending")]
    public int UpdatePending { get; set; }

    [Display("Translated words")]
    public int TranslatedWords { get; set; }

    [Display("Pending words")]
    public int PendingWords { get; set; }

    [Display("Finished words")]
    public int FinishedWords { get; set; }

    [Display("Current task step")]
    public CurrentTaskStep CurrentTaskStep { get; set; }
    public List<TaskStepDto> Steps { get; set; }
    public StatusDto Status { get; set; }

    [Display("Completion status")]
    public int CompletionStatus { get; set; }
    public List<Assignee> Assignees { get; set; }
    public string Comment { get; set; }

    [Display("Detailed comment")]
    public string DetailedComment { get; set; }

    [Display("Include in cost")]
    public bool IncludeInCost { get; set; }

    [Display("Target assets")]
    public List<string> TargetAssets { get; set; }

    [Display("Target locale")]
    public LocaleDto TargetLocale { get; set; }
}
