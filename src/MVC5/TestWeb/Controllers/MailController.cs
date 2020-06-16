using System;
using System.Web.Mvc;
using MailEngine;
using TestWeb.Models;

namespace TestWeb.Controllers
{
    public class MailController : Controller
    {
        /// <summary>
        /// Sends the or show email.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        /// <param name="model">The model.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="send">if set to <c>true</c> [send].</param>
        /// <returns></returns>
        private ActionResult SendOrShowEmail(string templatePath, object model, string subject, bool send = false)
        {
            try
            {
                if (send)
                {
                    Email.Create()
                        .SetSubject(subject)
                        .SetToAddress(MailConfig.ToEmailAddress, MailConfig.ToEmailName)
                        .SetContentTemplate(templatePath, model)
                        .Send();

                    return Content($"<p style='color: #0000ff'>Mail is sent, please check your inbox at {MailConfig.ToEmailAddress}</p>");
                }
                else
                {
                    return View(templatePath, model);
                }
            }
            catch (Exception e)
            {
                return Content($"<p style='color: #ff0000'>There is error: {e.Message}<br><br>{e.StackTrace.Replace("\n", "<br>")}</p>");
            }
        }

        public ActionResult TestViewBag(bool send)
        {
            ViewData["CustomerName"] = "Test customer 123";
            ViewBag.CustomerCount = 1000;

            ViewBag.Title = "TestViewBag";

            return SendOrShowEmail("~/Views/EmailTemplate/TestViewData.cshtml", null, nameof(TestViewBag), send);
        }

        public ActionResult TestModel(bool send)
        {
            var  model = new CustomerModel
            {
                CustomerName = "John Smith",
                OrderCount = 15,
                TotalBudget = 900000
            };

            ViewBag.Title = "TestModel";

            return SendOrShowEmail("~/Views/EmailTemplate/TestModel.cshtml", model, nameof(TestModel), send);
        }


        public ActionResult TestViewBase(bool send)
        {
            ViewBag.Title = "TestViewBase";

            return SendOrShowEmail("~/Views/EmailTemplate/TestViewBase.cshtml", null, nameof(TestViewBase), send);
        }
    }
}
