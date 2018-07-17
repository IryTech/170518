<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcBookMySeat.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcBookMySeat" %>
<%@ Register Src="~/UserControl/UcStudentCounsellingPersonelInfo.ascx" TagPrefix="ADMJ" TagName="StudentPersonelInfo" %>
<%@ Register Src="~/UserControl/UcCourse.ascx" TagPrefix="ADMJ" TagName="StudentCourseInfos" %>
<%@ Register Src="~/UserControl/UcStudentAcademic.ascx" TagPrefix="ADMJ" TagName="StudentAcademicInfo" %>
<%@ Register Src="~/UserControl/UcBookSeatInstruction.ascx" TagPrefix="ADMJ" TagName="BookSeatInstruction" %>
<%@ Register Src="~/UserControl/ucBookSeatavailability.ascx" TagPrefix="ADMJ" TagName="BookSeatAvailibity" %>

<div id="divFirstImage" runat="server" style="width:942px; margin:5px auto; display:block; height:32px;">
    <img src="/image.axd?Common=application_form.jpg" /></div>
    <div id="divSeccondImage" visible="false" runat="server" style="width:942px; margin:5px auto; display:block; height:32px;">
    <img src="/image.axd?Common=aplicationForm12.jpg" /></div>
<div class="wizardOutDiv">
              <asp:Wizard ID="wizardApplyForm"  runat="server" ActiveStepIndex="0" Width="100%"   
                        DisplaySideBar="false" StartNextButtonText="Next »" StepNextButtonText="Next »" OnFinishButtonClick="WizardApplyFormFinishButtonClick"
                        StepPreviousButtonText="« Previous" OnActiveStepChanged="WizardApplyFormActiveStepChanged" OnNextButtonClick="WizardApplyFormNextButtonClick"  
                  >
                        <StepStyle />
                        <SideBarStyle VerticalAlign="Top" Wrap="False" />
                        <NavigationButtonStyle   CssClass="button" />
                        <NavigationStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                        <WizardSteps>
                            <asp:WizardStep ID="personalDetails" runat="server" Title="Personal Details">
                            <ADMJ:StudentCourseInfos ID="StudentCourseInfo" runat="server"></ADMJ:StudentCourseInfos>
                            <ADMJ:StudentPersonelInfo ID="studentPerInfo" runat="server"></ADMJ:StudentPersonelInfo>
                            </asp:WizardStep>
                            <asp:WizardStep ID="AcedmicInfo" runat="server" Title="Your Scorecards">
                                <ADMJ:StudentAcademicInfo ID="StudentAcademicInfo" runat="server"></ADMJ:StudentAcademicInfo>
                            </asp:WizardStep>
                            <asp:WizardStep ID="SeatAvailability" runat="server" Title="Seat Availability">
                                <ADMJ:BookSeatAvailibity ID="UcSeatAvailiablity" runat="server" ></ADMJ:BookSeatAvailibity >
                            </asp:WizardStep>
                            <asp:WizardStep ID="Instrucation" runat="server" Title="Instructions">
                                <ADMJ:BookSeatInstruction ID="ucInstrucation" runat="server"></ADMJ:BookSeatInstruction>
                            </asp:WizardStep>
                              <asp:WizardStep ID="wzdPayment" runat="server" Title="Payment">
                              
            <div class="boxPlane mainBG">
           
                <h3 class="streamCompareH3">
                    Pay Now to Confirm Your participation :
                </h3>
                <hr class="hrline" />
                <strong class="fleft">Payment Options:</strong>
                <asp:RadioButtonList ID="rbtnPaymentType" runat="server" AutoPostBack="true" 
                    RepeatDirection="Horizontal" onselectedindexchanged="RbtnPaymentTypeSelectedIndexChanged"
                   >
                    <asp:ListItem Value="OnPayment" Selected="True"> Online Payment &nbsp;</asp:ListItem>
                    <asp:ListItem Value="0"> By Cheque &nbsp;</asp:ListItem>
                    <asp:ListItem Value="1"> By Demand Draft &nbsp;</asp:ListItem>
                    <asp:ListItem Value="2"> By Cash </asp:ListItem>
                </asp:RadioButtonList>
                <fieldset id="PaymentOnline">
       
          <legend>
                     Pay Now:
                  </legend>
            <ul class="horizontal CreditCard">
                <li>
                    <label>
                        Address</label>
                    <asp:TextBox ID="txtAddress" ToolTip="Enter the Address" TextMode="MultiLine" Rows="3"
                        runat="server"></asp:TextBox>
                        <sup>Special characters are not allowed in address(i.e @,#,-)</sup>
                    <asp:RequiredFieldValidator ID="rfvAddress" ValidationGroup="VldgOnlinePayment" CssClass="error" SetFocusOnError="true" runat="server" ControlToValidate="txtAddress"
                        Display="Dynamic">
                Address cannot be blank
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revAddress" ValidationExpression="^[a-zA-Z0-9\s]+$" ValidationGroup="VldgOnlinePayment" CssClass="error" SetFocusOnError="true" runat="server" ControlToValidate="txtAddress"
                        Display="Dynamic">
              Special Character not allowed in Address
                    </asp:RegularExpressionValidator>
                </li>
                <li>
                    <label>
                        Pin Code</label>
                    <asp:TextBox ID="txtPincode" ToolTip="Enter the Pincode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPincode" ValidationGroup="VldgOnlinePayment" SetFocusOnError="true" CssClass="error" runat="server" ControlToValidate="txtPincode"
                        Display="Dynamic">
                Pin Code cannot be blank
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPincode" ValidationExpression="^\d{6}$" ValidationGroup="VldgOnlinePayment" CssClass="error" SetFocusOnError="true" runat="server" ControlToValidate="txtPincode"
                        Display="Dynamic">
              Pin Code not in correct format(e.g. 201221)
                    </asp:RegularExpressionValidator>
                </li>
                <li>
                    <label>
                        Enter State</label>
                    <asp:TextBox ID="txtState" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvState" ValidationGroup="VldgOnlinePayment" CssClass="error" SetFocusOnError="true" runat="server" ControlToValidate="txtState"
                        Display="Dynamic">
                State Name cannot  be blank
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Enter City</label>
                   <asp:TextBox ID="txtCity" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCity" ValidationGroup="VldgOnlinePayment" CssClass="error" SetFocusOnError="true" runat="server" ControlToValidate="txtCity"
                         Display="Dynamic">
                City Name cannot  be blank
                    </asp:RequiredFieldValidator>
                </li>
              
            </ul>

            
        </fieldset>
            <fieldset id="CashPayment">
                <legend>Instruction:Pay By Cash</legend>
                <ol class="horizontal">
                    <li>You will need to deposit Rs.
                            <asp:Label runat="server" ID="lblCash3"></asp:Label> in the nearest HDFC BANK in the following account. </li>
                    <li><b>Account Name: Admissionjankari.com</b><br />
                        <b>Account No: 00032 0000 44418</b>  <br />
                        <b>RTGS/IFSC/NEFT Code: HDFC0000003  </b> 
                         <b>Branch:Kasturba Gandhi Marg,New Delhi </b>  </li>
                    <li>Mention your Reference id(Application form number), Name, Phone No and Email id at the back of the pay-in-slip.</li>
                    <li>To confirm the payment, please send your pay-in-slip at the following address (Via Speed/Registered Post):
                    </li>
                    <li class="addressStyle">Admissionjankari.com<br />
                        74, Amrit Chamber, 2nd floor,<br />
                        202-204 Scindia House Connaught Place,<br />
                        New Delhi-110001.<br />
                        Contact us : +91-11-43391978, +91-8800567711, +91-8800567733</li>
                </ol>
            </fieldset>
            <fieldset id="DDpayment">
                <legend>Instruction:Pay By DD</legend>
                <ol>
                    <li><b>Make a single Demand Draft</b> (DD) of Rs.
                            <asp:Label runat="server" ID="lblCash2"></asp:Label> in favor of <b>"Admissionjankari.com"</b> payable at <b>"New Delhi"
                        </li>
                        <li>Mention your Reference id(Application form number), Name, Phone No and Email id at the back of the Demand Draft.</li>
                    <li>To confirm the payment, please send your Demand Draft at the following address (Via Speed/Registered Post): </li>
                    <li class="addressStyle">Admissionjankari.com
                        <br />
                        74, Amrit Chamber, 2nd floor,
                        <br />
                        202-204 Scindia House Connaught Place,
                        <br />
                        New Delhi-110001.
                        <br />
                      Contact us : +91-11-43391978, +91-8800567711, +91-8800567733 </li>
                    <li style="margin-top: 10px;">
                        <img alt="DD Payment Mode" src="/image.axd?Common=DDFront.jpg" width="700" height="382" /></li>
                </ol>
                </b>
            </fieldset>
            <fieldset id="chequePayemnt">
                <legend>Instruction:Pay By Cheque</legend>
                <ol>
                    <li>Please make an account payee cheque of Rs.
                       <asp:Label runat="server" ID="lblCash1"></asp:Label>
                        in favor of <b>"Admissionjankari.com"</b> 
                    </li>
                    <li>Mention your Reference Id(Application form number), Name, Phone No and Email id at the back of the cheque.
                    </li>
                   <li>To confirm the payment, please send your cheque at the following address (Via Speed/Registered Post): </li>
                    <li class="addressStyle">Admissionjankari.com
                        <br />
                        74, Amrit Chamber, 2nd floor,
                        <br />
                        202-204 Scindia House Connaught Place,
                        <br />
                        New Delhi-110001.
                        <br />
                        Contact us : +91-11-43391978, +91-8800567711, +91-8800567733 </li>
                    <li style="margin-top: 10px;">
                        <img src="/image.axd?Common=Checks.jpg" alt="Check Payment Mode" width="700" height="382" />
                    </li>
                </ol>
                </b>
            </fieldset>
          
                 <center> <%--<asp:Button ID="btnFinish" CssClass="button" runat="server"  Text="Finish"  
                         onclick="btnFinish_Click" />--%></center>
               
            <input id="Merchant_Id" type="hidden" name="Merchant_Id" runat="server" />
            <input id="Amount" type="hidden" name="Amount" runat="server" />
            <input id="Order_Id" type="hidden" name="Order_Id" runat="server" />
            <input id="Redirect_Url" type="hidden" name="Redirect_Url" runat="server" />
            <input id="Checksum" type="hidden" name="Checksum" runat="server" />
            <input id="billing_cust_name" type="hidden" name="billing_cust_name" runat="server" />
            <input id="billing_cust_address" type="hidden" name="billing_cust_address" runat="server" />
            <input id="billing_cust_state" type="hidden" name="billing_cust_state" runat="server" />
            <input id="billing_cust_country" type="hidden" name="billing_cust_country" runat="server" />
            <input id="billing_cust_tel" type="hidden" name="billing_cust_tel" runat="server" />
            <input id="billing_cust_email" type="hidden" name="billing_cust_email" runat="server" />
            <input id="delivery_cust_name" type="hidden" name="delivery_cust_name" runat="server" />
            <input id="delivery_cust_address" type="hidden" name="delivery_cust_address" runat="server" />
            <input id="delivery_cust_state" type="hidden" name="delivery_cust_state" runat="server" />
            <input id="delivery_cust_country" type="hidden" name="delivery_cust_country" runat="server" />
            <input id="delivery_cust_tel" type="hidden" name="delivery_cust_tel" runat="server" />
            <input id="billing_cust_city" type="hidden" name="billing_cust_city" runat="server" />
            <input id="billing_zip_code" type="hidden" name="billing_zip_code" runat="server" />
            <input id="delivery_cust_city" type="hidden" name="delivery_cust_city" runat="server" />
            <input id="delivery_zip_code" type="hidden" name="delivery_zip_code" runat="server" />
            
            </div>
            <div class="clearBoth"></div>
            </asp:WizardStep>
                          </WizardSteps>
                        <HeaderTemplate>
                            <ul id="wizHeader">
                                <asp:Repeater ID="SideBarList" runat="server">
                                    <ItemTemplate>
                                        <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                            <%# Eval("Name")%></a> </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </HeaderTemplate>
                         <FinishNavigationTemplate>
                <asp:Button ID="FinishPreviousButton" runat="server" CssClass="button"
                 CommandName="MovePrevious" Text="Previous" />
                <asp:Button ID="FinishButton" runat="server"  CssClass="button"
                CommandName="MoveComplete" Text="Finish" />
                </FinishNavigationTemplate>
                    </asp:Wizard>              
    </div>
      <script type="text/javascript" defer="defer">
    
          $(document).ready(function () {
              CheckStatus();
              $('#<%= rbtnPaymentType.ClientID %>').change(function () {

                  CheckStatus();
              });
          });
          function pageLoad(sender, args) {
              if (args.get_isPartialLoad()) {
                  CheckStatus();

                  $('#<%= rbtnPaymentType.ClientID %>').change(function () {

                      CheckStatus();
                  });
              }
          }
          function CheckStatus() {
              var rblSelectedValue = $("#<%= rbtnPaymentType.ClientID %> input:checked");
              var radio_value = rblSelectedValue.val();
              if (radio_value == 'OnPayment') {
                  $("#PaymentOnline").show("slow");
                  $("#CashPayment").hide();
                  $("#DDpayment").hide();
                  $("#chequePayemnt").hide();
              }
              else if (radio_value == '2') {
                  $("#CashPayment").show("slow");
                  $("#DDpayment").hide();
                  $("#PaymentOnline").hide();
                  $("#chequePayemnt").hide();
              }
              else if (radio_value == "1") {
                  $("#DDpayment").show("slow");
                  $("#CashPayment").hide();
                  $("#PaymentOnline").hide();
                  $("#chequePayemnt").hide();
              }
              else if (radio_value == "0") {
                  $("#chequePayemnt").show("slow");
                  $("#DDpayment").hide();
                  $("#CashPayment").hide();
                  $("#PaymentOnline").hide();
              }
          }


          function PostDForm() {
              
              var form = document.createElement("form");
              form.setAttribute("method", "post");
              form.setAttribute("action", "https://www.ccavenue.com/shopzone/cc_details.jsp");
             
              
              var hiddenField = document.createElement("input");
              hiddenField.setAttribute("name", "Order_Id");
              hiddenField.setAttribute("value", $("#<%= Order_Id.ClientID %>").val());
              form.appendChild(hiddenField);

              var hiddenField1 = document.createElement("input");
              hiddenField1.setAttribute("name", "Amount");
              hiddenField1.setAttribute("value", $("#<%= Amount.ClientID %>").val());
              form.appendChild(hiddenField1);


              var hiddenField2 = document.createElement("input");
              hiddenField2.setAttribute("name", "Merchant_Id");
              hiddenField2.setAttribute("value", $("#<%= Merchant_Id.ClientID %>").val());
              form.appendChild(hiddenField2);

              var hiddenField3 = document.createElement("input");
              hiddenField3.setAttribute("name", "billing_cust_name");
              hiddenField3.setAttribute("value", $("#<%= billing_cust_name.ClientID %>").val());
              form.appendChild(hiddenField3);

              var hiddenField4 = document.createElement("input");
              hiddenField4.setAttribute("name", "billing_cust_address");
              hiddenField4.setAttribute("value", $("#<%= billing_cust_address.ClientID %>").val());
              form.appendChild(hiddenField4);

              var hiddenField5 = document.createElement("input");
              hiddenField5.setAttribute("name", "billing_cust_country");
              hiddenField5.setAttribute("value", $("#<%= billing_cust_country.ClientID %>").val());
              form.appendChild(hiddenField5);

              var hiddenField6 = document.createElement("input");
              hiddenField6.setAttribute("name", "billing_cust_tel");
              hiddenField6.setAttribute("value", $("#<%= billing_cust_tel.ClientID %>").val());
              form.appendChild(hiddenField6);

              var hiddenField7 = document.createElement("input");
              hiddenField7.setAttribute("name", "billing_cust_email");
              hiddenField7.setAttribute("value", $("#<%= billing_cust_email.ClientID %>").val());
              form.appendChild(hiddenField7);

              var hiddenField8 = document.createElement("input");
              hiddenField8.setAttribute("name", "billing_zip_code");
              hiddenField8.setAttribute("value", $("#<%= billing_zip_code.ClientID %>").val());
              form.appendChild(hiddenField8);

              var hiddenField9 = document.createElement("input");
              hiddenField9.setAttribute("name", "billing_cust_state");
              hiddenField9.setAttribute("value", $("#<%= billing_cust_state.ClientID %>").val());
              form.appendChild(hiddenField9);

              var hiddenField10 = document.createElement("input");
              hiddenField10.setAttribute("name", "billing_cust_city");
              hiddenField10.setAttribute("value", $("#<%= billing_cust_city.ClientID %>").val());
              form.appendChild(hiddenField10);

              var hiddenField11 = document.createElement("input");
              hiddenField11.setAttribute("name", "billing_cust_notes");
              hiddenField11.setAttribute("value", "");
              form.appendChild(hiddenField11);

              var hiddenField12 = document.createElement("input");
              hiddenField12.setAttribute("name", "delivery_cust_name");
              hiddenField12.setAttribute("value", "");
              form.appendChild(hiddenField12);

              var hiddenField13 = document.createElement("input");
              hiddenField13.setAttribute("name", "delivery_cust_address");
              hiddenField13.setAttribute("value", "");
              form.appendChild(hiddenField13);

              var hiddenField14 = document.createElement("input");
              hiddenField14.setAttribute("name", "delivery_cust_tel");
              hiddenField14.setAttribute("value", "");
              form.appendChild(hiddenField14);

              var hiddenField15 = document.createElement("input");
              hiddenField15.setAttribute("name", "delivery_zip_code");
              hiddenField15.setAttribute("value", "");
              form.appendChild(hiddenField15);

              var hiddenField16 = document.createElement("input");
              hiddenField16.setAttribute("name", "delivery_cust_state");
              hiddenField16.setAttribute("value", "");
              form.appendChild(hiddenField16);

              var hiddenField17 = document.createElement("input");
              hiddenField17.setAttribute("name", "delivery_cust_city");
              hiddenField17.setAttribute("value", "");
              form.appendChild(hiddenField17);


              var hiddenField18 = document.createElement("input");
              hiddenField18.setAttribute("name", "Checksum");
              hiddenField18.setAttribute("value", $("#<%= Checksum.ClientID %>").val());
              form.appendChild(hiddenField18);

              var hiddenField19 = document.createElement("input");
              hiddenField17.setAttribute("name", "Redirect_Url");
              hiddenField17.setAttribute("value", $("#<%= Redirect_Url.ClientID %>").val());
              form.appendChild(hiddenField19);

              document.body.appendChild(form);
              form.submit();
          }
      </script>

   