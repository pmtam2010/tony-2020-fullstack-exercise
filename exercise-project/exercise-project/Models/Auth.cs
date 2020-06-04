using System;
using System.Collections.Generic;
using System.Text;

namespace exercise_project.Models
{
    public class Auth
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Auth()
        {
        }

        public Auth(string userName)
        {
            UserName = userName;
        }

        public Auth(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
