using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using System.Data;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;

namespace IryTech.AdmissionJankari20.Web.College
{
    public partial class CourseComparison : PageBase
    {
        DataSet _ds;
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            _objCommon = new Common();
            if (IsPostBack) return;
            if (Request.QueryString["CourseId"] != null)
            {
                   _objCommon.CourseId = Convert.ToInt16(Request.QueryString["CourseId"]);
                 var courseDetails= CourseProvider.Instance.GetCourseById(  _objCommon.CourseId)
                                     .Where(
                                         result => result.CourseId== _objCommon.CourseId
                                         )
                                     .ToList().FirstOrDefault();


                if (courseDetails != null) _objCommon.CourseName = Utils.RemoveIllegealFromCourse(courseDetails.CourseName);
            }
            hdnCourse.Value = _objCommon.CourseId.ToString(CultureInfo.InvariantCulture);
            
          liStreamBasicInfo.Visible = false;
            DivStreamDetails.Visible = false;
            if (Request.QueryString["StreamId1"] != null && Request.QueryString["StreamId2"] != null)
            {
                //Bind All StreamDetails based on FirstStreamId and SecondStreamId 
                BindCompare(Convert.ToInt32(Request.QueryString["StreamId1"]),
                            Convert.ToInt32(Request.QueryString["StreamId2"]));
                BindPageTitleAndKeyWordsByCollegeCompare(txtFirsteStream.Text, txtSecondCollegeName.Text); DivStreamDetails.Visible = true;
            }
            else
            {
                BindPageTitleAndKeyWords();
            }

            
        }

