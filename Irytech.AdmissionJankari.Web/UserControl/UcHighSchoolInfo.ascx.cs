using System;
using IryTech.AdmissionJankari.BL;
using  System.Data;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcHighSchoolInfo : System.Web.UI.UserControl
    {
        Consulling _ObjConsulling;
        DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindBoard();
            ValidateControl();
        }

        #region Method
        //Method to Bind The Borad
        protected void BindBoard()
        {
            _ObjConsulling = new Consulling();
            _dt = new DataTable();
            try
            {
                _dt = _ObjConsulling.GetBoradList();
                if (_dt.Rows.Count <= 0) return;
                ddl10Board.DataSource = _dt;
                ddl10Board.DataTextField = "AjBoardFullName";
                ddl10Board.DataValueField = "AjBoardId";
                ddl10Board.DataBind();
                ddl10Board.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindBoard in UcHighSchoolInfo.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void ValidateControl()
        {
            rev10YerPass.ValidationExpression = ClsSingelton.aRegExpInteger;
            revtenthPer.ValidationExpression = ClsSingelton.aRegExpDecimal;
            
        }

        #endregion

        #region Properties
        public string SchoolName
        {
            get { return txt10SchoolName.Text; }
        }
        public int SchoolBoard
        {
            get { return Convert.ToInt32(ddl10Board.SelectedValue); }
        }

        public string SchoolYOP
        {
            get { return txt10YerPass.Text; }
        }
        public string SchoolCGPA
        {
            get { return txtTenthCGPA.Text; }
        }

        #endregion
                
    }
}