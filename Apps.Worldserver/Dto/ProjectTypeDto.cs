namespace Apps.Worldserver.Dto;
public class ProjectTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public LocaleDto SourceLocale { get; set; }
    public bool DueDateRequired { get; set; }
    public bool AttributeAvailability { get; set; }
    public DefaultWorkflow DefaultWorkflow { get; set; }
}

