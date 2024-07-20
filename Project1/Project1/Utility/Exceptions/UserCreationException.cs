using System;

namespace YourApp.Utility.Exceptions
{
    [Serializable]
    public class UserCreationException : Exception
    {
        public UserCreationException()
        {
        }

        public UserCreationException(string message) 
            : base(message)
        {
        }

        public UserCreationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
        
    }
}
