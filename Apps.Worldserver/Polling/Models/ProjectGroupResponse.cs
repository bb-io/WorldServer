using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Worldserver.Dto;

namespace Apps.Worldserver.Polling.Models
{
    public class ProjectGroupResponse
    {
        public List<ProjectPollingDto> Projects { get; set; }
        public int Total {  get; set; }
    }
    public class ProjectPollingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CreatorDto Creator { get; set; }
        public DateTime CompletionDate { get; set; }
        public int CompletedTasks { get; set; }
        public int TotalTasks { get; set; }
    }
    public class CreatorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
