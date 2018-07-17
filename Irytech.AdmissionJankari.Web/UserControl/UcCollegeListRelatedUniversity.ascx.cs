using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCollegeListRelatedUniversity : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ApplicationSettings.Instance.MostViewdCollegePageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCollegeList();
        }


        // Method to Bind the CollegeList accoding to university List
        protected void BindCollegeList()
        {
            var ObjCollegeList = new  List<CollegeBranchProperty>();
            try
            {
                ObjCollegeList = CollegeProvider.Instance.GetCollegeListByUniversityId(UniversityId, CourseId);
                if (ObjCollegeList.Count > 0)
                {
                    ucCustomPaging.BindDataWithPaging(rptCollegeList, Common.ConvertToDataTable(ObjCollegeList));
                }
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeList in UcCollegeListRelatedUniversity.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {

            try
            {
                BindCollegeList();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcCollegeListRelatedUniversity.axcx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }



        }

        public int CourseId
        {
            get;
            set;
        }
        public int UniversityId
        {
            get;
            set;
        }

    }
}