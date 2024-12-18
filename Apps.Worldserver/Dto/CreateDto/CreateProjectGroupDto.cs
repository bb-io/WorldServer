using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto.CreateDto;
public class CreateProjectGroupDto
{
    public CreateProjectGroupDto()
    {
        SystemFiles = new();
        Locales = new();
    }
    public string Name { get; set; }

    [Display("Client ID")]
    public string ClientId { get; set; }

    [Display("Project type ID")]
    public string ProjectTypeId { get; set; }

    [Display("System files")]
    public List<string> SystemFiles { get; set; }
    public List<CreateProjectGroupLocale> Locales { get; set; }
}

public class CreateProjectGroupLocale
{
    public string Id { get; set; }

    [Display("Due date")]
    public DateTime DueDate { get; set; }
}

