using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Client contacts")]
    public List<ClientContact> ClientContacts { get; set; }

    [Display("Project types")]
    public List<ProjectTypeDto> ProjectTypes { get; set; }
    public string Description { get; set; }
}

