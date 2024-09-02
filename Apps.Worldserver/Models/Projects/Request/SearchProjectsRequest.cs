using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Projects.Request;

public class SearchProjectsRequest
{
    [Display("Source locale ID")]
    [DataSource(typeof(LocaleDataHandler))]
    public string? LocaleId {  get; set; } 
}
