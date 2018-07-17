using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.Course
{
    public partial class StreamDecsription : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["StreamId"] != null)
            {
                BindStreamBasicDetails(Request.QueryString["StreamId"]);
            }
        }
       
        private void BindStreamBasicDetails(string streamId)
        {
            try
            {
                var basicDetails = StreamProvider.Instance.GetStreamListById(Convert.ToInt16(streamId));
                BindPageTitleAndKeyWords(basicDetails);
                if (basicDetails.Count > 0)
                {
                    usCourseStreamBasicDetails.Visible = true;
                    var query = basicDetails.Select(result => new
                        {
                            CourseName = result.CourseName,
                            CourseStreamName = result.CourseStreamName,
                            Description = result.CourseStreamMetaDesc,
                            History=result.CourseStreamHistory,
                            Future=result.CourseSteamFuture,
                            CoreCompanies=result.CourseStreamCoreCompanies,
                            RelatedIndustry=result.CourseStreamRelatedIndustry

                        }).First();
                    usCourseStreamBasicDetails.CourseName = query.CourseName != "" ? query.CourseName : "N/A";
                    usCourseStreamBasicDetails.CourseStreamName = query.CourseStreamName != "" ? query.CourseStreamName : "N/A";

                    if (!string.IsNullOrEmpty(query.Description))
                    {
                        usCourseStreamDescription.Visible = true;
                        usCourseStreamDescription.Description = query.Description != "" ? query.Description : "N/A";
                    }
                    else
                    {
                        usCourseStreamDescription.Visible = false;
                    }
                    lblHeaderCollegeName.Text = query.CourseName;
                    if (!string.IsNullOrEmpty(query.CoreCompanies))
                    {
                        usCourseStreamCoreCompanies.Visible = true;
                        usCourseStreamCoreCompanies.CoreCompanies = query.CoreCompanies != "" ? query.CoreCompanies : "N/A";
                    }
                    else 
                    {
                        usCourseStreamCoreCompanies.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(query.History))
                    {
                        usCourseStreamHistory.Visible=true;
                        usCourseStreamHistory.History = query.History != "" ? query.History : "N/A";
                    }
                    else
                    {
                        usCourseStreamHistory.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(query.Future))
                    {
                        usCourseStreamFuture.Visible=true;
                        usCourseStreamFuture.Future = query.Future != "" ? query.Future : "N/A";
                    }
                    else 
                    {
                        usCourseStreamFuture.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(query.Future))
                    {
                        usCourseStreamRelatedIndustry.Visible=true;
                        usCourseStreamRelatedIndustry.RealtedIndustry = query.RelatedIndustry != "" ? query.RelatedIndustry : "N/A";
                    }
                    else 
                    {
                        usCourseStreamRelatedIndustry.Visible = false;
                    }
                }
                else 
                {
                    usCourseStreamBasicDetails.Visible = false;
                }
            }
                catch(Exception ex)
                {
                    var err=ex.Message;
                    if(ex.InnerException!=null)
                     {

                             err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                     }
                        const string addInfo = "Error in Executing  BindStreamBasicDetails in StreamDescription.aspx :: -> ";
                        var objPub = new ClsExceptionPublisher();
                        objPub.Publish(err, addInfo);
                }

            }
        private void BindPageTitleAndKeyWords(List<CourseStreamProperty> objCourseStreamProperty)
        {

            if (objCourseStreamProperty.Count > 0)
            {
                Page.Title = "";
                Page.Title = objCourseStreamProperty.First().CourseStreamTitle.ToString();

                HtmlMeta metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = objCourseStreamProperty.First().CourseStreamMetaDesc.ToString();

                Page.Header.Controls.Add(metadesc);

                HtmlMeta MetaKeywords = new HtmlMeta();
                MetaKeywords.Name = "keywords";

                MetaKeywords.Content = objCourseStreamProperty.First().CourseStreamMetaTag.ToString();

                Page.Header.Controls.Add(MetaKeywords);

            }
        }
        }
    }
