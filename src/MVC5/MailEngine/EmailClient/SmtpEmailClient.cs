using System.Net.Mail;
using System.Threading.Tasks;

namespace MailEngine
{
    /// <summary>
    /// A email client based on SMTP for sending email
    /// <para>Wrapper of SmtpClient</para>
    /// </summary>
    /// <seealso cref="MailEngine.IEmailClient" />
    public class SmtpEmailClient : IEmailClient
    {
        /// <summary>
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>
        /// The SMTP client.
        /// </value>
        public SmtpClient SmtpClient { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Sends a MailMessage using a default instance of the .NET Smtp Client
        /// </summary>
        /// <param name="message">The MailMessage to send</param>
        void IEmailClient.Send(MailMessage message)
        {
            // Note: there is exception when sending 2 email at the same time using the same client

            SmtpClient.Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sends a MailMessage asynchronously using a default instance of the .NET Smtp Client
        /// </summary>
        /// <param name="message">The MailMessage to send</param>
        /// <returns></returns>
        async Task IEmailClient.SendAsync(MailMessage message)
        {
            await SmtpClient.SendMailAsync(message);
        }

        public void Dispose()
        {
            SmtpClient?.Dispose();
        }
    }
}