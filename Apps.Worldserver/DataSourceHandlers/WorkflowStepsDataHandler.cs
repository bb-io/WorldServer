using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Projects.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class WorkflowStepsDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    private GetProjectRequest ProjectRequest {  get; set; }

    public WorkflowStepsDataHandler(InvocationContext invocationContext,
        [ActionParameter] GetProjectRequest projectRequest) : base(invocationContext)
    {
        ProjectRequest = projectRequest;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(ProjectRequest.ProjectId))
            throw new ArgumentException("Please specify project first");

        var projectRequest = new WorldserverRequest($"/projects/{ProjectRequest.ProjectId}", Method.Get);
        var projectResponse = await Client.ExecuteWithErrorHandling<ProjectDto>(projectRequest);

        var workflowRequest = new WorldserverRequest($"/workflows/{projectResponse.Workflow.Id}", Method.Get);
        var workflowResponse = await Client.ExecuteWithErrorHandling<WorkflowDto>(workflowRequest);

        return workflowResponse.Steps
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}
