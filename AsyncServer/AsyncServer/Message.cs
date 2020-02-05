using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncServer
{
    class Message
    {
        public byte msgType;
        public string reciever;
        public string message;
        public const string EOH = "<EOH>"; // End of header
        public const string EOC = " "; // End of code
        public const char EOU = ';'; // End of user

        public Message(byte _type, string _reciever, string _message)
        {
            msgType = _type;
            reciever = _reciever;
            message = _message;
        }

        public override string ToString()
        {
            return FormString(msgType, reciever, message);
        }

        public static string FormString(byte msgType, string reciever, string message)
        {
            return String.Format("{0}{1}{2}{3}{4}", Convert.ToString(msgType), EOC, reciever, EOH, message);
        }

        public static Message ParseMsg(string value)
        {
            int eohIndex = value.IndexOf(EOH);
            if (eohIndex > 0)
            {
                string header = value.Substring(0, eohIndex);
                string msg = value.Substring(eohIndex + EOH.Length);
                int eocIndex = value.IndexOf(EOC);
                byte type = Convert.ToByte(value.Substring(0, eocIndex));
                string rec = header.Substring(eocIndex + EOC.Length);
                return new Message(type, rec, msg);
            }
            return null;
        }

        public static Message ServiceMsg(byte type)
        {
            return new Message(type, "null", "null");
        }
    }

    public enum UserStatus : byte
    {
        Online = MsgType.USER_ONLINE,
        Offline = MsgType.USER_OFFLINE,
        DND = MsgType.USER_DND,
        Away = MsgType.USER_AWAY
    };

    class MsgType
    {
        public const byte HELLO = 1;
        public const byte OK = 2;
        public const byte LOGIN = 3;
        public const byte REGISTER = 4;

        public const byte CHANGE_STATUS = 10;
        public const byte USER_CHANGED_STATUS = 11; // not used
        public const byte AV_USERS = 12;
        public const byte USER_ONLINE = 13;
        public const byte USER_DND = 14;
        public const byte USER_AWAY = 15;
        public const byte USER_OFFLINE = 16;

        public const byte SEND = 20;
        public const byte RECIEVE = 21;

        public const byte NO = 30;
        public const byte NAME_TOO_LONG = 31;
        public const byte PASSWORD_TOO_LONG = 32;
        public const byte PASSWORD_TOO_SMALL = 33;
        public const byte INVALID_NAME = 34;
        public const byte INVALID_PASSWORD = 35;
        public const byte NAME_EXISTS = 36;
    }
}
