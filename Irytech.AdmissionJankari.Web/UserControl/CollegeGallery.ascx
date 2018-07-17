<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeGallery.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeGallery" %>
<div class="box1" id="item1" style="background-color:#f1f4f8;">
<h3 >GALLERY</h3>
 <div id="features">
    <asp:Repeater ID="rptCollegeImageGallery" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div> 
             <img src='<%# String.Format("{0}{1}","/image.axd?CollegeGallery=",string.IsNullOrEmpty(Eval("CollegeBranchGalleryImageName").ToString()) ?"NoImage.jpg":Eval("CollegeBranchGalleryImageName")) %>'  style="cursor: pointer" border="0" width="500px" height="230px" alt='<%# Eval("CollegeBranchGalleryImageName") %>' height="40px" width="40px" /></a>
                <div style="position: relative; top: -10px; opacity: 0.83; background-color: Black; padding-top:6px; padding-left:10px; width:500px; height:40px;"
                        >
                        <%# Eval("CollegeBranchGalleryImageTitle")%>
                    </div>
                </a>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</div>
</div>
<script src="/Js/jquery.jshowoff.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#features').jshowoff();

    });
</script>
<style>
#features, #slidingFeatures, #labelFeatures, #basicFeatures, #thumbFeatures {
	background: #efefef;
	position: relative;
	overflow: hidden;
	width: 100%;
	height: 270px;
	-webkit-border-top-left-radius: 6px;
	-webkit-border-top-right-radius: 6px;
	-moz-border-radius-topleft: 6px;
	-moz-border-radius-topright: 6px;
	padding-left:2px;
	}
.jshowoff {
	width: 100%;
	margin: 10px 0;
	}
.jshowoff div {	
	width: auto;
	height: 270px;
	}
.jshowoff div, .jshowoff img, .jshowoff {
	-webkit-border-top-left-radius: 6px;
	-webkit-border-top-right-radius: 6px;
	}
#basicFeatures, .jshowoff.basicFeatures, .jshowoff.basicFeatures img, .jshowoff.basicFeatures div {
	-webkit-border-radius: 0;
	-moz-border-radius: 0;
	}
.jshowoff div p, .jshowoff div h2 {
	_background-color: #efefef;
	}
.jshowoff h2, .jshowoff p {
	font-size: 18px;
	padding: 15px 20px 0px;
	margin: 0;
	}
.jshowoff p {
	font-size: 13px;
	line-height: 15px;
	}
.eddie {
	float: right;
	padding: 15px 20px 15px 20px;
	}
.jshowoff p.jshowoff-slidelinks {
	position: absolute;
	bottom: 5px;
	right: 50px;
	margin: 0;
	padding: 0;
	}
.jshowoff-slidelinks a, .jshowoff-controls a {
	display: block;
	background-color: #000;
	color: #fff;
	padding: 5px 7px 5px;
	margin: 5px 0 0 5px;
	float: left;
	text-decoration: none;
	-moz-border-radius: 4px;
	-webkit-border-radius: 4px;
	outline: none;
	font-size: 11px;
	line-height: 14px;
	}
.jshowoff-slidelinks a:hover, .jshowoff-controls a:hover {
	color: #fff;
	}
.jshowoff-slidelinks a.jshowoff-active, .jshowoff-slidelinks a.jshowoff-active:hover {
	background-color: #fff;
	color: #000;
	}
p.jshowoff-controls {
	background: #aaa;
	overflow: auto;
	height: 1%;
	padding: 0 0 5px 2px;
	margin: 0 0 0 1px;
	-moz-border-radius-bottomleft: 2px;
	-moz-border-radius-bottomright: 2px;
	-webkit-border-bottom-left-radius: 2px;
	-webkit-border-bottom-right-radius: 2px;
	width:100%;
	}
.jshowoff-controls a {
	margin: 5px 5px 0 0;
	font-size: 12px;
	line-height: 15px;
	padding: 4px 8px 5px;
	}

.jshowoff-pausetext {
	color: #fff;
	}


/*-- Re-styled Thumbnail Demo --*/

.thumbFeatures p.jshowoff-slidelinks {
	background: #000;
	bottom: 0;
	padding: 5px 0 5px 5px;
	right: 113px;
	height: 32px;
	}
.thumbFeatures .jshowoff-slidelinks a {
	display: block;
	width: 60px;
	height: 30px;
	background-color: none;
	background-repeat: no-repeat;
	margin: 0 5px 0 0;
	padding: 0;
	border: 1px solid #4f4f4f;
	text-indent: -10000em;
	-moz-border-radius: 0;
	-webkit-border-radius: 0;
	}
.thumbFeatures .jshowoff-slidelinks a.jshowoff-active {
	border: 1px solid #fff;
	}
