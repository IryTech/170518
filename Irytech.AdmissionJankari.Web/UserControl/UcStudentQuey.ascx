<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentQuey.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentQuey" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<asp:UpdatePanel runat="server" ID="updateCollegeCityList">
<ContentTemplate>
<div class="box1 fleft">
<h3>Most Viewed College 
   </h3>
   <hr class="hrline" />
   <div class="boxPlane fleft">
    <asp:Repeater  ID="rptRecentActivity" runat="server">
         <ItemTemplate>
            <div class="ucDiv fleft">
        <ul class="vertical fleft marginbottom">
        <li>
             
                    
                </li>
              
                   
              </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>