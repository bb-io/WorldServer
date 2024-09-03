namespace Apps.Worldserver.Dto;
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
    public List<Group> Groups { get; set; }
    public List<Workgroup> Workgroups { get; set; }
    public List<WsLocale> WsLocales { get; set; }
    public List<UserClient> Clients { get; set; }
    public List<WorkflowRole> WorkflowRoles { get; set; }
}

public class UserClient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<ClientContact> ClientContacts { get; set; }
    public List<UserProjectType> ProjectTypes { get; set; }
    public string Description { get; set; }
}

public class ClientContact
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ClientId { get; set; }
    public bool AccountManager { get; set; }
}

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}

public class UserProjectType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool DueDateRequired { get; set; }
    public bool AttributeAvailability { get; set; }
}

public class WorkflowRole
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public string Description { get; set; }
}

public class WsLocale
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public Language Language { get; set; }
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}


