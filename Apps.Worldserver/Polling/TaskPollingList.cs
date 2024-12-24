using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Worldserver.Polling
{
    [PollingEventList]
    public class TaskPollingList : WorldserverInvocable
    {
        public TaskPollingList(InvocationContext invocationContext) : base(invocationContext) { }

        [PollingEvent("On task completed", "Triggered when tasks are completed in WorldServer")]
        public async Task<PollingEventResponse<TaskMemory, CompletedTasksResponse>> OnTaskCompleted(
            PollingEventRequest<TaskMemory> request)
        {
            if (request.Memory is null)
            {
                request.Memory = new TaskMemory
                {
                    LastPollingTime = null,
                    Triggered = false,
                    CompletedTaskIds = new List<string>()
                };
            }

            var tasksRequest = new WorldserverRequest("/v2/tasks", Method.Get);

            var allTasks = await Client.Paginate<TaskDto>(tasksRequest);

            if (request.Memory.LastPollingTime == null)
            {
                var alreadyCompletedIds = allTasks
                    .Where(t => t.Status?.Status == "COMPLETED")
                    .Select(t => t.Id.ToString())
                    .ToList();

                request.Memory.CompletedTaskIds.AddRange(alreadyCompletedIds);
                request.Memory.LastPollingTime = DateTime.UtcNow;
                request.Memory.Triggered = false;

                return new PollingEventResponse<TaskMemory, CompletedTasksResponse>
                {
                    FlyBird = false,
                    Memory = request.Memory,
                    Result = new CompletedTasksResponse
                    {
                        Tasks = new List<TaskCompletedResponse>() 
                    }
                };
            }

            var newlyCompleted = allTasks
               .Where(t => t.Status?.Status == "COMPLETED")
               .Where(t => !request.Memory.CompletedTaskIds.Contains(t.Id.ToString()))
               .ToList();

            if (!newlyCompleted.Any())
            {
                return new PollingEventResponse<TaskMemory, CompletedTasksResponse>
                {
                    FlyBird = false,
                    Memory = request.Memory ,
                    Result = new CompletedTasksResponse()
                };
            }

            var result = newlyCompleted.Select(t => new TaskCompletedResponse
            {
                Id = t.Id,           
                Status = t.Status?.Status,
                CompletionDetected = DateTime.UtcNow
            }).ToList();

            var resultWrapper = new CompletedTasksResponse
            {
                Tasks = result
            };

            request.Memory.CompletedTaskIds.AddRange(newlyCompleted.Select(t => t.Id.ToString()));
            request.Memory.LastPollingTime = DateTime.UtcNow;
            request.Memory.Triggered = true;


            return new PollingEventResponse<TaskMemory, CompletedTasksResponse>
            {
                FlyBird = true,
                Memory = request.Memory,
                Result = resultWrapper
            };
        }
    }
}
