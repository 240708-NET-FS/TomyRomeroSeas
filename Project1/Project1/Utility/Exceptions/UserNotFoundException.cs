using System;

namespace ReviewShelf.Utility.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) 
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
        
    }
}
