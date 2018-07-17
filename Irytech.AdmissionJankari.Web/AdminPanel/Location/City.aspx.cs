using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Location
{
    public partial class City : SecurePage 
    {
        Common _objCommon;
        ClsOleDBDataWrapper _objClsOledbdatalayer;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
             BindAllZoneList();
            BindAllStateList();
           
            BindAllCityList();
            BindAllCountryList();
            ValidationErrorMessages();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/CitySheet.xlsx");
            if (path == null) throw new ArgumentNullException("File not Found");
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

                                int _insert = CityProvider.Instacnce.InsertCityDetails(Convert.ToString(_ds.Tables[0].Rows[j]["CityName"]),
                                                                                       Convert.ToInt32(_ds.Tables[0].Rows[j]["StateId"].ToString()), LoggedInUserId, out errMsg);

                            }
                            lblMsg.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblMsg.Visible = true;
                            BindAllCityList();
                        }
                    }                  
                }
                else
                {
                    lblwarningMsg.Text = _objCommon.GetErrorMessage("lblErrMsg");
                    lblMsg.Visible = false;
                }
            }
            catch (Exception ex)
            { }
        }

        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvCityName.ErrorMessage = _objCommon.GetValidationMessage("rfvCityName");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
            revUploadExcel.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revUploadExcel.ErrorMessage = _objCommon.GetErrorMessage("revUploadExcel");

        }
        protected void BindAllCityList()
        {
            try
            {
                var cityLIst = CityProvider.Instacnce.GetAllCityList();
                if (cityLIst.Count > 0)
                {
               
                    rptCity.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCity, Common.ConvertToDataTable(cityLIst));

                    ddlCityList.DataSource = cityLIst;
                    ddlCityList.DataTextField = "CityName";
                    ddlCityList.DataValueField = "CityId";
                    ddlCityList.DataBind();
                    ddlCityList.Items.Insert(0, new ListItem("--Select City--", "0"));
                }
                else
                {
                    ddlCityList.Items.Insert(0, new ListItem("--Select City--", "0"));
                    rptCity.Visible = false;
                    lblRecordMsg.Visible = true;
                    lblRecordMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllCityList in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
        protected void BindAllCountryList()
        {
            try
            {
                var countryList = CountryProvider.Instance.GetAllCountry();
                if (countryList.Count > 0)
                {
                    ddlCountryList.DataSource = countryList;
                    ddlCountryList.DataTextField = "CountryName";
                    ddlCountryList.DataValueField = "CountryId";
                    ddlCountryList.DataBind();
                    ddlCountryList.Items.Insert(0, new ListItem("--Select Country--", "0"));
                }
                else
                {
                    ddlCountryList.Items.Insert(0, new ListItem("--Select Country--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllCountryList in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void BindAllStateList()
        {
            try
            {
                var stateList = StateProvider.Instance.GetAllState();
                if (stateList.Count > 0)
                {
                    ddlStateName.DataSource = stateList;
                    ddlStateName.DataTextField = "StateName";
                    ddlStateName.DataValueField = "StateId";
                    ddlStateName.DataBind();
                    ddlStateName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlStateName.Items.Insert(0, new ListItem("--Select--", "0"));
                   
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllStateList() in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void BindAllZoneList()
        {
            try
            {
                var zoneList = ZoneProvider.Instance.GetAllZoneList();
                if (zoneList.Count > 0)
                {
                    ddlZoneList.DataSource = zoneList;
                    ddlZoneList.DataTextField = "ZoneName";
                    ddlZoneList.DataValueField = "ZoneId";
                    ddlZoneList.DataBind();
                    ddlZoneList.Items.Insert(0, new ListItem("--Select Zone--", "0"));
                }
                else
                {
                    ddlZoneList.Items.Insert(0, new ListItem("--Select Zone--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllZoneList() in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        
        protected void InsertCityList()
        {
            lblMsg.Text = string.Empty;
            try
            {
                var erorrMsg = "";
                var insert = CityProvider.Instacnce.InsertCityDetails(Convert.ToString(txtCityName.Text.Trim()), Convert.ToInt32(ddlStateName.SelectedValue),
                                                                        LoggedInUserId, out erorrMsg);
                if (insert > 0)
                {
                   
                    BindAllCityList();
                    ClearFields();
                    lblMsg.Visible = true;
                    lblMsg.Text = erorrMsg;
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
                const string addInfo = "Error while executing InsertCityList in City.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void UpdateCityList()
        {
            lblMsg.Text = string.Empty;
            try
            {
                string erorrMsg;
                var update = CityProvider.Instacnce.UpdateCityDetails(Convert.ToInt32(ViewState["CityId"].ToString()), Convert.ToString(txtCityName.Text.Trim()),
                                                                       Convert.ToInt32(ddlStateName.SelectedValue), LoggedInUserId, out erorrMsg);
               
                if (update > 0)
                {
                   
                    BindAllCityList();
                    ClearFields();
                    btnCity.Text ="Add";
                    lblMsg.Visible = true;
                    lblMsg.Text = erorrMsg;
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
                const string addInfo = "Error while executing UpdateCityList in City.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void ClearFields()
        {
            txtCityName.Text = string.Empty;
            ddlStateName.ClearSelection();
        }
        protected void btnCity_Click(object sender, EventArgs e)
        {
            if (btnCity.Text == "Add")
            {
                InsertCityList();
            }
            else
            {
                UpdateCityList();
            }
        }

        protected void rptCity_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                lblMsg.Visible = false;
                var cityid = Convert.ToInt32(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Edit":
                        var cityList = CityProvider.Instacnce.GetCityById(cityid);
                        if (cityList.Count > 0)
                        {

                            ViewState["CityId"] = cityid;
                            ddlStateName.ClearSelection();
                            var query = from result in cityList
                                        select new
                                                   {
                                                       result.StateName, result.StateId, result.CityName
                                                   };
                            var sp = query.First();
                            txtCityName.Text = sp.CityName;
                            ddlStateName.Items.FindByValue(sp.StateId.ToString()).Selected = true;
                             btnCity.Text = "Update";
                             ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divCourseCategoryInsert','650px','sndAddCity');", true);
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
                const string addInfo = "Error while executing rptCity_ItemCommand in City.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

   protected void ddlCountryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objCommon = new Common();
                ddlZoneList.ClearSelection();
                ddlCityList.ClearSelection();
                var countryLIst = CityProvider.Instacnce.GetCityListByCountry(Convert.ToInt32(ddlCountryList.SelectedValue));
                if (countryLIst.Count > 0)
                {
                    rptCity.Visible = true;
                    lblRecordMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCity, Common.ConvertToDataTable(countryLIst));
                    lblMsg.Visible = false;
                }
                else
                {
                    lblMsg.Visible = false;
                    rptCity.Visible = false;
                    lblRecordMsg.Visible = true;
                    lblRecordMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ddlCountryList_SelectedIndexChanged in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void ddlZoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objCommon = new Common();
                ddlCityList.ClearSelection();
                ddlCountryList.ClearSelection();
                var cityLIst = CityProvider.Instacnce.GetCityListByZone(Convert.ToInt32(ddlZoneList.SelectedValue));
                if (cityLIst.Count > 0)
                {
                    
                    rptCity.Visible = true;
                    lblRecordMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCity, Common.ConvertToDataTable(cityLIst));
                    lblMsg.Visible = false;
                }
                else
                {
                    lblMsg.Visible = false;
                    rptCity.Visible = false;
                    lblRecordMsg.Visible = true;
                    lblRecordMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ddlZoneList_SelectedIndexChanged in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var cityLIst = CityProvider.Instacnce.GetAllCityList();
            //var data = _objCommon.GetApplicationExceptionList();
            if (cityLIst.Count > 0)
            {
                try
                {
                    rptCity.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCity, Common.ConvertToDataTable(cityLIst));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in City.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCity.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }



        protected void ddlCityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objCommon = new Common();
                ddlCountryList.ClearSelection();
                ddlZoneList.ClearSelection();
                var cityLIst = CityProvider.Instacnce.GetCityById(Convert.ToInt32(ddlCityList.SelectedValue));
                if (cityLIst.Count > 0)
                {
                    rptCity.Visible = true;
                    lblRecordMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCity, Common.ConvertToDataTable(cityLIst));
                    lblMsg.Visible = false;
                }
                else
                {
                    lblMsg.Visible = false;
                    rptCity.Visible = false;
                    lblRecordMsg.Visible = true;
                    lblRecordMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing btnSearch_Click in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


    }
}