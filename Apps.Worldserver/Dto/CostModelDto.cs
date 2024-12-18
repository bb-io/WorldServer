using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;

public class CostModelDto
{
    public int Id { get; set; }

    [Display("Source locale")]
    public LocaleDto SourceLocale { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }

    [Display("Access type")]
    public int AccessType { get; set; }

    [Display("Scoping configuration")]
    public ScopingConfiguration ScopingConfiguration { get; set; }
}
