using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.DataSourceHandlers.Enum;
public class ExportTaskTMDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            {"CONTENT", "Content" },
            {"LINK", "Link" },
            {"CONTENT_AND_LINK", "Content and link" },
        };
    }
}

