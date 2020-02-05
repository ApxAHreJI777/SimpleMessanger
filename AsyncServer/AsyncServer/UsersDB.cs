using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AsyncServer
{
    class UsersDB
    {

        private XDocument usersDB;
        public string dbPath;

        public UsersDB(string path)
        {
            LoadUsersDB(path);
        }

        public bool ValidateUser(string name, string pass)
        {
            name = name.ToLower();
            UserInfo user = FindUser(name);
            if (user != null)
            {
                if (user.Password == pass)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UserAlreadyExists(string name)
        {
            return FindUser(name) != null;
        }

        private UserInfo FindUser(string name)
        {
            name = name.ToLower();
            var user = usersDB.Descendants("User")
                .Where(n => n.Element("Name").Value == name).ToList().FirstOrDefault();
            if(user != null)
                return new UserInfo(user.Element("Name").Value, user.Element("Password").Value);
            return null;
        }

        public bool AddNewUser(string name, string pass)
        {
            name = name.ToLower();
            if (!UserAlreadyExists(name))
            {
                XElement newUser = new XElement("User",
                    new XElement("Name", name),
                    new XElement("Password", pass));
                XElement root = usersDB.Root;
                root.Add(newUser);
                Save();
                return true;
            }
            return false;
        }

        public void LoadUsersDB(string path)
        {
            dbPath = path;
            try
            {
                usersDB = XDocument.Load(path);
            }
            catch
            {
                Console.WriteLine("File doesn't exists");
                XElement rootElemetn = new XElement("UsersDB",
                    new XElement("User",
                        new XElement("Name", "admin"),
                        new XElement("Password", "admin")
                ));
                usersDB = new XDocument(rootElemetn);
                usersDB.Save(path);
                Console.WriteLine("New file has been created");
            }
        }

        public void Save()
        {
            usersDB.Save(dbPath);
        }

        public void Print()
        {
            Console.WriteLine(usersDB);
        }


        public void TestUsers(int c)
        {
            string pass = "pass";
            for (int i = 0; i < c; i++)
            {
                string n = "TestName" + i;
                AddNewUser(n, Utils.GetHashSha256(pass));
            }
        }
    }
}
