using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Collections;
using System;



namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentCounsellingPersonelInfo : System.Web.UI.UserControl
    {
        Common _ObjCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            UcDob.ClientValidation = "";
          
            if(IsPostBack) return;
             ValidateControl();
             
        }

        #region Properties
		public string StudentName
        {
            get { return txtCandidateName.Text; }
        }
        public string StudentEmaiLid
        {
            get { return txtEmailId.Text; }
        }
        public string StudentMobileNo
        {
            get { return txtContactNo.Text; }
        }
        public string StudentAlternameNo
        {
            get { return txtAlternateNo.Text; }
        }
        public string StudentGender
        {
            get { return ddlGender.SelectedValue.ToString(); }
        }

        public string  StudentDOB
        {
            get { return UcDob.SelectedDate.ToShortDateString(); }
        }
        public string FatherName
        {
            get { return txtUserFatherName.Text; }
        }

	#endregion

       
        #region Method
        // Method to validate The Control 
        protected void ValidateControl()
        {
           
            revAlterNameMobileNo.ValidationExpression = ClsSingelton.aRegExpMobile;
             revContactNo.ValidationExpression = ClsSingelton.aRegExpMobile;
           revEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
           
          

        }


       
	    #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        


    }
}