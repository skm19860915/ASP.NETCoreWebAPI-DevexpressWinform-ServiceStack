using System;
using System.Runtime.Serialization;

namespace Xperters.Core.Exceptions
{
    public class SqlValidationException : ValidationException
    {
        public SqlValidationException(string message)
            : base(message)
        {
        }

        public SqlValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SqlValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}