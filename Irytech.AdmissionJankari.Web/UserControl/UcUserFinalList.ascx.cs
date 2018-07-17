using System;
using System.Net.Mail;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcUserFinalList : System.Web.UI.UserControl
    {
        protected string SPonserValue;
        readonly SecurePage ObjSecure = new SecurePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserIntertestedList(ObjSecure.LoggedInUserId);

        }
        public string Sponser
        {
            get
            {
                return SPonserValue;

            }
            set { SPonserValue = value; }
        }
        // Method To Bind User Final Intertested List
        private void BindUserIntertestedList(int userId)
        {
            var objConsulling = new Consulling();
            try
            {
                var ds = objConsulling.GetIntertestedCollege(userId);
                if (ds.Tables[0].Rows.Count <= 0) return;
                rptCollegeDetails.DataSource = ds.Tables[0];
                rptCollegeDetails.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing BindUserIntertestedList in  UcUserFinalList.ascx   :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            var objMailTemplates = new MailTemplates();
            var collegeName="" ;
            var objConsulling = new Consulling();
            try
            {

                var ds = objConsulling.GetIntertestedCollege(ObjSecure.LoggedInUserId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        collegeName += "<div><strong>" + (i + 1) + ". " + ds.Tables[0].Rows[i]["AjCollegeBranchName"] +
                                       "</strong></div><br/>";
                    }
                    var mail = new MailMessage
                                   {
                                       From = new MailAddress(ApplicationSettings.Instance.Email),
                                       Subject = "AdmissionJankari:Counselling Information"
                                   };

                    var body = objMailTemplates.MailBodyForCOunsellingQuery(new SecurePage().LoggedInUserName,
                                                                            collegeName.TrimEnd(','));
                    mail.Body = body;
                    mail.To.Add(new SecurePage().LoggedInUserEmailId);
                    Utils.SendMailMessageAsync(mail);

                    Response.Redirect(Utils.ApplicationRelativeWebRoot + "counselling/Payment.aspx");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing btnConfirm_Click in  UcUserFinalList.ascx   :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}