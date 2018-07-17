<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeAvailableFacailties.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeAvailableFacailties" %>
 <div class="box1">
 <h3 class="streamCompareH3">Facalities</h3>
 <hr class="hrline" />
 <div class="box">
 <asp:Repeater ID="rptFacilities" runat="server" >
                            <ItemTemplate>
                                <div class="accordion">
                                
	                                  <h3 class="accord"><%#Eval("CollegeBranchCourseFacilitieName")%></h3>
	                             <p><%#Eval("CollegeBranchCourseFacilitieDesc")%></p>
                                    </span>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                            </div>
                            
<script type="text/javascript" defer="defer">
    $(document).ready(function () {
        $(".accordion h3:first").addClass("active");
        $(".accordion p:not(:first)").hide();
        $(".accordion h3").click(function () {
            $(this).next("p").slideToggle("slow")
		.siblings("p:visible").slideUp("slow");
            $(this).toggleClass("active");
            $(this).siblings("h3").removeClass("active");
        });
    });

</script>