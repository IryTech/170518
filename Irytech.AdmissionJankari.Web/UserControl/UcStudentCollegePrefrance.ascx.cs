using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Globalization;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Linq;


namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentCollegePrefrance : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Common objCommon=new Common();
            hndCourseId.Value = Convert.ToString(objCommon.CourseId);
        }

        #region Properties
        public string StudentCollegePrefrance1
        {
            get { return txtCollegePrefrance1.Text; }
        }
        public string StudentCollegePrefrance2
        {
            get { return txtCollegePrefrance2.Text; }
        }
        public string StudentCollegePrefrance3
        {
            get{return txtCollegePrefrance3.Text;}
        }


        #endregion


     
    }
}