using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class TaskDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public TaskDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var projectsRequest = new WorldserverRequest("/tasks/search", Method.Post);

        var filters = new List<FieldFilterV1Dto>();
        if (!string.IsNullOrEmpty(context.SearchString))
            filters.Add(new("assets", "like", $"%{context.SearchString}%"));

        projectsRequest.AddBody(new
        {
            @operator = "and",
            filters
        });

        var tasks = await Client.Paginate<TaskDto>(projectsRequest);
        return tasks.Take(50).ToDictionary(k => k.Id.ToString(), v => $"{Path.GetFileName(v.Assets.First())} ({v.Project.Name})");
    }
}
