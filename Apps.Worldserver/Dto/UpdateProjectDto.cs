namespace Apps.Worldserver.Dto;

public class UpdateProjectDto
{
    public int Id { get; set; }
    public DateTime? DueDate { get; set; }
    public UpdateCostModel CostModel { get; set; }
    public UpdateQualityModel QualityModel { get; set; }
}

public class UpdateCostModel
{
    public int Id { get; set; }
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
