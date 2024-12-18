using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Worldserver.Dto;
using Newtonsoft.Json;

namespace Apps.Worldserver.Models.Tasks.Response
{
    public class ProjectTasksResponse
    {
        [JsonProperty("tasks")]
        public List<ProjectTaskDto> Tasks { get; set; }
    }
    public class ProjectTaskDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
