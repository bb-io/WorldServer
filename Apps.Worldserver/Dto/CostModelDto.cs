namespace Apps.Worldserver.Dto;

public class CostModelDto
{
    public int Id { get; set; }
    public LocaleDto SourceLocale { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public int AccessType { get; set; }
    public ScopingConfiguration ScopingConfiguration { get; set; }
}
