using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.PresidentSpeech
{
    public partial class PresidentSpeechDetails : SecurePage
    {
        Common _objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack)return;
            BindCourseList();
            BindState(0);

            BindCity(0);
            hdncourseid.Value = "";
            hdncollegename.Value = "";
            BindSponserCollegeDetails();
            lblCollegePlacement.Text = "Add College Speeks";
           
          
        }

   

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            BindSponserCollegeDetails(false);

        }

        //Bind all SponserCourseList
        protected void BindSponserCollegeDetails(bool parameter=true)
        {
          
            lblErrorMessage.Visible = false;
            var objCommon = new Common();
            try
            {
                
                var sponserCollegeList = (string.IsNullOrEmpty(hdncourseid.Value.Trim().ToString()) || hdncourseid.Value == "0")? CollegeSpeechProvider.Instance.GetAllCollegeSpeechList() : CollegeSpeechProvider.Instance.GetCollegeSpeechByCourseId(Convert.ToInt32(hdncourseid.Value));
                if (!string.IsNullOrEmpty(hdncollegename.Value.Trim().ToString()))
                {
                    sponserCollegeList = sponserCollegeList.Where(result => result.CollegeName == hdncollegename.Value.Trim()).ToList();
                }
                ucCustomPaging.BindDataWithPaging(rptPresidentDetails, Common.ConvertToDataTable(sponserCollegeList),parameter);
                if(sponserCollegeList.Count > 0)
                {

                    rptPresidentDetails.Visible = true;
                
                }
                 else
               {
                  
                   lblErrorMessage.Visible = true;
                   lblErrorMessage.CssClass = "error";
                   lblErrorMessage.Text = objCommon.GetErrorMessage("noRecords");
                   rptPresidentDetails.Visible = false;
              }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindSponserCollegeDetails in PresidentSpeechDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        //Bind all CourseList
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
                    ddlCourseList.DataSource = courseList;
                    ddlCourseList.DataTextField = "CourseName";
                    ddlCourseList.DataValueField = "CourseId";
                    ddlCourseList.DataBind();
                    ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else { ddlCourse.Items.Insert(0, new ListItem("--Select--", "0")); ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0")); }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCourseList in PresidentSpeechDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            var objCommon = new Common();
            
            hdncollegename.Value = txtCollege.Text.Trim().ToString();
            hdncourseid.Value = ddlCourse.SelectedValue;
           
                BindSponserCollegeDetails();
           
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            ddlCourse.ClearSelection();
            txtCollege.Text = "";
            hdncollegename.Value = "";
            hdncourseid.Value = "";
            BindSponserCollegeDetails();
        }

        private void FillEditCollegeSpeechDetails(int collegeSpeechId)
        {
           
                try
                {
                 
                    var collegeSpeechList = CollegeSpeechProvider.Instance.GetCollegeSpeechById(collegeSpeechId);
                    if (collegeSpeechList.Count > 0)
                    {

                        var query = from result in collegeSpeechList
                                    select new
                                    {

                                        CollegeSpeechId = result.CollegeSpeechId,
                                        CollegeName = result.CollegeName,
                                        CollegeSpeechStatus = result.SpeechStatus,
                                        CollegeSpeechPersonName = result.CollegeSpeechPersonName,
                                        CollegeAboutKeyPerson = result.AboutKeyPerson,

                                        CollegeSpeechPersonDesgination = result.CollegeSpeechPersonDesignation,
                                        CollegeSpeechDescription = result.CollegeSpeechDesc,
                                        CollegeSpeechPersonImage = result.CollegeSpeechPersonImage,

                                    };
                        var sp = query.First();
                        lblCollegePlacement.Text = "Edit College Speek of the " + sp.CollegeName.ToString();
                        txtSelectCollegeFiltered.Text = sp.CollegeName.ToString();
                        txtSpeechPersonName.Text = sp.CollegeSpeechPersonName;
                        txtPersonDesignation.Text = sp.CollegeSpeechPersonDesgination;
                        txtAbouKeyPerson.Text = sp.CollegeAboutKeyPerson;
                        txtDescription.FckValue = sp.CollegeSpeechDescription;
                        string Img = sp.CollegeSpeechPersonImage != "" ? sp.CollegeSpeechPersonImage : "N/A";
                        var path = _objCommon.GetFilepath("UniversityImage");
                        hdnFileName.Value = Img;
                        personImagetag.Visible = true;
                        personImagetag.Src = path + Img;

                        chkStatus.Checked = sp.CollegeSpeechStatus;

                        btnSubmit.Text = "Update";
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing FillEditCollegeSpeechDetails in PresidentSpeech.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }

           
        }

        protected void rptPresidentDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ViewState["CollegePresedentId"]=e.CommandArgument;
                FillEditCollegeSpeechDetails(Convert.ToInt32(ViewState["CollegePresedentId"]));
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdwee", "OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePresedentSpeech');", true);
            }

                                  
        }
        private void BindState(int countryId)
        {
            try
            {
                List<StateProperty> data;
                data = countryId == 0
                           ? StateProvider.Instance.GetAllState()
                           : StateProvider.Instance.GetStateByCountry(countryId);
                if (data.Count > 0)
                {
                    ddlState.DataSource = data;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindState in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindCity(int stateId)
        {
            try
            {
                List<CityProperty> data;
                data = stateId == 0 ? CityProvider.Instacnce.GetAllCityList() : CityProvider.Instacnce.GetCityListByState(stateId);
                if (data.Count > 0)
                {
                    ddlSelectCity.Items.Clear();
                    ddlSelectCity.DataSource = data;
                    ddlSelectCity.DataTextField = "CityName";
                    ddlSelectCity.DataValueField = "CityId";
                    ddlSelectCity.DataBind();
                    ddlSelectCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSelectCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCity in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlState.SelectedIndex > 0)
            {
                BindCity(Convert.ToInt16(ddlState.SelectedValue));
            }
            else { BindCity(0); }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweewqe", "OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePresedentSpeech');", true);

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (uploadFile.HasFile)
            {
                var fileName = Server.MapPath(new Common().GetFilepath("UniversityImage") + uploadFile.FileName);
                var fileextension = System.IO.Path.GetExtension(fileName);
                if (fileextension != null)
                {
                    var index = fileName.LastIndexOf(fileextension, System.StringComparison.Ordinal);
                    fileName = fileName.Substring(0, index);
                    var newfname = fileName;

                    index = 0;
                    while (System.IO.File.Exists(newfname + fileextension))
                        newfname = fileName + index++;
                    uploadFile.SaveAs(newfname + fileextension);
                    hdnFileName.Value = System.IO.Path.GetFileName(newfname + fileextension);
                }
            }

            try
            {
                var objCollegeSpeech = new CollegeSpeechProperty
                {
                    CollegeName =
                        Convert.ToString(txtSelectCollegeFiltered.Text.Trim()),

                    CollegeSpeechPersonDesignation =
                        Convert.ToString(
                            txtPersonDesignation.Text.Trim()),
                    CollegeSpeechPersonName =
                        Convert.ToString(
                            txtSpeechPersonName.Text.Trim()),
                    AboutKeyPerson =
                        Convert.ToString(txtAbouKeyPerson.Text.Trim()),
                    CollegeSpeechDesc =
                        Convert.ToString(txtDescription.FckValue.Trim()),
                    CollegeSpeechPersonImage = hdnFileName.Value,
                    SpeechStatus = chkStatus.Checked
                };


                var errorMsg = "";
                var insert = 0;
                if (btnSubmit.Text == "Insert")
                {
                    insert = CollegeSpeechProvider.Instance.InsertCollegeSpeechDetails(objCollegeSpeech, LoggedInUserId, out errorMsg);


                    

                }
                else
                {
                    objCollegeSpeech.CollegeSpeechId = Convert.ToInt32(ViewState["CollegePresedentId"]);

                    insert = CollegeSpeechProvider.Instance.UpdateCollegeSpeechDetails(objCollegeSpeech, LoggedInUserId, out errorMsg);
                   
                    btnSubmit.Text = "Insert";
                 

                }
                ClearFields();
                lblSeccessMsg.Visible = true;

                lblSeccessMsg.Text = errorMsg;
                lblSeccessMsg.CssClass = insert > 0 ? "success" : "info";
                    
                BindSponserCollegeDetails();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  btnSubmit_Click in PresidentSpeech.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
                 ClearFields();
        }
        private void ClearFields()
        {
            lblSeccessMsg.Visible = false;
            lblErorrMsg.Visible = false;
            rbtnFilterCollege.ClearSelection();
            rbtnFilterCollege.Items[0].Selected = true;
            ddlCourseList.ClearSelection();
            ddlSelectCity.ClearSelection();
            ddlState.ClearSelection();
            txtSelectCollegeFiltered.Text = "";

            txtPersonDesignation.Text = string.Empty;
            txtDescription.FckValue = "<p></p>";
            txtSpeechPersonName.Text = string.Empty;
            txtAbouKeyPerson.Text = string.Empty;
            lblCollegePlacement.Text = "Add College Speeks";
            //imgUploader.clearControl();

        }
    }
}