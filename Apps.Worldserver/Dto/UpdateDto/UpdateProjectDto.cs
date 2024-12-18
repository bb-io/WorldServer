using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto.UpdateDto;

public class UpdateProjectDto
{
    public int Id { get; set; }

    [Display("Due date")]
    public DateTime? DueDate { get; set; }

    [Display("Cost model")]
    public UpdateCostModel CostModel { get; set; }

    [Display("Quality model")]
    public UpdateQualityModel QualityModel { get; set; }
}

public class UpdateCostModel
{
    public int Id { get; set; }

    [Display("Source locale")]
    public UpdateSourceLocale SourceLocale { get; set; }
}

public class UpdateQualityModel
{
    public int Id { get; set; }
}

public class UpdateSourceLocale
{
    public int Id { get; set; }
}
