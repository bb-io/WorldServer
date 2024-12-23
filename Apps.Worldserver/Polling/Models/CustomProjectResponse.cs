using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Worldserver.Polling.Models
{
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
