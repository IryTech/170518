using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeGallery :SecurePage
    {
        Common _objCommon;

        protected void Page_Load(object sender, EventArgs e)
        {

            ucCustomPaging.PageSize = ApplicationSettings.Instance.CollegePageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
                BindCollegeDetails();
                
               
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {

            BindCollegeDetails();
            
        }

       

        protected void BindCollegeDetails()
        {
            try
            {
                _objCommon = new Common();
                var data = _objCommon.GetCollegeImageGallery();
                if (data != null)
                {
                    if (data.Rows.Count > 0)
                    {
                        ucCustomPaging.Visible = true;
                        rptCollegeList.Visible = true;
                        lblErorrMsg.Visible = false;
                        ucCustomPaging.BindDataWithPaging(rptCollegeList, data);
                    }
                    else
                    {
                        lblErorrMsg.Visible = true;
                        ucCustomPaging.Visible = false;
                        rptCollegeList.Visible = false;
                        lblErorrMsg.Text = "No Record Found";

                    }
                }
                else
                {
                    lblErorrMsg.Visible = true;
                    ucCustomPaging.Visible = false;
                    rptCollegeList.Visible = false;
                    lblErorrMsg.Text = "No Record Found";
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }
              

        protected void btnSearch_Click(object sender, EventArgs e)
        {

             var collegeFilterGalleryData = CollegeProvider.Instance.GetCollegeGalleryList();
            collegeFilterGalleryData = collegeFilterGalleryData.Where(result => result.CollegeBranchName == txtCollegeData.Text.Trim()).ToList();
            if (collegeFilterGalleryData.Count > 0)
            {
                ucCustomPaging.Visible = true;
                rptCollegeList.Visible = true;
                lblErorrMsg.Visible = false;
                ucCustomPaging.BindDataWithPaging(rptCollegeList, Common.ConvertToDataTable(collegeFilterGalleryData));

            }
            else
            {
                lblErorrMsg.Visible = true;
                ucCustomPaging.Visible = false;
                rptCollegeList.Visible = false;
                lblErorrMsg.Text = "No Record Found";

            }
        }
    }
}