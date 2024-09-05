using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Tasks.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;
public class TaskStepTransitionDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    private GetTaskRequest TaskRequest { get; set; }

    public TaskStepTransitionDataHandler(InvocationContext invocationContext, [ActionParameter] GetTaskRequest taskRequest) : base(invocationContext)
    {
        TaskRequest = taskRequest;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(TaskRequest.TaskId))
            throw new ArgumentException("Please, specify task first");

        var request = new WorldserverRequest($"/v2/tasks/{TaskRequest.TaskId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<TaskDto>(request);

        return response.CurrentTaskStep.WorkflowTransitions
           .Where(str => context.SearchString is null || str.Text.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
           .Take(50)
           .ToDictionary(k => k.Id.ToString(), v => v.Text);
    }
}

