using System.Web.Mvc;

namespace MailEngine
{
    /// <summary>
    /// Mock controller for using MailResult without a request
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public sealed class MockController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockController"/> class
        /// </summary>
        public MockController()
        {
            this.ViewData = new ViewDataDictionary();
        }
    }
}
