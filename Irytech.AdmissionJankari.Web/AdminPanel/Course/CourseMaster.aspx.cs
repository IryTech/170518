using System;
using IryTech.AdmissionJankari.BL;


namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{
    public partial class CourseMaster :SecurePage
    {
        
        private Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
             BindCourseDetails();
       
       
        }
        //PagerPageIndexChanged   for paging ..By Indu Kumar Pandey 
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
            {
                try
                {
                    rptCourseCategoryData.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCourseCategoryData, Common.ConvertToDataTable(data));

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CourseMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCourseCategoryData.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }

        //method to Bind COurse Details..By Indu Kumar Pandey 
        private void BindCourseDetails()
         {
             _objCommon = new Common();
             var data = CourseProvider.Instance.GetAllCourseList();
             if (data.Count > 0)
             {
                 try
                 {
                     rptCourseCategoryData.Visible = true;
                     lblInform.Visible = false;
                     ucCustomPaging.BindDataWithPaging(rptCourseCategoryData, Common.ConvertToDataTable(data));

                 }
                 catch (Exception ex)
                 {
                     var err = ex.Message;
                     if (ex.InnerException != null)
                     {
                         err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                     }
                     const string addInfo = "Error in Executing  BindCourseDetails in CourseMaster.aspx :: -> ";
                     var objPub = new ClsExceptionPublisher();
                     objPub.Publish(err, addInfo);
                 }
             }
             else
             {
                 rptCourseCategoryData.Visible = false;
                 lblInform.Visible = true;
                 lblInform.Text = _objCommon.GetErrorMessage("noRecords");
             }
         }

        
        
    }
}