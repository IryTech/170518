using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeAvailableCourse : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomCoursePaging.PagerPageIndexChanged += PagerPageIndexChanged;
           
           
        }

        public int PageSizeCourse {
            set
        {
            ucCustomCoursePaging.PageSize = value;
        }
        }
        public int PageButtonCount
        {
            set {
                ucCustomCoursePaging.ButtonsCount = value;
            }
        }
        public string CourseName
        {
            set
            {
                hdnCourseName.Value = Utils.RemoveIllegealFromCourse(value);
            }
            get { return hdnCourseName.Value;
            }
        }

        public static List<CollegeBranchCourseStreamProperty> CourseStreamData;
        public List<CollegeBranchCourseStreamProperty> CourseStream
        {
            get { return CourseStreamData; }
           set
           {
               CourseStreamData = value;
                

                if (value.Count > 0)
                {
                    try
                    {
                        lblStreamresult.Visible = false;
                        divStreamresult.Visible = true;
                        ucCustomCoursePaging.BindDataWithPaging(rptCourse, Common.ConvertToDataTable(value));
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
                else
                {
                    lblStreamresult.Visible = true;
                    lblStreamresult.CssClass = "info";
                    lblStreamresult.Text = new Common().GetErrorMessage("noRecords");
                    divStreamresult.Visible = false;
                }
            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var collegeName = "";
            var objCommon = new Common();
            try
            {
                ucCustomCoursePaging.BindDataWithPaging(rptCourse, Common.ConvertToDataTable(CourseStreamData));
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