using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Projects.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class WorkflowStepTransitionDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    private GetProjectRequest ProjectRequest { get; set; }
    private GetWorkflowStepRequest StepRequest { get; set; }

    public WorkflowStepTransitionDataHandler(InvocationContext invocationContext,
        [ActionParameter] GetProjectRequest projectRequest,
        [ActionParameter] GetWorkflowStepRequest stepRequest) : base(invocationContext)
    {
        ProjectRequest = projectRequest;
        StepRequest = stepRequest;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(ProjectRequest.ProjectId))
            throw new ArgumentException("Please specify project first");

        if (string.IsNullOrEmpty(StepRequest.WorkflowStepId))
            throw new ArgumentException("Please specify workflow step first");

        var projectRequest = new WorldserverRequest($"/v2/projects/{ProjectRequest.ProjectId}", Method.Get);
        var projectResponse = await Client.ExecuteWithErrorHandling<ProjectDto>(projectRequest);

        var workflowRequest = new WorldserverRequest($"/v2/workflows/{projectResponse.Workflow.Id}", Method.Get);
        var workflowResponse = await Client.ExecuteWithErrorHandling<WorkflowDto>(workflowRequest);

        return workflowResponse.Steps.First(x => x.Id == int.Parse(StepRequest.WorkflowStepId)).OutboundTransitions
            .Where(str => context.SearchString is null || str.Text.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Text);
    }
}
