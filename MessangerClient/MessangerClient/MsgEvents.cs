using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessangerClient
{
    public delegate void ConnectedEventHendler(object sender, ConnectedEventArgs e);
    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(string s)
        {
            msg = s;
        }
        private string msg;
        public string Message
        {
            get { return msg; }
        }
    }

    public delegate void UserStatusEventHendler(object sender, UserStatusEventArgs e);
    public class UserStatusEventArgs : EventArgs
    {
        public UserStatusEventArgs(string user, bool isOlnine, byte status)
        {
            _user = user;
            online = isOlnine;
            code = status;
        }
        private string _user;
        private bool online;
        private byte code;

        public string User
        {
            get { return _user; }
        }
        public bool IsOnline
        {
            get { return online; }
        }
        public byte Status
        {
            get { return code; }
        }
    }

    public delegate void MessageRecievedEventHendler(object sender, MessageRecievedEventArgs e);
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageRecievedEventArgs(string user, string message)
        {
            _user = user;
            msg = message;
        }
        private string _user;
        private string msg;

        public string Message
        {
            get { return msg; }
        }
        public string User
        {
            get { return _user; }
        }
    }

    public class UsersEventInfo { 
        public string Name; 
        public byte Status;
        public UsersEventInfo(byte code, string name)
        {
            Name = name;
            Status = code;
        }
    }

    public delegate void UsersEventHendler(object sender, UsersEventArgs e);
    public class UsersEventArgs : EventArgs
    {
        public UsersEventArgs(List<UsersEventInfo> users)
        {
            _users = users;
        }
        private List<UsersEventInfo> _users;

        public List<UsersEventInfo> Users
        {
            get { return _users; }
        }
    }
    
    public delegate void LoginEventHendler(object sender, LoginEventArgs e);
    public class LoginEventArgs : EventArgs
    {
        public LoginEventArgs(bool succes, string message)
        {
            ok = succes;
            msg = message;
        }
        private bool ok;
        private string msg;

        public bool IsLogedin
        {
            get { return ok; }
        }
        public string Message
        {
            get { return msg; }
        }
    }

    public delegate void RegisterEventHendler(object sender, RegisterEventArgs e);
    public class RegisterEventArgs : EventArgs
    {
        public RegisterEventArgs(byte code, string message)
        {
            cd = code;
            msg = message;
        }
        private byte cd;
        private string msg;

        public byte Code
        {
            get { return cd; }
        }
        public string Message
        {
            get { return msg; }
        }
    }
}
