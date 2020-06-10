using System;

namespace InvoiceManager.Core.Exceptions
{
    public class InvalidSecretKeyException : Exception
    {
        public InvalidSecretKeyException()
            :base("Secret key is missing in request header.")
        {
        }

        public InvalidSecretKeyException(string key)
            : base($"Provided secret key: {key} was not correct.")
        {

        }

        public InvalidSecretKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        
    }
}
