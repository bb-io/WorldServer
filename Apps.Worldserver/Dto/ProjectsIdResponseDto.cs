using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Worldserver.Dto
{
    public class ProjectsIdResponseDto
    {
        [Display("Projects items")]
        [JsonProperty("items")]
        public List<ProjectIdDto> Items { get; set; }

        [Display("Total amount of project ID`s")]
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
