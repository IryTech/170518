using System.Web.UI.HtmlControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System;

namespace IryTech.AdmissionJankari20.Web
{
    public partial class ContentWriting : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            BindPageTitleAndKeyWords();

        }
        private void BindPageTitleAndKeyWords()
        {
            var objPage = new Common().GetPageTitleKeyWordAndDecsription("ContentWriting");

            try
            {
                if (objPage.Rows.Count > 0)
                {

                    Page.Title = "";
                    Page.Title = Convert.ToString(objPage.Rows[0]["AjPageTitle"].ToString());

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content = Convert.ToString(objPage.Rows[0]["AjPageDescription"].ToString());

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                    {
                        Name = "keywords",
                        Content =
                            Convert.ToString(objPage.Rows[0]["AjPageKeyword"].ToString())
                    };

                    Page.Header.Controls.Add(metaKeywords);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in ContentWriting.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}
