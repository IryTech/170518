using System;

using System.Data;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class RequestforRefund : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UcCustomPaging.PageSize = ClsSingelton.PageSize;
            UcCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            UcCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCourseList();
            bindRefundRequest();
            hdnCourseId.Value = "";
            hdnEmailId.Value = "";
            hdnFormNo.Value = "";
            validateEmail.ValidationExpression = ClsSingelton.aRegExpEmail;
        }
        protected void BindCourseList()
        {

            try
            {
                var courseList = CourseProvider.Instance.GetAllCourseList();
                if (courseList.Count > 0)
                {
                    ddlCourse.DataSource = courseList;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                    ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else { ddlCourse.Items.Insert(0, new ListItem("--Select--", "0")); }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCourseList in RequestforRefund.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void bindRefundRequest(bool parameter=true)
        {
            lblMsg.Visible = false;
            try
            {
                DataTable data = new Consulling().GetAllRefundRequest();
                var newData= data.AsEnumerable();
                if (!string.IsNullOrEmpty(hdnCourseId.Value.Trim().ToString()) && hdnCourseId.Value!="0")
                {
                    newData = newData.Where(result => result.Field<Int32>("AjCourseId") == Convert.ToInt32(hdnCourseId.Value));
                }
                if (!string.IsNullOrEmpty(hdnEmailId.Value.Trim().ToString()))
                {
                    newData = newData.Where(result => result.Field<string>("AjUserEmail")==hdnEmailId.Value.Trim().ToString());
                }
                if (!string.IsNullOrEmpty(hdnFormNo.Value.Trim().ToString()))
                {
                    newData = newData.Where(result => result.Field<string>("AjStudentFormNumber").StartsWith(hdnFormNo.Value.Trim().ToString(), StringComparison.OrdinalIgnoreCase));
                }
                if (newData.Count() > 0)
                {
                    UcCustomPaging.BindDataWithPaging(rptRefundRequest, newData.CopyToDataTable(), parameter);

                    rptRefundRequest.Visible = true;
                }
                else
                {
                    rptRefundRequest.Visible = false;
                    lblMsg.CssClass = "error";
                    lblMsg.Text = "No Records Found.";
                    lblMsg.Visible = true;
                }
            }
            catch(Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  bindRefundRequest in RequestforRefund.aspx.cs :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {

            bindRefundRequest(false);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            hdnCourseId.Value = ddlCourse.SelectedValue;
            hdnEmailId.Value = txtFilterbyEmail.Text.Trim().ToString();
            hdnFormNo.Value = txtFilterbyFormNo.Text.Trim().ToString();
            bindRefundRequest();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            hdnFormNo.Value = "";
            hdnEmailId.Value = "";
            hdnCourseId.Value = "";
            clearForm();
            bindRefundRequest();
        }
        protected void clearForm()
        {
            ddlCourse.ClearSelection();
            txtFilterbyEmail.Text = "";
            txtFilterbyFormNo.Text = "";
        }
    }
}