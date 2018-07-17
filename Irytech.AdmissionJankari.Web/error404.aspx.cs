using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
using IryTech.AdmissionJankari.BL;


namespace IryTech.AdmissionJankari20.Web
{
    public partial class error404 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindPageTitleAndKeyWords();
            
        }
        private void BindPageTitleAndKeyWords()
        {
            try
            {
                Page.Title = "Admissionjankari :Page not found";
                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "robots";
                metadesc.Content ="noindex,nofollow";
                Page.Header.Controls.Add(metadesc);
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in error404.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public Control metaKeywords { get; set; }

    
        
    }
}