using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;


namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Common _objCommon = new Common();
            try
            {
                var errMsg = "";
              

                DataSet _ds = new DataSet();
                var _objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var excelSheets = new String[0];
                string path = MapPath(fulUploadCollegeImage.FileName);
                fulUploadCollegeImage.SaveAs(path);
                excelSheets = _objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (string t in excelSheets)
                    {
                        _ds = _objClsOledbdatalayer.getdata(path, t);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j <= _ds.Tables[0].Rows.Count - 1; j++)
                            {

                                var _insert = _objCommon.UploadCollegeImage(Convert.ToString(_ds.Tables[0].Rows[j]["CollegeName"]), Convert.ToString(_ds.Tables[0].Rows[j]["CollegeImage"]));

                                if (_insert > 0)
                                {
                                    
                                }
                            }
                        }
                    }
                   
                }
               
            }
            catch (Exception ex)
            { }
        }



        
    }
}