using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;
public class ClientDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public ClientDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var clientRequest = new WorldserverRequest("/v2/clients", Method.Get);
        var clients = await Client.ExecuteWithErrorHandling<CollectionResponseDto<ClientDto>>(clientRequest);
        return clients.Items
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}

