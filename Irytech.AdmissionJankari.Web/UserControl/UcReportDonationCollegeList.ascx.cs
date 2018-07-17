using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcReportDonationCollegeList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPagingCollegeByCity.PageSize = ApplicationSettings.Instance.CollegePageSize;
            ucCustomPagingCollegeByCity.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPagingCollegeByCity.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCollegeList();
        }
        
        // Method to Bind The College Donation Repoted aginst
        private void BindCollegeList()
        {
            Common objCommon = new Common();

            try
            {
                var data = objCommon.RemoveDuplicateRows(objCommon.GetReportDonationCollegeList(objCommon.CourseId), "AjCollegeBranchCourseId").AsEnumerable().Where(college=>college.Field<bool>("AjReportStatus")==true).CopyToDataTable();
                if (data != null)
                {
                    if (data.Rows.Count > 0)
                    {
                        ucCustomPagingCollegeByCity.BindDataWithPaging(rptCollegeList, data);
                    }
                    else
                    {
                        divCollegeInCity.Visible = false;
                    }
                }
                else
                {
                    divCollegeInCity.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                divCollegeInCity.Visible = false;
            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            BindCollegeList();
        }
    }
}