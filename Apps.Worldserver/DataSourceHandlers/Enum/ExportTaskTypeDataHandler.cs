using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.DataSourceHandlers.Enum;
public class ExportTaskTypeDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            {"UNKNOWN", "Unknown" },
            {"WSXZ", "WSXZ" },
            {"BDX", "BDX" },
            {"XLIFF", "XLIFF" },
            {"REGULATOR_BUNDLE", "Regulator bundle" },
            {"XLZ", "XLZ" },
        };
    }
}

