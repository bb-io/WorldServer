namespace Apps.Worldserver.Dto;

public class TaskDto
{
    public int Id { get; set; }
    public Project Project { get; set; }
    public List<string> Assets { get; set; }
    public string TaskNumInProject { get; set; }
    public int OldestAncestorId { get; set; }
    public Workflow Workflow { get; set; }
    public Creator Creator { get; set; }
    public int Priority { get; set; }
    public DateTime CreationDate { get; set; }
    public int TotalWords { get; set; }
    public string DueDate { get; set; }
    public int ExpectedDuration { get; set; }
    public int UpdatePending { get; set; }
    public int TranslatedWords { get; set; }
    public int PendingWords { get; set; }
    public int FinishedWords { get; set; }
    public CurrentTaskStep CurrentTaskStep { get; set; }
    public List<TaskStepDto> Steps { get; set; }
    public StatusDto Status { get; set; }
    public int CompletionStatus { get; set; }
    public List<Assignee> Assignees { get; set; }
    public string Comment { get; set; }
    public string DetailedComment { get; set; }
    public bool IncludeInCost { get; set; }
    public List<string> TargetAssets { get; set; }
    public LocaleDto TargetLocale { get; set; }
}
