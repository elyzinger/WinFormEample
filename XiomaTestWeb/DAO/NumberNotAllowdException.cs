using System;
using System.Runtime.Serialization;

namespace XiomaTestWeb.DAO
{
    [Serializable]
    internal class NumberNotAllowdException : Exception
    {
        public NumberNotAllowdException()
        {
        }

        public NumberNotAllowdException(string message) : base(message)
        {
        }

        public NumberNotAllowdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NumberNotAllowdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}