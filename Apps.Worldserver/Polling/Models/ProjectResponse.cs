using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Worldserver.Polling.Models
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CreatorDto Creator { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
