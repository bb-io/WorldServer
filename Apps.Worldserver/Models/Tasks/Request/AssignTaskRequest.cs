﻿using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Tasks.Request;
public class AssignTaskRequest
{
    [Display("Assigned user IDs")]
    [DataSource(typeof(UserDataHandler))]
    public List<string>? AssignedUserIds { get; set; }

    [Display("Assigned role IDs")]
    [DataSource(typeof(WorkflowRoleDataHandler))]
    public List<string>? AssignedRoleIds { get; set; }

    [Display("Task step IDs")]
    public List<string>? TaskStepIds { get; set; }
}
