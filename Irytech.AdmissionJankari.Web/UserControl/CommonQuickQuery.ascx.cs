using System;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CommonQuickQuery : System.Web.UI.UserControl
    {
        Common _obCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            _obCommon = new Common();
            hndCourseId.Value = Convert.ToString(_obCommon.CourseId);
            hdnCourseInApp.Value = Convert.ToString(ApplicationSettings.Instance.CourseId);
            ucPayment.TotalAmountInserted = 1500.ToString();
            if (ApplicationSettings.Instance.ShowPackage)
            {
                hndShowPackage.Value = "1";
            }

        }



        // Method to get the 

        
    }
}
