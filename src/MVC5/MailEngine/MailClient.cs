using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailEngine
{
    /// <summary>
    /// The engine to store configuration and send email.
    /// <para>Wrapper of EmailClient and RazorEngineService</para>
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class MailClient : IDisposable
    {
        #region DefaultClient

        /// <summary>
        /// Get current instance which is initialized when application start
        /// </summary>
        public static MailClient Default { get; set; }

        #endregion DefaultClient

        #region Properties

        /// <summary>
        /// Gets the email client.
        /// </summary>
        /// <value>
        /// The email client.
        /// </value>
        internal IEmailClient EmailClient
        { get; }

        /// <summary>
        /// From Name
        /// </summary>
        public string FromName { get; }

        /// <summary>
        /// From Email
        /// </summary>
        public string FromEmail { get; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructs a EmailEngine instance responsible for converting Razor templates into either a MailMessage or string.
        /// <para /> N.B. As this class loads up templates from the file system, it should only be created once per instance of your application.
        /// </summary>
        /// <param name="emailClient">The method by which to send the email</param>
        /// <param name="fromEmail">The address the email is from. e.g. hello@yoursite.com</param>
        /// <param name="fromName">The name the email is from. e.g. Your Site</param>
        public MailClient(IEmailClient emailClient, string fromEmail, string fromName)
        {
            this.FromName = fromName;
            this.FromEmail = fromEmail;
            this.EmailClient = emailClient;
        }

        #endregion Constructor

        #region Send

        /// <summary>
        /// Sends the specified mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        public void Send(MailMessage mail)
        {
            this.EmailClient.Send(mail);
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns></returns>
        public async Task SendAsync(MailMessage mail)
        {
            await this.EmailClient.SendAsync(mail);
        }

        #endregion Send

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.EmailClient?.Dispose();
        }

        #endregion Dispose
    }
}