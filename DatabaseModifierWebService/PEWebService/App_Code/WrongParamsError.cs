using System;
using System.Runtime.Serialization;

namespace PE.DataReturn
{
    [Serializable]
    public class WrongParamsError : Exception
    {
        public WrongParamsError()
        {
        }

        public WrongParamsError(string message) : base(message)
        {
        }

        public WrongParamsError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongParamsError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}