using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class Zone : SecurePage 
    {
        Common _objCommon;
        ClsOleDBDataWrapper _objClsOledbdatalayer;
        ZoneProperty _objZoneProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindAllZoneList();
            ValidationErrorMessages();
        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var _ZoneList = ZoneProvider.Instance.GetAllZoneList();
            if (_ZoneList.Count > 0)
            {
                try
                {
                    rptZone.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptZone, Common.ConvertToDataTable(_ZoneList));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in Zone.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptZone.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }



        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/ZoneSheet.xlsx");
            if (path == null) throw new ArgumentNullException("File not Found");
            System.Diagnostics.Process objDocProcess = new System.Diagnostics.Process();
            objDocProcess.EnableRaisingEvents = false;
            objDocProcess.StartInfo.FileName = @path; objDocProcess.Start();
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
                        DataSet ds = objClsOledbdatalayer.getdata(path, t);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                            {

                                var errMsg = "";
                                _objZoneProperty = new ZoneProperty
                                   {
                                       ZoneName = ds.Tables[0].Rows[j]["ZoneName"].ToString(),

                                   };
                                var result = ZoneProvider.Instance.InsertZoneDetails(ds.Tables[0].Rows[j]["ZoneName"].ToString(), LoggedInUserId, out errMsg);

                            }
                            lblMsg.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblMsg.Visible = true;
                            BindAllZoneList();   
                       }                            
                    }
                    
                }
                else
                {
                    lblErrorMsg.Text = _objCommon.GetErrorMessage("lblErrMsg");
                    lblMsg.Visible = false;
                    lblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvZoneName.ErrorMessage = _objCommon.GetValidationMessage("rfvZoneName");
            rfvUploadExcel.ErrorMessage = _objCommon.GetValidationMessage("rfvUploadExcel");
            revUploadExcel.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revUploadExcel.ErrorMessage = _objCommon.GetErrorMessage("revUploadExcel");          

        }
        protected void InsertZoneList()
        {
            var ErorrMsg = "";
            try
            {
                int Insert = ZoneProvider.Instance.InsertZoneDetails(Convert.ToString(txtZoneName.Text), LoggedInUserId, out ErorrMsg);
                if (Insert > 0)
                {
                    lblMsg.CssClass = "success show";
                    lblMsg.Text = ErorrMsg;
                    BindAllZoneList();
                    ClearFields();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = ErorrMsg;
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
        [System.Web.Services.WebMethod]
        public static string  UpdateZoneList(int zoneId,string zoneName)
        {
            var ErorrMsg = "";
            try
            {
                int _update = ZoneProvider.Instance.UpdateZoneDetails(zoneId, zoneName,
                                                                      new SecurePage().LoggedInUserId, out ErorrMsg);
               
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
            return ErorrMsg;
        }
        protected void ClearFields()
        {
            txtZoneName.Text = string.Empty;
        }
        protected void btnZone_Click(object sender, EventArgs e)
        {
            InsertZoneList();
        }
        protected void BindAllZoneList()
        {
            try
            {
                var _ZoneList = ZoneProvider.Instance.GetAllZoneList();
                if (_ZoneList.Count > 0)
                {
                    rptZone.Visible = true;


                    ucCustomPaging.BindDataWithPaging(rptZone, Common.ConvertToDataTable(_ZoneList));
                                       
                }
                else
                {
                    rptZone.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;

            }

        }
        
    }
}