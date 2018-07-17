using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Web.UI.HtmlControls;
using System.Data;

namespace IryTech.AdmissionJankari20.Web
{
    public partial class ReportDonation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindPageTitleAndKeyWords();
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString[0] != null)
                {
                    BindStory(Convert.ToString(Request.QueryString[0]));

                }
                
            }
            else
            {
                Common ObjCommon = new Common();
                ucReportDonation.BindUserStory(courseId: ObjCommon.CourseId);
            }
        }
        private void BindPageTitleAndKeyWords()
        {
            var objPage = new Common().GetPageTitleKeyWordAndDecsription("ReportDonation");

            try
            {
                if (objPage.Rows.Count > 0)
                {

                    Page.Title = "";
                    Page.Title = Convert.ToString(objPage.Rows[0]["AjPageTitle"].ToString());

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content = Convert.ToString(objPage.Rows[0]["AjPageDescription"].ToString());

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                    {
                        Name = "keywords",
                        Content =
                            Convert.ToString(objPage.Rows[0]["AjPageKeyword"].ToString())
                    };

                    Page.Header.Controls.Add(metaKeywords);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in ReportDonation.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindStory(string collegeName)
        {
            Common objCommon = new Common();
            try
            {
                var collegeList = new Common().GetCollegeNameList(objCommon.CourseId);

                var query =
                    collegeList.Tables[0].AsEnumerable().Where(colleges => IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(collegeName).Equals(
                        IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                        StringComparison.OrdinalIgnoreCase)).Select(colleges => new
                        {
                            collegeBranchCourseId =
                        colleges.Field<int>(
                            "AjCollegeBranchCourseId")
                        }).FirstOrDefault();
                if (query != null)
                {
                    ucReportDonation.BindUserStory(collegeBranchCourseId:query.collegeBranchCourseId);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollege in UcReportDonation.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

    }
}