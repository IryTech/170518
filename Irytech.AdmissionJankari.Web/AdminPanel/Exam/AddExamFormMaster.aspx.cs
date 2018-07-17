using System;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Exam
{
    public partial class AddExamFormMaster : SecurePage 
    {
        Common _objCommon;
        ClsOleDBDataWrapper _objClsOledbdatalayer;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblHeader.Text = "Add Exam Form Master";
            ValidateFields();
            GetUrlTitle();
            BindCourse();
            if (Request.QueryString["ExamFormId"] == null) return;
            btnExamForm.Text = "Update";
            FillEditExamFormDetails();
        }
        private void GetUrlTitle()
        {
            hdnExamUrl.Value = Convert.ToString(ApplicationSettings.Instance.UrlLenght);
            hdnExamTag.Value = Convert.ToString(ApplicationSettings.Instance.MetaTagLenght);
            hdnExamTitle.Value = Convert.ToString(ApplicationSettings.Instance.TitleLenght);
            hdnExamMetaDesc.Value = Convert.ToString(ApplicationSettings.Instance.MetaKeywordLenght);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/ExamMaster.xlsx");
            if (path == null) throw new ArgumentNullException("File bit Found");
            System.Diagnostics.Process objDocProcess = new System.Diagnostics.Process();
            objDocProcess.EnableRaisingEvents = false;
            objDocProcess.StartInfo.FileName = @path; objDocProcess.Start();
        }
        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload") ?? "N/A";
            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {              
                var objExamFormProperty = new ExamFormProperty();
                _objClsOledbdatalayer = new ClsOleDBDataWrapper();
                string path = MapPath(fileUploadExcel.FileName);
                string[] excelSheets = _objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (var ds in excelSheets.Select(t => _objClsOledbdatalayer.getdata(path, t)).Where(_ds => _ds.Tables[0].Rows.Count > 0))
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count - 1; j++)
                        {
                            objExamFormProperty.ExamId = Convert.ToInt32(ds.Tables[0].Rows[j]["ExamId"]);
                            objExamFormProperty.ExamFormUrl = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormUrl"]);
                            objExamFormProperty.ExamFormTitle = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormTitle"]);
                            objExamFormProperty.ExamFormMetaDesc = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormMetaDesc"]);
                            objExamFormProperty.ExamFormKeywords = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormMetaTag"]);
                            objExamFormProperty.ExamFormSubject = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSubject"]);
                            objExamFormProperty.ExamFormYear = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormYear"]);
                            objExamFormProperty.ExamFormWebsite = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormWebsite"]);
                            objExamFormProperty.ExamFormSaleStartDate = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSaleStartDate"]);
                            objExamFormProperty.ExamFromSaleStartDateRemark = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSaleStartDateRemark"]);
                            objExamFormProperty.ExamFormSaleEndDate = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSaleEndDate"]);
                            objExamFormProperty.ExamFormSaleEndDateRemark = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSaleEndDateRemark"]);
                            objExamFormProperty.ExamFormSubmitDate = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSubmitDate"]);
                            objExamFormProperty.ExamFormSubmitDateRemark = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSubmitDateRemark"]);
                            objExamFormProperty.ExamFormResultDate = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormResultDate"]);
                            objExamFormProperty.ExamFormResultDateReamrk = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormResultDateRemark"]);
                            objExamFormProperty.ExamFormResultWebsite = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormResultWebsite"]);
                            objExamFormProperty.ExamFormPrice = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormPrice"]);
                            objExamFormProperty.ExamFormStore = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormStore"]);
                            objExamFormProperty.ExamFormCenter = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormCenter"]);
                            objExamFormProperty.ExamFormDd = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormDD"]);
                            objExamFormProperty.ExamFormSyllabus = Convert.ToString(ds.Tables[0].Rows[j]["ExamFormSyllabus"]);
                            objExamFormProperty.ExamFormStatus = Convert.ToBoolean(ds.Tables[0].Rows[j]["ExamFormStatus"]);
                            var errMsg = "";
                            var _insert = ExamProvider.Instance.InsertExamFormDetails(objExamFormProperty, LoggedInUserId, out errMsg);
                        }
                        lblSeccessMsg.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                        lblSeccessMsg.Visible = true;
                        Response.Redirect("ManageExamFormMaster.aspx", true);
                    }
                }
                else
                {
                    lblErorrMsg.Visible = true;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                string addInfo = "Error  :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #region methods
        
        protected void BindAllExamList()
        {
            try
            {
                var examList = ExamProvider.Instance.GetAllExamList().OrderBy(result=>result.ExamName).ToList();
                if (examList.Count > 0)
                {
                    ddlExamName.DataSource = examList;
                    ddlExamName.DataTextField = "ExamName";
                    ddlExamName.DataValueField = "ExamId";
                    ddlExamName.DataBind();
                    ddlExamName.Items.Insert(0, new ListItem("Select Exam","0"));

                }
                else { ddlExamName.Items.Insert(0, new ListItem("Select Exam", "0")); }
               
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        protected void FillEditExamFormDetails()
        {
            int examFormId = Convert.ToInt32(Request.QueryString["ExamFormId"].ToString());
            var ExamFormList = ExamProvider.Instance.GetExamFormDeatilsById(examFormId);
            if (ExamFormList.Count > 0)
            {
                ViewState["StateId"] = ExamFormList;
                var query = from result in ExamFormList
                            select new
                            {
                                ExamId = result.ExamId,
                                ExamName = result.ExamName,
                                ExamFromUrl = result.ExamFormUrl,
                                ExamFormTitle = result.ExamFormTitle,
                                ExamFormMetaDesc = result.ExamFormMetaDesc,
                                ExamFormMetaTeg = result.ExamFormKeywords,
                                ExamFormSubject = result.ExamFormSubject,
                                ExamFormYear = result.ExamFormYear,
                                ExamFormWebsite = result.ExamFormWebsite,
                                ExamFormSaleStartDate = result.ExamFormSaleStartDate,
                                ExamFormSaleStartDateRemark = result.ExamFromSaleStartDateRemark,
                                ExamFormSaleEndDate = result.ExamFormSaleEndDate,
                                ExamFromSaleEndDateRemark = result.ExamFormSaleEndDateRemark,
                                ExamFormSubmitDate = result.ExamFormSubmitDate,
                                ExamFormSubmitDateRemark = result.ExamFormSubmitDateRemark,
                                ExamFromResultDate = result.ExamFormResultDate,
                                ExamFromResultDateRemark = result.ExamFormResultDateReamrk,
                                ExamFormResultWebsite = result.ExamFormResultWebsite,
                                ExamFormPrice = result.ExamFormPrice,
                                ExamFromStore = result.ExamFormStore,
                                ExamFormCenter = result.ExamFormCenter,
                                ExamFormDD = result.ExamFormDd,
                                ExamFormSyllabus = result.ExamFormSyllabus,
                                ExamFromStatus = result.ExamFormStatus
                            };
                var sp = query.First();
                BindAllExamList();
                ddlExamName.ClearSelection();
                ddlExamName.Items.FindByValue(sp.ExamId.ToString()).Selected = true;
                txtExamFormUrl.Text = sp.ExamFromUrl;
                txtFormtitle.Text = sp.ExamFormTitle;
                txtFormMetaDesc.Text = sp.ExamFormMetaDesc;
                txtFormMataTag.Text = sp.ExamFormMetaTeg;
                txtFormSubject.Text = sp.ExamFormSubject;
                txtFormYear.Text = sp.ExamFormYear;
                txtFormWebsite.Text = sp.ExamFormWebsite;
                txtSaleExectStartDate1.Text = sp.ExamFormSaleStartDate;
                txtFormSaleStartDateRemark.Text = sp.ExamFormSaleStartDateRemark;
                txtSaleExectEndDate2.Text = sp.ExamFormSaleEndDate;
                txtFormSaleEndDateRemark.Text = sp.ExamFromSaleEndDateRemark;
                txtFormSubmitExectDate1.Text = sp.ExamFormSubmitDate;
                txtFormSubmitDateRemark.Text = sp.ExamFormSubmitDateRemark;
                txtResultExactDate.Text = sp.ExamFromResultDate;
                txtResultDateRemark.Text = sp.ExamFromResultDateRemark;
                txtFormResultWebsite.Text = sp.ExamFormResultWebsite;
                txtExamFormPrice.Text = sp.ExamFormPrice;
                txtExamFormStore.Text = sp.ExamFromStore;
                txtExamFormCenter.Text = sp.ExamFormCenter;
                txtExamFormDD.Text = sp.ExamFormDD;
                txtExamFormSyllabus.Text = sp.ExamFormSyllabus;
                if (sp.ExamFromStatus == true)
                {
                    chkFormStatus.Checked = true;
                }
                else
                {
                    chkFormStatus.Checked = false;
                }
                lblInsertUpdate.Text = "Record Update Of " + sp.ExamFormTitle;
                lblHeader.Text = "Update Exam Form Master";
            }
        }
        #endregion
        protected void btnExamForm_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            try
            {
                var objExamFormProperty = new ExamFormProperty();

                objExamFormProperty.ExamId = Convert.ToInt32(ddlExamName.SelectedValue.ToString());
                objExamFormProperty.ExamFormUrl = Convert.ToString(txtExamFormUrl.Text.Trim());
                objExamFormProperty.ExamFormTitle = Convert.ToString(txtFormtitle.Text.Trim());
                objExamFormProperty.ExamFormMetaDesc = Convert.ToString(txtFormMetaDesc.Text.Trim());
                objExamFormProperty.ExamFormKeywords = Convert.ToString(txtFormMataTag.Text.Trim());
                objExamFormProperty.ExamFormSubject = Convert.ToString(txtFormSubject.Text.Trim());
                objExamFormProperty.ExamFormYear = Convert.ToString(txtFormYear.Text.Trim());
                objExamFormProperty.ExamFormWebsite = Convert.ToString(txtFormWebsite.Text.Trim());
                if (rbtFormSaleDate.SelectedValue == "1")
                {
                    objExamFormProperty.ExamFormSaleStartDate = Convert.ToString(txtSaleExectStartDate1.Text.Trim());
                }
                else
                {

                    objExamFormProperty.ExamFormSaleStartDate = Convert.ToString(txtSaleNotExectStartDate1.Text.Trim()) + "," + Convert.ToString(txtSaleNotExectEndDate1.Text.Trim());
                }
                objExamFormProperty.ExamFromSaleStartDateRemark = Convert.ToString(txtFormSaleStartDateRemark.Text.Trim());
                if (rbtFormSaleEndDate.SelectedValue == "1")
                {
                    objExamFormProperty.ExamFormSaleEndDate = Convert.ToString(txtSaleExectEndDate2.Text.Trim());
                }
                else
                {
                    objExamFormProperty.ExamFormSaleEndDate = Convert.ToString(txtSaleNotExectStartDate2.Text.Trim()) + "," + Convert.ToString(txtSaleNotExectEndDate2.Text.Trim());
                }
                objExamFormProperty.ExamFormSaleEndDateRemark = Convert.ToString(txtFormSaleEndDateRemark.Text.Trim());
                if (rbtFormSubmitDate.SelectedValue == "1")
                {
                    objExamFormProperty.ExamFormSubmitDate = Convert.ToString(txtFormSubmitExectDate1.Text.Trim());
                }
                else
                {
                    objExamFormProperty.ExamFormSubmitDate = Convert.ToString(txtFormSubmitStartDate1.Text.Trim()) + "," + Convert.ToString(txtFormSubmitEndDate1.Text.Trim());
                }
                objExamFormProperty.ExamFormSubmitDateRemark = Convert.ToString(txtFormSubmitDateRemark.Text.Trim());
                if (rbtResultDate.SelectedValue == "1")
                {
                    objExamFormProperty.ExamFormResultDate = Convert.ToString(txtResultExactDate.Text.Trim());
                }
                else
                {
                    objExamFormProperty.ExamFormResultDate = Convert.ToString(txtResultStartDate.Text.Trim()) + "," + Convert.ToString(txtResultEndtDate.Text.Trim());
                }
                objExamFormProperty.ExamFormResultDateReamrk = Convert.ToString(txtResultDateRemark.Text.Trim());
                objExamFormProperty.ExamFormResultWebsite = Convert.ToString(txtFormResultWebsite.Text.Trim());
                objExamFormProperty.ExamFormPrice = Convert.ToString(txtExamFormPrice.Text.Trim());
                objExamFormProperty.ExamFormStore = Convert.ToString(txtExamFormStore.Text.Trim());

                objExamFormProperty.ExamFormCenter = Convert.ToString(txtExamFormCenter.Text.Trim());
                objExamFormProperty.ExamFormDd = Convert.ToString(txtExamFormDD.Text.Trim());
                objExamFormProperty.ExamFormSyllabus = Convert.ToString(txtExamFormSyllabus.Text.Trim());
                objExamFormProperty.ExamFormStatus = chkFormStatus.Checked == true ? true : false;
                int insert = 0;
                if (btnExamForm.Text == "Add")
                {
                    insert = ExamProvider.Instance.InsertExamFormDetails(objExamFormProperty, LoggedInUserId, out ErrorMsg);
                    Response.Redirect("ManageExamFormMaster.aspx",true);
                }
                else
                {
                    objExamFormProperty.ExamFormId = Convert.ToInt32(Request.QueryString["ExamFormId"].ToString());
                    insert = ExamProvider.Instance.UpdateExamFormDetails(objExamFormProperty, LoggedInUserId, out ErrorMsg);
                    btnExamForm.Text = "Add";
                    Response.Redirect("ManageExamFormMaster.aspx",false);
                    lblInsertUpdate.Text = "Insert";
                    lblHeader.Text = "Add Exam Form Master";
                }
                if (insert > 0)
                {
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = ErrorMsg;
                }
                else
                {
                    lblErorrMsg.Visible = true;
                    lblErorrMsg.Text = ErrorMsg;
                }
            }
            catch (Exception ex)
            {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing InsertStateDetails in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
        }

        // Method to Bind the course
        protected void BindCourse()
        {
            var courseDetails = CourseProvider.Instance.GetAllCourseList();
            ddlCourse.DataSource = courseDetails;
            ddlCourse.DataTextField = "CourseName";
            ddlCourse.DataValueField = "CourseId";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0,"Select");
            ddlExamName.Items.Insert(0, "Select");
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourse.SelectedIndex > 0)
            {
                ddlExamName.Items.Clear();
                var delExam = ExamProvider.Instance.GetExamListByCourseId(Convert.ToInt32(ddlCourse.SelectedValue));
                ddlExamName.DataSource = delExam;
                if (delExam.Any())
                {
                    ddlExamName.DataSource = delExam;
                    ddlExamName.DataTextField = "ExamName";
                    ddlExamName.DataValueField = "ExamId";
                    ddlExamName.DataBind();
                    ddlExamName.Items.Insert(0, "Select");
                }
                else
                {
                    ddlExamName.Items.Insert(0, "Select");
                }
            }
        }
    }
}