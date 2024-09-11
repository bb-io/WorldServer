using Apps.Worldserver.Api;
using Apps.Worldserver.Constants;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Dto.UpdateDto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Tasks.Request;
using Apps.Worldserver.Models.Tasks.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace Apps.Worldserver.Actions;

[ActionList]
public class TaskActions : WorldserverInvocable
{
    private readonly IFileManagementClient _fileManagementClient;
    public TaskActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("Search tasks", Description = "Search tasks")]
    public async Task<SearchTasksResponse> SearchTasks([ActionParameter] SearchTasksRequest searchTasksRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks/search", Method.Post);

        var filters = new List<FieldFilterV1Dto>();
        if (!string.IsNullOrEmpty(searchTasksRequest.ProjectId))
            filters.Add(new("project.id", "eq", searchTasksRequest.ProjectId));
        if (!string.IsNullOrEmpty(searchTasksRequest.ProjectId))
            filters.Add(new("assets", "like", $"%{searchTasksRequest.AssetName}%"));

        request.AddBody(new
        {
            @operator = "and",
            filters
        });
        var response = await Client.Paginate<TaskDto>(request);
        return new(response);
    }

    [Action("Get task", Description = "Get task")]
    public async Task<TaskDto> GetTask([ActionParameter] GetTaskRequest taskRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks/{taskRequest.TaskId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<TaskDto>(request);
        return response;
    }

    [Action("Claim task", Description = "Claim task")]
    public async Task ClaimTask([ActionParameter] GetTaskRequest taskRequest,
        [ActionParameter] ClaimTaskRequest claimRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks/claim", Method.Post);
        request.AddBody(new[] { new { id = int.Parse(taskRequest.TaskId) } });
        await Client.ExecuteWithErrorHandling(request);

        if (!string.IsNullOrEmpty(claimRequest.CostManagement))
        {
            if(claimRequest.CostManagement == "include")
            {
                var includeCostRequest = new WorldserverRequest($"/v2/tasks/includeCost", Method.Post);
                includeCostRequest.AddBody(new[]
                {
                    new { id = int.Parse(taskRequest.TaskId) }
                });
                await Client.ExecuteWithErrorHandling(includeCostRequest);
            }
            else
            {
                var excludeCostRequest = new WorldserverRequest($"/v2/tasks/excludeCost", Method.Post);
                excludeCostRequest.AddBody(new[]
                {
                    new { id = int.Parse(taskRequest.TaskId) }
                });
                await Client.ExecuteWithErrorHandling(excludeCostRequest);
            }
        }
    }

    [Action("Unclaim task", Description = "Unclaim task")]
    public async Task UnclaimTask([ActionParameter] GetTaskRequest taskRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks/unclaim", Method.Post);
        request.AddBody(new[] { new { id = int.Parse(taskRequest.TaskId) } });
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update task", Description = "Update task")]
    public async Task UpdateTask([ActionParameter] GetTaskRequest taskRequest,
        [ActionParameter] UpdateTaskRequest updateRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks", Method.Patch);

        var updateDto = new UpdateTaskDto() { Id = int.Parse(taskRequest.TaskId) };
        if (updateRequest.Priority.HasValue)
            updateDto.Priority = updateRequest.Priority.Value;
        if (updateRequest.DueDate.HasValue)
            updateDto.DueDate = updateRequest.DueDate.Value;
        if (updateRequest.ExpectedDuration.HasValue)
            updateDto.ExpectedDuration = updateRequest.ExpectedDuration.Value;

        request.AddBody(new[] { updateDto });
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Assign task", Description = "Assign task")]
    public async Task AssignTask([ActionParameter] GetTaskRequest taskRequest,
        [ActionParameter] AssignTaskRequest assignTaskRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks/changeAssignees", Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(new[]
        {
            new
            {
                id = int.Parse(taskRequest.TaskId),
                assignedUserIds = assignTaskRequest?.AssignedUserIds?.Select(x => int.Parse(x)).ToList(),
                assignedRoleIds = assignTaskRequest?.AssignedRoleIds?.Select(x => int.Parse(x)).ToList(),
                taskStepIds = assignTaskRequest?.TaskStepIds?.Select(x => int.Parse(x)).ToList(),
            }
        }, JsonConfig.Settings));
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Complete task step", Description = "Complete task step")]
    public async Task CompleteTaskStep([ActionParameter] GetTaskRequest taskRequest,
        [ActionParameter] CompleteTaskStepRequest completeStepRequest)
    {
        var request = new WorldserverRequest($"/v2/tasks/complete", Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(new[]
        {
            new
            {
                id = int.Parse(taskRequest.TaskId),
                transitionId = int.Parse(completeStepRequest.TransitionId),
                commnet = completeStepRequest.Comment
            }
        }, JsonConfig.Settings));
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Export task", Description = "Export task")]
    public async Task<FileReference> ExportTask([ActionParameter] GetTaskRequest taskRequest,
        [ActionParameter] ExportTaskRequest exportTaskRequest)
    {
        var startExportRequest = new WorldserverRequest($"/v2/tasks/export", Method.Post);

        var zippedFileTypes = new[] { "XLIFF", "BDX" };
        var ignoreAllowSplitAndMerge = new[] { "BDX" }.Contains(exportTaskRequest.Type);

        startExportRequest.AddJsonBody(JsonConvert.SerializeObject(new
        {
            ids = new[] { int.Parse(taskRequest.TaskId) },
            type = exportTaskRequest.Type,
            allowSplitAndMerge = ignoreAllowSplitAndMerge ? (bool?)null : (exportTaskRequest.AllowSplitAndMerge ?? false),
            tmInfo = exportTaskRequest.TMInfo,
            segmentExclusion = exportTaskRequest.SegmentExclusion ?? "NONE",
            tdFilterId = exportTaskRequest.TdFilterId,
            tmFilterId = exportTaskRequest.TmFilterId
        }, JsonConfig.Settings));
        var startExportResponse = await Client.ExecuteWithErrorHandling<ExportStartDto>(startExportRequest);

        var pollExportStatusRequest = new WorldserverRequest($"/v2/jobs/{startExportResponse.Response.Id}", Method.Get);
        var pollExportStatusResponse = await Client.ExecuteWithErrorHandling<JobDto>(pollExportStatusRequest);
        while (pollExportStatusResponse.Status == "STILL_IN_PROGRESS" || pollExportStatusResponse.Status == "NOT_STARTED")
        {
            await Task.Delay(1000);
            pollExportStatusResponse = await Client.ExecuteWithErrorHandling<JobDto>(pollExportStatusRequest);
        }

        var fileLink = pollExportStatusResponse.Links.First().Href.Split("ws-api")[1];
        var downloadExportedTaskRequest = new WorldserverRequest(fileLink, Method.Get);
        downloadExportedTaskRequest.AddHeader("Accept", "*/*");
        var downloadExportedTaskResponse = await Client.ExecuteAsync(downloadExportedTaskRequest);

        if (zippedFileTypes.Contains(exportTaskRequest.Type))
            return await UnzipFile(downloadExportedTaskResponse.RawBytes);

        using var stream = new MemoryStream(downloadExportedTaskResponse.RawBytes);
        var contentDisposition = ContentDispositionHeaderValue.Parse(downloadExportedTaskResponse.ContentHeaders.First(x => x.Name == "Content-Disposition").Value.ToString());
        var file = await _fileManagementClient.UploadAsync(stream, MediaTypeNames.Application.Octet, contentDisposition.FileNameStar);
        return file;
    }

    [Action("Import task", Description = "Import task")]
    public async Task ImportTask([ActionParameter] ImportTaskRequest importTaskRequest)
    {
        var fileActions = new FileActions(InvocationContext, _fileManagementClient);
        var uploadedAsset = await fileActions.UploadFileWithCustomForm(importTaskRequest.File);

        var startImportRequest = new WorldserverRequest($"/v2/tasks/import", Method.Post);
        startImportRequest.AddBody(new
        {
            file = uploadedAsset.InternalName,
            updateTM = importTaskRequest.UpdateTm ?? true
        });
        var startImportResponse = await Client.ExecuteWithErrorHandling<ExportStartDto>(startImportRequest);

        var pollImportStatusRequest = new WorldserverRequest($"/v2/jobs/{startImportResponse.Response.Id}", Method.Get);
        var pollImportStatusResponse = await Client.ExecuteWithErrorHandling<JobDto>(pollImportStatusRequest);
        while (pollImportStatusResponse.Status == "STILL_IN_PROGRESS" || pollImportStatusResponse.Status == "NOT_STARTED")
        {
            await Task.Delay(1000);
            pollImportStatusResponse = await Client.ExecuteWithErrorHandling<JobDto>(pollImportStatusRequest);
        }
    }

    private async Task<FileReference> UnzipFile(byte[] zippedFile)
    {
        using var fileSteaam = new MemoryStream(zippedFile);
        var files = await fileSteaam.GetFilesFromZip();
        var file = await _fileManagementClient.UploadAsync(files.First().FileStream, MediaTypeNames.Application.Octet, files.First().UploadName);
        return file;
    }
}
