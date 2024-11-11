using Database;
using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService
{
    public class UserService
    {

        public UserService() { }

        public static async void CreateUser(UserDto user)
        {
            //UserDto dto = new UserDto { Id = user.Id, Name = user.Name, UserTag = user.UserTag, Email = user.Email, Password = user.Password };

            await Database.Database.CreateUser(user);
        }
    }
}
