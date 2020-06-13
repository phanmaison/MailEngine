using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Instrumentation;

namespace MailEngine
{
    /// <summary>
    /// Mock HttpContextBase for using MailResult without a request
    /// </summary>
    /// <seealso cref="System.Web.HttpContextBase" />
    public sealed class MockHttpContextBase : HttpContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpContextBase"/> class
        /// </summary>
        public MockHttpContextBase()
        {
            // mock a request
            this.Request = new HttpRequestWrapper(new HttpRequest("mock-request-file", "http://mock-site/mock-action/", "")
            {
                Browser = new HttpBrowserCapabilities() { Capabilities = new HybridDictionary() }
            });

            // mock a response
            this.Response = new HttpResponseWrapper(new HttpResponse(new StringWriter())
            {

            });

            // PageInstrumentationService is used a lot if the helper
            this.PageInstrumentation = new PageInstrumentationService();

            // consider to create more mock object if you found exception when using the view

        }

        /// <summary>
        /// When overridden in a derived class, gets a key/value collection that can be used to organize and share data between a module and a handler during an HTTP request.
        /// </summary>
        public override IDictionary Items { get; } = new HybridDictionary();

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.
        /// </summary>
        public override HttpRequestBase Request { get; }

        /// <summary>
        /// When implemented in a derived class, gets a reference to the page-instrumentation service instance for this request.
        /// </summary>
        public override PageInstrumentationService PageInstrumentation { get; }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpResponse" /> object for the current HTTP response.
        /// </summary>
        public override HttpResponseBase Response { get; }

        //public override HttpSessionStateBase Session { get; }
    }

}
