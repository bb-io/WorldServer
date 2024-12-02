using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Worldserver.Dto;

namespace Apps.Worldserver.Models.Tasks.Response
{
    public class TaskResponseDto
    {
        public List<TaskDto> Items { get; set; }
        public int Total { get; set; }
    }
}
