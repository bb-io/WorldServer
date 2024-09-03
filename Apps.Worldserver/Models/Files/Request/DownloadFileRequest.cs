using Apps.Worldserver.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.Models.Files.Request;
public class DownloadFileRequest
{
    [StaticDataSource(typeof(AssetLocationTypeDataHandler))]
    [Display("Asset location type")]
    public string? AssetLocationType { get; set; }
}

