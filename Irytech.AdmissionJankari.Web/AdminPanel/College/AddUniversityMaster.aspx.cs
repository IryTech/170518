using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class AddUniversityMaster : SecurePage 
    {
        Common _objCommon;
        UniversityProperty _objUniversityPropertyr;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogoUpload.Click += BtnUploadImageClick;
            ValidateFields();
            if (IsPostBack) return;
            GetUrlTitle();
            lblHeader.Text = "Add University Master";
            if (Request.QueryString["UniversityId"] != null)
            {
                BindUniversityDetails(Convert.ToInt16(Request.QueryString["UniversityId"]));
            }
        }
        private void GetUrlTitle()
        {
            hdnUniversityUrl.Value = Convert.ToString(ApplicationSettings.Instance.UrlLenght);
            hdnUniversityTag.Value = Convert.ToString(ApplicationSettings.Instance.MetaTagLenght);
            hdnUniversityTitle.Value = Convert.ToString(ApplicationSettings.Instance.TitleLenght);
            hdnUniversityMetaDesc.Value = Convert.ToString(ApplicationSettings.Instance.MetaKeywordLenght);
        }
        private void ValidateFields()
        {
            _objCommon=new Common();
            rfvUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvUploadExcel") ?? "N/A";
            revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
       
        }
     
      private void BindUniversityDetails(int universityId)
        {
            lblHeader.Text = "Edit University Master";
            var data = UniversityProvider.Instance.GetUniversityListById(universityId);
            if (data.Count <= 0) return;
            var query = data.Select(result => new
                                                  {
                                                      universityId = result.UniversityId,
                                                      universityName = result.UniversityName,
                                                      universityshortName = result.UniversityshortName,
                                                      universityWebsite = result.UniversityWebsite,
                                                      universityUrl = result.UniversityUrl,
                                                      universityTitle = result.UniversityTitle,
                                                      universityStateId = result.UniversityStateId,
                                                      universityPopularName = result.UniversityPopularName,
                                                      universityPhoneNo = result.UniversityPhoneNo,
                                                      universityMobile = result.UniversityMobile,
                                                      universityMetaTag = result.UniversityMetaTag,
                                                      universityMetaDesc = result.UniversityMetaDesc,
                                                      universityLogo = result.UniversityLogo,
                                                      universityFax = result.UniversityFax,
                                                      universityEst = result.UniversityEst,
                                                      universityEmailId = result.UniversityEmailId,
                                                      universityDesc = result.UniversityDesc,
                                                      universityCountryId = result.UniversityCountryId,
                                                      universityCityId = result.UniversityCityId,
                                                      universityCategoryId = result.UniversityCategoryId,
                                                      universityAddrs = result.UniversityAddrs,
                                                  }).First();
            txtUniversityName.Text =query.universityName;
            txtUniversityTitle.Text=query.universityTitle;
            txtUniversityUrl.Text =query.universityUrl;
            txtUniversityWebsite.Text =query.universityWebsite;
            txtUniversityshortName.Text =query.universityshortName;
            txtUniversityPopularName.Text =query.universityPopularName;
            txtUniversityPhoneNo.Text =query.universityPhoneNo;
            txtUniversityMobile.Text=query.universityMobile;
            txtUniversityMetaDesc.Text =query.universityMetaDesc;
            txtUniversityFax.Text =query.universityFax;
            txtUniversityEstablished.Text =query.universityEst.ToString();
            txtUniversityEmailId.Text = query.universityEmailId;
            fckUniversityDesc.FckValue =query.universityDesc;
            txtUniversityAddrs.Text =query.universityAddrs;
            txtStreamMetaTag.Text = query.universityMetaTag;
            hdnCountry.Value = query.universityCountryId.ToString();
        
          string Img = query.universityLogo != "" ? query.universityLogo : "N/A";
          var path = _objCommon.GetFilepath("UniversityImage");
          hdnFileName.Value = Img;
          hdnState.Value = query.universityStateId.ToString();
          hdnCity.Value = query.universityCityId.ToString();
          hdnUniversityCategory.Value = query.universityCategoryId.ToString();
          lblInsertUpdate.Text = "Update Records Of " + query.universityName;
          btntUniversityMaster.Text = "Update";
        }
        #region ImageUploadHandler
        private void BtnUploadImageClick(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var relativeFolder = DateTime.Now.Year.ToString() + Path.DirectorySeparatorChar + DateTime.Now.Month +
                                 Path.DirectorySeparatorChar;

            var path = _objCommon.GetFilepath("UniversityImage");
            var folder = string.Format("{0}", _objCommon.GetFilepath("UniversityImage"));
            //var fileName = this.Logoupload.FileName;
          //  hdnFileName.Value = fileName;
           // this.Upload(folder, this.Logoupload, fileName);
      imgNews.Visible = true;
            //imgNews.ImageUrl = folder + fileName;
        }
        #endregion
        #region upload method
        private void Upload(string virtualFolder, FileUpload control, string fileName)
        {
            var folder = this.Server.MapPath(virtualFolder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            control.PostedFile.SaveAs(folder + fileName);

        }
        #endregion
        protected void BtntUniversityMasterClick(object sender, EventArgs e)
        {
            switch (btntUniversityMaster.Text)
            {
                case "Save":
                    InsertUniversityDetails();
                    break;
                case "Update":
                 UpdateUniversityDetails();
                    break;
            }

        }

        private void InsertUniversityDetails()
        { 
            string errMsg;
           var fileName = this.FlUpload.UploadedImageName;
                      
            _objUniversityPropertyr = new UniversityProperty
                                          {
                                              UniversityName = txtUniversityName.Text.Trim(),
                                              UniversityTitle = txtUniversityTitle.Text.Trim(),
                                              UniversityUrl = txtUniversityUrl.Text.Trim(),
                                              UniversityWebsite = txtUniversityWebsite.Text.Trim(),
                                              UniversityshortName = txtUniversityshortName.Text.Trim(),
                                              UniversityLogo = fileName,
                                              UniversityPopularName = txtUniversityPopularName.Text.Trim(),
                                              UniversityPhoneNo = txtUniversityPhoneNo.Text.Trim(),
                                              UniversityMobile = txtUniversityMobile.Text.Trim(),
                                              UniversityMetaDesc = txtUniversityMetaDesc.Text.Trim(),
                                              UniversityFax = txtUniversityFax.Text.Trim(),
                                              UniversityEst =!string.IsNullOrEmpty(txtUniversityEstablished.Text.Trim())?Convert.ToInt32(txtUniversityEstablished.Text.Trim()):0,
                                              UniversityEmailId = txtUniversityEmailId.Text.Trim(),
                                              UniversityDesc = fckUniversityDesc.FckValue.Trim(),
                                              UniversityAddrs = txtUniversityAddrs.Text.Trim(),
                                              UniversityMetaTag = txtStreamMetaTag.Text.Trim(),
                                              UniversityCategoryId = Convert.ToInt32(hdnUniversityCategory.Value),
                                              UniversityCityId = Convert.ToInt32(hdnCity.Value),
                                              UniversityCountryId = Convert.ToInt32(hdnCountry.Value),
                                              UniversityStateId = Convert.ToInt32(hdnState.Value)
                                          };
            var result = UniversityProvider.Instance.InsertUniversityDetails(_objUniversityPropertyr, LoggedInUserId, out errMsg);
            if(result>0)
            {
                Response.Redirect("UniversityMaster.aspx");
            }
        }
        private void UpdateUniversityDetails()
        {
            string errMsg;
          var fileName = this.FlUpload.UploadedImageName;
            if(!string.IsNullOrEmpty(fileName))
            hdnFileName.Value = fileName;
            
            _objUniversityPropertyr = new UniversityProperty
                                          {
                                              UniversityId = Convert.ToInt16(Request.QueryString["UniversityId"]),
                                              UniversityName = txtUniversityName.Text.Trim(),
                                              UniversityTitle = txtUniversityTitle.Text.Trim(),
                                              UniversityUrl = txtUniversityUrl.Text.Trim(),
                                              UniversityWebsite = txtUniversityWebsite.Text.Trim(),
                                              UniversityshortName = txtUniversityshortName.Text.Trim(),
                                              UniversityLogo = hdnFileName.Value,
                                              UniversityPopularName = txtUniversityPopularName.Text.Trim(),
                                              UniversityPhoneNo = txtUniversityPhoneNo.Text.Trim(),
                                              UniversityMobile = txtUniversityMobile.Text.Trim(),
                                              UniversityMetaDesc = txtUniversityMetaDesc.Text.Trim(),
                                              UniversityFax = txtUniversityFax.Text.Trim(),
                                              UniversityEst = !string.IsNullOrEmpty(txtUniversityEstablished.Text.Trim()) ? Convert.ToInt32(txtUniversityEstablished.Text.Trim()) : 0,
                                              UniversityEmailId = txtUniversityEmailId.Text.Trim(),
                                              UniversityDesc = fckUniversityDesc.FckValue.Trim(),
                                              UniversityAddrs = txtUniversityAddrs.Text.Trim(),
                                              UniversityMetaTag = txtStreamMetaTag.Text.Trim(),
                                              UniversityCategoryId = Convert.ToInt16(hdnUniversityCategory.Value),
                                              UniversityCityId = Convert.ToInt16(hdnCity.Value),
                                              UniversityCountryId = Convert.ToInt16(hdnCountry.Value),
                                              UniversityStateId = Convert.ToInt16(hdnState.Value)
                                          };
            var result = UniversityProvider.Instance.UpdateUniversityDetails(_objUniversityPropertyr, LoggedInUserId, out errMsg);
            if (result > 0)
            {
                Response.Redirect("UniversityMaster.aspx");
            }
        }
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/UniversityMaster.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start();
        }
       

        protected void BtnUploadClick(object sender, ImageClickEventArgs e)
        {
            _objCommon = new Common();
            var objClsOledbdatalayer = new ClsOleDBDataWrapper();
            var path = MapPath(fulUploadExcel.FileName);
            fulUploadExcel.SaveAs(path);
            var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
            if (excelSheets.Length <= 0) return;
            foreach (DataSet ds in excelSheets.Select(t => objClsOledbdatalayer.getdata(path, t)).Where(ds => ds.Tables[0].Rows.Count > 0))
            {
                for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                {

                    var errMsg = "";
                    _objUniversityPropertyr = new UniversityProperty
                    {
                        UniversityName = ds.Tables[0].Rows[j]["AjUniversityName"].ToString(),
                        UniversityWebsite = ds.Tables[0].Rows[j]["AjUniversityWebsite"].ToString(),
                        UniversityshortName = ds.Tables[0].Rows[j]["AjUniversityShortName"].ToString(),
                        UniversityPopularName = ds.Tables[0].Rows[j]["AjUniversityPopularName"].ToString(),
                        UniversityPhoneNo = ds.Tables[0].Rows[j]["AJuniversityPhone"].ToString(),
                        UniversityMobile = ds.Tables[0].Rows[j]["AjUniversityMobile"].ToString(),
                        UniversityFax = ds.Tables[0].Rows[j]["AjUniversityFax"].ToString(),
                        UniversityEst = Convert.ToInt32(ds.Tables[0].Rows[j]["AjUniversityEst"].ToString()),
                        UniversityEmailId = ds.Tables[0].Rows[j]["AjUniversityEmailId"].ToString(),
                        UniversityDesc = ds.Tables[0].Rows[j]["AjUniversityDesc"].ToString(),
                        UniversityAddrs = ds.Tables[0].Rows[j]["AjUniversityAddress"].ToString(),
                        UniversityCategoryName = ds.Tables[0].Rows[j]["AjUniversityCategoryId"].ToString(),
                        UniversityCityName = ds.Tables[0].Rows[j]["AjUniversityCityId"].ToString(),
                        UniversityCountryName =ds.Tables[0].Rows[j]["AjUniversityCountryId"].ToString(),
                        UniversityStateName =ds.Tables[0].Rows[j]["AjUniversityStateId"].ToString()
                    };
                    var result = UniversityProvider.Instance.InsertUniversityDetailsUpload(_objUniversityPropertyr, LoggedInUserId, out errMsg);

                }
                Response.Redirect("UniversityMaster.aspx");
            }
        }
    }
}