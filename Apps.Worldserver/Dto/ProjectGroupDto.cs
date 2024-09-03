namespace Apps.Worldserver.Dto;
public class ProjectGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SourceLocale SourceLocale { get; set; }
    public Workgroup Workgroup { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public int ExpectedDuration { get; set; }
    public int ActiveTasks { get; set; }
    public int TotalTasks { get; set; }
    public int PendingTasks { get; set; }
    public int ClaimedTasks { get; set; }
    public int AssignedTasks { get; set; }
    public int TotalWords { get; set; }
    public double TranslatedProgress { get; set; }
    public double FinishedProgress { get; set; }
    public int TranslatedWords { get; set; }
    public int InprogressWords { get; set; }
    public int PendingWords { get; set; }
    public int FinishedWords { get; set; }
    public Creator Creator { get; set; }
    public int Errors { get; set; }
    public int TotalAutoErrorTasks { get; set; }
    public int TotalOverdueTasks { get; set; }
    public int TotalTaskIssues { get; set; }
    public int TotalTcrTasks { get; set; }
    public Client Client { get; set; }
    public ProjectTypeDto ProjectType { get; set; }
    public List<Project> Projects { get; set; }
    public int TotalProjects { get; set; }
    public int CanceledTasks { get; set; }
    public int CompletedTasks { get; set; }
    public Cost Cost { get; set; }
    public string Type { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string Status { get; set; }
}

public class Cost
{
    public double CostByPricePerWord { get; set; }
    public Currency Currency { get; set; }
    public double GrandTotalCost { get; set; }
    public double ManualAdjustment { get; set; }
}

public class CostModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public int AccessType { get; set; }
}

public class Currency
{
    public string Code { get; set; }
    public string Symbol { get; set; }
    public string DisplayName { get; set; }
}

public class SourceLocale
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public Language Language { get; set; }
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}

public class TargetLocale
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}

public class Workgroup2
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}



