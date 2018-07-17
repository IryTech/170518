using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{
    public partial class AddCourseStreamMaster : SecurePage 
    {
      private  Common _objCommon;
      private CourseStreamProperty _objCourseStreamProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblHeader.Text = "Add  Course Stream Details";
            GetUrlTitle();
            ValidateFields();
            BindCourseList();
            if (Request.QueryString["StreamId"] != null)
            {
                BindStreamDetailsById(Convert.ToInt16(Request.QueryString["StreamId"]));
            }
        }
        private void GetUrlTitle()
        {
            hdnCourseStreamUrl.Value = Convert.ToString(ApplicationSettings.Instance.UrlLenght);
            hdnCourseStreamMetaTag.Value = Convert.ToString(ApplicationSettings.Instance.MetaTagLenght);
            hdnCourseStreamTitle.Value = Convert.ToString(ApplicationSettings.Instance.TitleLenght);
            hdnCourseStreamMetaDesc.Value = Convert.ToString(ApplicationSettings.Instance.MetaKeywordLenght);
        }
        //Method for validate RequiredFieldValidater
        private void ValidateFields()
        {
            _objCommon = new Common();
          
            rfvCourseId.ErrorMessage = _objCommon.GetValidationMessage("rfvCourseId");
           
            rfvCourseStreamName.ErrorMessage = _objCommon.GetValidationMessage("rfvCourseStreamName");
          
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }

        //Method to bind stream details by indu kumar pandey...
        protected void BindStreamDetailsById(int streamId)
        {
            var data = StreamProvider.Instance.GetStreamListById(streamId);
            //query for Retrive Stream Id 
            var query = data.Select(result => new
                                                  {
                                                      result.CourseId,
                                                      result.CourseStreamName,
                                                      result.CourseStreamUrl, 
                                                      result.CourseStreamTitle, 
                                                      result.CourseStreamMetaTag,
                                                      CourseStreamMetaDesc = result.CourseStreamDesc, 
                                                      result.CourseStreamDesc,
                                                      result.CourseStreamHistory, result.CourseSteamFuture,
                                                      result.CourseStreamRelatedIndustry, 
                                                      result.CourseStreamCoreCompanies, result.CourseStreamStatus
                                                  }).First();
            txtCourseStreamName.Text = query.CourseStreamName;
            ddlCourseId.SelectedValue = query.CourseId.ToString(CultureInfo.InvariantCulture);
            txtCourseStreamUrl.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamUrl))?query.CourseStreamUrl:"N/A";
            txtCourseStreamTitle.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamTitle))?query.CourseStreamTitle:"N/A";
            txtStreamMetaTag.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamMetaTag))?query.CourseStreamMetaTag:"N/A";
            txtStreamMetaDesc.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamMetaDesc))?query.CourseStreamMetaDesc:"N/A";
            fckStreamDesc.FckValue = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamDesc))?query.CourseStreamDesc:"N/A";
           txtCourseStreamHistory.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamHistory))?query.CourseStreamHistory:"N/A";
            txtCourseStreamFuture.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseSteamFuture))?query.CourseSteamFuture:"N/A";
            txtCourseStreamRelatedIndustry.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamRelatedIndustry))?query.CourseStreamRelatedIndustry:"N/A";
            txtCourseStreamCoreCompanies.Text = !string.IsNullOrEmpty(Convert.ToString(query.CourseStreamCoreCompanies))?query.CourseStreamCoreCompanies:"N/A";
            chkbCourseStreamStatus.Checked = query.CourseStreamStatus;
            lblInsertUpdate.Text = "Update Records Of " + query.CourseStreamName;
            btntCourseCategory.Text = "Update";
            lblHeader.Text = "Edit Course Stream Master";
        }

        //Method to see excel fromat fro stream by indu kumar pandey...
        protected void BtnSeeExcelFormatClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/StreamMaster.xlsx");
            var objDocProcess = new System.Diagnostics.Process
                                    {
                                        EnableRaisingEvents = false,
                                        StartInfo = {FileName = @path}
                                    };
            objDocProcess.Start();
        }

        //Method to bind course dropdown  by indu kumar pandey...
        protected void BindCourseList()
        {
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
            {
                ddlCourseId.DataSource = data;
                ddlCourseId.DataTextField = "CourseName";
                ddlCourseId.DataValueField = "CourseId";
                ddlCourseId.DataBind();
                ddlCourseId.Items.Insert(0, new ListItem("--Select--","0"));
            }
            else
            {
                ddlCourseId.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        //Method to InsertStreamMaster  by indu kumar pandey...
        protected void InsertStreamMaster()
        {
            try
            {
                string errMsg;
                _objCourseStreamProperty = new CourseStreamProperty
                                               {
                                                   CourseStreamName = txtCourseStreamName.Text.Trim(),
                                                   CourseId = Convert.ToInt16(ddlCourseId.Text.Trim()),
                                                   CourseStreamUrl = txtCourseStreamUrl.Text.Trim(),
                                                   CourseStreamTitle = txtCourseStreamTitle.Text.Trim(),
                                                   CourseStreamMetaTag = txtStreamMetaTag.Text.Trim(),
                                                   CourseStreamMetaDesc = txtStreamMetaDesc.Text.Trim(),
                                                   CourseStreamDesc = fckStreamDesc.FckValue.Trim(),
                                                   CourseStreamHistory = txtCourseStreamHistory.Text.Trim(),
                                                   CourseSteamFuture = txtCourseStreamFuture.Text.Trim(),
                                                   CourseStreamCoreCompanies = txtCourseStreamCoreCompanies.Text.Trim(),
                                                   CourseStreamRelatedIndustry =
                                                       txtCourseStreamRelatedIndustry.Text.Trim(),
                                                   CourseStreamStatus =
                                                       Convert.ToBoolean(chkbCourseStreamStatus.Checked)
                                               };
                var result = StreamProvider.Instance.InsertCourseStreamDetails(_objCourseStreamProperty, LoggedInUserId,
                                                                               out errMsg);
                if (result > 0)
                {
                    btntCourseCategory.Text = "Save";
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errMsg;
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing InsertStreamMaster in AddCourseStreamMaster.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        //Method to UpdateStreamMaster  by indu kumar pandey...
        protected void UpdateStreamMaster()
        {
            try
            {
                var errMsg = "";
                _objCourseStreamProperty = new CourseStreamProperty
                                               {
                                                   StreamId = Convert.ToInt16(Request.QueryString["StreamId"]),
                                                   CourseStreamName = txtCourseStreamName.Text.Trim(),
                                                   CourseId = Convert.ToInt16(ddlCourseId.Text.Trim()),
                                                   CourseStreamUrl = txtCourseStreamUrl.Text.Trim(),
                                                   CourseStreamTitle = txtCourseStreamTitle.Text.Trim(),
                                                   CourseStreamMetaTag = txtStreamMetaTag.Text.Trim(),
                                                   CourseStreamMetaDesc = txtStreamMetaDesc.Text.Trim(),
                                                   CourseStreamDesc = fckStreamDesc.FckValue.Trim(),
                                                   CourseStreamHistory = txtCourseStreamHistory.Text.Trim(),
                                                   CourseSteamFuture = txtCourseStreamFuture.Text.Trim(),
                                                   CourseStreamCoreCompanies = txtCourseStreamCoreCompanies.Text.Trim(),
                                                   CourseStreamRelatedIndustry =
                                                       txtCourseStreamRelatedIndustry.Text.Trim(),
                                                   CourseStreamStatus =
                                                       Convert.ToBoolean(chkbCourseStreamStatus.Checked)
                                               };
                int result = StreamProvider.Instance.UpdateCourseStreamDetails(_objCourseStreamProperty, LoggedInUserId,
                                                                               out errMsg);
                if (result > 0)
                {
                    btntCourseCategory.Text = "Update";
                    lblHeader.Text = "Add Course Stream Master";
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errMsg;
                    lblSuccess.Text = "";
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing UpdateStreamMaster in AddCourseStreamMaster.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        //event to BtntCourseCategoryClick  by indu kumar pandey...
        protected void BtntCourseCategoryClick(object sender, EventArgs e)
        {
            switch (btntCourseCategory.Text)
            {
                case "Save":
                    InsertStreamMaster();
                    Response.Redirect("CourseStreamMaster.aspx");
                    break;
                case "Update":
                    UpdateStreamMaster();
                    Response.Redirect("CourseStreamMaster.aspx");
                    break;
            }
        }

        //event  BtnUploadClick to upload stream detials  by indu kumar pandey...
        protected void BtnUploadClick(object sender, EventArgs e)
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
                            for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                            {

                                var errMsg = "";
                                _objCourseStreamProperty = new CourseStreamProperty
                                {
                                    CourseStreamName = Convert.ToString(ds.Tables[0].Rows[j]["AjCourseStreamName"]),
                                    CourseId = Convert.ToInt16(ds.Tables[0].Rows[j]["CourseId"]),
                                    CourseStreamHistory = Convert.ToString( ds.Tables[0].Rows[j]["AjCourseStreamHistory"]),
                                    CourseSteamFuture = Convert.ToString(ds.Tables[0].Rows[j]["AjCourseStreamFuture"]),
                                    CourseStreamRelatedIndustry =Convert.ToString(ds.Tables[0].Rows[j]["AjCourseStreamRelatedIndustry"]),
                                    CourseStreamStatus = ds.Tables[0].Rows[j]["AjCourseStreamStatus"].ToString() == "True" ? true : false,
                                    CourseStreamDesc = Convert.ToString(ds.Tables[0].Rows[j]["AjCourseStreamDesc"]),
                                    CourseStreamCoreCompanies = Convert.ToString(ds.Tables[0].Rows[j]["AjCourseStreamCoreCompanies"])
                                  
                                };
                                var result = StreamProvider.Instance.InsertCourseStreamDetails(_objCourseStreamProperty, LoggedInUserId, out errMsg);

                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");

                            Response.Redirect("CourseStreamMaster.aspx");
                        }
                    }

                }
                else
                {
                    lblError.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSeeExcelFormat_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/StreamMaster.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start(); 

        }
    }
}