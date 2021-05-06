using System;

namespace GameEngine.Exceptions
{
    public class DublicateComponentException : Exception
    {
        /// <summary>
        /// Exception ctor
        /// </summary>
        public DublicateComponentException()
            : base("Unable to create two instance of unique component")
        { }

        /// <summary>
        /// Exception ctor
        /// </summary>
        /// <param name="message">Error message</param>
        public DublicateComponentException(String message)
            : base(message)
        { }
    }
}
