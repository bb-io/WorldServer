using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.Worldserver.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var request = new WorldserverRequest($"/v2/projects", Method.Get);
            request.AddQueryParameter("limit", 1);
            var response = await new WorldserverClient(authenticationCredentialsProviders).ExecuteWithErrorHandling<CollectionResponseDto<ProjectDto>>(request);
            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
        
    }
}