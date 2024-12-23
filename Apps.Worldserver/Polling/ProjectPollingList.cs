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
        public async Task<PollingEventResponse<ProjectMemory, List<CustomProjectResponse>>> OnProjectCreated(PollingEventRequest<ProjectMemory> request)
        {
            if (request.Memory is null)
            {
                return new()
                {
                    FlyBird = false,
                    Memory = new()
                    {
                        LastPollingTime = DateTime.UtcNow,
                        Triggered = false,
                        LastProjectTotal = 0
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

            if (projects == null || !projects.Any())
            {
                return new PollingEventResponse<ProjectMemory, List<CustomProjectResponse>>
                {
                    FlyBird = false,
                    Memory = new ProjectMemory
                    {
                        LastPollingTime = DateTime.UtcNow,
                        Triggered = false,
                        LastProjectTotal = 0
                    }
                };
            }
 
            var lastPollingTime = request.Memory.LastPollingTime ?? DateTime.MinValue;

            var newProjects = projects
                .SelectMany(group => group.Projects)
                .Where(project => project.CreationDate > request.Memory.LastPollingTime)
                .Select(project => new CustomProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    CreationDate = project.CreationDate
                })
                .ToList();

            var currentTotal = projects.SelectMany(g => g.Projects).Count();
            request.Memory.LastProjectTotal = currentTotal;


            if (newProjects.Any())
            {
                var latestEventTime = newProjects.Max(p => p.CreationDate);

                return new PollingEventResponse<ProjectMemory, List<CustomProjectResponse>>
                {
                    FlyBird = true,
                    Memory = new ProjectMemory
                    {
                        LastPollingTime = latestEventTime,
                        Triggered = true,
                        LastProjectTotal = currentTotal
                    },
                    Result = newProjects
                };
            }

            return new PollingEventResponse<ProjectMemory, List<CustomProjectResponse>>
            {
                FlyBird = false,
                Memory = new ProjectMemory
                {
                    LastPollingTime = DateTime.UtcNow,
                    Triggered = false,
                    LastProjectTotal = currentTotal
                }
            };
        }


        [PollingEvent("On project completed", "Triggered when a project is completed")]
        public async Task<PollingEventResponse<ProjectMemory, List<ProjectCompletedResponse>>> OnProjectCompleted(PollingEventRequest<ProjectMemory> request)
        {
            if (request.Memory is null)
            {
                return new()
                {
                    FlyBird = false,
                    Memory = new()
                    {
                        LastPollingTime = DateTime.UtcNow,
                        Triggered = false,
                        LastProjectTotal = 0,
                        LastProjects = new List<ProjectMemoryItem>()
                    }
                };
            }

            var projectsRequest = new WorldserverRequest("/v2/projectGroups/search", Method.Post);

            projectsRequest.AddBody(new
            {
                @operator = "and",
                filters = new List<object>()
            });

            var projects = await Client.Paginate<ProjectGroupResponse>(projectsRequest);

            if (projects == null || !projects.Any())
            {
                var oldProjects = request.Memory.LastProjects ?? new List<ProjectMemoryItem>();

                if (oldProjects.Any())
                {
                    var disappearedAll = oldProjects.Select(op => new ProjectCompletedResponse
                    {
                        Id = op.Id,
                        Name = op.Name,
                        CompletionDate = DateTime.UtcNow
                    })
                    .ToList();

                    request.Memory.LastProjects = new List<ProjectMemoryItem>();
                    request.Memory.LastPollingTime = DateTime.UtcNow;
                    request.Memory.Triggered = true;
                    request.Memory.LastProjectTotal = 0;

                    return new()
                    {
                        FlyBird = true,
                        Memory = request.Memory,
                        Result = disappearedAll
                    };
                }
                else
                {
                    request.Memory.LastPollingTime = DateTime.UtcNow;
                    request.Memory.Triggered = false;
                    request.Memory.LastProjectTotal = 0;

                    return new()
                    {
                        FlyBird = false,
                        Memory = request.Memory
                    };
                }
            }

            var currentProjects = projects
                .SelectMany(group => group.Projects)
                .Select(proj => new ProjectMemoryItem
                {
                    Id = proj.Id,
                    Name = proj.Name
                })
                .ToList();

            var oldProjectList = request.Memory.LastProjects ?? new List<ProjectMemoryItem>();

            var disappearedProjects = oldProjectList
                .Where(oldP => !currentProjects.Any(cur => cur.Id == oldP.Id))
                .Select(oldP => new ProjectCompletedResponse
                {
                    Id = oldP.Id,
                    Name = oldP.Name,
                    CompletionDate = DateTime.UtcNow
                })
                .ToList();

            request.Memory.LastProjects = currentProjects;
            request.Memory.LastPollingTime = DateTime.UtcNow;
            request.Memory.LastProjectTotal = currentProjects.Count;

            if (disappearedProjects.Any())
            {
                request.Memory.Triggered = true;
                return new()
                {
                    FlyBird = true,
                    Memory = request.Memory,
                    Result = disappearedProjects
                };
            }
            else
            {
                request.Memory.Triggered = false;
                return new()
                {
                    FlyBird = false,
                    Memory = request.Memory
                };
            }
        }
    }
}
