﻿using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;
public class UserForTaskAssigningDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public UserForTaskAssigningDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var usersRequest = new WorldserverRequest("/v2/users", Method.Get);
        var users = await Client.Paginate<UserDto>(usersRequest);
        return users
            .Where(str => context.SearchString is null || str.Username.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Where(x => x.Workgroups.Any())
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Username);
    }
}

