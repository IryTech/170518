using System;
using System.Linq;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Web.UI.HtmlControls;

namespace IryTech.AdmissionJankari20.Web.counselling
{
    public partial class Confirmation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindPageTitleAndKeyWords();
            var objSecurePage = new SecurePage();
        
            lblUserName.Text = Common.GetStringProperCase(objSecurePage.LoggedInUserName);
            
            CheckPaymentStatus(objSecurePage.LoggedInUserId);
        }
        private void BindPageTitleAndKeyWords()
        {
            var objPage = new Common().GetPageTitleKeyWordAndDecsription("consConf");

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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in Confirmation.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        private void CheckPaymentStatus(int userId)
        {
            var objConsulling = new Consulling();
            try
            {
                var data= objConsulling.GetPaymentTransactionStatus(userId);
                if (data.Count > 0)
                {
                    var result = data.First();
                    if (result.StudentPaymentStatus == true)
                    {
                        sucess.Visible = true;
                        failure.Visible = false;
                    }
                    else
                    {
                        sucess.Visible = false;
                        failure.Visible = true;
                    }
                }
                else
                {
                    sucess.Visible = false;
                    failure.Visible = true;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching CheckPaymentStatus in Confirmation.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}