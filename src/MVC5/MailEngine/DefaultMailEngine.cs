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
    public class DefaultMailEngine : IDisposable
    {
        #region DefaultEngine

        /// <summary>
        /// Get current instance which is initialized when application start
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DefaultMailEngine DefaultEngine { get; set; }

        #endregion DefaultEngine

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
        public DefaultMailEngine(IEmailClient emailClient, string fromEmail, string fromName)
        {
            this.FromName = fromName;
            this.FromEmail = fromEmail;
            this.EmailClient = emailClient;
        }

        #endregion Constructor

        #region RazorEngineService

        #region BuildContentFromTemplate

        ///// <summary>
        ///// Compile and run the template without a model
        ///// </summary>
        ///// <param name="templatePath">Either absolute path or relative path (to the root email template folder)</param>
        ///// <returns></returns>
        //internal string BuildContentFromTemplate(string templatePath)
        //{
        //    ITemplateKey key = this.RazorEngineService.GetKey(templatePath);
        //    return this.RazorEngineService.RunCompile(key);
        //}

        ///// <summary>
        ///// Compile and run the template with model
        ///// </summary>
        ///// <typeparam name="T">Type of model</typeparam>
        ///// <param name="templatePath">Either absolute path or relative path (to the root email template folder)</param>
        ///// <param name="model">The model for the template</param>
        ///// <param name="viewBag">The view bag.</param>
        ///// <returns></returns>
        //internal string BuildContentFromTemplate<T>(string templatePath, T model, DynamicViewBag viewBag = null)
        //{
        //    ITemplateKey key = this.RazorEngineService.GetKey(templatePath);
        //    return this.RazorEngineService.RunCompile(key, typeof(T), model, viewBag);
        //}

        #endregion BuildContentFromTemplate

        #endregion RazorEngineService

        #region Send

        /// <summary>
        /// Sends the specified mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        public void Send(MailMessage mail)
        {
            this.EmailClient.SendAsync(mail);
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