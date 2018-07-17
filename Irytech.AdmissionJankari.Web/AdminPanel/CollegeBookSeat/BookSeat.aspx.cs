using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.CollegeBookSeat
{
    public partial class BookSeat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCollegeList.PageSize = ClsSingelton.PageSize;
            ucCollegeList.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCollegeList.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindCourse();
            lblBookSeat.Text = "Add College";
            BindBookSeatCollege();

        }
        #region course

        //to get course list....
        private void BindCourse()
        {
            var courseData = CourseProvider.Instance.GetAllCourseList();
            if (courseData.Count > 0)
            {
                ddlCourseList.DataSource = courseData;
                ddlCourseList.DataTextField = "CourseName";
                ddlCourseList.DataValueField = "CourseId";
                ddlCourseList.DataBind();
                ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlCourseSearch.DataSource = courseData;
                ddlCourseSearch.DataTextField = "CourseName";
                ddlCourseSearch.DataValueField = "CourseId";
                ddlCourseSearch.DataBind();
                ddlCourseSearch.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            else
            {
                ddlCourseSearch.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));

            }

        }
        #endregion

        #region booked seat college

        //to get booked college list.........
        private void BindBookSeatCollege()
        {
           var bookSeatList = GetBookedCollege(null);
            if (bookSeatList.Count > 0)
            {
                ucCollegeList.Visible = true;
                rptBookSeat.Visible = true;
                ucCollegeList.BindDataWithPaging(rptBookSeat, Common.ConvertToDataTable(bookSeatList));
            }
            else
            {
                ucCollegeList.Visible = false;
                rptBookSeat.Visible = false;
                
            }
        }

        //common method to get booked college list by id or all....
        private List<AdmissionJankari.BO.BookSeat> GetBookedCollege(int? bookSeatId)
        {
            var objCounsulling = new Consulling();
            var objBookSeatList = bookSeatId.HasValue
                                      ? objCounsulling.GetBookedCollegeByBookSeatId((int) bookSeatId).ToList()
                                      : objCounsulling.GetBookedCollege().ToList();
             return objBookSeatList.ToList();
        }

        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var objCounsulling = new Consulling();
            lblResult.Visible = false;
            try
            {
                var objBookSeatList = objCounsulling.GetBookedCollege().ToList();
                if (objBookSeatList.Count > 0)
                {
                    if (ddlCourseSearch.SelectedIndex > 0)
                    {
                        objBookSeatList =
                            objBookSeatList.Where(
                                x =>
                                x.CourseMaster.CourseId == Convert.ToInt32(ddlCourseSearch.SelectedValue))
                                           .ToList();
                    }
                    
                   if (objBookSeatList.Count > 0)
                    {
                        rptBookSeat.Visible = true;
                        ucCollegeList.BindDataWithPaging(rptBookSeat, Common.ConvertToDataTable(objBookSeatList));
                    }
                    else
                    {
                        lblResult.Visible = true;
                        lblResult.CssClass = "info";
                        lblResult.Text = "Sorry,No college found";
                        rptBookSeat.Visible = false;

                    }
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "info";
                    lblResult.Text = "Sorry,No college found";
                    rptBookSeat.Visible = false;

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in CollegeList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }


        }

        //booked college insert/update at button click...
        protected void BtnSubmitCollegeBasicInfoClick(object sender, EventArgs e)
        {
            lblResult.Visible = false;
            if (string.IsNullOrEmpty(Convert.ToString(hdnBookSeatId.Value)))
                InsertUpdateCollegeBookSeat(0);
            else
            {

                InsertUpdateCollegeBookSeat(Convert.ToInt32(hdnBookSeatId.Value));
                btnSubmit.Text = "Insert";
                hdnBookSeatId.Value = "0";

            }
           
        }

        //booked college insert/update.....
        private void InsertUpdateCollegeBookSeat(int bookSeatId)
        {
            lblResult.Visible = true;
            try
            {
                string eligibleTenPer = "", eligibleTweleve = "", eligibleFifteen = "";
                switch (hdnEligibity.Value)
                {
                    case "10":
                        eligibleTenPer = txtEligibity.Text.Trim();
                        break;
                    case "12":
                        eligibleTweleve = txtEligibity.Text.Trim();
                        break;

                    case "15":
                        eligibleFifteen = txtEligibity.Text.Trim();
                        break;
                }
                var errMsg = "";
                var result = new Common().InsertUpdateBookSeat(bookSeatId, Convert.ToInt32(hdnCourseId.Value), txtCollege.Text.Trim(), !string.IsNullOrEmpty(txtPayment.Text.Trim()) ? txtPayment.Text.Trim() : "25000",
                    chkStatus.Checked, new SecurePage().LoggedInUserId, out errMsg, fckInstruction.FckValue.Trim(), Convert.ToDateTime(txtStartDate.Text),Convert.ToDateTime(txtEndDate.Text), eligibleTenPer, eligibleTweleve, eligibleFifteen);
               
                lblResult.Text = errMsg;
                lblResult.CssClass = result > 0 ? "success" : "info";
                if (result>0){ ClearControl();
                BindBookSeatCollege();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  InsertUpdateCollegeEvent in CollegeBookSeat/BookSeat.aspx:: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        //to get particular booked college....
        protected void RptBookSeatItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                lblResult.Visible = false;
                var bookSeatId = e.CommandArgument.ToString();
                hdnBookSeatId.Value= bookSeatId;
                var bookSeatList = GetBookedCollege(Convert.ToInt32(bookSeatId));
                if (bookSeatList.Any())
                {
                    var data= bookSeatList.FirstOrDefault();
                    if (data != null)
                    {
                        btnSubmit.Text = "Update";
                        fckInstruction.FckValue = data.Instruction;
                        txtCollege.Text = data.CollegeBasicInfo.CollegeBranchName;
                        txtCollege.Enabled = false;
                        txtPayment.Text = data.BookSeatAmount;
                        ddlCourseList.SelectedValue = data.CourseMaster.CourseId.ToString();
                        ddlCourseList.Enabled = false;
                        hdnCourseId.Value = data.CourseMaster.CourseId.ToString();
                        chkStatus.Checked = data.BookSeatStatus;
                        var eligibity = data.CourseEligibity.CourseEligibiltyName;
                        hdnEligibity.Value = eligibity;
                        lblEligibilty.InnerHtml = "Eligiblty Percentage For " + eligibity;
                        lblBookSeat.Text="Edit book seat details for " +txtCollege.Text;
                        txtStartDate.Text = !string.IsNullOrEmpty(data.BookSeatStartDate)?Convert.ToDateTime(data.BookSeatStartDate).ToString("MM/dd/yyyy"):"";
                        txtEndDate.Text = !string.IsNullOrEmpty(data.BookSeatEndDate)?Convert.ToDateTime(data.BookSeatEndDate).ToString("MM/dd/yyyy"):""; 
                        switch (eligibity)
                        {

                            case "10":
                                txtEligibity.Text = data.Eligibity10;
                                txtEligibity.Enabled = true;
                                break;
                            case "12":
                                txtEligibity.Enabled = true;
                                txtEligibity.Text = data.Eligibity12;
                                break;
                            case "15":
                                txtEligibity.Enabled = true;
                                txtEligibity.Text = data.Eligibity15;
                                break;
                        }

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divUniversityCategoryInsert','750px','sndAddCollegeinBookSeat');", true);
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
                const string addInfo = "Error in Executing  RptBookSeatItemCommand in CollegeBookSeat/BookSeat.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        //to clear control.....
        private void ClearControl()
        {
            ddlCourseList.ClearSelection();
            txtCollege.Text = string.Empty;
            txtPayment.Text = string.Empty;
            txtCollege.Enabled = false;
            txtEligibity.Text = string.Empty;
            txtEligibity.Enabled = false;
            lblEligibilty.InnerHtml = "Eligiblty Percentage For ";
            fckInstruction.FckValue = string.Empty;
            ddlCourseList.Enabled = true;
            chkStatus.Checked = false;
            lblBookSeat.Text = "Add College to Book Seat";

        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            lblResult.Visible = false;
            var objCounsulling = new Consulling();
            var objBookSeatList = objCounsulling.GetBookedCollege().ToList();
            if (!string.IsNullOrEmpty(txtCollegeSearch.Text))
            {
                objBookSeatList =
                    objBookSeatList.Where(
                        x =>
                        x.CollegeBasicInfo.CollegeBranchName.Trim().ToLower() == txtCollegeSearch.Text.Trim().ToLower())
                                   .ToList();
                if (objBookSeatList.Count > 0)
                {
                    ucCollegeList.Visible = true;
                    rptBookSeat.Visible = true;
                    ucCollegeList.BindDataWithPaging(rptBookSeat, Common.ConvertToDataTable(objBookSeatList));
                   
                }
                else
                {
                    lblResult.Visible = true;
                    ucCollegeList.Visible = false;
                    lblResult.CssClass = "info";
                    lblResult.Text = "Sorry,No college found";
                    rptBookSeat.Visible = false;

                }
            }
            else
            {
                if (objBookSeatList.Count > 0)
                {
                    rptBookSeat.Visible = true;
                    rptBookSeat.DataSource = objBookSeatList;
                    rptBookSeat.DataBind();
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "info";
                    lblResult.Text = "Sorry,No college found";
                    rptBookSeat.Visible = false;

                }

            }

        }

        #endregion

        protected void ddlCourseSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResult.Visible = false;
            var objCounsulling = new Consulling();
            var objBookSeatList = objCounsulling.GetBookedCollege().ToList();
            if (ddlCourseSearch.SelectedIndex>0)
            {
                objBookSeatList =
                    objBookSeatList.Where(
                        x =>
                        x.CourseMaster.CourseId==Convert.ToInt32(ddlCourseSearch.SelectedValue))
                                   .ToList();
                if (objBookSeatList.Count > 0)
                {
                    ucCollegeList.Visible = true;
                    rptBookSeat.Visible = true;
                    ucCollegeList.BindDataWithPaging(rptBookSeat, Common.ConvertToDataTable(objBookSeatList));
                }
                else
                {
                    lblResult.Visible = true;
                    ucCollegeList.Visible = false;
                    lblResult.CssClass = "info";
                    lblResult.Text = "Sorry,No college found";
                    rptBookSeat.Visible = false;

                }
            }
            else
            {
                if (objBookSeatList.Count > 0)
                {
                    rptBookSeat.Visible = true;
                    rptBookSeat.DataSource = objBookSeatList;
                    rptBookSeat.DataBind();
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.CssClass = "info";
                    lblResult.Text = "Sorry,No college found";
                    rptBookSeat.Visible = false;

                }

            }

        }

       
    }
}