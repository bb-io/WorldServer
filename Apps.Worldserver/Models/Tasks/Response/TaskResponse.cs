using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Apps.Worldserver.Dto;
using Newtonsoft.Json;

namespace Apps.Worldserver.Models.Tasks.Response
{
    public class TaskResponse
    {
        [JsonProperty("items")]
        public List<TaskItem> Items { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class TaskItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }
    }

    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ExportResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("response")]
        public ExportResponseData Response { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class ExportResponseData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("output")]
        public List<object> Output { get; set; }
    }

    public class Link
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }


}
