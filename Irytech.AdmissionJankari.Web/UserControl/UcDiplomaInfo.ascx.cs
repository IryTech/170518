using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcDiplomaInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
                validateControl();
        }
        #region Method
        // Method To Validate Control
        protected void validateControl()
        {
            revDipMarks.ValidationExpression = ClsSingelton.aRegExpDecimal;
            revDipYOP.ValidationExpression = ClsSingelton.aRegExpInteger;
        }

        #endregion

        #region Property

        public string DicCollegeName
        {
            get { return txtDipCollegeName.Text; }
        }
        public string DicCourseName
        {
            get { return txtDipCourse.Text; }
        }

        public string DicPer
        {
            get { return txtDipMarks.Text; }
        }
        public string DicYOP
        {
            get { return txtDipYOP.Text; }
        }
        public string DicCGPA
        {
            get { return txtDipCGPA.Text; }
        }
        #endregion
    }
}