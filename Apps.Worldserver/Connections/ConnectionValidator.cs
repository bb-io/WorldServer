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
        var request = new WorldserverRequest($"/projects", Method.Get);
        var response = await new WorldserverClient(authenticationCredentialsProviders).ExecuteWithErrorHandling<CollectionResponseDto<ProjectDto>>(request);
        return new()
        {
            IsValid = true
        };
    }
}