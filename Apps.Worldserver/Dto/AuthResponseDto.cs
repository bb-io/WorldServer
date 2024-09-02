namespace Apps.Worldserver.Dto;

public class AuthResponseDto
{
    public string SessionId { get; set; }
    public int InstanceId { get; set; }
    public string ExpirationTime { get; set; }
    public UserDetails UserDetails { get; set; }
    public long LastUpdateTime { get; set; }
    public int DaysToPwdExpire { get; set; }
    public string LoginOutcome { get; set; }
}

public class AuthLanguage
{
    public int Id { get; set; }
    public string LanguageCode { get; set; }
    public string CountryCode { get; set; }
    public string IsoCode { get; set; }
    public string Locale { get; set; }
}

public class UserDetails
{
    public string UserType { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Fingerprint { get; set; }
    public string RegionalSettingsLocale { get; set; }
    public AuthLanguage Language { get; set; }
}
