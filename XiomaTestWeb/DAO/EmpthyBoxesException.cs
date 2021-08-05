using System;
using System.Runtime.Serialization;

namespace XiomaTestWeb.DAO
{
    [Serializable]
    internal class EmpthyBoxesException : Exception
    {
        public EmpthyBoxesException()
        {
        }

        public EmpthyBoxesException(string message) : base(message)
        {
        }

        public EmpthyBoxesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmpthyBoxesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}