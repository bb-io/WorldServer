using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.ProjectGroups.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class LocaleDataHandler : WorldserverInvocable, IAsyncDataSourceHandler
{
    public CreateProjectGroupRequest CreateProjectGroupRequest { get; set; }

    public LocaleDataHandler(InvocationContext invocationContext, 
        [ActionParameter] CreateProjectGroupRequest createProjectGroupRequest) : base(invocationContext)
    {
        CreateProjectGroupRequest = createProjectGroupRequest;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (CreateProjectGroupRequest?.ProjectTypeId == null)
            throw new ArgumentException("Please specify project type ID firsd");

        var localeRequest = new WorldserverRequest("/v2/locales", Method.Get);
        var locales = await Client.Paginate<LocaleDto>(localeRequest);

        var projectTypeRequest = new WorldserverRequest($"/v2/projectTypes/{CreateProjectGroupRequest.ProjectTypeId}", Method.Get);
        var projectType = await Client.ExecuteWithErrorHandling<ProjectTypeDto>(projectTypeRequest);

        return locales
            .Where(x => x.Id != projectType.SourceLocale.Id)
            .Where(str => context.SearchString is null || str.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(k => k.Id.ToString(), v => v.Name);
    }
}
