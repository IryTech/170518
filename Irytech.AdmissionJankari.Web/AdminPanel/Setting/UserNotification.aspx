<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="UserNotification.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Setting.UserNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="divHome">
        <div class=" accordion">
            <h3 class="accord">
                Query Notification
            </h3>
            <ul>
                <li>  <a href="../Query/QueryList.aspx?N=1"> 
                    <label>
                        College Query Not Moderate Yet :
                    </label>
                 <asp:Label ID="lblCollegeQuery" runat="server"></asp:Label></a>
                </li>
                <li> <a href="../Query/QueryList.aspx?N=2">
                    <label>
                        Exam Query Not Moderate Yet :
                    </label>
                       <asp:Label ID="lblExamQuery" runat="server"></asp:Label></a>
                </li>
                 <li> <a href="../Query/QueryList.aspx?N=3">
                    <label>
                        Loan Query Not Moderate Yet :
                    </label>
                       <asp:Label ID="lblLoanQuery" runat="server"></asp:Label></a>
                </li>
               
                 <li>
                   <a href="../Query/QueryList.aspx?N=4">  <label>
                        Course Query Not Moderate Yet :
                    </label>
                       <asp:Label ID="lblCommonQuery" runat="server"></asp:Label></a>
                </li>
            </ul>
        </div>
        <div class=" accordion">
            <h3 class="accord">
                Reply Notification
            </h3>
            <ul>
                <li>
                    <label>
                        Reply Not Moderate Yet :
                    </label>
                    <asp:Label ID="lblReply" runat="server"></asp:Label>
                </li>
                
            </ul>
        </div>
         <div class=" accordion">
            <h3 class="accord">
              Comment Notification
            </h3>
            <ul>
          
                <li>
                    <a href="../User/UserComment.aspx?N=1">
                    <label>
                        College Comment Not Moderate Yet :
                    </label>
                    <asp:Label ID="lblCollegeComment" runat="server"></asp:Label></a>
                </li>
                 <li>
                   <a href="../User/UserComment.aspx?N=2">
                    <label>
                        Exam Comment Not Moderate Yet :
                    </label>
                    <asp:Label ID="lblExamComment" runat="server"></asp:Label></a>
                </li>
                <li>
                  <a href="../User/UserComment.aspx?N=3">
                    <label>
                        News Comment Not Moderate Yet :
                    </label>
                    <asp:Label ID="lblNewsComment" runat="server"></asp:Label></a>
                </li>
                <li>
                 <a href="../User/UserComment.aspx?N=5">
                    <label>
                        Loan Comment Not Moderate Yet :
                    </label>
                    <asp:Label ID="lblLoanComment" runat="server"></asp:Label></a>
                </li>
            </ul>
        </div>
        <div class=" accordion">
            <h3 class="accord">
                College Registation Notification
            </h3>
            <ul>
                <li><a href="../College/UpdateNewCollegeRegisteration.aspx">
                    <label>
                        College Registation Not Moderate yet :
                    </label>
                    <asp:Label ID="lblCollegeLoginCount" runat="server"></asp:Label></a>
                </li>
                
            </ul>
        </div>
    </div>
    <script type="text/javascript">

       
        $(".accordion h3").click(function () {
            $(".accordion ul").hide();
            $(this).next("ul").removeClass("hide");
            $(this).next("ul").slideToggle("fast")
		.siblings("ul:visible").slideUp("slow");
            $(this).toggleClass("active");
            $(this).siblings("h3").removeClass("active");
        });

      
    </script>
</asp:Content>
