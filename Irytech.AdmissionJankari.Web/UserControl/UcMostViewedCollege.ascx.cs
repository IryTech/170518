using System;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcMostViewedCollege : System.Web.UI.UserControl
    {
        private string  _viewdType="";
        private int _cityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ApplicationSettings.Instance.MostViewdCollegePageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindMostViewdCollegeByCourse();

        }
       

        public string MostViewdType
        {
            set{ _viewdType=value;}
        }
        public int CityId
        {
            set { _cityId = value; }
        }
        protected void BindMostViewdCollege(string MostViewdType)
        {

        }
        private void BindMostViewdCollegeInCity()
        {

        }
        public void BindMostViewdCollegeByCourse()
        {
           var collegeName = "";
            var objCommon = new Common();
            var collegeCityData = CollegeProvider.Instance.GetMostViewdCollegeByCourse(Convert.ToInt16(objCommon.CourseId));

            if (collegeCityData.Count > 0)
            {
                try
                {
                    ucCustomPaging.BindDataWithPaging(rptMostviewdCollege, Common.ConvertToDataTable(collegeCityData));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcMostViewedCollege.axcx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var collegeName = "";
            var objCommon = new Common();
            var collegeCityData = CollegeProvider.Instance.GetMostViewdCollegeByCourse(Convert.ToInt16(objCommon.CourseId));

            if (collegeCityData.Count > 0)
            {
                try
                {
                    ucCustomPaging.BindDataWithPaging(rptMostviewdCollege, Common.ConvertToDataTable(collegeCityData));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcMostViewedCollege.axcx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }


        }
    }
}