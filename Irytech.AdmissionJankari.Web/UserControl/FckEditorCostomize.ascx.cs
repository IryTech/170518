using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class FckEditorCostomize : System.Web.UI.UserControl
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            txtFckEditorCostomize.config.toolbar = new object[]
            {
                new object[] {"Preview", "-"},
                new object[] { "Cut", "Copy", "Paste", "PasteText","SpellChecker" },
                new object[] { "Styles", "Format", "Font", "FontSize" },
            };
           
        }
        public  string FckValue
        {
            get { return txtFckEditorCostomize.Text; }
            set { txtFckEditorCostomize.Text = value; }
        }
   

        public string Fckrfv
        {
            set { rfvEditor.ValidationGroup = value; }
        }
        public string TooTip
        {
            set { txtFckEditorCostomize.ToolTip = value; }
        }

        public string ValidationGroup
        {
            set { rfvEditor.ValidationGroup = value; }
        }
        public string ErrorMessage
        {
            set { rfvEditor.ErrorMessage = value; }
        }

        public string Width
        {
            set { txtFckEditorCostomize.Width = new Unit(value); }
        }
    }
}