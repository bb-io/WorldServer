 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Worldserver.DataSourceHandlers
{
    public class ProjectIdDataHandler : WorldserverInvocable, IAsyncDataSourceItemHandler
    {
        public ProjectIdDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new WorldserverRequest("/v2/projects", Method.Get);
            request.AddQueryParameter("fields", "id,name");
            request.AddQueryParameter("limit", "50");

            if (!string.IsNullOrEmpty(context.SearchString))
            {
                request.AddQueryParameter("sortBy", "name");
                request.AddQueryParameter("sortDirection", "asc");
                request.AddQueryParameter("filter", context.SearchString);
            }

            var response = await Client.ExecuteWithErrorHandling<ProjectsIdResponseDto>(request);

            return response.Items.Select(project => new DataSourceItem
            {
                Value = project.Id.ToString(),
                DisplayName = $"{project.Name} ({project.Id})"
            });

        }
    }
}
