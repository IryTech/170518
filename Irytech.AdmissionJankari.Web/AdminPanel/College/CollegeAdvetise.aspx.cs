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
    public partial class CollegeAdvetise : SecurePage
    {
            
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ApplicationSettings.Instance.CollegePageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCourse();
            BindSponserType();
            BindSponserCollege();
        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {

            BindSponserCollege();

        }

        // Method to Bind The Course 
        private void BindCourse()
        {
            var courseData = CourseProvider.Instance.GetAllCourseList();
            if (courseData.Count > 0)
            {
                ddlCourseList.DataSource = courseData;
                ddlCourseList.DataTextField = "CourseName";
                ddlCourseList.DataValueField = "CourseId";
                ddlCourseList.DataBind();
                ddlCourseList.Items.Insert(0, new ListItem("Select Course", "0"));
                ddlAdstCourse.DataSource = courseData;
                ddlAdstCourse.DataTextField = "CourseName";
                ddlAdstCourse.DataValueField = "CourseId";
                ddlAdstCourse.DataBind();
                ddlAdstCourse.Items.Insert(0, new ListItem("Select Course", "0"));
            }

            else
            {
                ddlAdstCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                ddlCourseList.Items.Insert(0, new ListItem("Select Course", "0"));

            }

        }
        // Method to Bind The Sponser Type
        private void BindSponserType()
        {
            var collegeAssociation = CollegeProvider.Instance.GetAllCollegeAssociationCategoryType();
            if (collegeAssociation.Count > 0)
            {
                ddlAdvstType.DataSource = collegeAssociation;
                ddlAdvstType.DataTextField = "AssociationCategoryType";
                ddlAdvstType.DataValueField = "AssociationCategoryTypeId";
                ddlAdvstType.DataBind();
                ddlAdvstType.Items.Insert(0, new ListItem("Select Ads", "0"));
                rbtSponser.DataSource = collegeAssociation;
                rbtSponser.DataTextField = "AssociationCategoryType";
                rbtSponser.DataValueField = "AssociationCategoryTypeId";
                rbtSponser.DataBind();
                rbtSponser.Items.Insert(0, new ListItem("Select Ads", "0"));
                
            }
            else
            {
                ddlAdvstType.Items.Insert(0, new ListItem("Select Ads", "0"));
                rbtSponser.Items.Insert(0, new ListItem("Select Ads", "0"));
            }
        }

        // Method to Bind The Sponser College List
        private void BindSponserCollege()
        {
            var objCommon = new Common();
            var data = objCommon.GetCollegeSponser();
            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    rptCollegeList.Visible = true;
                    ucCustomPaging.Visible = true;
                   ucCustomPaging.BindDataWithPaging(rptCollegeList, data);
                }
                else
                {
                    rptCollegeList.Visible = false;
                    ucCustomPaging.Visible = false;
                    rptCollegeList.Visible = false;
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "info";
                    lblMsg.Text = "No data Found";
                }
            }
            else
            {
                rptCollegeList.Visible = false;
                ucCustomPaging.Visible = false;
                rptCollegeList.Visible = false;
                lblMsg.Visible = true;
                lblMsg.CssClass = "info";
                lblMsg.Text = "No data Found";
            }
        }


        // Method to Insert update the details

        private void InsertUpdateDetails()
        {
            var errMsg = "";
            var i = CollegeProvider.Instance.UpdateCollegeAssociation(txtCollegeAdvst.Text, Convert.ToInt32(ddlAdstCourse.SelectedValue),
                Convert.ToInt32(ddlAdvstType.SelectedValue), Common.GetDateFromString(txtStartDate.Text), Common.GetDateFromString(txtEndDate.Text), Convert.ToInt16(txtPriority.Text), txtRedirectURL.Text, new SecurePage().LoggedInUserId, chkbannerStatus.Checked == true ? true : false, out  errMsg);
            lblMsg.Visible = true;
            lblMsg.Text = errMsg;
            if (i > 0)
            {
                lblMsg.CssClass = "success";
                BindSponserCollege();
                ClearControl();
            }
            else
            {
                lblMsg.CssClass = "info";
            }
           
        }

        protected void btnCollegeAssociat_Click(object sender, EventArgs e)
        {
            InsertUpdateDetails();
        }

        protected void ddlCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourseList.SelectedIndex > 0)
            {
                BindSponserCollegeListByCourse(Convert.ToInt32(ddlCourseList.SelectedValue));
            }
        }

        protected void rbtSponser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtSponser.SelectedIndex > 0)
            {
                BindSponserCollegeListBySponserType(Convert.ToInt32(rbtSponser.SelectedValue));
            }
        }

        // Method to Bind The Sponser College List by Course

        private void BindSponserCollegeListByCourse(int courseId)
        {
            var objCommon = new Common();
            var data = objCommon.GetCollegeSponser(courseId: courseId);
            if (data != null)
            {
                if (data.Rows.Count > 0)
                {

                    ucCustomPaging.BindDataWithPaging(rptCollegeList, data);
                    rptCollegeList.Visible = true;
                    ucCustomPaging.Visible = true;
                    lblMsg.Visible = false;
                }
                else
                {
                    rptCollegeList.Visible = false;
                    ucCustomPaging.Visible = false;
                    rptCollegeList.Visible = false;
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "info";
                    lblMsg.Text = "No data Found";
                }
            }
            else
            {
                rptCollegeList.Visible = false;
                ucCustomPaging.Visible = false;
                rptCollegeList.Visible = false;
                lblMsg.Visible = true;
                lblMsg.CssClass = "info";
                lblMsg.Text = "No data Found";
            }
        }

        // Method to Bind The Sponser College List According To Spnsoer Type
        private void BindSponserCollegeListBySponserType(int sponserType)
        {
            var objCommon = new Common();
            var data = objCommon.GetCollegeSponser(sponserType: sponserType);
            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    rptCollegeList.Visible = true;
                    ucCustomPaging.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCollegeList, data);
                }
                else
                {
                    rptCollegeList.Visible = false;
                    ucCustomPaging.Visible = false;
                    rptCollegeList.Visible = false;
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "info";
                    lblMsg.Text = "No data Found";
                }
            }
            else
            {
                rptCollegeList.Visible = false;
                ucCustomPaging.Visible = false;
                rptCollegeList.Visible = false;
                lblMsg.Visible = true;
                lblMsg.CssClass = "info";
                lblMsg.Text = "No data Found";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCollegeName.Text))
            {
                var objCommon = new Common();
                var data = objCommon.GetCollegeSponser(collegeName: txtCollegeName.Text);
                if (data != null)
                {
                    if (data.Rows.Count > 0)
                    {
                        rptCollegeList.Visible = true;
                        ucCustomPaging.Visible = true;
                        lblMsg.Visible = false;
                        ucCustomPaging.BindDataWithPaging(rptCollegeList, data);
                    }
                    else
                    {
                        rptCollegeList.Visible = false;
                        ucCustomPaging.Visible = false;
                        rptCollegeList.Visible = false;
                        lblMsg.Visible = true;
                        lblMsg.CssClass = "info";
                        lblMsg.Text = "No data Found";
                    }
                }
                else
                {
                    rptCollegeList.Visible = false;
                    ucCustomPaging.Visible = false;
                    rptCollegeList.Visible = false;
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "info";
                    lblMsg.Text = "No data Found";
                }
            }
            else
            {
                BindSponserCollege();
            }
        }

        protected void rptCollegeList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            { 
                var objCommon = new Common();
                var collegeBranchCourseId=int.Parse(e.CommandArgument.ToString());
                var hndAssociationType = (HiddenField)(e.Item.FindControl("hndAdvstType"));
                var data = objCommon.GetCollegeSponser(collegeBranchCourseId: collegeBranchCourseId);
                var query = from r in data.AsEnumerable()
                            where r.Field<int>("AjCollegeAssociationCategoryId") == Convert.ToInt16(hndAssociationType.Value)
                            select r;
                if (query.FirstOrDefault() != null)
                {
                    foreach (var collegeDetails in query)
                    {
                        ddlAdvstType.ClearSelection();
                        ddlAdvstType.Items.FindByValue(hndAssociationType.Value).Selected = true;
                        ddlAdvstType.Enabled = false;
                        txtCollegeAdvst.Text = Convert.ToString(collegeDetails["AjCollegeBranchName"]);
                        txtCollegeAdvst.Enabled = false;
                        ddlAdstCourse.ClearSelection();
                        ddlAdstCourse.Items.FindByValue(Convert.ToString(collegeDetails["AjCourseId"])).Selected = true;
                        ddlAdstCourse.Enabled = false;
                        txtPriority.Text = Convert.ToString(collegeDetails["AjPriorityId"]);
                        txtRedirectURL.Text = Convert.ToString(collegeDetails["AjBannerUrl"]);
                        txtStartDate.Text = string.IsNullOrEmpty(Convert.ToString(collegeDetails["AjAdsBannerStartDate"])) ? "" : Convert.ToDateTime(collegeDetails["AjAdsBannerStartDate"]).ToString("dd/MM/yyyy");
                        txtEndDate.Text = string.IsNullOrEmpty(Convert.ToString(collegeDetails["AjAdsBannerEndDate"])) ? "" : Convert.ToDateTime(collegeDetails["AjAdsBannerEndDate"]).ToString("dd/MM/yyyy"); 
                        chkbannerStatus.Checked = string.IsNullOrEmpty(Convert.ToString(collegeDetails["AjBannerStatus"])) ?
                                                    false : Convert.ToBoolean(collegeDetails["AjBannerStatus"]) == true ? true : false;
                        btnCollegeAssociat.Text = "Update";
                        lblInsertUpdate.Text =  txtCollegeAdvst.Text;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divCollegeInsert','650px','sndCollegeInsert');return false;", true);
                    }
                }
                    
            }
        }


        // Method to Clear the control
        private void ClearControl()
        {
            ddlAdstCourse.ClearSelection();
            ddlAdstCourse.Enabled = true;
            txtCollegeAdvst.Enabled = true;
            txtCollegeAdvst.Text = "";
            ddlAdvstType.Enabled = true;
            ddlAdvstType.ClearSelection();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtRedirectURL.Text = "";
            btnCollegeAssociat.Text = "Save";
            lblInsertUpdate.Text = "Add Ads";
        }
    }
}