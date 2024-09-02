﻿using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Models.Projects.Response;
using Blackbird.Applications.Sdk.Common;
using Apps.Worldserver.Models.Projects.Request;

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
