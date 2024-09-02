namespace Apps.Worldserver.Dto;

public class QualityModelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> ErrorSeverities { get; set; }
    public List<string> ErrorTypes { get; set; }
}
