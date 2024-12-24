using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Polling.Models
{
    public class ProjectsCreatedResponse
    {
        [Display("Created projects")]

        public List<CustomProjectResponse> Projects { get; set; } = new();
    }
    public class CustomProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceLocale { get; set; }
        public DateTime CreationDate { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
    }

}
