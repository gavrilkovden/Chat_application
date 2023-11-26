using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Exceptions
{
    public class ChatInvalidInputException : Exception
    {
        public ChatInvalidInputException(string message)
   : base(message) { }
    }
}
