using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }

    [Display("First name")]
    public string FirstName { get; set; }

    [Display("Last name")]
    public string LastName { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
    public List<Group> Groups { get; set; }
    public List<Workgroup> Workgroups { get; set; }

    [Display("Ws locales")]
    public List<WsLocale> WsLocales { get; set; }
    public List<UserClient> Clients { get; set; }

    [Display("Workflow roles")]
    public List<WorkflowRoleDto> WorkflowRoles { get; set; }
}

public class UserClient
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Client contacts")]
    public List<ClientContact> ClientContacts { get; set; }

    [Display("Project types")]
    public List<UserProjectType> ProjectTypes { get; set; }
    public string Description { get; set; }
}

public class ClientContact
{
    public int Id { get; set; }

    [Display("User ID")]
    public int UserId { get; set; }

    [Display("Client ID")]
    public int ClientId { get; set; }

    [Display("Account manager")]
    public bool AccountManager { get; set; }
}

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public bool Disabled { get; set; }
}

public class UserProjectType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Display("Due date required")]
    public bool DueDateRequired { get; set; }

    [Display("Attribute availability")]
    public bool AttributeAvailability { get; set; }
}

public class WsLocale
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }
    public Language Language { get; set; }

    [Display("Language name")]
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}


