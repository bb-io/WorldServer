namespace Apps.Worldserver.Dto;

public class LocaleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public Language Language { get; set; }
    public string LanguageName { get; set; }
    public string Encoding { get; set; }
}
public class Language
{
    public int Id { get; set; }
    public string LanguageCode { get; set; }
    public string CountryCode { get; set; }
    public string IsoCode { get; set; }
    public string Locale { get; set; }
}
