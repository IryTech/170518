using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegePresidentSpeech : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string DirectorName
        {
            set { 
                lblDirectorName.InnerText = value;
                lblPersonName.InnerText = value;
                }
        }
        public string DirectorImage
        {
            set { 
                imgDirector.ImageUrl = value;
                imgDirectorImage.ImageUrl = value;
                }
        }
        public string DirectorImageAltText
        {
            set
            {
                imgDirector.AlternateText = value;
                imgDirectorImage.AlternateText = value;
            }
        }
        public string DirectorSpeech
        {
            set
            {
               
                lblDirectorSpeech.InnerHtml = value;
            }
        }
        public string DirectorSpeechFull
        {
            set
            {

                lblSpeech.InnerHtml = value;
            }
        }
        public string Designation
        {
            set
            {
                lblDesignation.InnerHtml = value;
                
            }
        }
    }
}