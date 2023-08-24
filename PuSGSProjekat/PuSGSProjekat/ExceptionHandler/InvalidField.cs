using System;

namespace PuSGSProjekat.ExceptionHandler
{
    public class InvalidField : Exception
    {
        public InvalidField()
        {
        }

        public InvalidField(string message) : base(message)
        {
        }

        public InvalidField(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
