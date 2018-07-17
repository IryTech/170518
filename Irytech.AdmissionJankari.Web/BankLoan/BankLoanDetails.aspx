<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankLoanDetails.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.BankLoan.BankLoanDetails" %>

<%@ Register Src="~/UserControl/UcLoanQuery.ascx" TagPrefix="ADMJ" TagName="LoanQuery" %>
<%@ Register Src="~/UserControl/UcCommonComment.ascx" TagPrefix="AJ" TagName="CommonComment" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<div class="five_sixth fleft last">

        <div class="boxPlane bgYellow marginbottom">
            <ul class="vertical">
            <li class="Imgarrow marginRight"><asp:ImageMap ID="CollegeImageHeader" runat="server" align="left" Height="100" Width="100" hspace="5" /></li>
            <li><h1><asp:Label ID="lblHeaderCollegeName" runat="server"></asp:Label></h1><h2 class="streamCompareH3">Admission Helpline No.: 8800 567 733</h2></li>
            </ul>
            <div class="clearBoth"></div>
        </div>

        <div class="pageTargetMenu">
            <ul class="vertical">
            <li><a href="#Overview"  title="Overview">Overview</a></li>
            <li><a href="#pnlDescrip"  title="Description">Description</a></li>
            <li><a href="#LRange"  title="Loan Range">Loan Range</a></li>
            <li><a href="#BankCDetails"  title="Bank Contact Details">Bank Contact Details</a></li>
            <li><a href="#CDetails"  title="Contact Details">Contact Details</a></li>
            </ul>
            <div class="clearBoth"></div>
        </div>


        <div class="four_fifth last fleft">

             <asp:Repeater ID="rptLoanInfo" runat="server" onitemdatabound="rptEntranceExam_ItemDataBound">
                        <ItemTemplate>
                            <div  id="column1">
                                <div  id="Overview" class="box1">
                                    <h3 class="streamCompareH3" >
                                        <%# Eval("BankName")%></h3>
                                        <hr class="hrline" />
                                    <div class="box">
                                        <ol class="vertical" >
                                        <li><strong class="strongDetails">Bank</strong> : <span><%# Eval("BankName")%></span></li>
                                        <li><strong class="strongDetails">ShortName</strong> : <span> <%# Eval("BankShortName")%></span> </li>
                                  
                                        
                                      
                                        </ol>
                                        <div class="clearBoth"></div>
                                        </div>
                                        
                                    </div>
                                   <div  class="box1" runat="server" id="pnlDescrip"  visible='<%#  !string.IsNullOrEmpty(Eval("BankShortDescription").ToString()).Equals(true) %>'><h3 >Description</h3><hr class="hrline" />
                                   <div class="box">
                                    <a href="http://twitter.com/#!/admissionjankar" class="twitter-share-button" data-count="horizontal">
                                                Tweet</a>
                                                 <%# Eval("BankShortDescription")%></div>
                                    </div>
                                <div  id="BankCDetails" class="box1">
                                     <h3 class="streamCompareH3" >
                                        Bank Contact Details</h3>
                                        <hr class="hrline" />
                                    <div class="box">
                                       <ol >
                                            <li ><strong class="strongDetails">Address:</strong> : <span><%# Eval("BankAddress")%></span></li>
                                            <li ><strong class="strongDetails">Website:</strong> : <span><a href='<%# String.Format("{0}{1}","http://" , Eval("BankUrl")) %>'
                                                target="_blank"  title='<%# Eval("BankUrl")%>'>
                                                <%# Eval("BankUrl")%>
                                            </a></span></li>
                                            <li><strong class="strongDetails">Phone:</strong> : <span>
                                                <%# Eval("BankPhoneNo")%></span>
                                            </li><asp:HiddenField runat="server" id="hdnBankId" value='<%# Eval("BankId")%>'></asp:HiddenField>
                                         <div class="clearBoth"></div>   
                                        </ol>
                                        </div>
                                  </div>

                                <asp:Panel  id="LRange" class="box1" runat="server"  >
                                    <div  id="item9">
                                         <h3 >Loan Range</h3>
                                         <div class="box">
                                        
                                            <asp:Repeater ID="rptLoan" runat="server">
                                                <ItemTemplate>

                                                    <div class="accordion">
                                                        <h4 class="accord" >
                                                            <%#Eval("Amount")%>
                                                              <%#Eval("StudyPlaceName")%>
                                                        </h4>
                                                        <p>
                                                            <strong>Duration : </strong>&nbsp;<%#Eval("RepaymentDuration")%>
                                                            <strong>Rate of Interest : </strong>&nbsp; <%#Eval("RateOfInterest")%>
                                                            <strong >Margin : </strong>&nbsp; <%#Eval("Margin")%>
                                                        </p>
                                                    </div>
                                          
                                                </ItemTemplate>
                                            </asp:Repeater>
                                      </div>
                                    </div>
                                </asp:Panel>
                            





                            <div  id="column4" class="box1">
                                <div  id="CDetails">
                                   <h3 class="streamCompareH3" >Contact Person Details</h3>
                                   <hr class="hrline" />
                                   <div class="box">
                                        <ol class="vertical" >
                                            <li><strong class="strongDetails">Name:</strong> : <span> <%# Eval("BankContactPerson")%> </span></li>
                                            <li><strong class="strongDetails">Designation:</strong> : <span><%# Eval("BankContactPersonDesignation")%></span></li>
                                            <li><strong class="strongDetails">Mobile:</strong> : <span> <%# Eval("BankContactPersonMobile")%></span></li>
                                            <li><strong class="strongDetails">Email Id:</strong> : 
                                            <span>
                                                
                                                <%# new Regex(@"([a-zA-Z_0-9.-]+\@[a-zA-Z_0-9.-]+\.\w+)",
                                                  RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(Eval("BankContactPersonEmailId").ToString(), "<a href=mailto:$1>$1</a>")%>
                                            </span></li>
                                        </ol>
                                        <div class="clearBoth"></div>
                                        </div>
                                        
                                   </div>
                             

                        </div>
                        </div>
                        </ItemTemplate>
                    </asp:Repeater>
                     <div class="box1" ><asp:UpdatePanel ID="updLoanCommentMaster" runat="server">
