using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailEngine;
using TestConsole.Models;

namespace TestConsole
{
    class Program
    {
        public const string ToEmailAddress = "test_mail_engine@yopmail.com";
        public const string ToEmailName = "Receiver Name";

        public const string FromEmailName = "Mail Engine";
        public const string FromEmailAddress = "quarreat@gmail.com";
        public const string FromEmailPassword = "mail@engine@123";

        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;

        /*
         * IMPORTANT NOTICE
         * 1) The console application will create a self-hosting application hence we need to
         * move the DLL into bin folder
         * Please create bin folder and copy the DLL + exe file there
         *
         * 2) Go to properties of View file and set Copy to output = Copy Always / Copy if newer
         *
         * 3) Model need to be serialized, simply put the attribute [Serializable] on each model
         *
         */

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("This application is to test sending mail from console (or background process) where there is no web request");

                // Setup smtp client

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

                // setup self-host
                SelfHost.Config(AppDomain.CurrentDomain.BaseDirectory);

                Console.WriteLine("1) Model");

                TestModel();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception found: {ex.Message}\n\n{ex.StackTrace}");
                Console.ReadLine();
            }

        }

        private static void TestModel()
        {
            var model = new CustomerModel
            {
                CustomerName = "John Smith",
                OrderCount = 15,
                TotalBudget = 900000
            };

            var content = SelfHost.Current.RenderPartialTemplate("~/Views/EmailTemplate/TestModel.cshtml", model);

            Email.Create()
                .SetSubject("TestModel")
                .SetToAddress(ToEmailAddress, ToEmailName)
                .SetContent(content)
                //.SetContentTemplate("~/Views/EmailTemplate/TestModel.cshtml", model)
                .Send();

        }
    }
}
