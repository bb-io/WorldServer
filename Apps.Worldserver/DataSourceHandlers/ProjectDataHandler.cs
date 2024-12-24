using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class ProjectDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public ProjectDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var projectsRequest = new WorldserverRequest("/v2/projectGroups/search", Method.Post);
        var filters = new List<FieldFilterV1Dto>();

        if (!string.IsNullOrEmpty(context.SearchString))
            filters.Add(new("projects(name)", "like", context.SearchString));

        projectsRequest.AddBody(new
        {
            @operator = "and",
            filters
        });
        var projects = await Client.Paginate<ProjectGroupDto>(projectsRequest);

        var jsonResult = System.Text.Json.JsonSerializer.Serialize(projects, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });

        return projects.SelectMany(x => x.Projects)
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}