        private void BindPageTitleAndKeyWords()
        {

            try
            {



                Page.Title = "Compare " + new Common().CourseName + " Courses | " + new Common().CourseName + " Course Comparisons - Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = "Compare two or more streams of " + new Common().CourseName +
                                   " course. Compare courses on brief history, future scope, related industries, core companies and average package - Admission Jankari";

                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content =
                        new Common().CourseName + " Course Comparison, Top " + new Common().CourseName + " Courses, Best " + new Common().CourseName + " Courses,"
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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CourseComparasion.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindPageTitleAndKeyWordsByCollegeCompare(string stream1, string stream2)
        {

            try
            {



                Page.Title = "Compare " + stream1 + " Vs. " + stream2 + " Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = "Compare " + stream1 + " Vs. " + stream2 +
                                   ". Compare courses on brief history, future scope, related industries, core companies and average package - Admission Jankari";
                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content = stream1 + " " + stream2 +
                        " ,Compare " + stream1 + " Vs. " + stream2 + 
                        " Top Comparison, Average Package, Related Industries, Cor Companies, Future Scope, Course Comparison, Stream Comparison "
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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CourseComparasion.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        // Method to Bind the Course Compare
        protected void BindCompare(int streamId1, int streamId2)
        {
            DivStreamDetails.Visible = true;
            DivStreamDetails.Focus();
            //Return StreamName based on First StreamId1
            var data = StreamProvider.Instance.GetStreamListById(Convert.ToInt32(Request.QueryString["StreamId1"]));

            if (data.Count > 0)
            {
                var query = data.Select(result => new
                {
                    StreamName = result.CourseStreamName,

                }).First();

                //Bind First StreamDatails based on firstStreamName
                txtFirsteStream.Text = query.StreamName;
                var sreamdata = StreamProvider.Instance.GetStreamListByStreamName(txtFirsteStream.Text.Trim());
                if (data.Count > 0)
                {

                    var streamquery = sreamdata.Select(result => new
                    {
                        result.CourseStreamName, result.CourseStreamHistory, result.CourseStreamDesc,
                        CourseStreamFuture = result.CourseSteamFuture, result.CourseStreamRelatedIndustry, result.CourseStreamCoreCompanies,

                    }).First();

                    lblStream1.Text = streamquery.CourseStreamName;
                    lblComFirstStreamName.Text = streamquery.CourseStreamName;
                    lblBriefHistory.Text = streamquery.CourseStreamHistory;
                    lblShortDesc.Text = streamquery.CourseStreamDesc;
                    lblFuruteScope.Text = streamquery.CourseStreamFuture;
                    lblReIndustry.Text = streamquery.CourseStreamRelatedIndustry;
                    lblCoreCompanies.Text = streamquery.CourseStreamCoreCompanies;


                }

            }
            //Return StreamName based on Second StreamId2
            var secondStreamdata = StreamProvider.Instance.GetStreamListById(Convert.ToInt32(Request.QueryString["StreamId2"]));

            if (secondStreamdata.Count > 0)
            {
                var secondStreamquery = secondStreamdata.Select(result => new

                {
                    StreamName = result.CourseStreamName,
                }).First();
                txtSecondCollegeName.Text = secondStreamquery.StreamName;
                //Bind Second StreamDatails based on SecondStreamName
                var secondStreamNamedata = StreamProvider.Instance.GetStreamListByStreamName(txtSecondCollegeName.Text.Trim());
                if (secondStreamNamedata.Count > 0)
                {
                    var selectSecondStreamNamedata = secondStreamNamedata.Select(result => new
                    {
                        result.CourseStreamName, result.CourseStreamHistory, result.CourseStreamDesc,
                        CourseStreamFuture = result.CourseSteamFuture, result.CourseStreamRelatedIndustry, result.CourseStreamCoreCompanies,

                    }).First();

                    lblStream2.Text = selectSecondStreamNamedata.CourseStreamName;
                    lblComSecondStreamName.Text = selectSecondStreamNamedata.CourseStreamName;
                    lblBriefHistory1.Text = selectSecondStreamNamedata.CourseStreamHistory;
                    lblShortDesc1.Text = selectSecondStreamNamedata.CourseStreamDesc;
                    lblFuruteScope1.Text = selectSecondStreamNamedata.CourseStreamFuture;
                    lblReIndustry1.Text = selectSecondStreamNamedata.CourseStreamRelatedIndustry;
                    lblCoreCompanies1.Text = selectSecondStreamNamedata.CourseStreamCoreCompanies;

                }

            }
        }


        //Method for Course to Compare
        private void ComapreCourse()
        {
            //To Retrive all StreamList Based on FirstStreamName.
              var data = StreamProvider.Instance.GetStreamListByStreamName(txtFirsteStream.Text.Trim());
             DivStreamDetails.Visible = true;
              DivStreamDetails.Focus();

            if (data.Count > 0)
            {

                var query = data.Select(result => new
                {
                    result.CourseStreamName,
                     result.CourseName,
                    result.CourseStreamHistory,
                     result.CourseStreamDesc,
                    result.CourseSteamFuture,
                     result.CourseStreamRelatedIndustry,
                    result.CourseStreamCoreCompanies,

                }).First();
                lblStream1.Text = query.CourseStreamName;
                liStreamBasicInfo.Visible = true;
                lblComFirstStreamName.Visible = true;
                lblComFirstStreamName.Text = query.CourseStreamName;
                lblComFirstStreamName.NavigateUrl = Utils.ApplicationRelativeWebRoot + ("Stream-Detail/" + Utils.RemoveIllegealFromCourse(query.CourseName) + "/" + Utils.RemoveIllegalCharacters(query.CourseStreamName)).ToLower();
                if (!(query.CourseStreamHistory.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                {
                    lblBriefHistory.Text =
                        !string.IsNullOrEmpty(
                            Convert.ToString(query.CourseStreamHistory.ToString(CultureInfo.InvariantCulture)))
                            ? query.CourseStreamHistory
                            : "N/A";
                }
                else
                    lblBriefHistory.Text = "N/A";
                if (!(query.CourseStreamDesc.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                {
                    lblShortDesc.Text =
                        !string.IsNullOrEmpty(
                            Convert.ToString(query.CourseStreamDesc.ToString(CultureInfo.InvariantCulture)))
                            ? query.CourseStreamDesc
                            : "N/A";
                }
                else
                    lblShortDesc.Text = "N/A";
                    
                if (!(query.CourseSteamFuture.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                {
                    lblFuruteScope.Text =
                        !string.IsNullOrEmpty(
                            Convert.ToString(query.CourseSteamFuture.ToString(CultureInfo.InvariantCulture)))
                            ? query.CourseSteamFuture
                            : "N/A";
                }
                else
                    lblFuruteScope.Text = "N/A";
                if (!(query.CourseStreamRelatedIndustry.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                {
                    lblReIndustry.Text =
                        !string.IsNullOrEmpty(
                            Convert.ToString(query.CourseStreamRelatedIndustry.ToString(CultureInfo.InvariantCulture)))
                            ? query.CourseStreamRelatedIndustry
                            : "N/A";
                }
                else
                    lblReIndustry.Text = "N/A";

                if (!(query.CourseStreamCoreCompanies.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                {
                    lblCoreCompanies.Text =
                        !string.IsNullOrEmpty(
                            Convert.ToString(query.CourseStreamCoreCompanies.ToString(CultureInfo.InvariantCulture)))
                            ? query.CourseStreamCoreCompanies
                            : "N/A";
                }
                else
                    lblCoreCompanies.Text = "N/A";
  
                }
          
         //To Retrive all StreamList based on SecondCollegeName 
            var data1 = StreamProvider.Instance.GetStreamListByStreamName(txtSecondCollegeName.Text.Trim());
            
            if (data1.Count > 0)
            {
                var query = data1.Select(result => new
                {
                    result.CourseStreamName, result.CourseStreamHistory, result.CourseStreamDesc,result.CourseName,
                    result.CourseSteamFuture, result.CourseStreamRelatedIndustry, result.CourseStreamCoreCompanies,

                }).First();
                lblStream2.Text = query.CourseStreamName;
                liStreamBasicInfo.Visible = true;
                lblComSecondStreamName.Visible = true;
                 lblComSecondStreamName.Text = query.CourseStreamName;
                 lblComSecondStreamName.NavigateUrl = Utils.ApplicationRelativeWebRoot + ("Stream-Detail/" + Utils.RemoveIllegealFromCourse(query.CourseName) + "/" + Utils.RemoveIllegalCharacters(query.CourseStreamName)).ToLower();
              
                 if (!(query.CourseStreamHistory.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                 {
                     lblBriefHistory1.Text =
                         !string.IsNullOrEmpty(
                             Convert.ToString(query.CourseStreamHistory.ToString(CultureInfo.InvariantCulture)))
                             ? query.CourseStreamHistory
                             : "N/A";
                 }
                 else
                     lblBriefHistory1.Text = "N/A";
                 if (!(query.CourseStreamDesc.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                 {
                     lblShortDesc1.Text =
                         !string.IsNullOrEmpty(
                             Convert.ToString(query.CourseStreamDesc.ToString(CultureInfo.InvariantCulture)))
                             ? query.CourseStreamDesc
                             : "N/A";
                 }
                 else
                     lblShortDesc1.Text = "N/A";

                 if (!(query.CourseSteamFuture.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                 {
                     lblFuruteScope1.Text =
                         !string.IsNullOrEmpty(
                             Convert.ToString(query.CourseSteamFuture.ToString(CultureInfo.InvariantCulture)))
                             ? query.CourseSteamFuture
                             : "N/A";
                 }
                 else
                     lblFuruteScope1.Text = "N/A";
                 if (!(query.CourseStreamRelatedIndustry.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                 {
                     lblReIndustry1.Text =
                         !string.IsNullOrEmpty(
                             Convert.ToString(query.CourseStreamRelatedIndustry.ToString(CultureInfo.InvariantCulture)))
                             ? query.CourseStreamRelatedIndustry
                             : "N/A";
                 }
                 else
                     lblReIndustry1.Text = "N/A";

                 if (!(query.CourseStreamCoreCompanies.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                 {
                     lblCoreCompanies1.Text =
                         !string.IsNullOrEmpty(
                             Convert.ToString(query.CourseStreamCoreCompanies.ToString(CultureInfo.InvariantCulture)))
                             ? query.CourseStreamCoreCompanies
                             : "N/A";
                 }
                 else
                     lblCoreCompanies1.Text = "N/A";
            }

        }

 
        //Event for Compare button to comapare all course details
        protected void btnCpmpareCollegeName_Click1(object sender, EventArgs e)
        {
           ComapreCourse();
        }
        //End Method for Compare all Course Details


        //Event for First Replace button
        protected void btnFirstSearchReplace_Click(object sender, EventArgs e)
        {
            txtFirsteStream.Text = txtFirstStreamReplace.Text;
            ComapreCourse();
            txtFirstStreamReplace.Text = "";
        }
        //End Event for First Replace button



        //Event for Second Repalce buton
        protected void btnSecondSearchReplace_Click(object sender, EventArgs e)
        {
            txtSecondCollegeName.Text = txtSecondStreamReplace.Text;
            ComapreCourse();
            txtSecondStreamReplace.Text = "";
        }
        //End Event For Second Replace

    }
}