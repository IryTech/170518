using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeAvailablePlaceMent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PlacementPager.PagerPageIndexChanged += PagerPageIndexChanged;
        }
        public int PageSizeCourse
        {
            set
            {
                PlacementPager.PageSize = value;
            }
        }
        public int PageButtonCount
        {
            set
            {
                PlacementPager.ButtonsCount = value;
            }
        }
        public static List<CollegeBranchCoursePlacementProperty> PlaceMentData;
        public List<CollegeBranchCoursePlacementProperty> CollegePlacementData
        {
            get { return PlaceMentData; }
            set
            {
                PlaceMentData = value;
                try
                {
                    if (value.Count > 0)
                    {
                        divCollegePlacement.Visible = true;
                        lblCollegePlacement.Visible = false;
                        PlacementPager.BindDataWithPaging(rptCollegePlacement, Common.ConvertToDataTable(value));
                    }
                    else
                    {
                        divCollegePlacement.Visible = false;
                        lblCollegePlacement.Visible = true;
                        lblCollegePlacement.CssClass = "info";
                        lblCollegePlacement.Text = new Common().GetErrorMessage("noRecords");
                        rptCollegePlacement.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo =
                        "Error in Executing  CollegePlacementData in CollegeAvailAblePlacement.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }

            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var collegeName = "";
            var objCommon = new Common();
            try
            {
                PlacementPager.BindDataWithPaging(rptCollegePlacement, Common.ConvertToDataTable(PlaceMentData));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  Pager_PageIndexChanged in CollegeAvailableCourse.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }


        }
    }
}