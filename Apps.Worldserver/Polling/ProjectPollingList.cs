using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Tasks.Request;
using Apps.Worldserver.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Worldserver.Polling
{
    [PollingEventList]
    public class ProjectPollingList : WorldserverInvocable
    {
        public ProjectPollingList(InvocationContext invocationContext):base(invocationContext) { }

        [PollingEvent("On project created", "Triggered when a new project was created")]
        public async Task<PollingEventResponse<ProjectMemory, List<ProjectResponse>>> OnProjectCreated(PollingEventRequest<ProjectMemory> request)
        {
            if (request.Memory is null)
            {
                return new()
                {
                    FlyBird = false,
                    Memory = new()
                    {
                        LastPollingTime = DateTime.UtcNow,
                        Triggered = false
                    }
                };
            }

            var projectsRequest = new WorldserverRequest("/v2/projectGroups/search", Method.Post);

            projectsRequest.AddBody(new
            {
                @operator = "and",
                filters = new List<object>()
            });

            var projects = await Client.Paginate<ProjectGroupDto>(projectsRequest);

            var newProjects = projects
                .SelectMany(group => group.Projects)
                .Where(project => project.CreationDate > request.Memory.LastPollingTime)
                .Select(project => new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Creator = $"{project.Creator.FirstName} {project.Creator.LastName}",
                    CreationDate = project.CreationDate
                })
                .ToList();

            return new PollingEventResponse<ProjectMemory, List<ProjectResponse>>
            {
                FlyBird = newProjects.Any(),
                Memory = new ProjectMemory
                {
                    LastPollingTime = DateTime.UtcNow,
                    Triggered = newProjects.Any()
                },
                Result = newProjects,
            };
        }
    }
}
