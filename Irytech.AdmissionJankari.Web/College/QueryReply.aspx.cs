using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.College
{
    public partial class QueryReply : PageBase
    {
        readonly Common _objCommon = new Common();
        readonly SecurePage _objSecurePage = new SecurePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["QueryId"] == null || Request["SourceId"] == null) return;
            BindQuery(Convert.ToInt32(Request.QueryString["QueryId"]));
            BindQueryReply(Convert.ToInt32(Request.QueryString["QueryId"]));
        }


        // Method to Bind The Query
        private void BindQueryReply(int queryId)
        {
            var data = _objCommon.GetQUeryReply(queryId, true);
            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    rptCollegeQueryReply.DataSource = data;
                    rptCollegeQueryReply.DataBind();
                }

            }
        }

        // Method to Bind The Query
        private void BindQuery(int queryId)
        {
            var data = _objCommon.GetQueryDetails(queryId);
            if (data.Rows.Count > 0)
            {
                lblQuery.Text = Convert.ToString(data.Rows[0]["AjStudentQueryText"]);
                lblQueryBy.Text = string.Format("{0} | {1}", Common.GetStringProperCase(Convert.ToString(data.Rows[0]["AjUserFullName"])), Convert.ToDateTime(Convert.ToString(data.Rows[0]["CreatedOn"])).ToString("MMM-dd-yyyy"));
            }
        }

        protected void btnReply_Click(object sender, EventArgs e)
        {
            if (! new SecurePage().IsLoggedInUSer)
            {
                string url = HttpContext.Current.Request.RawUrl;
                url = url.Substring(1, url.Length - 1);
                Response.Redirect(IryTech.AdmissionJankari.Components.Utils.AbsoluteWebRoot + "account/login?ReturnUrl=" +url);
            }
            else
            {
                var result = _objCommon.InsertUserQueryReply(_objSecurePage.LoggedInUserId, Convert.ToInt32(Request.QueryString["QueryId"]),txtQueryReply.Text.Trim());
                divResultModerate.Visible = true;
                var msg = result > 0 ? Resources.label.ThankYouQueryReply : "Something wrong while inserting your reply";
                SetStatus(result > 0 ? "resultSuccess" : "resultError", msg, divResultModerate);
                txtQueryReply.Text = string.Empty;

            }
        }
        // Method to Bind The College Query
        protected void BindCollegeQuery()
        {
            try
            {
                var objCommon = new Common();
                var data =
                    objCommon.GetCollegeQuery(Convert.ToInt32(Request["SourceId"]), "T");
                data = data.Rows.Count > 1 ? data.AsEnumerable()
                                .Where(x => x.Field<int>("AjStudentQueryId") != Convert.ToInt32(Request["QueryId"]))
                                .CopyToDataTable() : null;
                if (data != null && data.Rows.Count > 0)
                {

                    divResultModerate.Visible = false;
                    divNoResult.Visible = false;
                    rptCollegeQuery.DataSource = data;
                    rptCollegeQuery.DataBind();
                    rptCollegeQuery.Visible = true;
                    CollegeHeader.Visible = true;

                }
                else
                {
                    divNoResult.Visible = true;
                    divResultModerate.Visible = false;
                    SetStatus("resultSuccess", "Sorry no other query exists,related to this college",divNoResult);
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
                const string addInfo = "Error while executing BindCollegeQuery in QueryReply.aspx.cs :: -> ";
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

        protected void lnkViewMore_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["QueryId"] == null || Request["SourceId"] == null) return;
            BindCollegeQuery();
        }
        public void SetStatus(string status, string msg, HtmlGenericControl objcontrol)
        {

            objcontrol.Attributes.Clear();
            objcontrol.Attributes.Add("class", status);
            objcontrol.Visible = true;
            objcontrol.InnerHtml =
                string.Format(
                    "{0}<a href=\"javascript:HideMessageStatus()\" style=\"width:20px;float:right\">X</a>",
                    Server.HtmlEncode(msg));
        }
    }
}