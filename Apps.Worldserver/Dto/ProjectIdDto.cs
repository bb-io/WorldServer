

using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto
{
    public class ProjectIdDto
    {
        [Display("Project ID")]
        public string Id { get; set; }

        [Display("Project name")]
        public string Name {  get; set; }
    }
}
