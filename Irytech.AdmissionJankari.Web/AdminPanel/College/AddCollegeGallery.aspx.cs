using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class AddCollegeGallery : System.Web.UI.Page
    {
        Common objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindCourseList();
                if (Request.QueryString["collegeGalleryId"] != null)
                {
                    BindCollegeImageGallery(Convert.ToInt32(Request.QueryString["collegeGalleryId"]));
                }
               
            }
            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                "GetParticipatedCollege("+ddlCourseList.SelectedValue+","+rbtSponser.SelectedValue+");", true);
    
        }
  
        private void BindCourseList()
        {
            var courseData = CourseProvider.Instance.GetAllCourseList();
            if (courseData.Count > 0)
            {
                ddlCourseList.DataSource = courseData;
                ddlCourseList.DataTextField = "CourseName";
                ddlCourseList.DataValueField = "CourseId";
                ddlCourseList.DataBind();
                ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));
             
            }

            else
            {
               ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));

            }

        }

   
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            var fileCollection = Request.Files;
            if (btnUpload.Text == "Insert")
            {
                
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    var uploadfile = fileCollection[i];
                    var fileName = Path.GetFileName(uploadfile.FileName);
                    if (uploadfile.ContentLength > 0)
                    {
                        var uploadToDirectory = new Common().GetFilepath("CollegeGallery");
                        var directory = Server.MapPath(uploadToDirectory + fileName); hdnImage.Value = fileName;
                        CollegeGallery();
                       
                        uploadfile.SaveAs(directory);
                    }
                }
            }
            else
            {
                if (fileCollection[0].ContentLength > 0)
                {
                    var uploadfile = fileCollection[0];
                    var fileName = Path.GetFileName(uploadfile.FileName);
                    if (uploadfile.ContentLength > 0)
                    {
                        var uploadToDirectory = new Common().GetFilepath("CollegeGallery");
                        var directory = Server.MapPath(uploadToDirectory + fileName); hdnImage.Value = fileName;
                        CollegeGallery();
                       
                        uploadfile.SaveAs(directory);
                    }
                }
                else
                {
                    CollegeGallery();
                }
            }
            ClearControls();
        }

        private void CollegeGallery()
        {
            try
            {
                var objCollegeGalleryProperty = new CollegeBranchGallery
                                                    {
                                                        CollegeBranchName=txtCollegeSearch.Text.Trim(),
                                                        CollegeBranchGalleryImageTitle =
                                                            Convert.ToString(
                                                                txtImageTitle.Text.Trim()),
                                                        CollegeBranchGalleryImageName =hdnImage.Value,
                                                        CollegeBranchGalleryImageStatus =
                                                            chkStatus.Checked
                                                    };

                var errorMsg = "";
                var insert = 0;
                if (btnUpload.Text == "Insert")
                {
                    insert = CollegeProvider.Instance.InsertCollegeGallery(objCollegeGalleryProperty,
                                               new SecurePage().LoggedInUserId, out errorMsg);
                   
                }
                else
                {
                    objCollegeGalleryProperty.CollegeBranchGalleryId = Convert.ToInt32(Convert.ToInt32(Request.QueryString["collegeGalleryId"]));
                    insert = CollegeProvider.Instance.UpdateCollegeGallery(objCollegeGalleryProperty, new SecurePage().LoggedInUserId, out errorMsg);
                   
                    ClearControls();
                    txtCollegeSearch.Enabled = true; rbtSponser.Enabled = true; ddlCourseList.Enabled = true;
                    btnUpload.Text = "Insert";
                    Response.Redirect("CollegeGallery.aspx");
             
                }
                if (insert > 0)
                {
                    lblSeccessMsg.CssClass = "success";
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = errorMsg;
                }
                else
                {
                    lblSeccessMsg.CssClass = "error";
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = errorMsg;
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
        private void ClearControls()
        {

            txtImageTitle.Text = string.Empty;
            hdnImage.Value = "";
            imgGallery.ImageUrl = "";
            imgGallery.AlternateText = "";
            txtCollegeSearch.Text = String.Empty;
            
        }

        

       // Method to Bind The Image Control

        protected void BindCollegeImageGallery(int imageGalleryId)
        {
               
                    var data = CollegeProvider.Instance.GetCollegeGalleryById(imageGalleryId);

                    if (data.Count > 0)
                    {

                        var query = from result in data
                                    select new
                                               {
                                                   result.CollegeBranchName,
                                                   result.CollegeBranchId,
                                                   result.CollegeBranchGalleryImageTitle,
                                                   result.CollegeBranchGalleryImageName,
                                                   result.CollegeBranchGalleryImageStatus,
                                               };

                        txtCollegeSearch.Text = query.FirstOrDefault().CollegeBranchName;
                        txtCollegeSearch.Enabled = false;
                        rbtSponser.Enabled = false; ddlCourseList.Enabled = false;
                        txtImageTitle.Text = query.First().CollegeBranchGalleryImageTitle;
                        string Img = query.First().CollegeBranchGalleryImageName != ""
                                         ? query.First().CollegeBranchGalleryImageName
                                         : "N/A";
                        var path = objCommon.GetFilepath("CollegeGallery");
                        imgGallery.Visible = true;
                        imgGallery.ImageUrl = path + Img;
                        imgGallery.AlternateText = query.First().CollegeBranchGalleryImageTitle;
                        hdnImage.Value = Img;
                        chkStatus.Checked = query.First().CollegeBranchGalleryImageStatus;
                        btnUpload.Text = "Update";
                    }
        }


        protected void BtnResetClck(object sender, EventArgs e) 
        {
            txtCollegeSearch.Text = string.Empty;
            txtImageTitle.Text = "";
            imgGallery.Visible = false; txtCollegeSearch.Enabled = true;
        }

       
    }
}