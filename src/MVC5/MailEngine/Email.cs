using System;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MailEngine
{
    /// <summary>
    /// Mail Message extern
    /// </summary>
    /// <seealso cref="T:System.Net.Mail.MailMessage" />
    public class Email : MailMessage
    {
        #region MailClient

        /// <summary>
        /// Gets or sets the mail engine.
        /// </summary>
        public MailClient MailClient { get; }

        #endregion MailClient

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class
        /// </summary>
        /// <param name="client">The engine</param>
        public Email(MailClient client)
        {
            this.BodyEncoding = Encoding.UTF8;
            this.IsBodyHtml = true;
            this.MailClient = client;
            this.SubjectEncoding = Encoding.UTF8;

            this.From = new MailAddress(client.FromEmail, client.FromName);
        }

        #endregion Constructor

        #region Helper

        /// <summary>
        /// If the email address existed in To or Cc
        /// </summary>
        /// <param name="email">The email</param>
        private bool IsEmailExist(string email) =>
            this.To.Any(x => x.Address.Equals(email, StringComparison.InvariantCultureIgnoreCase)) ||
            this.CC.Any(x => x.Address.Equals(email, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// If the email address existed in To or Cc
        /// </summary>
        /// <param name="email">The email</param>
        private bool IsEmailExist(IEmailAddress email) => IsEmailExist(email?.EmailAddress);

        #endregion Helper

        #region Create

        /// <summary>
        /// Create new email
        /// </summary>
        /// <param name="client">The engine</param>
        public static Email Create(MailClient client = null)
        {
            if (client == null)
                client = MailClient.Default;

            var mail = new Email(client);

            return mail;
        }

        #endregion Create

        #region Send

        /// <summary>
        /// Sends this email using the mail client
        /// </summary>
        public void Send()
        {
            MailClient.Send(this);
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        public async Task SendAsync()
        {
            await MailClient.SendAsync(this);
        }


        #endregion Send

        #region Set Properties

        #region SetToAddress

        /// <summary>
        /// Sets to address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="name">The name.</param>
        public Email SetToAddress(string email, string name = "")
        {
            if (string.IsNullOrEmpty(email) || IsEmailExist(email))
                return this;

            this.To.Add(new MailAddress(email, name));

            return this;
        }

        /// <summary>
        /// Sets to address.
        /// </summary>
        /// <param name="email">The email</param>
        public Email SetToAddress(IEmailAddress email)
        {
            if (string.IsNullOrEmpty(email?.EmailAddress) || IsEmailExist(email)) return this;

            this.To.Add(new MailAddress(email.EmailAddress, email.EmailName));

            return this;
        }

        #endregion SetToAddress

        #region SetCCAddress

        /// <summary>
        /// Sets the cc address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="name">The name.</param>
        public Email SetCcAddress(string email, string name = "")
        {
            if (string.IsNullOrEmpty(email) || IsEmailExist(email))
                return this;

            this.CC.Add(new MailAddress(email, name));

            return this;
        }

        /// <summary>
        /// Sets the cc address.
        /// </summary>
        /// <param name="email">The email</param>
        public Email SetCcAddress(IEmailAddress email)
        {
            if (string.IsNullOrEmpty(email?.EmailAddress) || IsEmailExist(email)) return this;

            this.CC.Add(new MailAddress(email.EmailAddress, email.EmailName));

            return this;
        }

        #endregion SetCCAddress

        #region SetBCCAddress

        /// <summary>
        /// Sets the BCC address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="name">The name.</param>
        public Email SetBccAddress(string email, string name = "")
        {
            if (string.IsNullOrEmpty(email))
                return this;

            this.Bcc.Add(new MailAddress(email, name));

            return this;
        }

        /// <summary>
        /// Sets the BCC address.
        /// </summary>
        /// <param name="email">The email</param>
        public Email SetBccAddress(IEmailAddress email)
        {
            if (email == null) return this;

            this.Bcc.Add(new MailAddress(email.EmailAddress, email.EmailName));

            return this;
        }

        #endregion SetBCCAddress

        #region SetReplyToAddress

        /// <summary>
        /// Sets the reply to address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="name">The name.</param>
        public Email SetReplyToAddress(string email, string name = "")
        {
            if (!string.IsNullOrEmpty(email))
                this.ReplyToList.Add(new MailAddress(email, name));

            return this;
        }

        /// <summary>
        /// Sets the reply to address.
        /// </summary>
        /// <param name="email">The email</param>
        public Email SetReplyToAddress(IEmailAddress email)
        {
            this.ReplyToList.Add(new MailAddress(email.EmailAddress, email.EmailName));

            return this;
        }

        #endregion SetReplyToAddress

        #region SetSubject

        /// <summary>
        /// Set the subject of email
        /// </summary>
        /// <param name="subject">The subject.</param>
        public Email SetSubject(string subject)
        {
            // Some email provider may group the email into thread => need to find
            // solution to avoid this grouping => the title should be different

            this.Subject = subject;

            return this;
        }

        #endregion SetSubject

        #region SetContent

        /// <summary>
        /// Set body content of email
        /// </summary>
        /// <param name="content">The content.</param>
        public Email SetContent(string content)
        {
            this.Body = content;

            return this;
        }

        #endregion SetContent

        #region SetPriority

        /// <summary>
        /// Sets the priority.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public Email SetPriority(MailPriority priority)
        {
            this.Priority = priority;

            return this;
        }

        #endregion SetPriority

        #region SetContentTemplate

        /// <summary>
        /// Sets the content from mail result (with Layout and ViewStart)
        /// </summary>
        /// <param name="controllerContext">Add controller context to utilise the HtmlHelper and other context-based utilities</param>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        /// <param name="layoutPage">The layout page if any (can be set in ViewStart or the View itself)</param>
        public Email SetContentTemplate(ControllerContext controllerContext, string templatePath,
            object model = null, string layoutPage = "")
        {
            using (TemplateResult templateResult = new TemplateResult(controllerContext, templatePath, model, layoutPage))
            {
                this.Body = templateResult.Render();
            }

            return this;
        }

        /// <summary>
        /// Sets the content from mail result (with Layout and ViewStart)
        /// </summary>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        /// <param name="layoutPage">The layout page if any (can be set in ViewStart or the View itself)</param>
        /// <returns></returns>
        public Email SetContentTemplate(string templatePath, object model = null, string layoutPage = "")
        {
            using (TemplateResult templateResult = new TemplateResult(templatePath, model, layoutPage))
            {
                this.Body = templateResult.Render();
            }

            return this;
        }

        #endregion SetContentTemplate

        #region SetContentPartialTemplate

        /// <summary>
        /// Sets the content from partial mail result (without Layout and ViewStart)
        /// </summary>
        /// <param name="controllerContext">Add controller context to utilise the HtmlHelper and other context-based utilities</param>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Email SetContentPartialTemplate(ControllerContext controllerContext, string templatePath, object model = null)
        {
            using (TemplateResult templateResult = new PartialTemplateResult(controllerContext, templatePath, model))
            {
                this.Body = templateResult.Render();
            }

            return this;
        }

        /// <summary>
        /// Sets the content from partial mail result (without Layout and ViewStart)
        /// </summary>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Email SetContentPartialTemplate(string templatePath, object model = null)
        {
            using (TemplateResult templateResult = new PartialTemplateResult(templatePath, model))
            {
                this.Body = templateResult.Render();
            }

            return this;
        }

        #endregion SetContentPartialTemplate

        #region SetAttachment

        /// <summary>
        /// Sets the attachment.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        public Email SetAttachment(Attachment attachment)
        {
            this.Attachments.Add(attachment);

            return this;
        }

        /// <summary>
        /// Sets the attachment.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="contentType">MIME type of the content. If not provided, will be retrieved from MimeMapping</param>
        /// <returns></returns>
        public Email SetAttachment(string fileName, Stream stream, string contentType = "")
        {
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = System.Web.MimeMapping.GetMimeMapping(fileName);
            }

            Attachment attachment = new Attachment(stream, fileName, contentType)
            {
                NameEncoding = Encoding.UTF8
            };

            return SetAttachment(attachment);
        }

        /// <summary>
        /// Sets the attachment.
        /// </summary>
        /// <param name="attachments">The attachments.</param>
        /// <returns></returns>
        public Email SetAttachment(Attachment[] attachments)
        {
            if (attachments != null && attachments.Length > 0)
            {
                foreach (Attachment a in attachments)
                {
                    this.Attachments.Add(a);
                }
            }

            return this;
        }

        #endregion SetAttachment

        #endregion Set Properties
    }
}