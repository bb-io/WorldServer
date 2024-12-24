using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Polling.Models
{
    public class ProjectCompletedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
