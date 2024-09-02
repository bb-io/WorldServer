using Apps.Worldserver.Invocables;
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
}
