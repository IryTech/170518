using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ucExamQuickQuery : System.Web.UI.UserControl
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            _objCommon=new Common();
            hndCourseId.Value = _objCommon.CourseId.ToString();

            
        }

        public string ExamName
        {
            set { lblExamName.InnerText = value; }
        }
        public string ExamId
        {
            set { hndExamId.Value = value; }
        }
       
    }
}