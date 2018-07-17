using System;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;


namespace IryTech.AdmissionJankari20.Web
{
    public partial class _Default : PageBase
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
         BindPageTitleAndKeyWords();
        
        }
        // to show page title ,keyword and description
        private void BindPageTitleAndKeyWords()
        {
           
           
            try
            {
                   Page.Title = "Official details of colleges in India  -  Admission Jankari";
                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";
                metadesc.Content = "Top " + new Common().CourseName + " colleges, Best private " +
                                   new Common().CourseName +
                                   "  in India, find fees, rank, sources, management, establishment, placement, eligibility criteria, intake | College Search, " +
                                   " College Comparison, Course Comparison, Exams, News," +
                                   " Loan, Direct Admission, Notices, Admission in India, Add Your Institute, Advertise With Us - Admission Jankari ";
                    Page.Header.Controls.Add(metadesc);
                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content =
                                                   "AdmissionJankari, Higher Education, " + new Common().CourseName + ", " +
                                                   "Top " + new Common().CourseName + " Colleges, Best Private College, Top Private College, " + new Common().CourseName + " Exams, News," +
                                                   " Admission Notices, Direct Admission, Loan, Expert Board"
                                           };
                    Page.Header.Controls.Add(metaKeywords);
            }
            catch (Exception Ex)
            {
                var err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in Default.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        
    }
}
