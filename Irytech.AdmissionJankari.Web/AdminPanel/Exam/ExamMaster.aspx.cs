using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Web.UI;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Exam
{
    public partial class ExamMaster : SecurePage 
    {
        Common _objCommon;
        ExamProperty _objExamProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            btnUploadImage.Click += BtnUploadImageClick;
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = 10;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            
            BindAllCourseList();
            BindExamMasterDetail();
            ValidationErrorMessages();

        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlCourseNameSearch.SelectedIndex > 0)
            {
                var courseList = ExamProvider.Instance.GetExamListByCourseId(Convert.ToInt32(ddlCourseNameSearch.SelectedValue));
                if (courseList != null && courseList.Count > 0)
                {
                    lblSeccessMsg.Visible = false;
                    rptExamMaster.Visible = true;
                    rptExamMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamMaster, Common.ConvertToDataTable(courseList));
                }

            }
            else
            {
               
                if (!string.IsNullOrEmpty(txtExamNameSearch.Text.ToString()))
                {
                    _objCommon = new Common();
                    var courseList = ExamProvider.Instance.GetExamListByName(Convert.ToString(txtExamNameSearch.Text.Trim()));
                    if (courseList == null || courseList.Count <= 0) return;
                    lblSeccessMsg.Visible = false;
                    rptExamMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamMaster, Common.ConvertToDataTable(courseList));
                }
                else
                {
                    var data = ExamProvider.Instance.GetAllExamList();
                    rptExamMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamMaster, Common.ConvertToDataTable(data));
                }

            }
           

        }
       
        #region methods
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvExamName1.ErrorMessage = _objCommon.GetValidationMessage("rfvExamName1");
            rfvCourse.ErrorMessage = _objCommon.GetValidationMessage("rfvCourse");
            rfvExamFullName.ErrorMessage = _objCommon.GetValidationMessage("rfvExamFullName");
            rfvPopularName.ErrorMessage = _objCommon.GetValidationMessage("rfvPopularName");
         
        }

        #region BindAllCourseList Method
        protected void BindAllCourseList()
        {
            try
            {
                var courseList = CourseProvider.Instance.GetAllCourseList();
                if (courseList.Count > 0)
                {
                    ddlCourseName.DataSource = courseList;
                    ddlCourseName.DataTextField = "CourseName";
                    ddlCourseName.DataValueField = "CourseId";
                    ddlCourseName.DataBind();
                    ddlCourseName.Items.Insert(0, new ListItem("--Select--","0"));

                    ddlCourseNameSearch.DataSource = courseList;
                    ddlCourseNameSearch.DataTextField = "CourseName";
                    ddlCourseNameSearch.DataValueField = "CourseId";
                    ddlCourseNameSearch.DataBind();
                    ddlCourseNameSearch.Items.Insert(0, new ListItem("--Select--", "0"));

                }
                
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }
        #endregion

        #region ClearFields Method
        protected void ClearFields()
        {
            ddlCourseName.ClearSelection();
            txtEligiblityCriteria.Text = string.Empty;
            txtExamDescription.FckValue = string.Empty;
            txtExamFullName.Text = string.Empty;
            txtExamName.Text = string.Empty;
            txtPopularName.Text = string.Empty;
            txtWebSite.Text = string.Empty;
            chkStatus.Checked = false;
            imgExamLogo.Visible = false;
        }
        #endregion

        #region Insert update Exam Master Details
        protected void BindExamMasterDetail()
        {
            _objCommon = new Common();
            var data = ExamProvider.Instance.GetAllExamList();
            if (data.Count > 0)
            {
                try
                {
                    rptExamMaster.Visible = true;
                 
                    ucCustomPaging.BindDataWithPaging(rptExamMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ExamMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptExamMaster.Visible = false;

            }

        }
        #endregion

        #endregion
        private void BtnUploadImageClick(object sender, EventArgs e)
        {
            _objCommon = new Common();

            _objCommon.GetFilepath("ExamImg");
            string.Format("{0}", _objCommon.GetFilepath("ExamImg"));
          
           imgExamLogo.Visible = true;
           
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/ExamMaster.xlsx");
            if (path == null) throw new ArgumentNullException("File bit Found");
            var objDocProcess = new System.Diagnostics.Process
                                    {
                                        EnableRaisingEvents = false,
                                        StartInfo = {FileName = @path}
                                    };
            objDocProcess.Start();
        }

        protected void BtnUploadClick(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                _objExamProperty = new ExamProperty();

                var _ds = new DataSet();
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var path = MapPath(fileUploadExcel.FileName);
                fileUploadExcel.SaveAs(path);
                var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (var t in excelSheets)
                    {
                        _ds = objClsOledbdatalayer.getdata(path, t);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j <= _ds.Tables[0].Rows.Count - 1; j++)
                            {
                                _objExamProperty.CourseId = Convert.ToInt32(_ds.Tables[0].Rows[j]["CourseId"]);
                                _objExamProperty.ExamName = Convert.ToString(_ds.Tables[0].Rows[j]["ExamName"]);
                                _objExamProperty.ExamFullName = Convert.ToString(_ds.Tables[0].Rows[j]["ExamFullName"]);
                                _objExamProperty.ExamLogo = Convert.ToString(_ds.Tables[0].Rows[j]["ExamLogo"]);
                                _objExamProperty.ExamDesc = Convert.ToString(_ds.Tables[0].Rows[j]["ExamDesc"]);
                                _objExamProperty.ExamWebSite = Convert.ToString(_ds.Tables[0].Rows[j]["ExamWebsite"]);
                                _objExamProperty.ExamPopularName = Convert.ToString(_ds.Tables[0].Rows[j]["ExamPopularName"]);
                                _objExamProperty.ExamEligiblityCriteria = Convert.ToString(_ds.Tables[0].Rows[j]["ExamEligiblityCriteria"]);
                                _objExamProperty.ExamStatus = Convert.ToBoolean(_ds.Tables[0].Rows[j]["ExamStatus"]);
                                var errMsg = "";
                                var insert = ExamProvider.Instance.InsertExamDetails(_objExamProperty, 1, out errMsg);
                                if (insert <= 0) continue;
                                lblRecordsInserted.Text = "";
                                lblRecordsInserted.Text = j + " row inserted out of " + _ds.Tables[0].Rows.Count;
                            }
                        }
                    }
                    lblSeccessMsg.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                    lblSeccessMsg.Visible = true;
                    BindAllCourseList();
                }
                else
                {
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            { }
        }
        #region Insert update Exam Master Details
        protected void btnExam_Click(object sender, EventArgs e)
        {
            var fileName = this.flUploadImage.FileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                hdnFileName.Value = fileName;
                flUploadImage.SaveAs(Server.MapPath(new Common().GetFilepath("ExamImg") + fileName));
            }
            try
            {
                var objExamProperty = new ExamProperty
                                          {
                                              CourseId =
                                                  Convert.ToInt32(ddlCourseName.SelectedValue.ToString()),
                                              ExamName = Convert.ToString(txtExamName.Text.Trim()),
                                              ExamFullName = Convert.ToString(txtExamFullName.Text.Trim()),
                                              ExamPopularName =
                                                  Convert.ToString(txtPopularName.Text.Trim()),
                                              ExamEligiblityCriteria =
                                                  Convert.ToString(txtEligiblityCriteria.Text.Trim()),
                                              ExamLogo = hdnFileName.Value,
                                              ExamDesc =
                                                  Convert.ToString(txtExamDescription.FckValue.Trim()),
                                              ExamWebSite = Convert.ToString(txtWebSite.Text.Trim()),
                                              ExamStatus = chkStatus.Checked
                                          };

                string errorMsg;
                int insert;
                if (btnCourse.Text == "Add")
                {
                    insert = ExamProvider.Instance.InsertExamDetails(objExamProperty,LoggedInUserId, out errorMsg);
                    BindExamMasterDetail();
                    ClearFields();
                }
                else
                {
                    objExamProperty.ExamId = Convert.ToInt32(hdnExamMaster.Value);
                    insert = ExamProvider.Instance.UpdateExamDetails(objExamProperty, LoggedInUserId, out errorMsg);
                    BindExamMasterDetail();
                    ClearFields();
                    btnCourse.Text = "Add";
                    lblRecordsInserted.Text = "Insert";
                 
                    lblInsertUpdate.Text = "Add Exam Details";
                }
                if (insert > 0)
                {
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = errorMsg;
                }
                else
                {
                    lblErorrMsg.Visible = true;
                    lblErorrMsg.Text = errorMsg;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion
        protected void RptExamMasterItemCommand(object source, RepeaterCommandEventArgs e)
        {
         
            lblSeccessMsg.Visible = false;
         
            hdnExamMaster.Value = e.CommandArgument.ToString();
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        var examMasterList = ExamProvider.Instance.GetExamListById(Convert.ToInt32(hdnExamMaster.Value));
                        if (examMasterList.Count > 0)
                        {
                            ddlCourseName.ClearSelection();
                            var query = examMasterList.Select(result => new
                                                                            {
                                                                                result.ExamName,
                                                                                result.ExamFullName,
                                                                                result.ExamPopularName,
                                                                                result.ExamEligiblityCriteria,
                                                                                result.ExamLogo,
                                                                                result.ExamDesc,
                                                                                result.ExamWebSite, result.CourseName, result.CourseId, result.ExamStatus
                                                                            });
                            var sp = query.First();
                            BindAllCourseList();
                            ddlCourseName.Items.FindByValue(sp.CourseId.ToString()).Selected = true;
                            txtExamName.Text = sp.ExamName;
                            txtExamFullName.Text = sp.ExamFullName;
                            txtPopularName.Text = sp.ExamPopularName;
                            txtEligiblityCriteria.Text = sp.ExamEligiblityCriteria;
                            txtExamDescription.FckValue = sp.ExamDesc;
                            var img = sp.ExamLogo != "" ? sp.ExamLogo : "N/A";
                            hdnFileName.Value = img;
                            txtWebSite.Text = sp.ExamWebSite;
                            chkStatus.Checked = sp.ExamStatus;
                           
                            btnCourse.Text = "Update";
                           lblInsertUpdate.Text = "Update Exam Details of  " + sp.ExamName;
                           ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divInstituteTypeInsert','650px','sndAddExam');return false;", true);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _objCommon = new Common();
            ddlCourseName.ClearSelection();
            if (!string.IsNullOrEmpty(txtExamNameSearch.Text.ToString()))
            {
                var courseList = ExamProvider.Instance.GetExamListByName(Convert.ToString(txtExamNameSearch.Text.Trim()));
                if (courseList == null || courseList.Count <= 0) return;
                lblSeccessMsg.Visible = false;
                rptExamMaster.Visible = true;
                ucCustomPaging.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptExamMaster, Common.ConvertToDataTable(courseList));
            }
            else
            {
                rptExamMaster.Visible = true;
                BindExamMasterDetail();
            }
        }
        protected void ddlCourseNameSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourseNameSearch.SelectedIndex > 0)
            {
                var courseList = ExamProvider.Instance.GetExamListByCourseId(Convert.ToInt32(ddlCourseNameSearch.SelectedValue));
                if (courseList != null && courseList.Count > 0)
                {
                    lblSeccessMsg.Visible = false;
                    rptExamMaster.Visible = true;
                    rptExamMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptExamMaster, Common.ConvertToDataTable(courseList));
                }
                
            }
        }
    }
}