namespace Apps.Worldserver.Dto.CreateDto;
public class CreateProjectGroupDto
{
    public CreateProjectGroupDto()
    {
        SystemFiles = new();
        Locales = new();
    }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string ProjectTypeId { get; set; }
    public List<string> SystemFiles { get; set; }
    public List<CreateProjectGroupLocale> Locales { get; set; }
}

public class CreateProjectGroupLocale
{
    public string Id { get; set; }
    public DateTime DueDate { get; set; }
}

