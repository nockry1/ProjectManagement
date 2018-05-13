using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GanttApplication.Models;

namespace GanttApplication.Controllers
{
    public class UserController : ApiController
    {
        private GanttContext db = new GanttContext();

        // GET api/User
        [System.Web.Http.HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return db
                .Users
                .ToList()
                .Select(u => (UserDto)u);
        }
    }
}
