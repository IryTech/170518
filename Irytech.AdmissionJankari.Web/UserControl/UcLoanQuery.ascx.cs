using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcLoanQuery : System.Web.UI.UserControl
    {
        Common _ObjCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
             _ObjCommon=new Common();
             hndCourseId.Value = _ObjCommon.CourseId.ToString();
        }
    }
}