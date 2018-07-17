using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class UpdateNewCollegeRegisteration : SecurePage
    {
        private readonly Common _objCommon = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCollegeList.PageSize = ClsSingelton.PageSize;
            ucCollegeList.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCollegeList.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
          
            BindRegisteredCollege(0);


        }

        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var collegeDtaa = _objCommon.GetCollegeRegistered(0, null);
                if (collegeDtaa.Tables.Count > 0)
                {
                    if (collegeDtaa.Tables[0].Rows.Count > 0)
                    {
                        ucCollegeList.Visible = true;
                        ucCollegeList.BindDataWithPaging(rptCollegeList, collegeDtaa.Tables[0]);
                    }
                    else
                    {
                        ucCollegeList.Visible = false;
                        rptCollegeList.Visible = false;
                        
                    }
                }
                else
                {
                    ucCollegeList.Visible = false;
                    rptCollegeList.Visible = false;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  PagerPageIndexChanged in UpdateCollegeRegisteration.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindRegisteredCollege(int collegeId)
        {
            try
            {
                var collegeDtaa = _objCommon.GetCollegeRegistered(collegeId, null);
                if (collegeDtaa.Tables.Count > 0)
                {
                    if (collegeDtaa.Tables[0].Rows.Count > 0)
                    {
                        rptCollegeList.Visible = true;
                        ucCollegeList.Visible = true;
                        ucCollegeList.BindDataWithPaging(rptCollegeList, collegeDtaa.Tables[0]);

                    }
                    else
                    {
                        ucCollegeList.Visible = false;
                        rptCollegeList.Visible = false;
                    }
                }
                else
                {
                    ucCollegeList.Visible = false;
                    rptCollegeList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo =
                    "Error while executing BindRegisteredCollege in AdminPanel/UpdateCollegeRegisterationStatus.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
         
        protected void RptCollegeListItemCommand(object source, RepeaterCommandEventArgs e)
        {
           
        }

       

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCollege.Text.ToString()))
                SearchByCollegeName();
            else
                BindRegisteredCollege(0);
        }
        private void SearchByCollegeName()
        {
            try
            {
                var collegeDtaa = _objCommon.GetCollegeRegistered(0, txtCollege.Text.Trim());
                if (collegeDtaa.Tables.Count > 0)
                {
                    if (collegeDtaa.Tables[0].Rows.Count > 0)
                    {
                        rptCollegeList.Visible = true;
                        ucCollegeList.Visible = true;
                        ucCollegeList.BindDataWithPaging(rptCollegeList, collegeDtaa.Tables[0]);

                    }
                    else
                    {
                        ucCollegeList.Visible = false;
                        rptCollegeList.Visible = false;
                    }
                }
                else
                {
                    ucCollegeList.Visible = false;
                    rptCollegeList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo =
                    "Error while executing SearchByCollegeName() in AdminPanel/UpdateCollegeRegisterationStatus.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public string GetModerateCollege(object collegeUserId)
        {
          
            return _objCommon.CheckModerateCollegeUser(Convert.ToInt32(collegeUserId));
        }

        protected void btnUniversityCategoryName_Click(object sender, EventArgs e)
        {
            try
            {
                var errMsg = "";
                var i = new Common().UpdateUserStatus(Convert.ToInt32(hndCollegeBranchId.Value), out errMsg, LoggedInUserId, Convert.ToBoolean(rbtLoginStatus.SelectedValue));
                var collegeDtaa = new Common().GetCollegeuserDetails(Convert.ToInt32(hndCollegeBranchId.Value));
                if (Convert.ToBoolean(rbtLoginStatus.SelectedValue))
                {
                   
                    if (collegeDtaa.Rows.Count > 0)
                    {
                        var objMailTemplete = new MailTemplates();
                        var userData = UserManagerProvider.Instance.GetUserListById(Convert.ToInt32(Convert.ToInt32(hndCollegeBranchId.Value)));
                        var objMail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject =
                               collegeDtaa.Rows[0]["AjCollegeBranchName"].ToString() +
                                " College Verification Mail By AdmissionJankari.com"
                        };
                        var url = "<a href=" + Utils.AbsoluteWebRoot + "account/college-login" + ">College Login</a>";
                        var mailbody =
                            objMailTemplete.MailToCollegeForRegisterationVerification(
                                userData.First().UserFullName.Trim(),
                               collegeDtaa.Rows[0]["AjCollegeBranchName"].ToString(),
                                userData.First().UserEmailid.Trim(), userData.First().MobileNo.Trim(), url);
                        objMail.Body = mailbody;
                        objMail.To.Add("naval.ipec@gmail.com");
                        objMail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(objMail);
                    }
                }


                if (!string.IsNullOrEmpty(txtCollege.Text.Trim()))
                    SearchByCollegeName();
                else
                    BindRegisteredCollege(0);
                lblErrMsg.CssClass = "success";
                lblErrMsg.Text =  collegeDtaa.Rows[0]["AjCollegeBranchName"].ToString() + " college status updated successfully";
            }

            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing RptCollegeListItemCommand in AdminPanel/UpdateCollegeRegisterationStatus.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
                
    }
}