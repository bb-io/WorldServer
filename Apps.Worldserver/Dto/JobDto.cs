using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class JobDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }

    [Display("Creation date")]
    public DateTime CreationDate { get; set; }

    [Display("Start date")]
    public DateTime StartDate { get; set; }

    [Display("Last modified date")]
    public DateTime LastModifiedDate { get; set; }

    [Display("Completion date")]
    public DateTime CompletionDate { get; set; }
    public string Engine { get; set; }
    public List<JobLink> Links { get; set; }
}

public class JobLink
{
    public string Rel { get; set; }
    public string Href { get; set; }
}

