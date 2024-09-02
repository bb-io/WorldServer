using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Models.Projects.Response;
using Blackbird.Applications.Sdk.Common;
using Apps.Worldserver.Models.Projects.Request;
using Newtonsoft.Json;
using Apps.Worldserver.Constants;

namespace Apps.Worldserver.Actions;

[ActionList]
public class ProjectActions : WorldserverInvocable
{
    public ProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Search projects", Description = "Search projects")]
    public async Task<SearchProjectsResponse> SearchProjects([ActionParameter] SearchProjectsRequest searchProjectsRequest)
    {
        var request = new WorldserverRequest($"/projects", Method.Get);

        if (!string.IsNullOrEmpty(searchProjectsRequest.LocaleId))
            request.AddQueryParameter("localeId", searchProjectsRequest.LocaleId);

        var response = await Client.ExecuteWithErrorHandling<CollectionResponseDto<ProjectDto>>(request);
        return new(response);
    }

    [Action("Get project", Description = "Get project")]
    public async Task<ProjectDto> GetProject([ActionParameter] GetProjectRequest projectRequest)
    {
        var request = new WorldserverRequest($"/projects/{projectRequest.ProjectId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ProjectDto>(request);
        return response;
    }

    [Action("Update project", Description = "Update project")]
    public async Task UpdateProject([ActionParameter] GetProjectRequest projectRequest,
        [ActionParameter] UpdateProjectRequest updateRequest)
    {
        var getProjectRequest = new WorldserverRequest($"/projects/{projectRequest.ProjectId}", Method.Get);
        var getProjectResponse = await Client.ExecuteWithErrorHandling<ProjectDto>(getProjectRequest);

        var updateDto = new UpdateProjectDto() { Id = int.Parse(projectRequest.ProjectId) };

        if (updateRequest.DueDate.HasValue)
            updateDto.DueDate = updateRequest.DueDate;

        if (!string.IsNullOrEmpty(updateRequest.CostModelId))
        {
            updateDto.CostModel = new()
            {
                Id = int.Parse(updateRequest.CostModelId),
                SourceLocale = new() { Id = getProjectResponse.SourceLocale.Id }
            };
        }

        if (!string.IsNullOrEmpty(updateRequest.QualityModelId))
            updateDto.QualityModel = new() { Id = int.Parse(updateRequest.QualityModelId) };

        var request = new WorldserverRequest($"/projects/{projectRequest.ProjectId}", Method.Patch);
        request.AddJsonBody(JsonConvert.SerializeObject(new[] { updateDto }, JsonConfig.Settings));
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Complete project step", Description = "Complete project step")]
    public async Task CompleteProjectStep(
        [ActionParameter] GetProjectRequest projectRequest,
        [ActionParameter] GetWorkflowStepRequest workflowStepRequest,
        [ActionParameter] GetWorkflowStepTransitionRequest transitionRequest)
    {
        var completeStepRequest = new WorldserverRequest($"/projects/complete", Method.Post);
        completeStepRequest.AddBody(new[]
        {
            new
            {
                id = int.Parse(projectRequest.ProjectId),
                transitionId = int.Parse(transitionRequest.TransitionId)
            }
        });
        await Client.ExecuteWithErrorHandling(completeStepRequest);
    }
}
