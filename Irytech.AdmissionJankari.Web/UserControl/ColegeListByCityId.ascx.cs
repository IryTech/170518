using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ColegeListByCityId : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPagingCollegeByCity.PageSize = ApplicationSettings.Instance.CollegePageSize;
            ucCustomPagingCollegeByCity.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPagingCollegeByCity.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            if (Request.QueryString["CollegeBranchCourseId"] != null)
            {
                BindCollegeListByCityId(Request.QueryString["CollegeBranchCourseId"]);
            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var collegeName = "";
            var objCommon = new Common();
            var collegeCityData = CollegeProvider.Instance.GetCollegeListByCityId(
                      Convert.ToInt16(Request.QueryString["CollegeBranchCourseId"]), out collegeName);

            if (collegeCityData.Count > 0)
            {
                try
                {

                    ucCustomPagingCollegeByCity.BindDataWithPaging(dtlCollegeCity, Common.ConvertToDataTable(collegeCityData));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ColegeListByCityId.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
           

        }

        private  void BindCollegeListByCityId(string collegeBranchCourseId)
        {
            var objCommon = new Common();
            string collegeName = "";
            var collegeCityData = CollegeProvider.Instance.GetCollegeListByCityId(
                      Convert.ToInt16(collegeBranchCourseId), out collegeName);
            try
            {
                if (collegeCityData.Count > 0)
                {

                    var query = collegeCityData.Select(data => new
                                                                   {
                                                                       CityName = data.CollegeBranchCityName
                                                                   }
                        ).First();
                    lblCollegeName.Text = query.CityName;
                    ucCustomPagingCollegeByCity.BindDataWithPaging(dtlCollegeCity, Common.ConvertToDataTable(collegeCityData));
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
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  BindCollegeListByCityId in ColegeListByCityId.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
        }
    }
