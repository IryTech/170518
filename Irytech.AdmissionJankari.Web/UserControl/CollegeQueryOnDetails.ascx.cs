using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeQueryOnDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetCurrentPageName();

        }
        public void GetCurrentPageName()
        {
            var path = HttpContext.Current.Request.Url.AbsolutePath;
            var info = new System.IO.FileInfo(path);
            var pageName = info.Name;
            hdnPageName.Value= pageName;
        } 
        public string CollegeEmailId
        {
            get {return hdnCollegeEmailId.Value; }
            set { hdnCollegeEmailId.Value = value; }
        }
        public string CollegeName
        {
            get { return lblCollegeaNameForQuery.Text; }
            set { lblCollegeaNameForQuery.Text = value; }
        }
        public int BranchCourseId
        {
            get { return Convert.ToInt16(hdnBranchCourseId.Value); }
            set { hdnBranchCourseId.Value = Convert.ToString(value); } 
        }
        public int CourseId
        {
            get { return Convert.ToInt16(hdnCourse.Value); }
            set { hdnCourse.Value = Convert.ToString(value); }
        }
        public string CityName
        {
            get { return hdnCityId.Value; }
            set { hdnCityId.Value = value; } 
        }
        
        public List<CollegeBranchCourseStreamProperty> CourseStream
        {
            set
            {
                if(value.Count>0){
                slctCoursStream.DataSource = value;
                slctCoursStream.DataTextField = "StreamName";
                slctCoursStream.DataValueField = "StreamId";
                slctCoursStream.DataBind();
                slctCoursStream.Items.Insert(0,new ListItem("--Select-","0"));
                }
                else{
                slctCoursStream.Items.Insert(0,new ListItem("--Select-","0"));
                }
            }
        }
    }
}