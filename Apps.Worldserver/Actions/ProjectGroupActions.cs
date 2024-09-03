using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.ProjectGroups.Request;
using Apps.Worldserver.Models.ProjectGroups.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.Actions;

[ActionList]
public class ProjectGroupActions : WorldserverInvocable
{
    public ProjectGroupActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Search project groups", Description = "Search project groups")]
    public async Task<SearchProjectGroupsResponse> SearchProjectGroups([ActionParameter] SearchProjectGroupsRequest searchProjectGroupsRequest)
    {
        var request = new WorldserverRequest($"/projectGroups/search", Method.Post);
        var filters = new List<FieldFilterV2Dto>();

        if (!string.IsNullOrEmpty(searchProjectGroupsRequest.Name))
            filters.Add(new("name", "TEXT", searchProjectGroupsRequest.Name));

        var response = await Client.Paginate<ProjectGroupDto>(request);
        return new(response);
    }
}

