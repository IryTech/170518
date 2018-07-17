using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{
    public partial class CourseStreamMaster : SecurePage
    {
        Common _objCommon;

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindStreamMaster();
            BindGetAllCourseList();

        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var data = StreamProvider.Instance.GetAllStreamList();
            if (data.Count > 0)
            {
                try
                {
                    rptCourseStreamData.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptCourseStreamData, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CourseStreamMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCourseStreamData.Visible = false;

            }

        }


        //Bind Methord For Dropdown 
        protected void BindGetAllCourseList()
        {
            var data = CourseProvider.Instance.GetAllCourseList();

            if (data.Count > 0)
            {
                ddlCourseId.DataSource = data;
                ddlCourseId.DataTextField = "CourseName";
                ddlCourseId.DataValueField = "CourseId";
                ddlCourseId.DataBind();
                ddlCourseId.Items.Insert(0, new ListItem("Please Select","0"));

            }
            else
            {
                ddlCourseId.Items.Insert(0, new ListItem("Please Select","0"));

            }
        }
        protected void BindStreamMaster()
        {
            _objCommon = new Common();
            var data = StreamProvider.Instance.GetAllStreamList();
            if (data.Count > 0)
            {

                ucCustomPaging.BindDataWithPaging(rptCourseStreamData, Common.ConvertToDataTable(data));
               
            }
            else
            {
                rptCourseStreamData.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }

        //Bind Methord For Search Through Stream Name
        protected void BindGetStreamListByStreamName()
        {
            _objCommon = new Common();
            var data = StreamProvider.Instance.GetStreamListByStreamName(txtStreamName.Text.Trim());
            if (data.Count > 0)
            {

                ucCustomPaging.BindDataWithPaging(rptCourseStreamData, Common.ConvertToDataTable(data));
               
            }
            else
            {
                rptCourseStreamData.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
        //Bind Methord For Search Through Course
        protected void BindGetStreamListByCourse()
        {
            _objCommon = new Common();
            var data = StreamProvider.Instance.GetStreamListByCourse(Convert.ToInt16(ddlCourseId.SelectedValue));
            if (data.Count > 0)
            {

                ucCustomPaging.BindDataWithPaging(rptCourseStreamData, Common.ConvertToDataTable(data));
              
            }
            else
            {
                rptCourseStreamData.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        //Bind Methord For Search Through Stream Name & Course Both
        protected void BindGetStreamListByStreamNameCourseId()
        {
            _objCommon = new Common();
            var data = StreamProvider.Instance.GetStreamListByStreamNameCourseId(Convert.ToInt16(ddlCourseId.SelectedValue), txtStreamName.Text.Trim());
            if (data.Count > 0)
            {

                ucCustomPaging.BindDataWithPaging(rptCourseStreamData, Common.ConvertToDataTable(data));
              
            
            }
            else
            {
                rptCourseStreamData.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }

        protected void BtnSreachClick(object sender, EventArgs e)
        {
            if ((ddlCourseId.SelectedIndex > 0) && string.IsNullOrEmpty(txtStreamName.Text))
            {
                BindGetStreamListByCourse();

            }
            else if (ddlCourseId.SelectedIndex <=0 && !string.IsNullOrEmpty(txtStreamName.Text))
            {
                BindGetStreamListByStreamName();
            }
            else if ((ddlCourseId.SelectedIndex > 0) && !string.IsNullOrEmpty(txtStreamName.Text))
            {
                BindGetStreamListByStreamNameCourseId();
            }
            else
            {
                BindStreamMaster();

            }
        }

    }

}
