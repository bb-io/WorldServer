using Apps.Worldserver.Constants;
using Apps.Worldserver.Dto;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
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
        if (string.IsNullOrEmpty(response.Content))
        {
            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                return new PluginApplicationException($"Request failed with status code {response.StatusCode}");
            }
            
            return new PluginApplicationException(response.ErrorMessage);
        }
        
        var firstError = TryDeserialize<WorldserverError>(response.Content)?.Errors?.FirstOrDefault();
        if (firstError != null)
        {
            return new PluginApplicationException(
                $"Error type: {firstError.Type}; Message: {firstError.Message}");
        }

        return new PluginApplicationException($"Request failed with status code {response.StatusCode}. Content: {response.Content}");
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

    public static Uri GetUri(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentials)
    {
        return new Uri($"{authenticationCredentials.First(x => x.KeyName == CredsNames.Url).Value.TrimEnd('/')}/ws-api");
    }

    public string ObtainSessionToken(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentials)
    {
        var username = authenticationCredentials.First(x => x.KeyName == CredsNames.Username).Value;
        var password = authenticationCredentials.First(x => x.KeyName == CredsNames.Password).Value;

        var request = new RestRequest("/v2/login", Method.Post);
        request.AddBody(new
        {
            username,
            password
        });
        var response = ExecuteWithErrorHandling(request).GetAwaiter().GetResult();
        var authResponse = TryDeserialize<AuthResponseDto>(response.Content);
        if (string.IsNullOrWhiteSpace(authResponse?.SessionId))
        {
            throw new PluginMisconfigurationException(
                $"Failed to log in to WorldServer. Login outcome: {authResponse?.LoginOutcome ?? "unknown"}. " +
                "Please check the instance URL, username and password in the connection.");
        }

        return authResponse.SessionId;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        RestResponse restResponse = await ExecuteAsync(request);
        if (!restResponse.IsSuccessStatusCode)
        {
            throw ConfigureErrorException(restResponse);
        }

        try
        {
            var errorsWrapper = JsonConvert.DeserializeObject<WorldserverErrorWrapper>(restResponse.Content!)!;
            if (errorsWrapper?.Status == "ERROR" && 
                errorsWrapper.Errors != null && 
                errorsWrapper.Errors.Any())
            {
                var firstError = errorsWrapper.Errors.First();
                throw new PluginMisconfigurationException($"Error type: {firstError.Type}.\nMessage: {firstError.Message}");
            }
            else if (errorsWrapper?.Status == "ERROR" &&
                errorsWrapper.Response != null &&
                errorsWrapper.Response.Any() &&
                errorsWrapper.Response.First().Errors != null &&
                errorsWrapper.Response.First().Errors.Any())
            {
                var firstError = errorsWrapper.Response.First().Errors.First();
                throw new PluginMisconfigurationException($"Error type: {firstError.Type}.\nMessage: {firstError.Message}");
            }           
        }
        catch (Exception exception) when (exception is not PluginMisconfigurationException)
        {
        }

        return restResponse;
    }

    private static T? TryDeserialize<T>(string? content) where T : class
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}