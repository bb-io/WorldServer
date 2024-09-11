namespace Apps.Worldserver.Dto;
public class ExportStartDto
{
    public string Status { get; set; }
    public ExportStartResponse Response { get; set; }
}

public class ExportStartResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
}

