using System;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CommentCount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
        }
        public string TotalCommentCount
        {
          set {

              lblCommentCount.Text = !string.IsNullOrEmpty(value)?value:"0"; 

          }
            
        }
        public string TotalCommentTooltip
        {
            set { lblCommentCount.ToolTip = "Total comments for " + value; }
        }
    }
}