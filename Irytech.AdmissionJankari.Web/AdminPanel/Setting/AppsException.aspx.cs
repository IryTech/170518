using System;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Setting
{
    public partial class AppsException : SecurePage 
    {
        Common _objCommon;
        DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {

            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (!IsPostBack)

                BindExceptionList();



        }
        #region Method
        // Method To Bind The Exception List
        protected void BindExceptionList()
        {
            _objCommon = new Common();
            _dt = new DataTable();
            try
            {
                _dt = _objCommon.GetApplicationExceptionList();
                if (_dt.Rows.Count > 0)
                {
                    rptAppsException.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptAppsException, _dt);


                    //rptAppsException.DataSource = _dt;
                    //rptAppsException.DataBind();
                    //lblRecordMsg.Visible = false;
                }
                else
                {
                    lblRecordMsg.Visible = true;
                    lblRecordMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindExceptionList in AppsException.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        #endregion

        #region Event
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = _objCommon.GetApplicationExceptionList();
            if (data.Rows.Count > 0)
            {
                try
                {
                    rptAppsException.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptAppsException, data);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in AppException.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptAppsException.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
     
        #endregion

        protected void rptAppsException_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _objCommon = new Common();
            if (e.CommandName == "Delete")
            {

                int ExceptionId = Convert.ToInt32(e.CommandArgument.ToString());
                if (Convert.ToInt32(ExceptionId) >= 0)
                {
                    int data = _objCommon.DeleteApplicationException(Convert.ToInt32(ExceptionId));
                    if (data > 0)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Selected Application Exception has been Deleted";
                        BindExceptionList();

                    }

                }
            }
        }
    }
}