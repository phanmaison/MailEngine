using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MailEngine
{
    /// <summary>
    /// Mock controller context for using MailResult without a request
    /// <para>Caution when using the Route table (such as Url.Action) as the RouteTable is created manually</para>
    /// </summary>
    /// <seealso cref="System.Web.Mvc.ControllerContext" />
    public sealed class MockControllerContext : ControllerContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockControllerContext"/> class
        /// </summary>
        public MockControllerContext()
        {
            this.Controller = new MockController();

            if (System.Web.HttpContext.Current != null)
            {
                this.HttpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
            }
            else
            {
                this.HttpContext = new MockHttpContextBase();
            }

            this.RouteData.Values.Add("controller", "MailEngineMockController");
            this.RouteData.Values.Add("action", "MailEngineMockControllerAction");

            this.RequestContext = new RequestContext(this.HttpContext, this.RouteData);
        }

        public static MockControllerContext New => new MockControllerContext();
    }
}
