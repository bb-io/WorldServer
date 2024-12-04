using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;
public class ClientDataHandler : WorldserverInvocable, IAsyncDataSourceItemHandler
{
    public ClientDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var clientRequest = new WorldserverRequest("/v2/clients", Method.Get);
        var clients = await Client.ExecuteWithErrorHandling<CollectionResponseDto<ClientDto>>(clientRequest);

        return clients.Items
           .Where(client => string.IsNullOrWhiteSpace(context.SearchString) ||
                            client.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
           .Take(50)
           .Select(client => new DataSourceItem(client.Id.ToString(), client.Name));
    }
}

