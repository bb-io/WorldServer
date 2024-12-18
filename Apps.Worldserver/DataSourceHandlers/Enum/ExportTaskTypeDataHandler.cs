using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.DataSourceHandlers.Enum;
public class ExportTaskTypeDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            {"WSXZ", "WSXZ" },
            {"BDX", "Bilingual DOCX" },
            {"XLIFF", "XLIFF" },
        };
    }
}

