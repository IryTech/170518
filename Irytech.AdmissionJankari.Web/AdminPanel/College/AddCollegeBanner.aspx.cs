using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class AddCollegeBanner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FileUpload1.uploadToDirectory = new Common().GetFilepath("BannerImage");
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCourse();
            BindBanner();
            GetBannerAdsList();
        }
        private void BindBannerDetails(DataSet data)
        {
       
            if (data.Tables.Count > 0)
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    rptCollegeList.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptCollegeList, data.Tables[0]);
                }
                else
                {
                    ucCustomPaging.Visible = false;
                    rptCollegeList.Visible = false;
                    lblResult.Visible = true;
                    lblResult.CssClass = "info";
                    lblResult.Text = "No Data Found";
                }
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptCollegeList.Visible = false;
                lblResult.Visible = true;
                lblResult.CssClass = "info";
                lblResult.Text = "No Data Found";
            }
        }
        private void BindCourse()
        {
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
            {
                ddlCourse.DataSource = data;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseID";
                ddlCourse.DataBind();
                ddlCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                ddlCourseList.DataSource = data;
                ddlCourseList.DataTextField = "CourseName";
                ddlCourseList.DataValueField = "CourseID";
                ddlCourseList.DataBind();
                ddlCourseList.Items.Insert(0, new ListItem("Select Course", "0"));

            }
            else
            {
                ddlCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                ddlCourseList.Items.Insert(0, new ListItem("Select Course", "0"));
            }

        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourseList.SelectedIndex > 0)
            {
                DataSet data = new Common().GetBannerById(courseId: Convert.ToInt16(ddlCourseList.SelectedValue));
                BindBannerDetails(data);
            }
            else
            {
                if (ddlAdsType.SelectedIndex > 0)
                {
                    DataSet data = new Common().GetBannerById(collegeName: txtCollegeSearch.Text);
                    BindBannerDetails(data);
                }
                else
                {
                    GetBannerAdsList();
                }
            }
            
        }
       
        private  void BindBanner()
        {
            var data = new Common().GetBanner();
            if (data.Tables.Count > 0)
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    ddlBannerPosition.DataSource = data;
                    ddlBannerPosition.DataTextField = "AjBannerPosition";
                    ddlBannerPosition.DataValueField = "AjBannerPositionId";
                    ddlBannerPosition.DataBind();
                    ddlBannerPosition.Items.Insert(0, new ListItem("Select Ads Code", "0"));
                    ddlAdsType.DataSource = data;
                    ddlAdsType.DataTextField = "AjBannerPosition";
                    ddlAdsType.DataValueField = "AjBannerPositionId";
                    ddlAdsType.DataBind();
                    ddlAdsType.Items.Insert(0, new ListItem("Select Ads Code", "0"));
                    

                }
                else
                {
                    ddlAdsType.Items.Insert(0, new ListItem("Select Ads Code", "0"));
                    ddlBannerPosition.Items.Insert(0, new ListItem("Select Ads Code", "0"));
                }
            }
            else
            {
                ddlAdsType.Items.Insert(0, new ListItem("Select Ads Code", "0"));
                ddlBannerPosition.Items.Insert(0, new ListItem("Select Ads Code", "0"));
            }


        }

        
        
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            var errMsg = "";
            var fileName = this.FileUpload1.UploadedImageName;
            if (fileName != null)
            {
                hdnImageFile.Value = fileName;
            }
            if (btnUpload.Text == "Save")
            {
                
                var result = new Common().InsertBanner(new SecurePage().LoggedInUserId,
                                                       Convert.ToInt32(ddlCourse.SelectedValue), txtCollegeName.Text, hdnImageFile.Value, txtToolTip.Text.Trim(), txtUrl.Text.Trim(),
                                                       Convert.ToInt32(txtPriority.Text),
                                                       Convert.ToInt32(ddlBannerPosition.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text),chkbannerStatus.Checked==true?true:false, out errMsg, Convert.ToInt32(hdnBannerId.Value));
                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "success";
                    lblResult.Text = errMsg;
                    ClearControl();
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "error";
                    lblResult.Text = errMsg;
                    
                }
            }
            else
            {
                var result = new Common().InsertBanner(new SecurePage().LoggedInUserId,
                                                      Convert.ToInt32(ddlCourse.SelectedValue),txtCollegeName.Text, hdnImageFile.Value, txtToolTip.Text.Trim(), txtUrl.Text.Trim(),
                                                       Convert.ToInt32(txtPriority.Text),
                                                       Convert.ToInt32(ddlBannerPosition.SelectedValue),Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text),chkbannerStatus.Checked==true?true:false, out errMsg, Convert.ToInt32(hdnBannerId.Value), Convert.ToInt32(hfcollegeBranchCourseId.Value));
                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "success";
                    lblResult.Text = errMsg;
                    btnUpload.Text = "Save";
                    ClearControl();
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "error";
                    lblResult.Text = errMsg;

                }
                
            }
            GetBannerAdsList(); 
        }

        protected void RptCollegeListItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblResult.Visible = false;
            var data = new Common().GetBannerById(0, Convert.ToInt32(e.CommandArgument.ToString()));
         
            if (data.Tables.Count > 0)
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    hdnBannerId.Value = e.CommandArgument.ToString();
                    hfcollegeBranchCourseId.Value = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjCollegeBranchCourseId"].ToString())
                                                   ? data.Tables[0].Rows[0]["AjCollegeBranchCourseId"].ToString()
                                                   : "0";
                    txtCollegeName.Text = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjCollegeBranchName"].ToString())
                                                   ? data.Tables[0].Rows[0]["AjCollegeBranchName"].ToString()
                                                   : "0";

                    ddlCourse.SelectedValue = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjCourseId"].ToString())
                                                  ? data.Tables[0].Rows[0]["AjCourseId"].ToString()
                                                  : "0";
                    ddlBannerPosition.SelectedValue = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjBannerPositionId"].ToString())
                                                 ? data.Tables[0].Rows[0]["AjBannerPositionId"].ToString()
                                                 : "0";
                    txtPriority.Text = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjPriorityId"].ToString())
                                                 ? data.Tables[0].Rows[0]["AjPriorityId"].ToString()
                                                 : "0";
                    var img = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjBannerImage"].ToString())
                                                 ? data.Tables[0].Rows[0]["AjBannerImage"].ToString()
                                                 : "N/A";
                    txtUrl.Text = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjBannerUrl"].ToString())
                                             ? data.Tables[0].Rows[0]["AjBannerUrl"].ToString()
                                             : "N/A";
                    txtToolTip.Text = !string.IsNullOrEmpty(data.Tables[0].Rows[0]["AjBannerToolTip"].ToString())
                                                 ? data.Tables[0].Rows[0]["AjBannerToolTip"].ToString()
                                                 : "N/A";
                    txtStartDate.Text = !string.IsNullOrEmpty(Convert.ToString(data.Tables[0].Rows[0]["AjAdsBannerStartDate"]))
                                                 ? Convert.ToDateTime(data.Tables[0].Rows[0]["AjAdsBannerStartDate"]).ToString("MM/dd/yyyy")
                                                 : "";
                    txtEndDate.Text = !string.IsNullOrEmpty(Convert.ToString(data.Tables[0].Rows[0]["AjAdsBannerEndDate"]))
                                                 ? Convert.ToDateTime(data.Tables[0].Rows[0]["AjAdsBannerEndDate"]).ToString("MM/dd/yyyy")
                                                 : "";
                    chkbannerStatus.Checked = !string.IsNullOrEmpty(Convert.ToString(data.Tables[0].Rows[0]["AjBannerStatus"]))
                        ? Convert.ToBoolean(data.Tables[0].Rows[0]["AjBannerStatus"])==true?true:false
                                                 : false;

                    if (img.Equals("N/A"))
                    {
                        FileUpload1.SetImgUrl = "";
                    }
                    else
                    {
                        Imgbanner.Visible = true;
                        Imgbanner.ImageUrl = new Common().GetFilepath("BannerImage") + img;
                        FileUpload1.SetImgUrl = new Common().GetFilepath("BannerImage") + img;
                        hdnImageFile.Value = img;

                    }
                    btnUpload.Text = "Update";
                    lblHeading.InnerText = txtCollegeName.Text;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divRankSourceInsert','650px','sndAddCollegeBanner');", true);
                }
            }
        }

       private void ClearControl()
       {
           ddlCourse.ClearSelection();
            txtPriority.Text = string.Empty;
           ddlBannerPosition.ClearSelection();
           txtCollegeName.Text = string.Empty;
           lblHeading.InnerText = "Add Display Ads";
       }

        // Method to Get The Banner Ads Details 
       private void GetBannerAdsList()
       {
           DataSet data = new Common().GetBannerById();
           BindBannerDetails(data);
       }

       protected void ddlCourseList_SelectedIndexChanged(object sender, EventArgs e)
       {
           ddlAdsType.ClearSelection();
           txtCollegeSearch.Text = "";
           lblResult.Visible = false;
           if (ddlCourseList.SelectedIndex > 0)
           {
               DataSet data = new Common().GetBannerById(courseId:Convert.ToInt16(ddlCourseList.SelectedValue));
               BindBannerDetails(data);
           }
           else
           {
               GetBannerAdsList();
           }
       }

       protected void ddlAdsType_SelectedIndexChanged(object sender, EventArgs e)
       {
           lblResult.Visible = false;
           ddlCourseList.ClearSelection();
           txtCollegeSearch.Text = "";
           if (ddlAdsType.SelectedIndex > 0)
           {
               DataSet data = new Common().GetBannerById(adsType: Convert.ToInt16(ddlAdsType.SelectedValue));
               BindBannerDetails(data);
           }
           else
           {
               GetBannerAdsList();
           }
       }

       protected void btnSearch_Click(object sender, EventArgs e)
       {
           ddlAdsType.ClearSelection();
           ddlCourseList.ClearSelection();
           if (!string.IsNullOrEmpty(txtCollegeSearch.Text))
           {
               DataSet data = new Common().GetBannerById(collegeName: txtCollegeSearch.Text);
               BindBannerDetails(data);
           }
           else
           {
               GetBannerAdsList();
           }
       }      
    }
}


