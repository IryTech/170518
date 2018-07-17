using System;
using System.Collections;
using IryTech.AdmissionJankari.BL;
using System.Web.UI;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.Account
{
    public partial class UserProfile : PageBase
    {
        SecurePage objSecurePage = new SecurePage();
        protected void Page_Load(object sender, EventArgs e)
        {
         
           if (IsPostBack) return; 
            BindPageTitleAndKeyWords();
            if (!objSecurePage.IsLoggedInUSer)
                Response.Redirect("~/Account/Login.aspx");
                BindGender();
        }
        protected void BindGender()
        {
            Hashtable ht = ClsSingelton.GetGenders();
            if (ht.Count <= 0) return;
            rbtGender.DataSource = ht;
            rbtGender.DataValueField = "Key";
            rbtGender.DataTextField = "Value";
            rbtGender.DataBind();
        }

       
        private void BindPageTitleAndKeyWords()
        {
           
            try
            {
                
                    Page.Title = "";
                    Page.Title = new SecurePage().LoggedInUserName+" profile";

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content ="profile of" + new SecurePage().LoggedInUserName +
                                ".Get your all setting and recent information";

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content ="profile of" + new SecurePage().LoggedInUserName +
                                ".Get your all setting and recent information"
                                                  
                                           };

                Page.Header.Controls.Add(metaKeywords);
             

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in UserProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            var filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);


            var i = UserManager.Instance.UpdateUserProfile(AsyncFileUpload1.FileName, "AjUserImage", objSecurePage.LoggedInUserId);

            if (i > 0)
            {
                AsyncFileUpload1.SaveAs(Server.MapPath(new Common().GetFilepath("UserImage")) + filename);
            }
            
        }
    }
}