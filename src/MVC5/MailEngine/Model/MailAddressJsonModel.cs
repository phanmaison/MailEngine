using System.Net.Mail;

namespace MailEngine
{
    /// <summary>
    /// Model to convert MailAddress to string and vice versa
    /// </summary>
    public class MailAddressJsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailAddressJsonModel"/> class.
        /// </summary>
        public MailAddressJsonModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailAddressJsonModel"/> class.
        /// </summary>
        /// <param name="mailAddress">The mail address.</param>
        public MailAddressJsonModel(MailAddress mailAddress)
        {
            this.Address = mailAddress.Address;
            this.DisplayName = mailAddress.DisplayName;
        }

        /// <summary>
        /// Full email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }
    }
}
