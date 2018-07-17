<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.BankLoan.BankList" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControl/customPaging.ascx" TagPrefix="ADMJ" TagName="Paging" %>
<%@ Register Src="~/UserControl/UcLoanQuery.ascx" TagPrefix="ADMJ" TagName="LoanQuery" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
   <asp:UpdatePanel ID="updateBankList" runat="server">
       <ContentTemplate>
<div class="three_fourth fleft last">

<h1>Education Loans</h1>
<hr class="hrline" />
<h3>Overview</h3>
                    <p>
                        Banks give educational loans to deserving students who are unable to pursue further
                        studies due to the lack of financial support. Loans are given to students pursuing
                        graduation, post-graduation and professional courses from institutes approved by
                        the state and central government as well as foreign universities that are eligible
                        for a loan.
                    </p>
                   <h3>
                        Eligibility</h3>
                        <ol>
                        <li>Should be a citizen of India</li>
                        <li>For vocational training courses and job oriented course age of the individual
                        should be between 15-28 years, and 18-30 years in case of post graduate courses and/or
                        studies abroad.</li>
                        <li>The applicant should have a good academic record.</li>
                        <li>For studies abroad, the candidate should have secured admission in a particular
                        university after having appeared in the specific entrance examinations.</li>
                        <li>The applicant should have secured a minimum of 50% marks.</li>
                        <li>In case of SC/ST candidates eligibility norms may be relaxed.</li>
                        
                        </ol>
                    
                    <h3>
                        Process</h3>
                    <p>
                        Individuals can visit a bank branch to apply for loans or they can even apply online
                        and get a loan sanctioned. Usually, banks study the financial viability of the borrower
                        based on personal discussions with the student, family's assets and annual income,
                        the nature of the course and reputation of the institute etc. In most banks for
                        loans up to Rs. 4 lakhs no collateral or margin is required and the interest rate
                        will not exceed the Prime Lending Rates (PLR). Repayment of the loan usually doesn't
                        begin till one has finished their studies with a grace period of one year after
                        the completion of studies. The mode of repayment differs for each bank and the form
                        of loan taken.
                    </p>
                
                         

<div class="searchBoxOuter bgGray">
<div class="searchbox">
<asp:HiddenField runat="server" id="hdnSearchBankName"></asp:HiddenField>
  <asp:TextBox runat="server" ID="txtBankName" class="search" ToolTip="Please enter bank name" placeholder="Enter bank name to search" ></asp:TextBox>
<%--Editing By Saurabh from here--%>
  <asp:Button ID="btnSearch" runat="server" validationgroup="searchBank" Text="Search"  class="submit" onclick="btnSearch_Click" />

  
</div> 
<div style="margin:-5px 0 15px 24%;">
<asp:RequiredFieldValidator runat="server" validationgroup="searchBank" ControlToValidate="txtBankName" display="dynamic" ErrorMessage="Search Field cannot be blank" cssClass="error"></asp:RequiredFieldValidator>
</div>
<%--Editing end--%>
</div>    
 
 <div class="clearBoth">
 <div class="box1 marginTop">
 <h3>Banks</h3>
 <hr class="hrline" />
 <div class="box">
    <asp:Repeater ID="rptBankList" runat="server"  onitemdatabound="rptBankList_ItemDataBound">
        <ItemTemplate>
     
                   
                   <ul class="clgCompare">
                   <li><strong class="width30Percent"><a rel="canonical" href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Loan-Details/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Eval("BankName").ToString())).ToLower()%>'>
                                    <%# Eval("BankName")%></a> 
                            <asp:HiddenField runat="server" id="hdnBankId" value='<%# Eval("BankId")%>'></asp:HiddenField></strong>
                   
                   
                    <strong class="width30Percent">Loan for India
                            <asp:Image ID="imgIndiaCorrect" runat="server" ImageUrl="/image.axd?Common=rightTick.png"
                                                        Visible="false"  />
                       <asp:Image ID="imgIndiaWrong" runat="server" ImageUrl="/image.axd?Common=cross1.png"
                                                         Visible="false" /></strong>  
                   <strong class="width30Percent">Loan for Abroad
                              <asp:Image ID="imgAbroadCorrect" runat="server" ImageUrl="/image.axd?Common=rightTick.png"
                                                        Visible="false" />
                                                    <asp:Image ID="imgAbroadWrong" runat="server" ImageUrl="/image.axd?Common=cross1.png"
                                                        Visible="false" /></strong>  </li>
                   
                   </ul>
                   
                   
                   
            
        </ItemTemplate>
    </asp:Repeater>
    
     <ADMJ:Paging ID="Pager" runat="server"  />       <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>   
        </div>
</div>
     <div class="clearBoth"></div>
     </div>
</div>
  </ContentTemplate>
  </asp:UpdatePanel>
<div class="one_fourth fright last box1 mainBG">
    <h2>Request For Education Loan</h2>
    <hr class="hrline" />
    <ADMJ:LoanQuery ID="LoanQuery" runat="server"  />
    </div>
      <div class="one_fourth fright last box1 mainBG">
            <a href="http://www.hotelscombined.com/?a_aid=94784&label=Image300250" target="_blank" rel="nofollow"><img src="http://media.datahc.com/banners/affiliate/en/inspirational_300x250.gif" alt="Compare hotel prices and find the best deal - HotelsCombined.com" title="Compare hotel prices and find the best deal - HotelsCombined.com" border="0" width="99%" /></a>

      </div>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
             FillBankName($("#<%=txtBankName.ClientID %>"));
            }
        }
       
        FillBankName($("#<%=txtBankName.ClientID %>"));
        
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         // Add initializeRequest and endRequest
         prm.add_initializeRequest(prm_InitializeRequest);
         prm.add_endRequest(prm_EndRequest);

         // Called when async postback begins
         function prm_InitializeRequest(sender, args) {
             // get the divImage and set it to visible
             $("#fade").show();
             $("#divImage").show();

         }

         // Called when async postback ends
         function prm_EndRequest(sender, args) {
             $("#fade").hide();
             $("#divImage").hide();

         }
    </script>
    </asp:content>
