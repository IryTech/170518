using System;
using System.Data;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ucBookSeatavailability : System.Web.UI.UserControl
    {
        private SecurePage objSecurePage = new SecurePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCollegeList.PageSize = ClsSingelton.PageSize;
            ucCollegeList.ButtonsCount = 10;
            ucCollegeList.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack)return;

        }


        public string DataStatus
        {
            set
            {
                lblResult.Visible = true;
                lblResult.CssClass = "success";
                lblResult.Text = value;
            }
        }

        public string CollegeBranchCourseId
        {
            get { return hdnCollegeCourseId.Value; }
            set { hdnCollegeCourseId.Value = value; }

        }
        public DataSet BindRepesater
        {
            set
            {
                try
                {
                    if (value.Tables.Count > 0)
                    {
                        if (value.Tables[0].Rows.Count > 0)
                        {
                            ViewState["CollegeData"] = value.Tables[0];
                            rptCollegeDetails.Visible = true;
                            ucCollegeList.BindDataWithPaging(rptCollegeDetails, value.Tables[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  BindRepesater property in ucBookSeatavailibity.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
        }

        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var collegeData = ViewState["CollegeData"] as DataTable;
           
                try
                {
                    rptCollegeDetails.Visible = true;

                    ucCollegeList.BindDataWithPaging(rptCollegeDetails, collegeData);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  PagerPageIndexChanged in ucBookSeatavailibity.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            
        }
        


    }
}