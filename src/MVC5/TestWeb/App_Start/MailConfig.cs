using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void Init()
        {
            var smtpEmailClient = new SmtpEmailClient
            {
                SmtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 995,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(
                        "",
                        ""),
                    Timeout = 20000
                }
            };

            DefaultMailEngine.DefaultEngine = new DefaultMailEngine(
                smtpEmailClient,
                "test@mailengine.com",
                "Test Mail Engine"
            );

        }
    }
}