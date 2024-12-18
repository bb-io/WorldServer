using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;

public class QualityModelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Error severities")]
    public List<string> ErrorSeverities { get; set; }

    [Display("Error types")]
    public List<string> ErrorTypes { get; set; }
}
