using RestSharp;

namespace Apps.Worldserver.Api;

public class WorldserverRequest : RestRequest
{
    public WorldserverRequest(string resource, Method method) : base(resource, method)
    {
    }
}