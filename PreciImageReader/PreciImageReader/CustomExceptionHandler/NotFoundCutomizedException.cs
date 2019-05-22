using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PreciImageReader.CustomExceptionHandler
{
    public class NotFoundCutomizedException: BaseExceptionHandler
    {
        public NotFoundCutomizedException(string message, string description) : base(message, description, (int)HttpStatusCode.NotFound)
        {
        }
    }
}
