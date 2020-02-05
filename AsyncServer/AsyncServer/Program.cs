using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AsyncServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"test4.xml";
            UsersDB db = new UsersDB(path);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 12345;
            Server sv = new Server(ip, port, db);
            Console.WriteLine("Enter your command:");
            string command = String.Empty;
            while (command != "stop")
            {
                command = Console.ReadLine();
                if (command == "av_users")
                {
                    foreach (string user in sv.GetAvailableUsers())
                    {
                        Console.WriteLine("<{0}> available", user);
                    }
                }
            }
        }
    }
}
