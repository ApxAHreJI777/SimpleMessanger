using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace AsyncServer
{
    class Client
    {
        public string UserName;
        public byte status = (byte)UserStatus.Online;
        bool isConnected = false;
        //public string Password;
        //public bool LoggedIn;

        TcpClient connection;
        public NetworkStream netStream;  // Raw-data stream of connection.
        public BinaryReader br;
        public BinaryWriter bw;
        public Server server;

        public Client()
        {
            return;
        }

        public Client(TcpClient _conn, Server _server)
        {
            connection = _conn;
            server = _server;
            isConnected = true;
            StartClient();
        }

        public void StartClient()
        {
            try
            {
                Console.WriteLine("[{0}] New connection!", DateTime.Now);
                netStream = connection.GetStream();
                br = new BinaryReader(netStream, Encoding.UTF8);
                bw = new BinaryWriter(netStream, Encoding.UTF8);
                Write(Message.ServiceMsg(MsgType.HELLO).ToString());
                Message msg = Message.ParseMsg(br.ReadString());
                if (msg.msgType == MsgType.HELLO)
                {
                    msg = Message.ParseMsg(br.ReadString());
                    if (msg.msgType == MsgType.LOGIN)
                    {
                        if (server.ValidateUser(msg.reciever, msg.message))
                        {
                            UserName = msg.reciever;
                            server.Broadcast(Message.FormString(
                                MsgType.USER_CHANGED_STATUS, 
                                UserName, 
                                status.ToString()
                            ));
                            server.users.Add(UserName, this);
                            Console.WriteLine("[{0}] New user online: {1}", DateTime.Now, UserName);
                            Write(Message.ServiceMsg(MsgType.OK).ToString());
                            StartProcessing();
                        }
                        else 
                        {
                            string noMsg = "Wrong name or password!";
                            Write(Message.FormString(MsgType.NO, "null", noMsg));
                            Console.WriteLine(noMsg);//TODO: delet
                        }
                    }
                    else if (msg.msgType == MsgType.REGISTER)
                    {
                        string noMsg = "Can't register.";
                        Console.WriteLine("Registragion request: {0} {1}", msg.reciever, msg.message);
                        byte answerCode = server.ResisterUser(msg.reciever, msg.message);
                        Write(Message.FormString(answerCode, "null", noMsg));
                        Console.WriteLine("Registragion: {0} {1}", answerCode == MsgType.OK, answerCode);//TODO: delet
                    }
                }
                CloseConnection();
            }
            catch 
            {
                CloseConnection();
            }
        }

        void CloseConnection()
        {
            if (isConnected)
            {
                if (UserName != null)
                {
                    server.UserDisconected(UserName);
                }
                br.Close();
                bw.Close();
                netStream.Close();
                connection.Close();
                isConnected = false;
                server.Broadcast(Message.FormString(MsgType.USER_OFFLINE, UserName, ""));
                Console.WriteLine("[{0}]{1} Connection closed!", DateTime.Now,
                    UserName != null && UserName != String.Empty ? " <" + UserName + ">" : "");
            }
        }

        public void Write(string msg)
        {
            bw.Write(msg);
            bw.Flush();
        }

        public string ReadString()
        {
            return br.ReadString();
        }

        void StartProcessing()
        {
            Message msg;
            while(connection.Connected)
            {
                msg = Message.ParseMsg(this.ReadString());
                if (msg.msgType == MsgType.SEND)
                {
                    if (server.IsUserAvailable(msg.reciever))
                    {
                        string msgSend = Message.FormString(msg.msgType, UserName, msg.message);
                        server.users[msg.reciever].Write(msgSend);
                    }
                }
                else if (msg.msgType == MsgType.AV_USERS)
                {
                    string msgString = String.Join(Message.EOU.ToString(), server.GetAvailableUsers());
                    string msgSend = Message.FormString(MsgType.AV_USERS, UserName, msgString);
                    Console.WriteLine(msgString);
                    Write(msgSend);
                }
                else if (msg.msgType == MsgType.CHANGE_STATUS)
                {
                    byte newStatus = Convert.ToByte(msg.reciever);
                    if (newStatus == status)
                        continue;
                    status = newStatus;
                    if (status != MsgType.USER_OFFLINE)
                    {
                        server.Broadcast(Message.FormString(
                            MsgType.USER_CHANGED_STATUS,
                            UserName,
                            status.ToString()
                        ));
                        Console.WriteLine("User {0} changed status to: {1}", UserName, status);
                    }
                    else 
                    {
                        CloseConnection();
                    }
                }
            }
        }
    }
}
