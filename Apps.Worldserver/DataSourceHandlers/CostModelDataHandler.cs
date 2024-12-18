﻿using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class CostModelDataHandler : WorldserverInvocable, IAsyncDataSourceItemHandler
{
    public CostModelDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var costModelRequest = new WorldserverRequest("/v2/costModels", Method.Get);
        var costModels = await Client.ExecuteWithErrorHandling<CollectionResponseDto<CostModelDto>>(costModelRequest);

        return costModels.Items.Where(costModel => string.IsNullOrWhiteSpace(context.SearchString) ||
            costModel.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(50)
            .Select(costModel => new DataSourceItem(costModel.Id.ToString(), costModel.Name));
    }
}
