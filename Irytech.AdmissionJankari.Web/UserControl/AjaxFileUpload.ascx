<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxFileUpload.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.AjaxFileUpload" %>
 
 
<div style="width:auto;display:inline-block;vertical-align:top">
                     <div style="display:none;"> 
                          <asp:Image ID="PrevImage"  alt="" CssClass="imageWidthHight" Width="40px" Height="40px" runat="server"  />
                          
                          </div>
                           
                               <Ajex:AsyncFileUpload ID="AsyncUpldItemImage" runat="server" PersistFile="true" PersistedStoreType="Session" 
             CompleteBackColor="Transparent"  OnUploadedComplete="AsyncUpldItemImage_UploadedComplete"
              />
                          
        </div>