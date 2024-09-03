using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Worldserver.Models.ProjectGroups.Request;
public class CreateProjectGroupRequest
{
    public string Name { get; set; }

    [Display("Project type ID")]
    [DataSource(typeof(ProjectTypeDataHandler))]
    public string ProjectTypeId { get; set; }
    public List<FileReference> Files {  get; set; }

    [DataSource(typeof(LocaleDataHandler))]
    public List<string> Locales {  get; set; } 
}

