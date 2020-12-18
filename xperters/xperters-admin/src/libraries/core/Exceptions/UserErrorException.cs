using System;
using System.Runtime.Serialization;

namespace Xperters.Core.Exceptions
{
    public class UserErrorException : Exception
    {
        public UserErrorException(string message)
            : base(message)
        {
        }

        public UserErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}