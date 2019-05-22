using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreciImageReader.CustomExceptionHandler
{

    public class BaseExceptionHandler : Exception
    {
        private int errorCode;
        private string errorDescription;

        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
        }
        public string ErrorDescription
        {
            get
            {
                return errorDescription;
            }
        }

        public BaseExceptionHandler(string message, string description, int code) : base(message)
        {
            errorCode = code;
            errorDescription = description;
        }
    }
}
