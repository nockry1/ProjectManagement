using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanttApplication.Models
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; }

        public static explicit operator UserDto(User user)
        {
            return new UserDto
            {
                id = user.Id,
                name = user.Name
            };
        }
        public static explicit operator User(UserDto user)
        {
            return new User
            {
                Id = user.id,
                Name = user.name
            };
        }
    }
}