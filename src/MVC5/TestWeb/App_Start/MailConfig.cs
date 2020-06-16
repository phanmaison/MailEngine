using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using MailEngine;

namespace TestWeb
{
    /// <summary>
    /// Config for email
    /// </summary>
    public static class MailConfig
    {
        public const string ToEmailAddress = "test_mail_engine@yopmail.com";
        public const string ToEmailName = "Receiver Name";

        public const string FromEmailName = "Mail Engine";
        public const string FromEmailAddress = "quarreat@gmail.com";
        public const string FromEmailPassword = "mail@engine@123";

        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Init()
        {
            var smtpEmailClient = new SmtpEmailClient
            {
                SmtpClient = new SmtpClient
                {
                    Host = SmtpServer,
                    Port = SmtpPort,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(
                        FromEmailAddress,
                        FromEmailPassword),
                    Timeout = 20000
                }
            };

            MailClient.Default = new MailClient(
                smtpEmailClient,
                FromEmailAddress,
                FromEmailName
            );
        }
    }
}