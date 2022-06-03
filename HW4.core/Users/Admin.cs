using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core.Users
{
    public class Admin
    {
        public string Login { set; get; }
        public string Password { set; get; }

        public Admin(string login, string password)
        {
            Login = login;
            Password = password;
        }

    }
}
