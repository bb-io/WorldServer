using Apps.Worldserver.Constants;
using Apps.Worldserver.Dto;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Worldserver.Api;

public class WorldserverClient : BlackBirdRestClient
{
    private const int Limit = 30;

    public WorldserverClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentials) : base(new()
    {
        BaseUrl = GetUri(authenticationCredentials)
    })
    {
        this.AddDefaultHeader("token", ObtainSessionToken(authenticationCredentials));
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var errors = JsonConvert.DeserializeObject<WorldserverError>(response.Content!)!;

        if(errors.Errors.Any())
            return new($"Error type: {errors.Errors.First().Type}.\nMessage: {errors.Errors.First().Message}");

        return new("Unknown error");
    }

    public async Task<List<T>> Paginate<T>(RestRequest request)
    {
        var offset = 0;
        var baseUrl = request.Resource;

        var result = new List<T>();
        CollectionResponseDto<T> response;
        do
        {
            request.Resource = baseUrl
                .SetQueryParameter("offset", offset.ToString())
                .SetQueryParameter("limit", Limit.ToString());

            response = await ExecuteWithErrorHandling<CollectionResponseDto<T>>(request);
            result.AddRange(response.Items);

            offset += Limit;
        } while (result.Count < response.Total);

        return result;
    }

    private static Uri GetUri(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentials)
    {
        return new Uri($"{authenticationCredentials.First(x => x.KeyName == CredsNames.Url).Value.TrimEnd('/')}/ws-api/v2");
    }

    private string ObtainSessionToken(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentials)
    {
        var username = authenticationCredentials.First(x => x.KeyName == CredsNames.Username).Value;
        var password = authenticationCredentials.First(x => x.KeyName == CredsNames.Password).Value;

        var request = new RestRequest("/login", Method.Post);
        request.AddBody(new
        {
            username,
            password
        });
        return this.Execute<AuthResponseDto>(request).Data.SessionId;
    }
}