using System;
using System.Runtime.Serialization;

namespace XiosTestWebService.DAO
{
    [Serializable]
    internal class AlreadyExistEmployee : Exception
    {
        public AlreadyExistEmployee()
        {
        }

        public AlreadyExistEmployee(string message) : base(message)
        {
        }

        public AlreadyExistEmployee(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistEmployee(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}