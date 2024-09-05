using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Worldserver.Dto;
public class WorkflowRoleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public string Description { get; set; }
}

