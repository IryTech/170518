using System;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcBookSeatInstruction : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string CollegeInstuction
        {
            set { spnCollegeInstruction.InnerHtml = value; }
        }

        public bool CollegeVisible
        {
            set { liCollegeInstruction.Visible = value; }
        }

        public string TotalAmountFees
        {
            set { spnTotalAmount.InnerHtml = value; }
            
        }

       
    }
}