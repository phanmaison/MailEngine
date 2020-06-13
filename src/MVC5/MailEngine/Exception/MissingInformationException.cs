using System;

namespace MailEngine
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Exception" />
    public class MissingInformationException : Exception
    {
        public MissingInformationException(string message) : base(message)
        {
        }
    }
}