using System;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MailEngine
{
    // https://stackoverflow.com/questions/3702526/is-there-a-way-to-process-an-mvc-view-aspx-file-from-a-non-web-application


    public class SelfHost : MarshalByRefObject
    {
        #region Config

        /// <summary>
        /// Gets the current SelfHost instance
        /// </summary>
        /// <value>
        /// The current
        /// </value>
        public static SelfHost Current { get; private set; }

        /// <summary>
        /// Configure the self host
        /// </summary>
        /// <param name="physicalDir">The absolute path to the web application (root folder of the host)</param>
        /// <param name="virtualDir">The virtual folder of the application if any</param>
        public static void Config(string physicalDir, string virtualDir = "/")
        {
            Current = (SelfHost)ApplicationHost.CreateApplicationHost(
                typeof(SelfHost), virtualDir, physicalDir);
        }

        #endregion Config

        //public TemplateResult TestCreateTemplate(string templatePath)
        //{
        //    ObjRef obj = this.CreateObjRef(typeof(TemplateResult));

        //    //obj.

        //}

        #region Template

        #region CreateTemplate

        public TemplateResult CreateTemplate(ControllerContext context, string templatePath,
            object model = null, string layoutPage = "")
        {
            return new TemplateResult(context, templatePath, model, layoutPage);
        }

        public TemplateResult CreateTemplate(string templatePath, object model = null, string layoutPage = "")
        {
            return new TemplateResult(templatePath, model, layoutPage);
        }

        #endregion CreateTemplate

        #region CreatePartialTemplate

        public PartialTemplateResult CreatePartialTemplate(ControllerContext context, string templatePath, object model = null)
        {
            return new PartialTemplateResult(context, templatePath, model);
        }

        public PartialTemplateResult CreatePartialTemplate(string templatePath, object model = null)
        {
            return new PartialTemplateResult(templatePath, model);
        }

        #endregion CreateTemplatePartial

        #endregion Template

        #region RenderTemplate
        
        #region RenderTemplate

        public string RenderTemplate(ControllerContext context, string templatePath,
            object model = null, string layoutPage = "")
        {
            return (new TemplateResult(context, templatePath, model, layoutPage)).Render();
        }

        public string RenderTemplate(string templatePath, object model = null, string layoutPage = "")
        {
            return (new TemplateResult(templatePath, model, layoutPage)).Render();
        }

        #endregion CreateTemplate

        #region CreatePartialTemplate

        public string RenderPartialTemplate(ControllerContext context, string templatePath, object model = null)
        {
            return (new PartialTemplateResult(context, templatePath, model)).Render();
        }

        public string RenderPartialTemplate(string templatePath, object model = null)
        {
            return (new PartialTemplateResult(templatePath, model)).Render();
        }

        #endregion CreateTemplatePartial




        #endregion RenderTemplate




        //public string ViewToString(string viewPath)
        //{
        //    ControllerContext controllerContext = new MockControllerContext();

        //    RazorViewEngine viewEngine = new RazorViewEngine();

        //    RazorView view = true ?
        //        new RazorView(controllerContext, viewPath, null, false, FileExtensions) :
        //        new RazorView(controllerContext, viewPath, "layout-path", true, FileExtensions);
        //    ViewDataDictionary viewData = controllerContext.Controller.ViewData;

        //    using (StringWriter writer = new StringWriter())
        //    {
        //        ViewContext viewContext = new ViewContext(controllerContext, view, viewData,
        //            new TempDataDictionary(), writer);

        //        // render the view to StringWriter
        //        view.Render(viewContext, writer);
        //        viewEngine.ReleaseView(controllerContext, view);

        //        return writer.ToString();
        //    }


        //    //StringBuilder sb = new StringBuilder();
        //    //using (StringWriter sw = new StringWriter(sb))
        //    //{
        //    //    using (HtmlTextWriter tw = new HtmlTextWriter(sw))
        //    //    {
        //    //        SimpleWorkerRequest workerRequest = new SimpleWorkerRequest(viewPath, "", tw);
        //    //        HttpContext.Current = new HttpContext(workerRequest);

        //    //        ViewDataDictionary<T> viewDataDictionary = new ViewDataDictionary<T>(model);
        //    //        foreach (KeyValuePair<string, object> pair in viewData)
        //    //        {
        //    //            viewDataDictionary.Add(pair.Key, pair.Value);
        //    //        }

        //    //        WebViewPage view = (WebViewPage)BuildManager.CreateInstanceFromVirtualPath(viewPath, typeof(object));

        //    //        HttpContext.Current.Server.Execute(viewPath, sw);

        //    //        return sb.ToString();

        //    //        //Type viewType = view.GetType();

        //    //        //ViewPage viewPage = view as ViewPage;
        //    //        //if (viewPage != null)
        //    //        //{
        //    //        //    viewPage.ViewData = viewDataDictionary;
        //    //        //}
        //    //        //else
        //    //        //{
        //    //        //    ViewUserControl viewUserControl = view as ViewUserControl;
        //    //        //    if (viewUserControl != null)
        //    //        //    {
        //    //        //        viewPage = new ViewPage();
        //    //        //        viewPage.Controls.Add(viewUserControl);
        //    //        //    }
        //    //        //}

        //    //        //if (viewPage != null)
        //    //        //{
        //    //        //    HttpContext.Current.Server.Execute(viewPage, tw, true);

        //    //        //    return sb.ToString();
        //    //        //}

        //    //        //throw new InvalidOperationException();
        //    //    }
        //}

        ///// <summary>
        ///// Creates the host.
        ///// </summary>
        ///// <returns></returns>
        //public static SelfHost CreateHost(string physicalDir, string virtualDir)
        //{
        //    // create a self-host application

        //    return (SelfHost)ApplicationHost.CreateApplicationHost(
        //        typeof(SelfHost), virtualDir, physicalDir);

        //    //Type hostType = typeof(SelfHost);

        //    //const string _appId = "SelfHostAppId";

        //    //ApplicationManager currentApplicationManager = ApplicationManager.GetApplicationManager();


        //    //// create BuildManagerHost in the worker app domain
        //    ////ApplicationManager appManager = ApplicationManager.GetApplicationManager();
        //    //Type buildManagerHostType = typeof(HttpRuntime).Assembly.GetType("System.Web.Compilation.BuildManagerHost");
        //    //IRegisteredObject buildManagerHost = currentApplicationManager.CreateObject(_appId, buildManagerHostType, virtualDir,
        //    //    physicalDir, false);

        //    //// call BuildManagerHost.RegisterAssembly to make Host type loadable in the worker app domain
        //    //buildManagerHostType.InvokeMember("RegisterAssembly",
        //    //    BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
        //    //    null,
        //    //    buildManagerHost,
        //    //    new object[] { hostType.Assembly.FullName, hostType.Assembly.Location });

        //    //// create Host in the worker app domain
        //    //// FIXME: getting FileLoadException Could not load file or assembly 'WebDev.WebServer20, Version=4.0.1.6, Culture=neutral, PublicKeyToken=f7f6e0b4240c7c27' or one of its dependencies. Failed to grant permission to execute. (Exception from HRESULT: 0x80131418)
        //    //// when running dnoa 3.4 samples - webdev is registering trust somewhere that we are not
        //    ////return (SelfHost)currentApplicationManager.CreateObject(_appId, hostType, virtualDir, physicalDir, false);

        //}
    }
}
