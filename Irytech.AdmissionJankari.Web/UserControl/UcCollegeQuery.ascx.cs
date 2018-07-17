using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCollegeQuery : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack) return;

            BindCollegeQuery();
        }

        public int CollegeBranchCourseId
        {
            get;
            set;
        }
        public int CollegeQueryCount
        {
            get;
            set;
        }
        public string CollegeBranchName
        {
            get;
            set;
        }
        // Method to Bind The College Query
        protected void BindCollegeQuery()
        {
            try
            {
                var objCommon = new Common();
                var data = objCommon.GetCollegeQuery(CollegeBranchCourseId, "T");
                DataView dView = new DataView(data);  
                        //.Where(result => !string.IsNullOrEmpty(Convert.ToString(result["AjQueryStatus"])) ? Convert.ToBoolean(result["AjQueryStatus"]) : false == true).cas;
                hndCourseId.Value = objCommon.CourseId.ToString();

                if (dView != null)
                {
                    if (dView.Table.Rows.Count > 0)
                    {
                        rptCollegeQuery.DataSource = dView;
                        rptCollegeQuery.DataBind();
                        rptCollegeQuery.Visible = true;
                        CollegeHeader.Visible = true;
                        lnkViewQuery.Visible = dView.Table.Rows.Count > 2 ? true : false;
                        lnkViewQuery.HRef =
                            (Utils.AbsoluteWebRoot + "college-query/" +
                             Utils.RemoveIllegealFromCourse(Convert.ToString(CourseName)) + "/" +
                             Utils.RemoveIllegalCharacters(Convert.ToString(CollegeBranchName))).ToLower();
                        
                        lnkViewQuery.Title = CollegeBranchName;
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
        public int CollegeBranchId
        {
            get { return Convert.ToInt32(ViewState["CollegeBranchId"]); }
            set { ViewState["CollegeBranchId"] = value; }
        }
        public int CousreId
        {
            get { return Convert.ToInt32(ViewState["CousreId"]); }
            set { ViewState["CousreId"] = value; }
        }
        public string CourseName
        {
            get { return Convert.ToString(ViewState["CourseName"]); }
            set { ViewState["CourseName"] = value; }
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