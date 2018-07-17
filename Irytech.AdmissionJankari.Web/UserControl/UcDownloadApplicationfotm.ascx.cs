using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Net.Mail;
using IryTech.AdmissionJankari.BO;
using System.IO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcDownloadApplicationfotm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
                BindCourse();
                ValidateControl();
        }


        // Method to Bind The Course

        private void BindCourse()
        {
            Common objCommon = new Common();
            var data = objCommon.GetCourseListForOnlineConsulling();
            try
            {
                if (data.Rows.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataValueField = "AjCourseId";
                    ddlCourse.DataTextField = "AjOnlineConsullingCourseText";
                    ddlCourse.DataBind();
                    ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlCourse.Items.FindByValue(objCommon.CourseId.ToString()).Selected = true;
                }
                else
                {
                    ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in UcDownloadApplicationfotm.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void ValidateControl()
        {
            revContactNo.ValidationExpression = ClsSingelton.aRegExpMobile;
            revEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
           
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Common _objCommon = new Common();
            string Msg = "";
            var userid = 0;
            if (Utils.IsEmailValid(txtEmailId.Text))
            {
                MailTemplates _objMailTemplete = new MailTemplates();
                if (Utils.IsMobileValid(txtMobile.Text))
                {
                    QueryProperty objQueryProperty = new QueryProperty
                    {
                        StudentName =txtName.Text   ,
                        UserEmailId = txtEmailId.Text,
                        UserMobileNo = txtMobile.Text,
                        StudentCityName = "",
                        StudentCourseId = Convert.ToInt16(ddlCourse.SelectedValue),
                        StudentQuery = "User Download the Application from",
                        StudentCourseName = ddlCourse.SelectedItem.ToString(),
                    };
                    int i = QueryProvider.Instance.InsertCommonQuickQuery(objQueryProperty, out Msg,out  userid);
                    if (i == 2)
                    {
                        var ObjMail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Registration"
                        };
                        var mailbody = _objMailTemplete.MailBodyForRegistation(txtName.Text, txtEmailId.Text, txtMobile.Text);
                        ObjMail.Body = mailbody;
                        ObjMail.To.Add(objQueryProperty.UserEmailId);
                        ObjMail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(ObjMail);
                    }
                    var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari:Download the application form"
                    };
                    var body = _objMailTemplete.MailBodyForDownloadApplicationfrom(txtName.Text, ddlCourse.SelectedItem.ToString());
                    mail.Body = body;
                    mail.To.Add(objQueryProperty.UserEmailId);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);

                     mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari:Download the application form"
                    };
                    body = _objMailTemplete.MailBodyForAdminToDownloadApplicationfrom(txtName.Text, ddlCourse.SelectedItem.ToString(), txtMobile.Text);
                    mail.Body = body;
                    mail.To.Add(ClsSingelton.bccDirectAdmission);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);
                    ClearControl();

                    try
                    {
                        ClearControl();
                        lblMsg.Visible = true;
                        lblMsg.Text = "Thank you for downloading the application form";
                        string fName = HttpContext.Current.Server.MapPath("\\Resource\\Download") + @"\" + "ApplicationForm_FinalDec12_2012.pdf";

                        FileInfo fi = new FileInfo(fName);
                        long sz = fi.Length;

                        Response.ClearContent();
                        Response.ContentType = MimeType(Path.GetExtension(fName));
                        Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)));
                        Response.AddHeader("Content-Length", sz.ToString("F0"));
                        Response.TransmitFile(fName);
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        ClearControl();
                        lblMsg.Visible = true;
                        lblMsg.Text = "Thank you for downloading the application form";

                    }

                }
                else
                {
                    lblMsg.Visible = true;
                  lblMsg.Text= _objCommon.GetValidationMessage("revContactNo");
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetValidationMessage("revEmail");
               
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        // Method to To clear The Control
        protected void ClearControl()
        {
            txtEmailId.Text = "";
            txtMobile.Text = "";
            txtName.Text = "";
        }
        private  string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;

            string ext = Extension.ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }
    }
}