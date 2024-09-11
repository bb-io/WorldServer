using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Worldserver.Models.Tasks.Request;
public class ImportTaskRequest
{
    public FileReference File { get; set; }

    [Display("Update TM")]
    public bool? UpdateTm { get; set; }
}

