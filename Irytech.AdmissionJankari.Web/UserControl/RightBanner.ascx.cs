using System;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class RightBanner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)return;
            BindRightBanner();
        }

        private void BindRightBanner()
        {
            var objDataSet = new Common().GetBannerById(new Common().CourseId);
            if (objDataSet.Tables.Count <= 0) return;
            var dv = objDataSet.Tables[0].DefaultView;
            dv.RowFilter = " AjBannerPositionId=3";
            if (dv.Count > 0)
            {
                dtlRightBanner.DataSource = dv.Table;
                dtlRightBanner.DataBind();
            }
        }
    }
}