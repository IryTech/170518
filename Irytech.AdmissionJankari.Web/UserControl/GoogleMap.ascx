<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.GoogleMap" %>
<div class="box1 ">
<h3 class="streamCompareH3"> Road Map For The College</h3>
<hr class="hrline" />
<div class="boxPlane">
        <center><strong  id="distance_direct"></strong></center>
	    <center><strong  id="distance_road"></strong></center>
          <hr class="hrline" />
	   <div id="map_canvas"  style="height:400px; margin:5px;"></div>
        <hr class="hrline" />
        <ul class="box1" style="margin-left:0px;">
        <li><label class="strongDetails textalignRight">Source</label><asp:TextBox runat="server" ID="txtAddress2" Width="50%" ></asp:TextBox></li>
        <li><label class="strongDetails textalignRight">Destination</label><asp:TextBox  id="txtAddress1"  runat="server" Width="50%"  /></li>
        <li><label class="strongDetails">&nbsp;</label><input type="button" value="Show Map" class="button"  onclick="initialize();"/>
            <asp:Xml ID="Xml1" runat="server"></asp:Xml></li>
        </ul>
        
   
   </div>
    </div>
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAA5caqtlf_puNkgcj3l6TlwhR2TRoQfLJPmjdBSxS8BIPQJQz4LRSxpCxPwZ6Z99ZdsxiBr9n-v-35XA"></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
<script type="text/javascript">

    var location1;
    var location2;

    var address1;
    var address2;

    var latlng;
    var geocoder;
    var map;

    var distance;
    initialize();
    // finds the coordinates for the two locations and calls the showMap() function
    function initialize() {

        geocoder = new google.maps.Geocoder(); // creating a new geocode object

        // getting the two address values
        address1 = document.getElementById("<%=txtAddress1.ClientID %>").value

        address2 = document.getElementById("<%=txtAddress2.ClientID %>").value


        // finding out the coordinates
        if (geocoder) {
            geocoder.geocode({ 'address': document.getElementById("<%=txtAddress1.ClientID %>").value }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    //location of first address (latitude + longitude)

                    location1 = results[0].geometry.location;

                } else {
                    //alert('Please Check The College address');
                }
            });
            geocoder.geocode({ 'address': address2 }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    //location of second address (latitude + longitude)
                    location2 = results[0].geometry.location;

                    // calling the showMap() function to create and show the map 
                    showMap();
                } else {
                    alert('Please Check The Second address');
                }
            });
        }
    }

    // creates and shows the map
    function showMap() {
        // center of the map (compute the mean value between the two locations)
        latlng = new google.maps.LatLng((location1.lat() + location2.lat()) / 2, (location1.lng() + location2.lng()) / 2);

        // set map options
        // set zoom level
        // set center
        // map type
        var mapOptions =
		{
		    zoom: 1,
		    center: latlng,
		    //mapTypeId: google.maps.MapTypeId.ROADMAP
		    mapTypeId: google.maps.MapTypeId.TERRAIN

		};

        // create a new map object
        // set the div id where it will be shown
        // set the map options
        map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

        // show route between the points
        directionsService = new google.maps.DirectionsService();
        directionsDisplay = new google.maps.DirectionsRenderer(
		{
		    suppressMarkers: true,
		    suppressInfoWindows: true
		});
        directionsDisplay.setMap(map);
        var request = {
            origin: location1,
            destination: location2,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };
        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
                distance = "<span style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; color:#244e7e;'>The distance between the two points on the chosen route is: </span>" + response.routes[0].legs[0].distance.text;
                distance += "<br/><span style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; color:#244e7e;'>The aproximative driving time is: </span>" + response.routes[0].legs[0].duration.text;
                document.getElementById("distance_road").innerHTML = distance;
            }
        });

        // show a line between the two points
        var line = new google.maps.Polyline({
            map: map,
            path: [location1, location2],
            strokeWeight: 7,
            strokeOpacity: 0.8,
            strokeColor: "#FFAA00"
        });

        // create the markers for the two locations		
        var marker1 = new google.maps.Marker({
            map: map,
            position: location1,
            title: "First location"
        });
        var marker2 = new google.maps.Marker({
            map: map,
            position: location2,
            title: "Second location"
        });

        // create the text to be shown in the infowindows
        var text1 = '<div id="content">' +
				'<h1 id="firstHeading">First location</h1>' +
				'<div id="bodyContent">' +
				'<p>Coordinates: ' + location1 + '</p>' +
				'<p>Address: ' + document.getElementById("<%=txtAddress1.ClientID %>").value + '</p>' +
				'</div>' +
				'</div>';

        var text2 = '<div id="content">' +
			'<h1 id="firstHeading">Second location</h1>' +
			'<div id="bodyContent">' +
			'<p>Coordinates: ' + location2 + '</p>' +
			'<p>Address: ' + address2 + '</p>' +
			'</div>' +
			'</div>';

        // create info boxes for the two markers
        var infowindow1 = new google.maps.InfoWindow({
            content: text1
        });
        var infowindow2 = new google.maps.InfoWindow({
            content: text2
        });

        // add action events so the info windows will be shown when the marker is clicked
        google.maps.event.addListener(marker1, 'click', function () {
            infowindow1.open(map, marker1);
        });
        google.maps.event.addListener(marker2, 'click', function () {
            infowindow2.open(map, marker1);
        });

        // compute distance between the two points
        var R = 6371;
        var dLat = toRad(location2.lat() - location1.lat());
        var dLon = toRad(location2.lng() - location1.lng());

        var dLat1 = toRad(location1.lat());
        var dLat2 = toRad(location2.lat());


    }

    function toRad(deg) {
        return deg * Math.PI / 180;
    }


    $(document).ready(function () {
        initialize();

   });
</script>
