using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.DataSourceHandlers.Enum;
public class AssetLocationTypeDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            {"SOURCE", "Source" },
            {"TARGET", "Target" },
            {"SOURCE_TARGET", "Source and target" },
        };
    }
}

