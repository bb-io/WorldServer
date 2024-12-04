using System.Text.Json.Serialization;
using Apps.Worldserver.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.Models.Tasks.Request
{
    public class ExportAllTasksRequest
    {
        [StaticDataSource(typeof(ExportTaskTypeDataHandler))]
        public string Type { get; set; }

        [Display("Allow split and merge")]
        public bool? AllowSplitAndMerge { get; set; }

        [Display("Segment exclusion")]
        [StaticDataSource(typeof(ExportTaskSegmentationDataHandler))]
        public string? SegmentExclusion { get; set; }
    }
}
