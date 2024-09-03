using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class QualityModelDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public QualityModelDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var qualityModelRequest = new WorldserverRequest("/qualityModels", Method.Get);
        var qualityModelResponse = await Client.ExecuteWithErrorHandling<CollectionResponseDto<QualityModelDto>>(qualityModelRequest);
        return qualityModelResponse.Items
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}
