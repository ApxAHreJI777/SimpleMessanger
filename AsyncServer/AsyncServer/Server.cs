using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;

namespace AsyncServer
{
    class Server
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public TcpListener server;
        public bool running = true;
        public Dictionary<string, Client> users = new Dictionary<string, Client>();
        public UsersDB usersDB;

        public Server(IPAddress serverIP, int port, UsersDB db)
        {
            usersDB = db;
            server = new TcpListener(serverIP, port);
            Console.WriteLine("Starting...");
            server.Start();
            Console.WriteLine("Started.");
            Thread tr = new Thread(Listen);
            tr.Start();
        }

        public void Listen()
        {
            while (running)
            {
                //Console.WriteLine("Waiting for a connection...");
                allDone.Reset();
                server.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), server);
                allDone.WaitOne();
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();
            // Get the socket that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(ar);

            new Client(client, this);
        }

        public void UserDisconected(string name)
        {
            if (users.ContainsKey(name))
            {
                users.Remove(name);
            }
        }

        public byte ResisterUser(string name, string pass)
        {
            if (name == String.Empty || pass == String.Empty)
                return MsgType.NO;
            if (name.Length >= 40)
                return MsgType.NAME_TOO_LONG;
            if (pass.Length < 4)
                return MsgType.PASSWORD_TOO_SMALL;
            if (pass.Length >= 20)
                return MsgType.PASSWORD_TOO_LONG;
            if (Utils.ContainsPunctuation(name))
                return MsgType.INVALID_NAME;
            if (usersDB.UserAlreadyExists(name))
                return MsgType.NAME_EXISTS;
            bool ok = usersDB.AddNewUser(name, Utils.GetHashSha256(pass));
            if(!ok)
                return MsgType.NO;
            return MsgType.OK;
        }

        public bool ValidateUser(string name, string password)
        {
            return usersDB.ValidateUser(name, Utils.GetHashSha256(password));
        }

        public bool IsUserAvailable(string name)
        {
            return users.ContainsKey(name);
        }

        public List<string> GetAvailableUsers()
        {
            //List<string> userNames = users.Keys.ToList();
            var userNames = users.Values
                .Select(u =>  String.Concat(u.status.ToString(), Message.EOC, u.UserName));
            return userNames.ToList();
        }

        public void Broadcast(string msg)
        {
            foreach (Client user in users.Values)
            {
                user.Write(msg);
            }
        }
    }
}
