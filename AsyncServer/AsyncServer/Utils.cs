using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AsyncServer
{
    class Utils
    {
        private static char[] PUNTUATIONS = new char[] { '<', '>', '/' };
        public static bool ContainsPunctuation(string text)
        {
            return text.IndexOfAny(PUNTUATIONS) != -1;
        }

        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            StringBuilder hashString = new StringBuilder();
            foreach (byte x in hash)
            {
                hashString.AppendFormat("{0:x2}", x);
            }
            return hashString.ToString();
        }
    }
}
