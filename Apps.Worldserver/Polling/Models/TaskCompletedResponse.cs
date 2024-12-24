using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Polling.Models
{
    public class TaskCompletedResponse
    {
        [Display("Task ID")]
        public int Id { get; set; }

        [Display("Task status")]
        public string? Status { get; set; }

        [Display("Task completion time")]
        public DateTime CompletionDetected { get; set; }
    }
}
