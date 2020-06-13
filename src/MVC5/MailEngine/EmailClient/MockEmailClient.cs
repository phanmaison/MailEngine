using System.Net.Mail;
using System.Threading.Tasks;

namespace MailEngine
{
    /// <summary>
    /// A mock email client which send nothing (for testing)
    /// </summary>
    /// <seealso cref="MailEngine.IEmailClient" />
    public class MockEmailClient : IEmailClient
    {
        /// <summary>
        /// Pretends to send a MailMessage
        /// </summary>
        /// <param name="message">The MailMessage to send</param>
        public void Send(MailMessage message)
        {
            // Do nothing
        }

        /// <summary>
        /// Pretends to asynchronously send a MailMessage
        /// </summary>
        /// <param name="message">The MailMessage to send</param>
        /// <returns></returns>
        public Task SendAsync(MailMessage message)
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {
        }
    }
}