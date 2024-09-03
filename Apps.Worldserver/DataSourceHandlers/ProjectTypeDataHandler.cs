using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Clients.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;
public class ProjectTypeDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    private GetClientRequest ClientRequest {  get; set; }

    public ProjectTypeDataHandler(InvocationContext invocationContext, 
        [ActionParameter] GetClientRequest clientRequest) : base(invocationContext)
    {
        ClientRequest = clientRequest;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(ClientRequest.ClientId))
            throw new ArgumentException("Please specify client first.");

        var typesRequest = new WorldserverRequest("/v2/projectTypes", Method.Get);
        typesRequest.AddQueryParameter("clientId", ClientRequest.ClientId);
        
        var types = await Client.Paginate<ProjectTypeDto>(typesRequest);
        return types
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(50)
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}

