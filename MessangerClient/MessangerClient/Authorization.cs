using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace MessangerClient
{
    public class Authorization
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        BinaryReader br;
        BinaryWriter bw;
        NetworkStream netStream;
        TcpClient client; 
        public string name;
        public string pass;
        bool conn = false;
        bool login = false;
        UserStatus status;
        public bool IsLodedIn { get { return login; } }
        public bool IsConnected { get { return conn; } }
        public UserStatus Status { get { return status; } set { ChageStatus(value); } }

        public event ConnectedEventHendler Connected;
        public event LoginEventHendler LogedIn;
        public event RegisterEventHendler Registered;
        public event UsersEventHendler UsersOnline;
        public event UserStatusEventHendler UserChangedStatus;
        public event MessageRecievedEventHendler MessageRecieved;

        public Authorization()
        {
            name = String.Empty;
            pass = String.Empty;
        }

        public Authorization(string _name, string _pass)
        {
            name = _name;
            pass = _pass;
        }

        public void Connect(string _name, string _pass, bool isLogin)
        {
            name = _name;
            pass = _pass;
            status = UserStatus.Online;
            Connect(isLogin);
        }

        public void Connect(bool isLogin)
        {
            if (name.Trim() != String.Empty && pass.Trim() != String.Empty)
            {
                allDone.Reset();
                client = new TcpClient();
                if (isLogin)
                    client.BeginConnect(Server.IP, Server.Port,
                        new AsyncCallback(ConnectCallbackLogin), client);
                else
                    client.BeginConnect(Server.IP, Server.Port,
                        new AsyncCallback(ConnectCallbackReg), client);
                allDone.WaitOne();
            }
        }

        public void ConnectCallbackLogin(IAsyncResult ar)
        {
            if (ConnectCallback(ar))
            {
                Login();
                StartResiving();
            }
            else 
                CloseConnection(); 

        }

        public void ConnectCallbackReg(IAsyncResult ar)
        {
            if (ConnectCallback(ar))
            {
                Register();
            }
            CloseConnection();

        }

        public bool ConnectCallback(IAsyncResult ar)
        {
            allDone.Set();
            TcpClient t = (TcpClient)ar.AsyncState;
            t.EndConnect(ar);
            netStream = t.GetStream();
            br = new BinaryReader(netStream, Encoding.UTF8);
            bw = new BinaryWriter(netStream, Encoding.UTF8);
            string msg = br.ReadString();
            MessengerMsg m = MessengerMsg.ParseMsg(msg);
            if (m.msgType == MsgType.HELLO)
            {
                Write(MessengerMsg.ServiceMsg(MsgType.HELLO).ToString());
                conn = true;
                OnConnected(new ConnectedEventArgs("Connected to the Server"));
                return true;
            }
            return false;
        }

        public void Login()
        {
            if (IsConnected)
            {
                Write(MessengerMsg.FormString(MsgType.LOGIN, name, pass));
                MessengerMsg m = MessengerMsg.ParseMsg(br.ReadString());
                if (m.msgType == MsgType.OK)
                {
                    login = true;
                    OnLogin(new LoginEventArgs(true, "Logged-in succesfully"));
                }
                else if (m.msgType == MsgType.NO)
                {
                    login = false;
                    OnLogin(new LoginEventArgs(false, m.message));
                }
            }
        }

        public void Register()
        {
            if (IsConnected)
            {
                Write(MessengerMsg.FormString(MsgType.REGISTER, name, pass));
                MessengerMsg m = MessengerMsg.ParseMsg(br.ReadString());
                if (m.msgType == MsgType.OK)
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "Registered succesfully"));
                }
                else if (m.msgType == MsgType.NAME_TOO_LONG)
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "Name must be less than 40 symbols"));
                }
                else if (m.msgType == MsgType.PASSWORD_TOO_SMALL)
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "Password must be more than 4 symbols"));
                }
                else if (m.msgType == MsgType.PASSWORD_TOO_LONG)
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "Password must be less than 20 symbols"));
                }
                else if (m.msgType == MsgType.INVALID_NAME)
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "Name must not contein \"< > / ; :\" symbols"));
                }
                else if (m.msgType == MsgType.NAME_EXISTS)
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "User with this name already exists"));
                }
                else 
                {
                    OnRegister(new RegisterEventArgs(m.msgType, "Can't register"));
                }
            }
        }

        void StartResiving()
        {
            try
            {
                MessengerMsg msg;
                while (client.Connected)
                {
                    msg = MessengerMsg.ParseMsg(br.ReadString());
                    if (msg.msgType == MsgType.AV_USERS)
                    {
                        string[] users = msg.message.Split(MessengerMsg.EOU);
                        var usersWithStatus =
                            from u in users
                            let i = u.IndexOf(MessengerMsg.EOC)
                            select new UsersEventInfo(
                                Convert.ToByte(u.Substring(0, i)),
                                u.Substring(i + MessengerMsg.EOC.Length));
                        OnUsersOnline(new UsersEventArgs(usersWithStatus.ToList()));
                    }
                    else if (msg.msgType == MsgType.USER_CHANGED_STATUS)
                    {
                        if (msg.reciever != name)
                            OnUserChangedStatus(new UserStatusEventArgs(msg.reciever, true, 
                                Convert.ToByte(msg.message)));
                    }
                    else if (msg.msgType == MsgType.USER_OFFLINE)
                    {
                        if (msg.reciever != name)
                            OnUserChangedStatus(new UserStatusEventArgs(msg.reciever, false, 
                                MsgType.USER_OFFLINE));
                    }
                    else if (msg.msgType == MsgType.SEND)
                    {
                        OnMessageRecieved(new MessageRecievedEventArgs(msg.reciever, msg.message));
                    }
                }
            }
            catch
            {
                CloseConnection();
            }
        }

        public void GetUsersOnline()
        {
            this.Write(MessengerMsg.FormString(MsgType.AV_USERS, String.Empty, String.Empty));
        }

        public void ChageStatus(UserStatus s)
        {
            status = s;
            this.Write(MessengerMsg.FormString(MsgType.CHANGE_STATUS, ((byte)status).ToString(), String.Empty));
        }

        public void Write(string message)
        {
            bw.Write(message);
            bw.Flush();
        }

        void CloseConnection()
        {
            conn = false;
            br.Close();
            bw.Close();
            netStream.Close();
            client.Close();
        }

        public void Logout()
        {
            name = String.Empty;
            pass = String.Empty;
            CloseConnection();
        }

        protected virtual void OnConnected(ConnectedEventArgs e)
        {
            ConnectedEventHendler handler = Connected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnLogin(LoginEventArgs e)
        {
            LoginEventHendler handler = LogedIn;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRegister(RegisterEventArgs e)
        {
            RegisterEventHendler handler = Registered;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMessageRecieved(MessageRecievedEventArgs e)
        {
            MessageRecievedEventHendler handler = MessageRecieved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnUsersOnline(UsersEventArgs e)
        {
            UsersEventHendler handler = UsersOnline;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnUserChangedStatus(UserStatusEventArgs e)
        {
            UserStatusEventHendler handler = UserChangedStatus;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
