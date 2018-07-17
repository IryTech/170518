using System;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Message
{
    public partial class ImagesPath : SecurePage
    {
        Common _objCommon;
        DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {


            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
                BindFilePath();
                ValidateControl();
        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            _dt = _objCommon.GetXmlMessage("FilePath");
            if (_dt.Rows.Count > 0)
            {
                try
                {
                    rptFilePath.Visible = true;
                    lblMsg.Visible = false;
                    DataTable dtb = new DataTable();
                    dtb.Columns.Add("SrNo");
                    dtb.Columns.Add("PathId");
                    dtb.Columns.Add("lgpath");
                    dtb.Columns.Add("description");
                    dtb.Columns.Add("FilePathId_Text");
                    dtb.Merge(_dt);
                    int i = 1;
                    foreach (DataRow drow in dtb.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }
                    ucCustomPaging.BindDataWithPaging(rptFilePath, dtb);
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
                rptFilePath.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }

        #region Method
        protected void BindFilePath()
        {
            _objCommon = new Common();
            _dt = new DataTable();
            try
            {
                _dt = _objCommon.GetXmlMessage("FilePath");
                if (_dt.Rows.Count > 0)
                {
                    DataTable dtb = new DataTable();
                    dtb.Columns.Add("SrNo");
                    dtb.Columns.Add("PathId");
                    dtb.Columns.Add("lgpath");
                    dtb.Columns.Add("description");
                    dtb.Columns.Add("FilePathId_Text");
                    dtb.Merge(_dt);
                    int i = 1;
                    foreach (DataRow drow in dtb.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }
                    ucCustomPaging.BindDataWithPaging(rptFilePath, dtb);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindValidationMessage in ValidationMessage.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        // method to Validate the Control
        protected void ValidateControl()
        {
            _objCommon = new Common();
            rfvFilePath.ErrorMessage = _objCommon.GetValidationMessage("rfvCommon");
            rfvFilePathId.ErrorMessage = _objCommon.GetValidationMessage("rfvCommon");
        }
        #endregion

        #region Event
        protected void UpdateFilePath(object sender, EventArgs e)
        {
            _objCommon = new Common();
            int i = _objCommon.UpdateMessage(txtFilePathId.Text, txtFilePath.Text, "FilePath");
            if (i > 0)
            {
                lblMsg.Text = "You successfully Updated the details";
                lblMsg.Visible = true;
                updateFilePathMessage.Visible = false;
                BindFilePath();
                txtFilePathId.Text = "";
                txtFilePath.Text = "";
            }

        }

        protected void rptFilePathItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _objCommon = new Common();
            switch (e.CommandName)
            {
                case "Edit":
                    {
                        updateFilePathMessage.Visible = true;
                        updateFilePathMessage.Focus();
                        lblMsg.Visible = false;
                        txtFilePathId.Text = e.CommandArgument.ToString();
                        txtFilePath.Text = _objCommon.GetFilepath(txtFilePathId.Text);
                        break;
                    }
            }
        }
        #endregion

        
    }
}