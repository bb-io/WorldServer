namespace Apps.Worldserver.Dto;
public class JobDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public string Engine { get; set; }
    public List<JobLink> Links { get; set; }
}

public class JobLink
{
    public string Rel { get; set; }
    public string Href { get; set; }
}