<ContentTemplate>
        <AJ:CommonComment runat="server" ID="UcComment"   /> </ContentTemplate>
</asp:UpdatePanel>
        </div>
                    
        </div>
        
        <div class="one_third fright last border mainBG"><h3 class="streamCompareH3">Request For Education Loan</h3><hr class="hrline" />
                      <ADMJ:LoanQuery ID="LoanQuery" runat="server"  />
                    
                    </div>
                   
                     <div class="one_third fright last" style="margin-top:5px;">
         <div class="box1" id="divLatestNews" >
                <a href="http://www.hotelscombined.com/?a_aid=94784&label=Image300250" target="_blank" rel="nofollow">
                <img width="99%" src="http://media.datahc.com/banners/affiliate/en/inspirational_300x250.gif" alt="Compare hotel prices and find the best deal - HotelsCombined.com" title="Compare hotel prices and find the best deal - HotelsCombined.com" border="0" /></a>
            </div>
            </div>
    
    </div>

   <%--<div class="one_sixth last fright border" style="min-height:600px;" >
    <strong>Advertising Banner</strong>
    </div>--%>

<script type="text/javascript" defer="defer">
    $(document).ready(function () {

        $(".accordion h4:first").addClass("active");
        $(".accordion p:not(:first)").hide();

        $(".accordion h4").click(function () {
            $(this).next("p").slideToggle("slow")
		.siblings("p:visible").slideUp("slow");
            $(this).toggleClass("active");
            $(this).siblings("h4").removeClass("active");
        });


    });

</script>

</asp:content>
