using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;

public class LocaleDto
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
public class Language
{
    public int Id { get; set; }

    [Display("Language name")]
    public string LanguageCode { get; set; }

    [Display("Country code")]
    public string CountryCode { get; set; }

    [Display("Iso code")]
    public string IsoCode { get; set; }
    public string Locale { get; set; }
}
