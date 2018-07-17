using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{
    public partial class AddCourse : SecurePage
    {
        private Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindCourseCategory();
            BindCourseEligibilty();
            ValidationErrorMessages();
            GetUrlTitle();
            if (Request.QueryString["courseId"] != null)
            {
                BindCourseDetails(Convert.ToInt32(Request.QueryString["courseId"]));
            }
        }

        //method to get length   for course url,tilte ,metatag and desc field..By Indu Kumar Pandey 
        private void GetUrlTitle()
        {
            hdnCourseUrl.Value = Convert.ToString(ApplicationSettings.Instance.UrlLenght);
            hdnCourseMetaTag.Value = Convert.ToString(ApplicationSettings.Instance.MetaTagLenght);
            hdnCourseTitle.Value = Convert.ToString(ApplicationSettings.Instance.TitleLenght);
            hdnCourseMetaDesc.Value = Convert.ToString(ApplicationSettings.Instance.MetaKeywordLenght);
        }
        //method to Bind Course Category..By Indu Kumar Pandey 
        private void BindCourseCategory()
        {

            var courseCategory = CourseProvider.Instance.GetAllCourseCategoryList();
            if (courseCategory.Count > 0)
            {
                ddlCourseCategory.DataSource = courseCategory;
                ddlCourseCategory.DataTextField = "CourseCategoryName";
                ddlCourseCategory.DataValueField = "CourseCategoryId";
                ddlCourseCategory.DataBind();
                ddlCourseCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlCourseCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        //method to Bind Course Eligibilty..By Indu Kumar Pandey 
        private void BindCourseEligibilty()
        {
            var courseEligibilty = CourseProvider.Instance.GetAllCourseEligibiltyList();
            if (courseEligibilty.Count > 0)
            {
                ddlCourseEligibility.DataSource = courseEligibilty;
                ddlCourseEligibility.DataTextField = "CourseEligibiltyName";
                ddlCourseEligibility.DataValueField = "CourseEligibilityId";
                ddlCourseEligibility.DataBind();
                ddlCourseEligibility.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlCourseEligibility.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        //method to Insert Update Course Details..By Indu Kumar Pandey 
        private void InsertUpdateCourseMaster()
        {
            try
            {
                var errMsg = "";
                int result;
                var imageName = Common.NoImageSubstitute;
                var fileName = this.impUploader.UploadedImageName;
                if (fileName != null)
                {
                    hdnFileName.Value = fileName;
                }

                if (ckbIsBookSeatVisible.Checked == false)
                {
                    ckbIsBookSeatVisible.Checked = true;
                }
                if (btntCourse.Text == "Save")
                {
                    result = CourseProvider.Instance.InsertCourseMasterDetails(txtCourseName.Text.Trim(),

                                                                               txtCourseUrl.Text.Trim(),
                                                                               txtCourseTitle.Text.Trim(),
                                                                               txtCourseMetaTag.Text.Trim(),
                                                                               txtCourseMetaDesc.Text.Trim(),
                                                                               fckCourseDecsription.FckValue.Trim(),
                                                                               txtShortName.Text.Trim(),
                                                                               txtPopularName.Text.Trim(),
                                                                               Convert.ToInt16(
                                                                                   ddlCourseCategory.SelectedValue),
                                                                               Convert.ToInt16(
                                                                                   ddlCourseEligibility.SelectedValue),
                                                                               LoggedInUserId,
                                                                               out errMsg, fileName,
                                                                               txtHelplineNo.Text.Trim(),
                                                                               chkCourseStatus.Checked,
                                                                               ckbIsBookSeatVisible.Checked
                                                                              );
                    if (result > 0)
                    {
                        lblSuccess.Visible = true;
                        lblSuccess.Text = errMsg;
                       
                        ClearFileds();
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = errMsg;
                    }
                }
                else
                {
                    result = CourseProvider.Instance.UpdateCourseMasterDetails(Convert.ToInt32(Request.QueryString["courseId"]),
                                                                               txtCourseName.Text.Trim(),
                                                                               txtCourseUrl.Text.Trim(),
                                                                               txtCourseTitle.Text.Trim(),
                                                                               txtCourseMetaTag.Text.Trim(),
                                                                               txtCourseMetaDesc.Text.Trim(),
                                                                               fckCourseDecsription.FckValue.Trim(),
                                                                               txtShortName.Text.Trim(),
                                                                               txtPopularName.Text.Trim(),
                                                                               Convert.ToInt16(
                                                                                   ddlCourseCategory.SelectedValue),
                                                                               Convert.ToInt16(
                                                                                   ddlCourseEligibility.SelectedValue),
                                                                               LoggedInUserId,
                                                                               out errMsg, fileName,
                                                                               txtHelplineNo.Text.Trim(),
                                                                               chkCourseStatus.Checked, ckbIsBookSeatVisible.Checked);
                    if (result > 0)
                    {
                        btntCourse.Text = "Save";
                        lblInsertUpdate.Text = "Insert";

                        lblSuccess.Visible = true;
                        lblSuccess.Text = errMsg;
                     
                        ClearFileds();
                        Response.Redirect("CourseMaster.aspx",false);
                       
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = errMsg;
                    }
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateCourseMaster in CourseMatser.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        //method to Clear fields.By Indu Kumar Pandey 
        private void ClearFileds()
        {
            txtCourseName.Text = string.Empty;
            fckCourseDecsription.FckValue = string.Empty;
            txtPopularName.Text = string.Empty;
            txtHelplineNo.Text = string.Empty;
            txtShortName.Text = string.Empty;
            chkCourseStatus.Checked = false;
            ckbIsBookSeatVisible.Checked = false;
            ddlCourseCategory.ClearSelection();
            ddlCourseEligibility.ClearSelection();
            txtCourseUrl.Text = string.Empty;
            txtCourseTitle.Text = string.Empty;
            txtCourseMetaTag.Text = string.Empty;
            txtCourseMetaDesc.Text = string.Empty;
            fckCourseDecsription.FckValue = "";
        }
        //BtnUploadClick event  to see excel format .. by Indu Kumar Pandey 
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/CourseMaster.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start();
        }

        //BtnUploadClick1 event  to upload course data from excel  .. by Indu Kumar Pandey 
        protected void BtnUploadClick1(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var path = MapPath(fulUploadExcel.FileName);
                fulUploadExcel.SaveAs(path);
                var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (string t in excelSheets)
                    {
                        DataSet ds = objClsOledbdatalayer.getdata(path, t);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                            {

                                var errMsg = "";
                                var result =
                                    CourseProvider.Instance.InsertCourseMasterDetails(ds.Tables[0].Rows[j]["CourseName"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseUrl"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseTitle"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseMetaTag"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseMetaDesc"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseDesc"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseShortName"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CoursePopularName"].ToString(),
                                                                                      Convert.ToInt16(
                                                                                          ds.Tables[0].Rows[j]["CourseCategoryId"].ToString
                                                                                              ()),
                                                                                      Convert.ToInt16(
                                                                                          ds.Tables[0].Rows[j]["CourseEligibiltyId"].ToString
                                                                                              ()), LoggedInUserId, out errMsg,
                                                                                              ds.Tables[0].Rows[j]["AjCourseImage"].ToString(),
                                                                                          ds.Tables[0].Rows[j]["HelpLineNo"].ToString(),
                                                                                      ds.Tables[0].Rows[j]["CourseStatus"].ToString() ==
                                                                                      "True" ? true
                                                                                          : false);
                            }
                        }
                    }
                    lblExcelSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                    lblExcelSuccess.Visible = true;
                  
                }
                else
                {
                    lblError.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            { }
        }
        //method to get error message for course field..By Indu Kumar Pandey 
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvCourseName.ErrorMessage = _objCommon.GetValidationMessage("rfvCourse") ?? "N/A";
            rfvHelplineNo.ErrorMessage = _objCommon.GetValidationMessage("rfvHelplineNo") ?? "N/A";
            rfvCourseCategory.ErrorMessage = _objCommon.GetValidationMessage("rfvCourseCategory") ?? "N/A";
            rfvCourseEligibilty.ErrorMessage = _objCommon.GetValidationMessage("rfvCourseEligibilty") ?? "N/A";
            revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");

        }
        protected void BtnCourseClick(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            InsertUpdateCourseMaster();
        }

        // Method to Get The Course Details for Update
        private void BindCourseDetails(int courseId)
        {
         
            var data = CourseProvider.Instance.GetCourseById(courseId);
            if (data.Count < 0) return;
            {
                var records = data.Select(result => new
                {
                    courseName = result.CourseName,
                    courseUrl = result.CourseUrl,
                    courseTitle = result.CourseTitle,
                    courseMetaTag = result.CourseMetaTag,
                    courseMetaDesc = result.CourseMetaDesc,
                    courseCategory = result.CourseCategoryId,
                    courseEligibilty = result.CourseEligibiltyId,
                    courseDesc = result.CourseDesc,
                    courseShortName = result.CourseShortName,
                    coursePopularNAme = result.CoursePopularName,
                    courseImage = result.CourseImage,
                    courseStatus = result.CourseStatus,
                    result.HelpLineNo,
                    result.IsBookSeatVisible
                }).First();

                if (records == null) throw new ArgumentNullException("No Records Found");
                txtCourseName.Text = !string.IsNullOrEmpty(Convert.ToString(records.courseName)) ? records.courseName : "N/A";
                txtHelplineNo.Text = !string.IsNullOrEmpty(Convert.ToString(records.HelpLineNo)) ? records.HelpLineNo : "N/A";
                fckCourseDecsription.FckValue = !string.IsNullOrEmpty(Convert.ToString(records.courseDesc)) ? records.courseDesc : "N/A";
                txtPopularName.Text = !string.IsNullOrEmpty(Convert.ToString(records.coursePopularNAme)) ? records.coursePopularNAme : "N/A";
                txtShortName.Text = !string.IsNullOrEmpty(Convert.ToString(records.courseShortName)) ? records.courseShortName : "N/A";
                var img = !string.IsNullOrEmpty(Convert.ToString(records.courseImage)) ? records.courseImage : "N/A";
                hdnFileName.Value = img;
                chkCourseStatus.Checked = records.courseStatus;
                ckbIsBookSeatVisible.Checked = records.IsBookSeatVisible;
                txtCourseUrl.Text = !string.IsNullOrEmpty(Convert.ToString(records.courseUrl)) ? records.courseUrl : records.courseName;
                txtCourseTitle.Text = !string.IsNullOrEmpty(Convert.ToString(records.courseTitle)) ? records.courseTitle : records.courseName;
                txtCourseMetaTag.Text = !string.IsNullOrEmpty(Convert.ToString(records.courseMetaTag)) ? records.courseMetaTag : records.courseName;
                txtCourseMetaDesc.Text = !string.IsNullOrEmpty(Convert.ToString(records.courseMetaDesc)) ? records.courseMetaDesc : records.courseName;
                ddlCourseCategory.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(records.courseCategory)) ? Convert.ToString(records.courseCategory) : "0";
                ddlCourseEligibility.SelectedValue =
                    !string.IsNullOrEmpty(Convert.ToString(records.courseEligibilty))
                        ? Convert.ToString(records.courseEligibilty)
                        : "0";

            }
            btntCourse.Text = "Update";
        }

    }
}