.thumbFeatures .jshowoff-slidelink-0 {
	background-image: url(http://farm5.static.flickr.com/4065/4439060414_c11002d183_o_d.jpg);
	}
.thumbFeatures .jshowoff-slidelink-1 {
	background-image: url(http://farm5.static.flickr.com/4049/4438283469_5ddf465356_o_d.jpg);
	}
.thumbFeatures .jshowoff-slidelink-2 {
	background-image: url(http://farm5.static.flickr.com/4033/4439060472_02efbb3955_o_d.jpg);
	}
.thumbFeatures .jshowoff-slidelink-3 {
	background-image: url(http://farm5.static.flickr.com/4041/4438283519_4f08cb4a57_o_d.jpg);
	}
.thumbFeatures p.jshowoff-controls {
	background: none;
	height: 38px;
	overflow: visible;
	padding: 0;
	position: absolute;
	top: 100px;
	width: 100%;
	z-index: 150;
	}
.thumbFeatures .jshowoff-controls a {
	display: block;
	width: 22px;
	height: 38px;
	background: none;
	background-repeat: no-repeat;
	margin: 0;
	padding: 0;
	text-indent: -10000em;
	position: absolute;
	}

.thumbFeatures .jshowoff-controls a.jshowoff-prev {
	left: 15px;
	background-image: url(http://farm5.static.flickr.com/4035/4438728886_fd55756fc5_o_d.gif);
	}
.thumbFeatures .jshowoff-controls a.jshowoff-next {
	right: 15px;
	background-image: url(http://farm3.static.flickr.com/2743/4438728872_07e935da40_o_d.gif);
	}
.thumbFeatures .jshowoff-controls a.jshowoff-play {
	display: none;
	}
.jshowoff.thumbFeatures {
	height: 270px;
	}
.jshowoff.thumbFeatures div, .jshowoff.thumbFeatures img, .jshowoff.thumbFeatures {
	-webkit-border-radius: 6px;
	}
#jquery-overlay {
	position: absolute;
	top: 0;
	left: 0;
	z-index: 90;
	width: 100%;
	height: 500px;
}
#jquery-lightbox {
	position: absolute;
	top: 0;
	left: 0;
	width: 100%;
	z-index: 100;
	text-align: center;
	line-height: 0;
}
#jquery-lightbox a img { border: none; }
#lightbox-container-image-box {
	position: relative;
	background-color: #fff;
	width: 250px;
	height: 250px;
	margin: 0 auto;
}
#lightbox-container-image { padding: 10px; }
#lightbox-loading {
	position: absolute;
	top: 40%;
	left: 0%;
	height: 25%;
	width: 100%;
	text-align: center;
	line-height: 0;
}
#lightbox-nav {
	position: absolute;
	top: 0;
	left: 0;
	height: 100%;
	width: 100%;
	z-index: 10;
}
#lightbox-container-image-box > #lightbox-nav { left: 0; }
#lightbox-nav a { outline: none;}
#lightbox-nav-btnPrev, #lightbox-nav-btnNext {
	width: 49%;
	height: 100%;
	zoom: 1;
	display: block;
}
#lightbox-nav-btnPrev { 
	left: 0; 
	float: left;
}
#lightbox-nav-btnNext { 
	right: 0; 
	float: right;
}
#lightbox-container-image-data-box {
	font: 10px Verdana, Helvetica, sans-serif;
	background-color: #fff;
	margin: 0 auto;
	line-height: 1.4em;
	overflow: auto;
	width: 100%;
	padding: 0 10px 0;
}
#lightbox-container-image-data {
	padding: 0 10px; 
	color: #666; 
}
#lightbox-container-image-data #lightbox-image-details { 
	width: 70%; 
	float: left; 
	text-align: left; 
}	
#lightbox-image-details-caption { font-weight: bold; }
#lightbox-image-details-currentNumber {
	display: block; 
	clear: left; 
	padding-bottom: 1.0em;	
}			
#lightbox-secNav-btnClose {
	width: 66px; 
	float: right;
	padding-bottom: 0.7em;	
}
#lightbox img{ width: 500px; height: 400px;}
.adv_CorvesStrip_Header
{	
-moz-border-radius-bottomleft:5px;
-moz-border-radius-bottomright:5px;
-moz-border-radius-topleft:5px;
-moz-border-radius-topright:5px;

-webkit-border-radius-bottomleft:5px;
-webkit-border-radius-bottomright:5px;
-webkit-border-radius-topleft:5px;
-webkit-border-radius-topright:5px;

background-color:#efffec;
border:1px solid #2e8d1c;
padding-left:2px;
padding-right:2px;
border-radius: 5px;
height:28px;
font-size:12px;
}</style>
