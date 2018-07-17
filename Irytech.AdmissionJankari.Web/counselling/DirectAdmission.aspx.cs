using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.counselling
{
    public partial class DirectAdmission : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["CourseId"] != null)
            {
                Common objCommon = new Common();
                objCommon.CourseId = Convert.ToInt16(Request.QueryString["CourseId"]);
                var courseDetails = CourseProvider.Instance.GetCourseById(objCommon.CourseId)
                                    .Where(
                                        result => result.CourseId == objCommon.CourseId
                                        )
                                    .ToList().FirstOrDefault();


                objCommon.CourseName = courseDetails.CourseName;
            }
            BindPageTitleAndKeyWords();

        }
        // to show page title ,keyword and description
        private void BindPageTitleAndKeyWords()
        {
            
            try
            {
                

                    Page.Title = "";
                    Page.Title = "Direct Admission in "+new Common().CourseName+" Colleges under Management Quota Seats - Admission Jankari";

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content =
                        "Hurry Up! Get Direct Admission in " + new Common().CourseName +
                        "  Colleges under Management Quota Seats. Apply online to multiple colleges for Direct admission against vaccant seats. No Donation! Free Admissions!. An anti donation campaign. - Admission Jankari";

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content =
                                                  "Direct Admission in " + new Common().CourseName + " Colleges under Management Quota Seats - Admission Jankari"
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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in DirectAdmission.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}