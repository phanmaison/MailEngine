namespace MailEngine
{
    /// <summary>
    /// Email information
    /// </summary>
    public interface IEmailAddress
    {
        /// <summary>
        /// Gets the email address
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// Gets the name of the person
        /// </summary>
        string EmailName { get; }
    }
}
