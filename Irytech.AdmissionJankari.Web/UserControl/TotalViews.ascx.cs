using System;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class TotalViews : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string TotalViewCount
        {
            get { return lblTotalViews.Text; }
            set { lblTotalViews.Text = value; }
        }
        public string TotalViewsTooltip
        {
            set { lblTotalViews.ToolTip = "Total views for " + value; }
        }
    }
}