using System;
using System.Web;
using System.Web.UI;

namespace IryTech.AdmissionJankari.Components.Web.HttpModules
{
    public class MaintainScrollPositionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(ContextPreRequestHandlerExecute);
        }


        void ContextPreRequestHandlerExecute(object sender, EventArgs e)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
            {
                page.PreInit += new EventHandler(PagePreInit);
            }
        }


        static void PagePreInit(object sender, EventArgs e)
        {
            var page = sender as Page;
            if (page != null)
            {
                page.MaintainScrollPositionOnPostBack = true;
            }
        }


        public void Dispose()
        {
        }
    }
}
