using System;
using System.Web.Mvc;

namespace MailEngine
{
    /// <summary>
    /// Render the view with layout and _webStart
    /// </summary>
    /// <seealso cref="TemplateResultBase" />
    public class TemplateResult : TemplateResultBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateResult" /> class
        /// </summary>
        /// <param name="context">Add controller context to utilise the HtmlHelper and other context-based utilities</param>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        /// <param name="layoutPage">The layout page if any (can be set in ViewStart or the View itself)</param>
        /// <exception cref="ArgumentNullException">templatePath</exception>
        public TemplateResult(ControllerContext context, string templatePath, object model = null, string layoutPage = "")
        {
            if (string.IsNullOrEmpty(templatePath))
                throw new ArgumentNullException(nameof(templatePath));

            this.ControllerContext = context ?? new MockControllerContext();

            this.TemplatePath = templatePath;
            this.LayoutPage = layoutPage;

            this.ViewData.Model = model;
            this.IsPartialView = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateResult" /> class with MockControllerContext
        /// </summary>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        /// <param name="layoutPage">The layout page if any (can be set in ViewStart or the View itself)</param>
        public TemplateResult(string templatePath, object model = null, string layoutPage = "") : this(null, templatePath, model, layoutPage)
        {
        }

        #endregion Constructor
    }
}
