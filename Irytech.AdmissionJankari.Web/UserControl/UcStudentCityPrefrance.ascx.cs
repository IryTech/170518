using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentCityPrefrance : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Property

        public string StudentCityPrefrance1
        {
            get { return txtCityPrefrance1.Text; }
        }

        public string StudentCityPrefrance2
        {
            get { return txtCityPrefrance2.Text; }
        }
        public string StudentCityPrefrance3
        {
            get { return txtCityPrefrance3.Text; }
        }

        #endregion
    }
}