using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncServer
{
    class UserInfo
    {
        public string UserName;
        public string Password;
        //public bool LoggedIn;
        //public Client Connection;
        public UserInfo(string name, string pass)
        {
            UserName = name;
            Password = pass;
        }
    }
}
