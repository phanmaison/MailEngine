using System;
using System.IO;
using System.Web.Mvc;

namespace MailEngine
{
    /// <summary>
    /// Abstract class for email template
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class TemplateResultBase : IDisposable
    {
        #region Properties

        /// <summary>
        /// The controller context for rendering the view
        /// </summary>
        /// <value>
        /// The controller context
        /// </value>
        public ControllerContext ControllerContext { get; protected set; }
        /// <summary>
        /// Absolute path to the view
        /// </summary>
        /// <value>
        /// The view path
        /// </value>
        protected string TemplatePath { get; set; }
        /// <summary>
        /// Layout page if any
        /// </summary>
        /// <value>
        /// The layout page
        /// </value>
        protected string LayoutPage { get; set; }

        /// <summary>
        /// Render as partial view or not
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is partial view; otherwise, <c>false</c>
        /// </value>
        protected bool IsPartialView { get; set; }

        #region ViewBag

        /// <summary>
        /// Gets the view bag.
        /// </summary>
        public dynamic ViewBag => ControllerContext.Controller.ViewBag;

        #endregion ViewBag

        #region ViewData

        /// <summary>
        /// Gets or sets the view data
        /// </summary>
        /// <value>
        /// The view data
        /// </value>
        public ViewDataDictionary ViewData
        {
            get
            {
                if (ControllerContext == null)
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentNullException("ControllerContext");

                if (ControllerContext.Controller == null)
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentNullException("ControllerContext.Controller");

                return ControllerContext.Controller.ViewData;
            }
        }

        #endregion ViewData

        #endregion Properties

        #region Render

        private static readonly string[] FileExtensions = {
            "cshtml",
            "vbhtml",
        };

        /// <summary>
        /// Renders the view and return the string output
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            RazorViewEngine viewEngine = new RazorViewEngine();

            RazorView view = this.IsPartialView ?
                new RazorView(this.ControllerContext, this.TemplatePath, null, false, FileExtensions) :
                new RazorView(this.ControllerContext, this.TemplatePath, this.LayoutPage, true, FileExtensions);

            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(this.ControllerContext, view, ViewData,
                    new TempDataDictionary(), writer);

                // render the view to StringWriter
                view.Render(viewContext, writer);
                viewEngine.ReleaseView(this.ControllerContext, view);

                return writer.ToString();
            }
        }

        #endregion Render

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion IDisposable
    }
}
