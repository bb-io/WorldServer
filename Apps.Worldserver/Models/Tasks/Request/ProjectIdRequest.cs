using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Worldserver.Models.Tasks.Request
{
    public class ProjectIdRequest
    {
        [Display("Project ID")]
        [DataSource(typeof(ProjectDataHandler))]
        public string ProjectId { get; set; }
    }
}
