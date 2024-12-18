using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.ProjectGroups.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers;

public class LocaleDataHandler : WorldserverInvocable, IAsyncDataSourceItemHandler
{
    public CreateProjectGroupRequest CreateProjectGroupRequest { get; set; }

    public LocaleDataHandler(InvocationContext invocationContext, 
        [ActionParameter] CreateProjectGroupRequest createProjectGroupRequest) : base(invocationContext)
    {
        CreateProjectGroupRequest = createProjectGroupRequest;
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (CreateProjectGroupRequest?.ProjectTypeId == null)
            throw new ArgumentException("Please specify project type ID first");

        var localeRequest = new WorldserverRequest("/v2/locales", Method.Get);
        var locales = await Client.Paginate<LocaleDto>(localeRequest);

        var projectTypeRequest = new WorldserverRequest($"/v2/projectTypes/{CreateProjectGroupRequest.ProjectTypeId}", Method.Get);
        var projectType = await Client.ExecuteWithErrorHandling<ProjectTypeDto>(projectTypeRequest);

        return locales.Where(locale => locale.Id != projectType.SourceLocale.Id)
            .Where(locale => string.IsNullOrWhiteSpace(context.SearchString) ||
            locale.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(locale=> new DataSourceItem(locale.Id.ToString(), locale.Name));
    }
}
