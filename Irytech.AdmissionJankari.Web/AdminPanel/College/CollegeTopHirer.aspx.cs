using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;


namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeTopHirer : SecurePage
    {
        Common _objComman;


        
        protected void Page_Load(object sender, EventArgs e)
        {

            UcCustomPaging.PageSize = ClsSingelton.PageSize;
            UcCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            UcCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;


            if (IsPostBack) return;
                BindCourseList();
                BindCollegeDetails();
                lblCollegePlacement.Text = "Add College Placement";
                hndCollegeTopHirer.Value = ddlCourse.SelectedValue;

              
                
               
                validateYear.MaximumValue = Convert.ToString(DateTime.Now.Year);
        }

        #region "MemberFunction"


        //protected void PagerPageIndexChanged(object sender, EventArgs e)
        protected void bindRepeater(bool parameter=true)
        {
            lblMsg.Visible = false;
            lblInfo.Visible = false;
            //var collegeData = CollegeProvider.Instance.GetCollegeList();
            //if (collegeData.Count > 0)
            if (string.IsNullOrEmpty(hdncollegename.Value.Trim().ToString()))
            {
               BindCollegeDetails(parameter);
                
            }
            else
            {
                try
                {
                    rptCollegeTopHirer.Visible = true;

                    //UcCustomPaging.BindDataWithPaging(rptCollegeTopHirer, Common.ConvertToDataTable(collegeData));
                    var collegeData = CollegeProvider.Instance.GetCollegeCourseTopHirerListByCollegeName(hdncollegename.Value.Trim());

                    if (!(string.Equals(hdncourseid.Value.ToString(), "Select Course", StringComparison.OrdinalIgnoreCase)))

                        collegeData = collegeData.Where(result => result.CourseName == hdncourseid.Value.ToString()).OrderByDescending(result => result.CollegeBranchCoursePlacementYear).ToList();
                    else
                        collegeData = collegeData.OrderByDescending(result => result.CollegeBranchCoursePlacementYear).ToList();

                    if (collegeData.Count > 0)
                    {
                        
                        rptCollegeTopHirer.Visible = true;

                        UcCustomPaging.BindDataWithPaging(rptCollegeTopHirer, Common.ConvertToDataTable(collegeData));
                      
                       
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.CssClass = "error";
                        lblMsg.Text = "No Records are found.";
                        rptCollegeTopHirer.Visible = false;

                    }
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                 
                    const string addInfo = "Error in Executing  bindRepeater in CollegeTopHirer.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            //else
            //{
            //    rptCollegeTopHirer.Visible = false;

            //}
           
        }

        //Bind all SponserCourseList
        //protected void BindSponserCollegeList()
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {

            //try
            //{

            //    var data = CollegeProvider.Instance.GetCollegeList();
            //    // var SponserCollegeList = CollegeProvider.Instance.GetAllSponserCollegeList();
            //    if (data.Count > 0)
            //    {
            //        drpColleges.DataSource = data;
            //        drpColleges.DataTextField = "CollegeBranchName";
            //        drpColleges.DataValueField = "CollegeBranchCourseId";
            //        drpColleges.DataBind();
            //        drpColleges.Items.Insert(0, ListItem.FromString("Select College"));



            //    }

            //}
            //catch (Exception ex)
            //{
            //    string err = ex.Message;
            //}

            lblInfo.Visible = false;
            lblMsg.Visible = false;
            bindRepeater(false);
        }



        //Bind all CourseList
        protected void BindCourseList()
        {

            try
            {
                var CourseList = CourseProvider.Instance.GetAllCourseList();
                if (CourseList.Count > 0)
                {
                    ddlCourse.DataSource = CourseList;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                   // ddlCourse.Items.Insert(0, "Select Course");
                    ddlCourse.Items.Insert(0, new ListItem("Select Course", "0"));



                    ddlInsertCourse.DataSource = CourseList;
                    ddlInsertCourse.DataTextField = "CourseName";
                    ddlInsertCourse.DataValueField = "CourseId";
                    ddlInsertCourse.DataBind();
                    ddlInsertCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                  
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCourseList in CollegeTopHirer.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }




        //protected void BindCollegeDetails()
        protected void BindCollegeDetails(bool parameter = true)
        {

           
            try
            {

               // var data = CollegeProvider.Instance.GetCollegeList();
                var data = CollegeProvider.Instance.GetCollegeCourseTopHirerList();

                if (!(string.Equals(ddlCourse.SelectedItem.ToString(), "Select Course", StringComparison.OrdinalIgnoreCase)))
                {
                    data = data.Where(result => result.CourseName == ddlCourse.SelectedItem.ToString()).OrderByDescending(result => result.CollegeBranchCoursePlacementYear).ToList();

                }
                else
                    data = data.OrderByDescending(result => result.CollegeBranchCoursePlacementYear).ToList();
                UcCustomPaging.BindDataWithPaging(rptCollegeTopHirer, Common.ConvertToDataTable(data), parameter);
              if (data.Count > 0)
                {


                    UcCustomPaging.BindDataWithPaging(rptCollegeTopHirer, Common.ConvertToDataTable(data));
                    rptCollegeTopHirer.Visible = true;
                   
                  
                }

              else
              {
                  lblMsg.Visible = true;
                  lblMsg.CssClass = "error";
                  lblMsg.Text = "No Records are found.";
                  rptCollegeTopHirer.Visible = false;

              }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeDetails in CollegeTopHirer.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

       
        #endregion
     

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            lblMsg.Visible = false;
            string ErrorMsg = "";
            var insert = 0;
            try
            {

                CollegeBranchCoursePlacementProperty objcollegePlacementProperty = new CollegeBranchCoursePlacementProperty();
                objcollegePlacementProperty.CollegeBranchName = txtSelectCollegeFiltered.Text.Trim().ToString();
                objcollegePlacementProperty.CourseId = Convert.ToInt32(ddlInsertCourse.SelectedValue);
               
                objcollegePlacementProperty.CollegeBranchCoursePlacementCompanyName = Convert.ToString(txtPlacementCompanyName.Text.Trim());
                objcollegePlacementProperty.CollegeBranchCoursePlacementYear = Convert.ToString(txtCompanyPlacementYear.Text.Trim());
                objcollegePlacementProperty.CollegeBranchCoursePlacementNoOfStudentHired = Convert.ToString(txtnoOfStudentHired.Text.Trim());
                objcollegePlacementProperty.CollegeBranchCoursePlacementAvgSalaryOffered = Convert.ToString(txtCompanySalryOffer.Text.Trim());
                objcollegePlacementProperty.CollegeBranchCoursePlacementStatus = ChkStatus.Checked;

                if (btnSubmit.Text == "Submit")
                {
                    insert = CollegeProvider.Instance.InsertCollegePlacementByCourse(objcollegePlacementProperty, LoggedInUserId, out ErrorMsg);

                   
                }
                else
                {
                    objcollegePlacementProperty.CollegeBranchCoursePlacementId = Convert.ToInt32(hndCollegeTopHirer.Value);
                    insert = CollegeProvider.Instance.UpdateCollegePlacementByCourse(objcollegePlacementProperty, LoggedInUserId, out ErrorMsg);
                   

                      btnSubmit.Text = "Submit";
                    
                 
                   
                }
                if (insert > 0)
                {
                   
                    lblMsg.CssClass = "success";
                    lblMsg.Text = ErrorMsg;
                }
                else
                {
                   
                    lblMsg.CssClass = "error";
                    lblMsg.Text = ErrorMsg;
                }

                bindRepeater();
                ClearFields();
                lblMsg.Visible = true;
            }

            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  btnSubmit_Click in CollegeTopHirer.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        protected void rptCollegeList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
                   
            lblMsg.Visible = false;
            try
            {
                if (e.CommandName == "Edit")
                {
                        hndCollegeTopHirer.Value = e.CommandArgument.ToString();
                        HiddenField hndCollegeBranchCourseID = (HiddenField)e.Item.FindControl("hndCollegeBranchCourseID");

                        var data = CollegeProvider.Instance.GetCollegeTopHirerByPlacementID(Convert.ToInt32(hndCollegeTopHirer.Value));
                   

                        if (data.Count > 0)
                        {

                            lblCollegePlacement.Text = "Update the Placement details of " + data[0].CollegeBranchName;
                            txtPlacementCompanyName.Text = data[0].CollegeBranchCoursePlacementCompanyName;
                            txtCompanySalryOffer.Text =data[0].CollegeBranchCoursePlacementAvgSalaryOffered;
                            txtCompanyPlacementYear.Text = data[0].CollegeBranchCoursePlacementYear;
                            txtnoOfStudentHired.Text = data[0].CollegeBranchCoursePlacementNoOfStudentHired;
                            txtSelectCollegeFiltered.Text = data[0].CollegeBranchName;
                            ddlInsertCourse.SelectedValue = Convert.ToString(data[0].CourseId);
                            filterList.Visible = false;
                            ddlInsertCourse.Enabled = false;
                            txtSelectCollegeFiltered.Enabled = false;
                           
                            if (data[0].CollegeBranchCoursePlacementStatus == true)
                            {
                                ChkStatus.Checked = true;
                            }
                            else
                            {
                                ChkStatus.Checked = false;
                            }
                            
                            
                       
                            btnSubmit.Text = "Update";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePlacement');", true);
                        }

                       
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  RptCollegeList_ItemCommand in CollegeTopHirer.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        #region ClearFields Method
        protected void ClearFields()
        {
           
            lblMsg.Visible = false;
            ddlCourse.ClearSelection();
            txtCollegeTopHirer.Text = string.Empty;
            lblCollegePlacement.Text = "Add Placement Record";
            ddlInsertCourse.Enabled = true;
            txtSelectCollegeFiltered.Enabled = true;
            filterList.Visible = true;
            ddlInsertCourse.ClearSelection();
            txtSelectCollegeFiltered.Text = string.Empty;
            rbtnFilterCollege.ClearSelection();
            rbtnFilterCollege.Items[0].Selected = true;
            txtPlacementCompanyName.Text = string.Empty;
            txtCompanySalryOffer.Text = string.Empty;
            txtCompanyPlacementYear.Text = string.Empty;
            txtnoOfStudentHired.Text = string.Empty;
            ChkStatus.Checked = false;
            btnSubmit.Text = "Submit";
            
            
        }
        #endregion


        protected void btnReset_Click(object sender, EventArgs e)
        {
            
            ClearFields();
            BindCollegeDetails();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            lblMsg.Visible = false;
            hdncollegename.Value = txtCollegeTopHirer.Text.Trim().ToString();
            hdncourseid.Value = ddlCourse.SelectedItem.ToString();
            if (string.IsNullOrEmpty(txtCollegeTopHirer.Text.Trim().ToString()))
            {

                BindCollegeDetails();
            }
            else
            {
                try
                {

                    var CollegeDetails = CollegeProvider.Instance.GetCollegeCourseTopHirerListByCollegeName(txtCollegeTopHirer.Text.Trim());
                    if (!(string.Equals(ddlCourse.SelectedItem.ToString(), "Select Course", StringComparison.OrdinalIgnoreCase)))

                        CollegeDetails = CollegeDetails.Where(result => result.CourseName == ddlCourse.SelectedItem.ToString()).OrderByDescending(result => result.CollegeBranchCoursePlacementYear).ToList();
                    else
                        CollegeDetails = CollegeDetails.OrderByDescending(result => result.CollegeBranchCoursePlacementYear).ToList();
                    UcCustomPaging.BindDataWithPaging(rptCollegeTopHirer, Common.ConvertToDataTable(CollegeDetails),true);
                    if (CollegeDetails.Count > 0)
                    {
                        rptCollegeTopHirer.Visible = true;
                       
                    }
                    else
                    {

                        lblMsg.Visible = true;
                        lblMsg.CssClass = "error";
                        lblMsg.Text = "No Records are found.";
                        rptCollegeTopHirer.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  btnSearch_Click in CollegeTopHirer.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);

                }
            }
        }



      
    }
}