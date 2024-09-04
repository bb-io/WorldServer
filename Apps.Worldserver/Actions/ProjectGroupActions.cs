using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Dto.CreateDto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Clients.Request;
using Apps.Worldserver.Models.ProjectGroups.Request;
using Apps.Worldserver.Models.ProjectGroups.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Worldserver.Actions;

[ActionList]
public class ProjectGroupActions : WorldserverInvocable
{
    private readonly IFileManagementClient _fileManagementClient;
    public ProjectGroupActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("Search project groups", Description = "Search project groups")]
    public async Task<SearchProjectGroupsResponse> SearchProjectGroups([ActionParameter] SearchProjectGroupsRequest searchProjectGroupsRequest)
    {
        var request = new WorldserverRequest($"/v2/projectGroups/search", Method.Post);
        var filters = new List<FieldFilterV2Dto>();

        if (!string.IsNullOrEmpty(searchProjectGroupsRequest.Name))
            filters.Add(new("name", "TEXT", searchProjectGroupsRequest.Name));

        request.AddBody(filters);
        var response = await Client.Paginate<ProjectGroupDto>(request);
        return new(response);
    }

    [Action("Get project group", Description = "Get project group")]
    public async Task<ProjectGroupDto> GetProjectGroup([ActionParameter] GetProjectGroupRequest projectGroupRequest)
    {
        var request = new WorldserverRequest($"/v2/projectGroups/{projectGroupRequest.ProjectGroupId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ProjectGroupDto>(request);
        return response;
    }

    [Action("Create project group", Description = "Create project group")]
    public async Task<int> CreateProjectGroup(
        [ActionParameter] GetClientRequest clientRequest,
        [ActionParameter] CreateProjectGroupRequest projectGroupRequest)
    {
        var createProjectGroupDto = new CreateProjectGroupDto();

        var fileActions = new FileActions(InvocationContext, _fileManagementClient);
        foreach(var file in projectGroupRequest.Files)
        {
            var uploadedFile = await fileActions.UploadFileWithCustomForm(file);
            createProjectGroupDto.SystemFiles.Add(uploadedFile.InternalName);
        }
        createProjectGroupDto.Name = projectGroupRequest.Name;
        createProjectGroupDto.ClientId = clientRequest.ClientId;
        createProjectGroupDto.ProjectTypeId = projectGroupRequest.ProjectTypeId;

        foreach(var locale in projectGroupRequest.Locales)
        {
            createProjectGroupDto.Locales.Add(new()
            {
                Id = locale,
                DueDate = DateTime.UtcNow.AddYears(1),
            });
        }

        var request = new WorldserverRequest($"/v2/projectGroups/create", Method.Post);
        request.AddBody(new[] { createProjectGroupDto });
        var result = await Client.ExecuteWithErrorHandling<CreateProjectGroupResponse>(request);
        if (result.Response.Any() && result.Response.First().Response.HasValue)
            return result.Response.First().Response.Value;
        else if(result.Response.Any() && result.Response.First().Warnings != null && result.Response.First().Warnings.Any())
            throw new ArgumentException(result.Response.First().Warnings.First().Message);
        throw new ArgumentException("Unknown error");
    }
}

