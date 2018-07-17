using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Exam
{
    public partial class ManageExamFormMaster : SecurePage
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindAllExamFormList();
            BindAllExamList();
            BindAllCourseList();
           
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var data = ExamProvider.Instance.GetAllExamFormDetails();
            if (data.Count > 0)
            {
                try
                {
                    rptExamFromMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamFromMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ExamFormMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptExamFromMaster.Visible = false;
                
            }

        }
        #region  Method
        #region BindAllExamList Method
        protected void BindAllExamList()
        {
            try
            {
                var examList = ExamProvider.Instance.GetAllExamList();
                if (examList.Count > 0)
                {
                    ddlExamName.DataSource = examList;
                    ddlExamName.DataTextField = "ExamName";
                    ddlExamName.DataValueField = "ExamId";
                    ddlExamName.DataBind();
                    ddlExamName.Items.Insert(0,ListItem.FromString("Select Exam"));
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        #endregion

        #region BindAllCourseList Method
        protected void BindAllCourseList()
        {
            try
            {
                var courseList = CourseProvider.Instance.GetAllCourseList();
                if (courseList.Count > 0 )
                {
                    lblSeccessMsg.Visible = false;
                    ddlCourseName.DataSource = courseList;
                    ddlCourseName.DataTextField = "CourseName";
                    ddlCourseName.DataValueField = "CourseId";
                    ddlCourseName.DataBind();
                    ddlCourseName.Items.Insert(0,ListItem.FromString("Select Course"));
                }
                else
                {
                    ddlCourseName.ClearSelection();
                    ddlCourseName.Items.Insert(0, ListItem.FromString("Select Course"));
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }
        #endregion

        #region BindAllExamFormList Method
        protected void BindAllExamFormList()
        {
            var data = ExamProvider.Instance.GetAllExamFormDetails();
            if (data.Count > 0)
            {
                try
                {
                    rptExamFromMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamFromMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ExamFormMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptExamFromMaster.Visible = false;

            }

        }
        #endregion

        #region bindExamFormBySubjectName Method
        protected void BindExamFormBySubjectName()
        {
            try
            {
                var examFromList = ExamProvider.Instance.GetExamFormDetailsByExamSubject(Convert.ToString(txtSubjectName.Text));
                if (examFromList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    rptExamFromMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamFromMaster, Common.ConvertToDataTable(examFromList));
                }
                else
                {
                    rptExamFromMaster.Visible = false;
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        #endregion

        #region bindExamFormByCourseId Method
        protected void BindExamFormByCourseId()
        {
            try
            {
                var examFromList = ExamProvider.Instance.GetExamFormDetailByCourseId(Convert.ToInt32(ddlCourseName.SelectedValue.ToString()));
                if (examFromList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    rptExamFromMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamFromMaster, Common.ConvertToDataTable(examFromList));
                }
                else
                {
                    rptExamFromMaster.Visible = false;
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        #endregion

        #region bindExamFormByCourseIdSubjectId Method
        protected void BindExamFormByCourseIdSubjectId()
        {
            try
            {
                var examFromList = ExamProvider.Instance.GetExamFormDetailsByExamSubjectCourseId(Convert.ToInt32(ddlCourseName.SelectedValue.ToString()), Convert.ToString(txtSubjectName.Text));
                if (examFromList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    rptExamFromMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamFromMaster, Common.ConvertToDataTable(examFromList));
                }
                else
                {
                    rptExamFromMaster.Visible = false;
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        #endregion

        #endregion

        protected void RptExamFromMasterItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblSeccessMsg.Text = string.Empty;
            lblSeccessMsg.Visible = false;
            var examFormId = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("AddExamFormMaster.aspx?ExamFormId=" + examFormId,true);
                    break;
            }
        }

        protected void BtnSubmitClick(object sender, EventArgs e)
        {
            lblSeccessMsg.Text = string.Empty;
            lblSeccessMsg.Visible = false;
            _objCommon = new Common();
            ddlExamName.ClearSelection();
            if ((!string.IsNullOrEmpty(txtSubjectName.Text.ToString())) && (ddlCourseName.SelectedIndex > 0))
            {
                BindExamFormByCourseIdSubjectId();
            }
            else if ((string.IsNullOrEmpty(txtSubjectName.Text.ToString())) && (ddlCourseName.SelectedIndex > 0))
            {
                BindExamFormByCourseId();
            }
            else if ((!string.IsNullOrEmpty(txtSubjectName.Text.ToString())) && (ddlCourseName.SelectedIndex == 0))
            {
                BindExamFormBySubjectName();
            }
            else if ((string.IsNullOrEmpty(txtSubjectName.Text.ToString())) && (ddlCourseName.SelectedIndex == 0))
            {
                rptExamFromMaster.Visible = true;
                BindAllExamFormList();
            }
            else
            {
                rptExamFromMaster.Visible = true;
                BindAllExamFormList();
            }
        }

        protected void DdlExamNameSelectedIndexChanged(object sender, EventArgs e)
        {
            lblSeccessMsg.Text = string.Empty;
            lblSeccessMsg.Visible = false;
            _objCommon = new Common();
            ddlCourseName.ClearSelection();
            txtSubjectName.Text = string.Empty;
            try
            {
                var examFromList = ExamProvider.Instance.GetExamFormDetailsByExamId(Convert.ToInt32(ddlExamName.SelectedValue.ToString()));
                if (examFromList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    rptExamFromMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamFromMaster, Common.ConvertToDataTable(examFromList));
                }
                else
                {
                    ucCustomPaging.Visible = false;
                    rptExamFromMaster.Visible = false;
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        
    }
 }