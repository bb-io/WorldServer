using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Clients.Request;
public class GetClientRequest
{
    [Display("Client ID")]
    [DataSource(typeof(ClientDataHandler))]
    public string ClientId { get; set; }
}

