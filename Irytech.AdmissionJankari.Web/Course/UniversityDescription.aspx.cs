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
    public partial class UniversityDescription : PageBase
    {
        Common _ObjCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            _ObjCommon = new Common();
            ucUniversityRealtedCollege.CourseId = _ObjCommon.CourseId;
            ucUniversityRealtedCollege.UniversityId = Convert.ToInt32(Request.QueryString["UniversityId"]);
             if (IsPostBack) return;
          
             if (Request.QueryString["UniversityId"] != null)
             {
                 BindUniversityBasicDetails(Request.QueryString["UniversityId"]);
                
             }
           
            
        }
        private void BindUniversityBasicDetails(string universityId)
        {
            try
            {
                var basicDetails = UniversityProvider.Instance.GetUniversityListById(Convert.ToInt16(universityId));
                BindPageTitleAndKeyWords(basicDetails);
                
                if (basicDetails.Count > 0)
                {
                    usUniversityBasicDetails.Visible = true;
                    var query = basicDetails.Select(result => new
                        {
                            UniversityName = result.UniversityName,
                            PopularName = result.UniversityPopularName,
                            Establishment = result.UniversityEst,
                            PhoneNo = result.UniversityPhoneNo,
                            MobileNo = result.UniversityMobile,
                            EmailId = result.UniversityEmailId,
                            Website = result.UniversityWebsite,
                            Fax = result.UniversityFax,
                            Address = result.UniversityAddrs,
                            Description = result.UniversityDesc,
                            State=result.UniversityStateName,
                            CategoryName=result.UniversityCategoryName
                        }).First();
                    usUniversityBasicDetails.UniversityName = query.UniversityName != "" ? query.UniversityName : "N/A";
                    
                    usUniversityBasicDetails.PopularName = query.PopularName != "" ? query.PopularName : "N/A";
                    usUniversityBasicDetails.Establishment = Convert.ToString(query.Establishment) != "" ? Convert.ToString(query.Establishment) : "N/A";
                    usUniversityBasicDetails.UniversityCategoryName = query.CategoryName != "" ? query.CategoryName : "N/A";
                    usUniversityDescription.Description = query.Description != "" ? query.Description : "N/A";
                    usUniversityContactDetails.PhoneNo = query.PhoneNo != "" ? query.PhoneNo : "N/A";
                    usUniversityContactDetails.Fax = query.Fax != "" ? query.Fax : "N/A";
                    usUniversityContactDetails.Address = query.Address != "" ? query.Address : "N/A";
                    usUniversityContactDetails.Website = query.Website != "" ? query.Website : "N/A";
                    usUniversityContactDetails.State = query.State != "" ? query.State : "N/A";
                  
                }
                else
                {
                    usUniversityBasicDetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindUniversityBasicDetails in UniversityDescription.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        private void BindPageTitleAndKeyWords(List<UniversityProperty> objUniversityProperty)
        {

            if (objUniversityProperty.Count > 0)
            {
                Page.Title = "";
                Page.Title = objUniversityProperty.First().UniversityTitle.ToString();

                HtmlMeta metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = objUniversityProperty.First().UniversityMetaDesc.ToString();

                Page.Header.Controls.Add(metadesc);

                HtmlMeta MetaKeywords = new HtmlMeta();
                MetaKeywords.Name = "keywords";

                MetaKeywords.Content = objUniversityProperty.First().UniversityMetaTag.ToString();

                Page.Header.Controls.Add(MetaKeywords);

            }
        }
    }
}