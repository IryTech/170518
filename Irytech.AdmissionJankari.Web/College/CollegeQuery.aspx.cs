using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.College
{
    public partial class CollegeQuery : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCollegeQuery();
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var objCommon = new Common();
                var data = objCommon.GetCollegeQuery(Convert.ToInt32(Request["CollegeBranchCourseId"]), "");
                hndCourseId.Value = objCommon.CourseId.ToString();

                if (data != null)
                {
                    if (data.Rows.Count > 0)
                    {
                        ucCustomPaging.Visible = true;
                        rptCollegeQuery.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptCollegeQuery, data);
                        CollegeHeader.Visible = true;
                    }
                    else
                    {
                        rptCollegeQuery.Visible = false;
                        CollegeHeader.Visible = false;

                    }
                }
                else
                {
                    rptCollegeQuery.Visible = false;
                    CollegeHeader.Visible = false;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeQuery in UcCollegeQuery.ascx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        // Method to Bind The College Query
        protected void BindCollegeQuery()
        {
            try
            {
                var objCommon = new Common();
                var data = objCommon.GetCollegeQuery(Convert.ToInt32(Request["CollegeBranchCourseId"]), "");
                hndCourseId.Value = objCommon.CourseId.ToString();
                System.Data.DataView dView = new System.Data.DataView(data);
                if (dView != null)
                {
                    if (dView.Table.Rows.Count > 0)
                    {
                        ucCustomPaging.Visible = true;
                        rptCollegeQuery.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptCollegeQuery, dView.Table);
                        CollegeHeader.Visible = true;
                    }
                    else
                    {
                        rptCollegeQuery.Visible = false;
                        CollegeHeader.Visible = false;

                    }
                }
                else
                {
                    rptCollegeQuery.Visible = false;
                    CollegeHeader.Visible = false;
                }
                var basicDetails =
                    CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(
                        Convert.ToInt32(Request["CollegeBranchCourseId"]));

                if (basicDetails.Count > 0)
                {
                    var query = basicDetails.Select(result => new
                        {
                            result.CollegeBranchName,
                        });
                    var @default = query.FirstOrDefault();
                    if (@default != null) spnCollege.InnerHtml = @default.CollegeBranchName;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeQuery in UcCollegeQuery.ascx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
       
        protected void rptCollegeQuery_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var objCommon = new Common();
                var lit = (Literal)e.Item.FindControl("ltrQueryId");
                var rptAnswer = (Repeater)(e.Item.FindControl("rptCollegeQueryReply"));

                if (rptAnswer != null)
                {
                    var data = objCommon.GetQUeryReply(Convert.ToInt32(lit.Text), true);
                    if (data != null)
                    {
                        if (data.Rows.Count > 0)
                        {
                            rptAnswer.DataSource = data;
                            rptAnswer.DataBind();
                        }

                    }
                }

            }
        }
    }
}