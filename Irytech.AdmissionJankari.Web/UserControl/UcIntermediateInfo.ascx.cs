using System;
using IryTech.AdmissionJankari.BL;
using System.Data;



namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcIntermediateInfo : System.Web.UI.UserControl
    {
        Consulling _ObjConsulling;
        DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckInterMediateStatus();
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
                ddl12Board.DataSource = _dt;
                ddl12Board.DataTextField = "AjBoardFullName";
                ddl12Board.DataValueField = "AjBoardId";
                ddl12Board.DataBind();
                ddl12Board.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindBoard in UcIntermediateInfo.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        // Method to validate The Control
        protected void ValidateControl()
        {
            rev12YerPass.ValidationExpression = ClsSingelton.aRegExpInteger;
            rev12PreMarks.ValidationExpression = ClsSingelton.aRegExpDecimal;
            rev12CourseCombinationPercentage.ValidationExpression = ClsSingelton.aRegExpDecimal;

        }
        #endregion

        #region Properties

        public string CollegeName
        {
            get { return txt12SchoolName.Text; }
        }

        public int CollegeBoard
        {
            get { return Convert.ToInt32(ddl12Board.SelectedValue); }
        }

        public string CollegeYOP
        {
            get { return txt12YrPass.Text; }
        }

        public string CollegePer
        {
            get { return txt12PreMarks.Text; }
        }
        public string CollegeCGPA
        {
            get { return ""; }
        }
        public string CollegeCourseCombination
        {
            get { return txt12CourseCombination.Text; }
        }
        public string CollegeCourseCombinationPer
        {
            get { return txt12CourseCombinationPercentage.Text; }
        }

        public bool IntermediateType
        {
            set { liIntermediateType.Visible = value; }
        }

        public bool ValidationPer
        {
            set { rfvCGPA.Enabled = value; }

        }
        #endregion

        protected void rbtItermediateType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if(rbtItermediateType.SelectedValue=="0")
                rfvCGPA.Enabled=false;
            else
                rfvCGPA.Enabled = true;
        }

        private void CheckInterMediateStatus()
        {
           if (rbtItermediateType.SelectedValue == "0")
           {
               liHide1.Visible = false;
               liHide2.Visible = false;
               liHide3.Visible = false;
           }
           else
           {
               liHide1.Visible = true;
               liHide2.Visible = true;
               liHide3.Visible = true;
               
           }
        }
    }
}