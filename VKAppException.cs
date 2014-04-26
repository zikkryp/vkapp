using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vkapp.Log;

namespace vkapp
{
    public class VKAppException : Exception
    {
        public VKAppException()
            : base()
        {
            Logger.Log.Error("NotImplemented VKAppException");
        }

        public VKAppException(String message)
            : base(message)
        {
            Logger.Log.Error(message);
        }

        public VKAppException(String message, VKAppException innerException)
            : base(message, innerException)
        {
            Logger.Log.Error(innerException + " : " + message);
        }
    }

    public class AuthenticationException : VKAppException
    {
        public AuthenticationException()
            : base()
        {
            
        }

        public AuthenticationException(String message)
            : base(message)
        {
            
        }

        public AuthenticationException(String message, AuthenticationException innerException)
            : base(message, innerException)
        {

        }
    }

    public class SQLiteException : VKAppException
    {
        public SQLiteException()
            : base()
        {

        }

        public SQLiteException(String message)
            : base(message)
        {

        }

        public SQLiteException(String message, SQLiteException innerException)
            : base(message, innerException)
        {

        }
    }
}
