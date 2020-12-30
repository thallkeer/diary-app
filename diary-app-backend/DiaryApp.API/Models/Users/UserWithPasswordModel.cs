using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Models.Users
{
    public class UserWithPasswordModel : UserModel
    {
        public string Password { get; set; }
    }
}
