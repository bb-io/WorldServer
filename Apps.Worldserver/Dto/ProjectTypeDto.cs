using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class ProjectTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Source locale")]
    public LocaleDto SourceLocale { get; set; }

    [Display("Due date required")]
    public bool DueDateRequired { get; set; }

    [Display("Attribute availability")]
    public bool AttributeAvailability { get; set; }

    [Display("Default workflow")]
    public DefaultWorkflow DefaultWorkflow { get; set; }
}

