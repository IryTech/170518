using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentUpdateStreamPrioty : System.Web.UI.UserControl
    {
        protected string SPonserValue;
        SecurePage objSecurePage = new SecurePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserIntertestedList(objSecurePage.LoggedInUserId);
        }
        //protected void rptCollegeDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    var hndCollegeBranchCourseId = (HiddenField)(e.Item.FindControl("hndCollegeBranchCourseId"));
        //    var rptStream = (Repeater)(e.Item.FindControl("rptCourse"));
        //    if (rptStream != null)
        //    {
              
        //        var ObjConsulling= new Consulling();
        //        var streamData = ObjConsulling.GetStudentIntertestedStreamList(Convert.ToInt32(hndCollegeBranchCourseId.Value), objSecurePage.LoggedInUserId);
        //        if (streamData.Rows.Count> 0)
        //        {
        //            rptStream.DataSource = streamData;
        //            rptStream.DataBind();
        //        }
        //    }
        //}
        public string Sponser
        {
            get
            {
                return SPonserValue;

            }
            set { SPonserValue = value; }
        }
        // Method To Bind User Final Intertested List
        protected void BindUserIntertestedList(int userId)
        {
            Consulling ObjConsulling = new Consulling();
            DataSet ds = new DataSet();
            try
            {

                ds = ObjConsulling.GetIntertestedCollege(userId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hndCollegeList.Value = ds.Tables[0].Rows.Count.ToString();
                    rptCollegeDetails.DataSource = ds.Tables[0];
                    rptCollegeDetails.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}