using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;
public class ProjectGroupDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public ProjectGroupDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new WorldserverRequest($"/v2/projectGroups/search", Method.Post);
        var filters = new List<FieldFilterV2Dto>();

        if (!string.IsNullOrEmpty(context.SearchString))
            filters.Add(new("name", "TEXT", context.SearchString));

        request.AddBody(filters);
        var projectGroups = await Client.Paginate<ProjectGroupDto>(request);

        return projectGroups
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}

