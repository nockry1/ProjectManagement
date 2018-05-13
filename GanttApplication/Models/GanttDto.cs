using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanttApplication.Models
{
    public class GanttDto
    {
        public IEnumerable<TaskDto> data { get; set; }
        public IEnumerable<LinkDto> links { get; set; }
        public IEnumerable<UserDto> users { get; set; }
    }
}