using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BO;
 

namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class State : SecurePage 
    {
        Common _objCommon;
        ClsOleDBDataWrapper _objClsOledbdatalayer;
        StateProperty _objStateProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ApplicationSettings.Instance.ExamPageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.ExamPageCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
                BindAllState();
                BindAllZoneList();
                BindAllCountry();
            ValidateFields();      
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var stateList = StateProvider.Instance.GetAllState();
            if (stateList.Count > 0)
            {
                try
                {
                    rptState.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptState, Common.ConvertToDataTable(stateList));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in State.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptState.Visible = false;

            }

        }


        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvConutryName.ErrorMessage = _objCommon.GetValidationMessage("rfvConutryName");
            rfvStateNameEnter.ErrorMessage = _objCommon.GetValidationMessage("rfvStateNameEnter");
            rfvZoneName.ErrorMessage = _objCommon.GetValidationMessage("rfvZoneName");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/StateSheet.xlsx");
            if (path == null) throw new ArgumentNullException("File bit Found");
            var objDocProcess = new System.Diagnostics.Process
                                    {
                                        EnableRaisingEvents = false,
                                        StartInfo = {FileName = @path}
                                    };
            objDocProcess.Start();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var path = MapPath(fileUploadExcel.FileName);
                fileUploadExcel.SaveAs(path);
                var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (string t in excelSheets)
                    {
                        DataSet _ds = objClsOledbdatalayer.getdata(path, t);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            for (var j = 0; j <= _ds.Tables[0].Rows.Count - 1; j++)
                            {
                                var errMsg = "";
                                _objStateProperty = new StateProperty
                                {
                                    CountryId = Convert.ToInt16(_ds.Tables[0].Rows[j]["CountryId"]),
                                    ZoneId = Convert.ToInt16(_ds.Tables[0].Rows[j]["ZoneId"]),
                                    StateName = _ds.Tables[0].Rows[j]["StateName"].ToString()

                                };
                                int _insert = StateProvider.Instance.InsertStateDetails(Convert.ToString(_ds.Tables[0].Rows[j]["StateName"]),
                                                                                        Convert.ToInt32(_ds.Tables[0].Rows[j]["ZoneId"]),
                                                                                        Convert.ToInt32(_ds.Tables[0].Rows[j]["CountryId"]), LoggedInUserId, out errMsg);

                            }
                            lblMsg.Focus();
                            lblMsg.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblMsg.Visible = true;
                            BindAllState();
                        }
                    }
                    

                }

                else
                {
                    lblErrorMsg.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            {
                
            };
        }
        protected void BindAllZoneList()
        {
            try
            {
                var zoneList = ZoneProvider.Instance.GetAllZoneList();
                if (zoneList.Count > 0)
                {
                    ddlZoneName.DataSource = zoneList;
                    ddlZoneName.DataTextField = "ZoneName";
                    ddlZoneName.DataValueField = "ZoneId";
                    ddlZoneName.DataBind();
                    ddlZoneName.Items.Insert(0, new ListItem("--Select--","0"));

                    ddlSearchZoneName.DataSource = zoneList;
                    ddlSearchZoneName.DataTextField = "ZoneName";
                    ddlSearchZoneName.DataValueField = "ZoneId";
                    ddlSearchZoneName.DataBind();
                    ddlSearchZoneName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlZoneName.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlSearchZoneName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllZoneList() in state.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void BindAllState()
        {
            try
            {
                var stateList= StateProvider.Instance.GetAllState();
                if(stateList.Count>0)
                {
                    rptState.Visible = true;

                    ucCustomPaging.BindDataWithPaging(rptState, Common.ConvertToDataTable(stateList));
                   
                    ddlSearchStateName.DataSource = stateList;
                    ddlSearchStateName.DataTextField = "StateName";
                    ddlSearchStateName.DataValueField = "StateId";
                    ddlSearchStateName.DataBind();
                    ddlSearchStateName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    rptState.Visible = false;
                    ddlSearchStateName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllState() in State.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void BindAllCountry()
        {
            try
            {
                var countryList = CountryProvider.Instance.GetAllCountry();
                if (countryList.Count > 0)
                {
                    ddlCountryName.DataSource = countryList;
                    ddlCountryName.DataTextField = "CountryName";
                    ddlCountryName.DataValueField = "CountryId";
                    ddlCountryName.DataBind();
                    ddlCountryName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlCountryName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllCountry() in State.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
    
        protected void InsertState()
        {
            try
            {
                string erorrMsg;
                var insert = StateProvider.Instance.InsertStateDetails(Convert.ToString(txtStateName.Text), Convert.ToInt32(ddlZoneName.SelectedValue),
                                                                       Convert.ToInt32(ddlCountryName.SelectedValue), LoggedInUserId, out erorrMsg);
                if (insert > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = erorrMsg;
                    BindAllState();
                    ClearFields();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = erorrMsg;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing InsertState in State.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void UpdateState()
        {
            try
            {
                var erorrMsg = "";
                var update = StateProvider.Instance.UpdateStateDetails(
                    Convert.ToInt32(ViewState["StateId"].ToString()), Convert.ToString(txtStateName.Text),
                    Convert.ToInt32(ddlZoneName.SelectedValue.ToString()),
                    Convert.ToInt32(ddlCountryName.SelectedValue.ToString()), LoggedInUserId, out erorrMsg);
                if (update > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = erorrMsg;
                    btnState.Text = "Add";
                    BindAllState();
                    ClearFields();
                   }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = erorrMsg;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing UpdateState in State.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void ClearFields()
        {
            txtStateName.Text = string.Empty;
            ddlCountryName.ClearSelection();
            ddlZoneName.ClearSelection();
        }
        protected void btnState_Click(object sender, EventArgs e)
        {
            if (btnState.Text == "Add")
            {
                InsertState();
            }
            else
            {
                UpdateState();
                
            }
        }
        protected void rptState_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                lblMsg.Visible = false;
                var stateId = Convert.ToInt32(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Edit":
                        var stateList = StateProvider.Instance.GetStateByStateId(stateId);
                        if (stateList.Count > 0)
                        {
                            ViewState["StateId"] = stateId;
                            ddlCountryName.ClearSelection();
                            ddlZoneName.ClearSelection();
                            var query = from result in stateList
                                        select new
                                                   {
                                                       result.StateName,
                                                       result.CountryId,
                                                       result.ZoneId,
                                                       result.ZoneName
                                                   };
                            var sp = query.First();
                            txtStateName.Text = sp.StateName;
                            ddlCountryName.Items.FindByValue(sp.CountryId.ToString(CultureInfo.InvariantCulture))
                                          .Selected = true;
                            ddlZoneName.Items.FindByValue(sp.ZoneId.ToString(CultureInfo.InvariantCulture)).Selected =
                                true;
                            btnState.Text = "Update";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeesdswqe", "OpenPoup('divCourseCategoryInsert','650px','sndAddState');", true);

                        }
                        break;
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing rptState_ItemCommand in State.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
   
        protected void ddlSearchStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objCommon = new Common();
                ddlSearchZoneName.ClearSelection();
                var state = StateProvider.Instance.GetStateByStateId(Convert.ToInt32(ddlSearchStateName.SelectedValue));
                if (state.Count > 0)
                {
                    rptState.Visible = true;
                    lblwarning.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptState, Common.ConvertToDataTable(state));
                }
                else
                {
                    rptState.Visible = false;
                    lblwarning.Visible = true;
                    lblwarning.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ddlSearchStateName_SelectedIndexChanged in State.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void ddlSearchZoneName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objCommon = new Common();
                ddlSearchStateName.ClearSelection();
                var zone = StateProvider.Instance.GetStateByZone(Convert.ToInt32(ddlSearchZoneName.SelectedValue));
                if (zone.Count > 0)
                {
                    rptState.Visible = true;
                    lblwarning.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptState, Common.ConvertToDataTable(zone));
                }
                else
                {
                    rptState.Visible = false;
                    lblwarning.Visible = true;
                    lblwarning.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ddlSearchStateName_SelectedIndexChanged in State.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        
    }
}