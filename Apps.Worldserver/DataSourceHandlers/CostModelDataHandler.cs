using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class CostModelDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public CostModelDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var costModelRequest = new WorldserverRequest("/costModels", Method.Get);
        var costModels = await Client.ExecuteWithErrorHandling<CollectionResponseDto<CostModelDto>>(costModelRequest);
        return costModels.Items
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}
