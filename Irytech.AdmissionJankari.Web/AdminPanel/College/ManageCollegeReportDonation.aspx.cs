using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class ManageCollegeReportDonation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ApplicationSettings.Instance.CollegePageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
                BindCourse();
                BindCollegeList();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            Common objCommon = new Common();
            DataTable DT = new DataTable();
            DT = objCommon.GetReportDonationCollegeList(courseId:Convert.ToInt16(ddlCourse.SelectedValue));
            ucCustomPaging.BindDataWithPaging(rptCollegeList, objCommon.RemoveDuplicateRows(DT, "AjCollegeBranchCourseId"));
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
                    ddlCourse.Items.FindByValue(objCommon.CourseId.ToString()).Selected = true;
                  
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
                const string addInfo = "Error while executing BindCourse in ManageCollegeReportDonation.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        // Method to Bind the college List for report donation
        private void BindCollegeList()
        {
            try
            {
                Common objCommon = new Common();

                var DT = objCommon.GetReportDonationCollegeList(courseId :Convert.ToInt16(ddlCourse.SelectedValue));
             ucCustomPaging.BindDataWithPaging(rptCollegeList, objCommon.RemoveDuplicateRows(DT,"AjCollegeBranchCourseId"));

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeList in ManageCollegeReportDonation.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCollegeList();
        }

        protected void rptCollegeList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Common objCommon = new Common();
                string errMsg="";
                int i = objCommon.UpdateReportDonationStatus( out errMsg,collegeBranchCourseId:Convert.ToInt32(e.CommandArgument));
                lblMsg.Visible = true;
                if (i > 0)
                {
                    lblMsg.CssClass = "success";
                    lblMsg.Text = errMsg;
                    BindCollegeList();
                }
                else
                {
                    lblMsg.CssClass = "error";
                    lblMsg.Text = "Some problem while updating details";
                }
            }
        }

        protected void rptCollegeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Common _objCommon = new Common();
                var rptContactDetails = (Repeater)e.Item.FindControl("rptContactDetails");
                var hndCollegeBranchCourseId = (Literal)(e.Item.FindControl("hndCollegeBranchCourseId"));
                if (rptContactDetails != null)
                {

                    var dt =
                        _objCommon.GetReportDonationCollegeList(CollegeBranchCourseId: Convert.ToInt32(hndCollegeBranchCourseId.Text));



                      if (dt.Rows.Count > 0)
                        {
                            rptContactDetails.Visible = true;
                            rptContactDetails.DataSource = dt;
                            rptContactDetails.DataBind();

                        }
                   
                    
                }
            }
        }
        protected void rptContactDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        
            try
            {
                if (e.CommandName == "Edit")
                {
                    Common objCommon = new Common();
                    string errMsg = "";
                    int i = objCommon.UpdateReportDonationStatus(out errMsg, reportDonationId: Convert.ToInt32(e.CommandArgument));
                    lblMsg.Visible = true;
                    if (i > 0)
                    {
                        lblMsg.CssClass = "success";
                        lblMsg.Text = errMsg;
                        BindCollegeList();
                    }
                    else
                    {
                        lblMsg.CssClass = "error";
                        lblMsg.Text = "Some problem while updating details";
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching rptCollegeContactDetails_ItemCommand in ManageCollegeReportDonation.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

    }
}