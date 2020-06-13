using System;

namespace MailEngine
{
    /// <inheritdoc />
    /// <summary>
    /// Email client is not provided
    /// </summary>
    /// <seealso cref="T:System.Exception" />
    public class MissingEmailClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingEmailClientException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingEmailClientException(string message) : base(message)
        {
        }
    }
}