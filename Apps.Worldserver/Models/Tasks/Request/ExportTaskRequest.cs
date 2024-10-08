﻿using Apps.Worldserver.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Worldserver.Models.Tasks.Request;
public class ExportTaskRequest
{
    [StaticDataSource(typeof(ExportTaskTypeDataHandler))]
    public string Type { get; set; }

    [Display("Allow split and merge")]
    public bool? AllowSplitAndMerge { get; set; }

    [Display("Translation memory info")]
    [StaticDataSource(typeof(ExportTaskTMDataHandler))]
    public string? TMInfo { get; set; }

    [Display("Segment exclusion")]
    [StaticDataSource(typeof(ExportTaskSegmentationDataHandler))]
    public string? SegmentExclusion { get; set; }

    [Display("TD Filter ID")]
    public int? TdFilterId { get; set; }

    [Display("TM Filter ID")]
    public int? TmFilterId { get; set; }
}

