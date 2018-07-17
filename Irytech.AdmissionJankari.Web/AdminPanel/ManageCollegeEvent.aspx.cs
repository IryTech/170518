using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class ManageCollegeEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ApplicationSettings.Instance.CollegePageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
                BindCourse();
                BindEventList();
        }


        // method to Bind The Course 
        private void BindCourse()
        {
            try
            {
                Common objCommon = new Common();
                var courseData = CourseProvider.Instance.GetAllCourseList();
                if (courseData.Count > 0)
                {
                    ddlCourse.DataSource = courseData;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                    ddlCourse.Items.FindByValue(objCommon.CourseId.ToString()).Selected=true;
                    ddlCourse.Items.Insert(0, new ListItem("Select", "0"));
                }

                else
                {
                    ddlCourse.Items.Insert(0, new ListItem("Select", "0"));
                  
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in ManageCollegeEvent.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void txtSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Convert.ToString(hndEventId.Value )))
                    InsertUpdateCollegeEvent(0, Convert.ToInt16(ddlCourse.SelectedValue), txtCollegeName.Text.Trim(), txtEventName.Text, txtEventLocation.Text, Common.GetDateFromString(txtEventDate.Text), chkEventStatus.Checked == true ? true : false, txtEventDesc.Text);
            else
            {
                InsertUpdateCollegeEvent(Convert.ToInt32(hndEventId.Value), Convert.ToInt16(ddlCourse.SelectedValue), txtCollegeName.Text.Trim(), txtEventName.Text, txtEventLocation.Text, Common.GetDateFromString(txtEventDate.Text), chkEventStatus.Checked == true ? true : false, txtEventDesc.Text);
                    btnSave.Text="Save";
                    hndEventId.Value="";
            }

        }



        // Method to Insert Update The College event
        private void InsertUpdateCollegeEvent(int evnetId, int courseId,string collegeName,string eventName, string eventLocation,DateTime eventDateTime, bool evenetStatus, string eventDesc)
        {
            string errMsg = "";
            try
            {
                int i = CollegeProvider.Instance.InsertUpdateCollegeEvent(collegeName, courseId, eventName, eventLocation, eventDateTime,eventDesc, evenetStatus, out errMsg, evnetId);
                lblMsg.Visible = true;
                lblMsg.Text=errMsg;
                if (i > 0)
                {
                    lblMsg.CssClass = "success";
                    ClearControl();
                    BindEventList();
                }

                else
                    lblMsg.CssClass = "error";
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateCollegeEvent in ManageCollegeEvent.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        // Method to clear The Control
        private void ClearControl()
        {
            txtCollegeName.Text = "";
            txtEventDate.Text = "";
            txtEventDesc.Text = "";
            txtEventLocation.Text = "";
            txtEventName.Text = "";
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var DT = new System.Data.DataTable();
            DT = CollegeProvider.Instance.GetAllEvent();
            ucCustomPaging.BindDataWithPaging(rptEventList, DT);
        }


         // Method to Bind The Event List
        private void BindEventList()
        {
            System.Data.DataTable DT = new System.Data.DataTable();
            try
            {
                DT = CollegeProvider.Instance.GetAllEvent();
                ucCustomPaging.BindDataWithPaging(rptEventList, DT);
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindEventList in ManageCollegeEvent.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void rptEventList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                System.Data.DataTable DT = new System.Data.DataTable();
                DT = CollegeProvider.Instance.GetEventById(Convert.ToInt32(e.CommandArgument));
                if (DT.Rows.Count > 0)
                {
                    txtCollegeName.Text = Convert.ToString(DT.Rows[0]["AjCollegeBranchName"]);
                    txtEventDate.Text = Convert.ToString(DT.Rows[0]["AjCollegeEventDate"]);
                    txtEventDesc.Text = Convert.ToString(DT.Rows[0]["AjCollegeBranchEventDesc"]);
                    txtEventLocation.Text = Convert.ToString(DT.Rows[0]["AjCollegeEventLocation"]);
                    txtEventName.Text = Convert.ToString(DT.Rows[0]["AjCollegeEventName"]);
                    chkEventStatus.Checked = Convert.ToBoolean(DT.Rows[0]["AjCollegeEventStatus"]);
                    hndEventId.Value = e.CommandArgument.ToString();
                    btnSave.Text = "Update";
                }

            }
        }
    }
}