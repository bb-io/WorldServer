namespace Apps.Worldserver.Dto;
public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<ClientContact> ClientContacts { get; set; }
    public List<ProjectTypeDto> ProjectTypes { get; set; }
    public string Description { get; set; }
}

