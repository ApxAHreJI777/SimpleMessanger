using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessangerClient
{
    class Utils
    {
        private static char[] PUNTUATIONS = new char[] { '<', '>', '/', ';', ':' };
        public static bool ContainsPunctuation(string text)
        {
            return text.IndexOfAny(PUNTUATIONS) != -1;
        }

        public static byte ValidateUserInfo(string name, string pass)
        {
            if (name.Length >= 40)
                return MsgType.NAME_TOO_LONG;
            if (pass.Length < 4)
                return MsgType.PASSWORD_TOO_SMALL;
            if (pass.Length >= 20)
                return MsgType.PASSWORD_TOO_LONG;
            if (Utils.ContainsPunctuation(name))
                return MsgType.INVALID_NAME;
            return MsgType.OK;
        }
    }
}
