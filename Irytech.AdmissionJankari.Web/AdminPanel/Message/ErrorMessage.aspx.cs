using System;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Message
{
    public partial class ErrorMessage : SecurePage 
    {
        Common _objCommon;
        DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindErrorMessage();
            ValidateControl();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            _dt = _objCommon.GetXmlMessage("Error");
            if (_dt.Rows.Count > 0)
            {
                try
                {
                    rptErrorMessage.Visible = true;
                    lblMsg.Visible = false;
                    DataTable dtb = new DataTable();
                    dtb.Columns.Add("SrNo");
                    dtb.Columns.Add("MessageID");
                    dtb.Columns.Add("description");
                    dtb.Columns.Add("message_Text");
                    dtb.Merge(_dt);
                    int i = 1;
                    foreach (DataRow drow in dtb.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }
                    ucCustomPaging.BindDataWithPaging(rptErrorMessage, dtb);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ValidationMessage.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptErrorMessage.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }

        
        #region Method
        // Method to Bind the Error Message 
        protected void BindErrorMessage()
        {
            _objCommon = new Common();
            _dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                _dt = _objCommon.GetXmlMessage("Error");
                if (_dt.Rows.Count > 0)
                {
                    DataTable dtb = new DataTable();
                    dtb.Columns.Add("SrNo");
                    dtb.Columns.Add("MessageID");
                    dtb.Columns.Add("description");
                    dtb.Columns.Add("message_Text");                                       
                    dtb.Merge(_dt);
                    int i = 1;
                    foreach (DataRow drow in dtb.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }
                    ucCustomPaging.BindDataWithPaging(rptErrorMessage, dtb);


                    //rptErrorMessage.DataSource = _dt;
                    //rptErrorMessage.DataBind();
                }
            }

            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindErrorMessage in ErrorMessage.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        // Method to Validate the control
        protected void ValidateControl()
        {
            _objCommon = new Common();
            rfvErrorMessage.ErrorMessage = _objCommon.GetValidationMessage("rfvCommon");
            rfvErrorMessageId.ErrorMessage = _objCommon.GetValidationMessage("rfvCommon");

        }



        #endregion

        #region Event

          protected void rptErrorMessageItemCommand(object source, RepeaterCommandEventArgs e)
          {
              _objCommon=new Common();
            switch (e.CommandName)
            {


                case "Edit":
                    {
                        updateErrorMessage.Visible = true;
                        lblMsg.Visible = false;
                        txtErrorMessageId.Text = e.CommandArgument.ToString();
                        txtErrorMessageValue.Text = _objCommon.GetErrorMessage(txtErrorMessageId.Text);
                        break;
                    }
            }
          }

        protected void UpdateErrorMessage(object sender, EventArgs e)
        {
            _objCommon = new Common();
            int i = _objCommon.UpdateMessage(txtErrorMessageId.Text, txtErrorMessageValue.Text, "Error");
            if (i > 0)
            {
                lblMsg.Text = "You successfully Updated the details";
                lblMsg.Visible = true;
                updateErrorMessage.Visible = false;
                BindErrorMessage();
                txtErrorMessageId.Text = "";
                txtErrorMessageValue.Text = "";
            }


        }
        #endregion

      


    }
}