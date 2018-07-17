using System;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.UserControl
{
    

    public partial class ExcelSuccessCount : System.Web.UI.UserControl
    {
        
		//This call is required by the Web Form Designer.
		[System.Diagnostics.DebuggerStepThrough()]

		private void InitializeComponent()
		{

		}

		private void Page_Init(System.Object sender, System.EventArgs e)
		{
			//CODEGEN: This method call is required by the Web Form Designer
			//Do not modify it using the code editor.
			InitializeComponent();
		}

	

		public void SetProgress(int current, int max)
		{
			var percent = double.Parse((current * 100 / max).ToString()).ToString("0");

			if (!percent.Equals("0") ) {
				lblProgress.Text = percent + "% complete (" + current.ToString() + " of " + max.ToString() + ")";

				lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width=200><TR><TD bgcolor=#000066 width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
			}
            if (int.Parse(percent) <10)
            {
                lblProgress.Visible = false;
            }
		     else {
				lblProgress.Visible = true;
			}
		}
		public void  ProgressBar()
		{
			Init += Page_Init;
		}
    }
}