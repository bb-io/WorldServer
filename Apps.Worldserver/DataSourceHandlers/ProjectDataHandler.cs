﻿using Apps.Worldserver.Api;
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
        var projectsRequest = new WorldserverRequest("/projects", Method.Get);
        var projects = await Client.ExecuteWithErrorHandling<CollectionResponseDto<ProjectDto>>(projectsRequest);
        return projects.Items
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}
