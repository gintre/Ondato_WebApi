using System;

namespace Ondato_WebApi.Exceptions
{
    public class ExpirationDateException : Exception
    {
        public ExpirationDateException() : base("Expiration date too long")
        { }
    }
}
