using System.Web.Mvc;

namespace MailEngine
{
    /// <summary>
    /// Render partial the view
    /// </summary>
    /// <seealso cref="TemplateResult" />
    public class PartialTemplateResult : TemplateResult
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PartialTemplateResult"/> class
        /// </summary>
        /// <param name="context">Add controller context to utilise the HtmlHelper and other context-based utilities</param>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        public PartialTemplateResult(ControllerContext context, string templatePath, object model = null) : base(context, templatePath, model)
        {
            this.IsPartialView = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartialTemplateResult" /> class with MockControllerContext
        /// </summary>
        /// <param name="templatePath">Absolute path to the template</param>
        /// <param name="model">The model</param>
        public PartialTemplateResult(string templatePath, object model = null) : base(templatePath, model)
        {
            this.IsPartialView = true;
        }

        #endregion Constructor

    }
}
