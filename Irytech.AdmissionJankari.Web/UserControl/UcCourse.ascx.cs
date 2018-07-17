using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCourse : System.Web.UI.UserControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindCourse();
        }

        #region property
        public int StudentCourse
        {
            get
            {
                return !string.IsNullOrEmpty(Convert.ToString(ddlCourse.SelectedValue)) &&
                       ddlCourse.SelectedItem.ToString() != "Other"
                           ? Convert.ToInt16(ddlCourse.SelectedValue)
                           : new Common().CourseId;
            }
        }
        public string StudentCourseEligibilty
        {
            get
            {
                return !string.IsNullOrEmpty(Convert.ToString(chkelgibilty.SelectedValue))
                           ? Convert.ToString(chkelgibilty.SelectedValue)
                           : "";
            }
        }

        public bool CourseVisibility
        {
            set
            {
                ddlCourse.Enabled = value;
            }
        }
        #endregion 

        #region method
        //..........method to bind course.........
        protected void BindCourse()
        {
              var objCommon=new Common();
              var data = objCommon.GetCourseListForOnlineConsulling();
            try
            {

                if (data.Rows.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataValueField = "AjCourseId";
                    ddlCourse.DataTextField = "AjOnlineConsullingCourseText";
                    ddlCourse.DataBind();
                    ddlCourse.Items.Insert(0, "Select");
                    ddlCourse.Items.Insert((data.Rows.Count+1), "Other");
                    ddlCourse.Items.FindByValue(objCommon.CourseId.ToString()).Selected = true;
                }
                else
                {
                    ddlCourse.Items.Insert(0, "Select");
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in UcCourse.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
      /*......course indexchange......*/
        protected void DdlCourseSelectedIndexChanged(object sender, EventArgs e)
        {
            var objCommon=new Common();
            if (ddlCourse.SelectedValue != "Select")
            {
                if (ddlCourse.SelectedItem.ToString() != "Other")
                {
                    lblOtherCourse.Visible = false;
                    rfvCourse.Enabled = true;
                    objCommon.CourseId = Convert.ToInt16(ddlCourse.SelectedValue);
                    objCommon.CourseName = Utils.RemoveIllegealFromCourse(ddlCourse.SelectedItem.ToString());
                    CheckBoxRequired.Enabled = false;
                   
                }
                else
                {
                    lblOtherCourse.Visible = true;
                    rfvCourse.Enabled = false;
                    CheckBoxRequired.Enabled = true;
                    rfvOtherCourse.Enabled = true;
                   
                }
            }
        }
        protected void CheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (!string.IsNullOrEmpty(chkelgibilty.SelectedValue))
            {
                e.IsValid = true;
            }
        }
        #endregion
    }
}