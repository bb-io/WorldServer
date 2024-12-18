using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.DataSourceHandlers.Enum;
public class ExportTaskSegmentationDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            {"NONE", "None" },
            {"ICE", "ICE" },
            {"ICE100", "ICE and 100%" },
        };
    }
}

