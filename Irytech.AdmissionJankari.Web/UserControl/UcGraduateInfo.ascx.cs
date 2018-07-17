using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcGraduateInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ValidateControl();
        }

        #region Method
        protected void ValidateControl()
        {
            revGraduationYOP.ValidationExpression = ClsSingelton.aRegExpInteger;
           
            
        }


        #endregion

        #region Properties
        public string GrdCollegeName
        {
            get { return txtGrdCollegeName.Text; }
        }

        public string GrdSpecialization
        {
            get { return txtGrdSpecialization.Text; }
        }
        public string GrdYOP
        {
            get { return txtGraduationYOP.Text; }
        }
        public string GrdPer
        {
            get { return txtGrdPer.Text; }
        }
        public string GrdCGPA
        {
            get { return ""; }
        }


        #endregion
    }
}