using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bsensor
{
    class GoogleTripView
    {
        #region HTML

        #region html_start_script

        private const String html_start_script =

            "<!doctype html>" +
            "<html lang='en' >" +
            
            "	<head>" +
            "		<meta charset='utf-8' />" +
            "		<meta http-equiv='X-UA-Compatible' content='IE=EmulateIE11' />" +
            "		<meta name='viewport' content='initial-scale=1.0, user-scalable=no' />" +
            "       <title>Trip 167</title>" +
            "		<style>" +
            "			html, body {" +
            "			height: 100%;" +
            "			margin: 0;" +
            "			padding: 0;" +
            "			}" +
            "           #map {" +
            "               height: 100%;" +
            "			}" +
            "		</style>" +
            "	</head>" +
            
            "	<body>" +
            "		<div id='map' ></div>" +
            "       <script src='https://maps.googleapis.com/maps/api/js?sensor=false'></script>" +
            "		<script>" +
            "			var mapOptions = {" +
            "                center: new google.maps.LatLng(45.50934, -122.68149)," +
            "                zoom: 13," +
            "                mapTypeId: google.maps.MapTypeId.ROADMAP" +
            "            };" +

            "           var map = new google.maps.Map(document.getElementById('map'), mapOptions);" +
            "           var cursor_marker = null;";

        #endregion html_start_script

        #region js_function_color_coordinates
        private const String js_function_color_coordinates =
            "function color_coordinate(start, stop, color, sw) {"+
            "var rangeCoordinates = [];"+
            "var j = 0;" +

            "for (i = start; i <= stop; ++i) {" +
            "   rangeCoordinates[j] = busRouteCoords[i];" +
            "	++j;" +
            "}" +

            "var newPath = new google.maps.Polyline({" +
            "  path: rangeCoordinates," +
            "  geodesic: true," +
            "  strokeColor: color," +
            "  strokeOpacity: 1.0," +
            "  strokeWeight: sw" +
            "  });" +
            "    newPath.setMap(map);" +
            "}";
        #endregion js_function_color_coordinates

        #region js_function_set_cursor_marker
        private const String js_function_set_cursor_marker =
            "function setcursormarker(lat, lng) {" +
            "    var markerOptions = {position: new google.maps.LatLng(lat, lng)};" +
            "    if (cursor_marker != null) {" +
            "        cursor_marker.setMap(null);" +
            "    }" +
            "    cursor_marker = new google.maps.Marker(markerOptions);" +
            "    cursor_marker.setMap(map);" +
            "}";
        #endregion js_function_set_cursor_marker

        #region js_function_set_data_marker
        private const String js_function_set_data_marker =
            "function setdatamarker(lat, lng, scaleIn, color, opacity) {" +
            //"    var marker_shape = {coords: [0,0,50,50], type: \"rect\"};" +
            "    var image = 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png';" +
            "    var markerOptions = {" +
            "    position: new google.maps.LatLng(lat, lng)," +
            //"    shape: marker_shape," +
            "    icon: { " +
            "       path: google.maps.SymbolPath.CIRCLE," +
            "       scale: scaleIn," +
            //"       fillColor: 'red'," +      // red
            "       fillColor: '#000000'," +    // clear
                                                //"       fillOpacity: 0.8," +
            "       fillOpacity: 0.0," +
            "       strokeColor: color," +
            "       strokeOpacity: opacity," +
            //"       strokeWeight: 14" +
            "       }," +
            //"    icon: image," +
            "    title: \"hello world\"," +
            //"    label: \"!\"" +
            "    };" +
            "    misc_marker = new google.maps.Marker(markerOptions);" +
            "    misc_marker.setMap(map);" +
            "}";
        #endregion js_function_set_data_marker

        #region js_function_update_data_marker

        private const String js_function_update_data_marker =
            "function update_data_marker(marker_index, scaleIn, markercolor, fill_opacity, stroke_opacity, dataLabel, routecolor, routestrokeweight) {" +
            "bus_stops[marker_index].setMap(null);" +
            "    var markerOptions = {" +
            "    position: locations[marker_index]," +
            "    icon: { " +
            "       path: google.maps.SymbolPath.CIRCLE," +
            "       scale: scaleIn," +
            //"       fillColor: '#000000'," +    // clear
            "       fillColor: markercolor," +    // clear
            "       fillOpacity: fill_opacity," +
            "       strokeColor: markercolor," +
            "       strokeOpacity: stroke_opacity," +
            "       }," +
            "    title: dataLabel" +
            "    };" +
            "    bus_stops[marker_index] = new google.maps.Marker(markerOptions);" +
            "    bus_stops[marker_index].setMap(map);" +
            "}";

        #endregion js_function_update_data_marker

        #region js_function_update_route

        private const String js_function_update_route =

            "function update_route(routestrokeweight) {" +
            "    flightPath.setMap(null);" +
            "    flightPath = new google.maps.Polyline({" +
            "      path: busRouteCoords, " +
            "      geodesic: true, " +
            "      strokeColor: '#000000', " +
            "      strokeOpacity: 1.0, " +
            "      strokeWeight: routestrokeweight });" +
            "    flightPath.setMap(map);" +
            "}";

        #endregion js_function_update_route

        #region html_end_script

        private const String html_end_script =
            "		</script>" +
            "	</body>" +
            "</html>";

        #endregion html_end_script

        #endregion HTML

        private StringBuilder _html = new StringBuilder();

        public GoogleTripView(IList<LatLng> latlngs, GoogleMapSettings mapSettings)
        {
            StringBuilder html = new StringBuilder();

            _html.Append(html_start_script);
            _html.Append(js_start_stop_markers(latlngs[0].Latitude, latlngs[0].Longitude, latlngs[latlngs.Count - 1].Latitude, latlngs[latlngs.Count - 1].Longitude));
            _html.Append(js_latlngs(latlngs, mapSettings.RouteStrokeWeight));
            _html.Append(js_function_color_coordinates);
            _html.Append(js_function_set_cursor_marker);
            _html.Append(js_function_update_route);
            _html.Append(html_end_script);
        }

        public GoogleTripView(IList<LatLng> latlngs, GoogleMapSettings mapSettings, IList<LatLng> sdLatlngs)
        {
            StringBuilder html = new StringBuilder();

            _html.Append(html_start_script);
            _html.Append(js_start_stop_markers(latlngs[0].Latitude, latlngs[0].Longitude, latlngs[latlngs.Count - 1].Latitude, latlngs[latlngs.Count - 1].Longitude));
            _html.Append(js_stops_latlng_array(sdLatlngs));
            _html.Append(js_latlngs(latlngs, mapSettings.RouteStrokeWeight));
            _html.Append(js_function_color_coordinates);
            _html.Append(js_function_set_cursor_marker);
            _html.Append(js_function_update_data_marker);
            _html.Append(js_function_update_route);
            _html.Append(html_end_script);
        }

        /// <summary>
        /// Java Script for coordinate array
        /// </summary>
        /// <param name="latlngs"></param>
        /// <returns></returns>
        private string js_latlngs(IList<LatLng> latlngs, int strokeWeight, double offset = 0, string color = "#000000")
        {
            LatLng point;
            StringBuilder js = new StringBuilder();

            // generate map lat lng array
            js.Append("var busRouteCoords = [");

            for (int i = 0; i < latlngs.Count; ++i)
            {
                point = latlngs[i];
                if (i > 0)
                {
                    js.Append(", ");
                }
                // {lat: " + point.Latitude + ", lng: " + point.Longitude + "}";
                js.Append("{lat: ");
                js.Append(point.Latitude + offset);
                js.Append(",lng: ");
                js.Append(point.Longitude + offset);
                js.Append("}");
            }
            js.Append("];");

            js.Append("			var flightPath = new google.maps.Polyline({");
            js.Append("                path: busRouteCoords, ");
            js.Append("                geodesic: true, ");
            js.Append("                strokeColor: '");
            js.Append(color);
            js.Append("', ");
            js.Append("                strokeOpacity: 1.0, ");
            js.Append("                strokeWeight: ");
            js.Append(strokeWeight);
            js.Append("			  });");
            js.Append("  flightPath.setMap(map);");


            return js.ToString();
        }

        private string js_stops_latlng_array(IList<LatLng> latlngs)
        {
            LatLng point;
            StringBuilder js = new StringBuilder();

            // generate map lat lng array
            js.Append("var locations = [");

            for (int i = 0; i < latlngs.Count; ++i)
            {
                point = latlngs[i];
                if (i > 0)
                {
                    js.Append(", ");
                }
                // {lat: " + point.Latitude + ", lng: " + point.Longitude + "}";
                js.Append("{lat: ");
                js.Append(point.Latitude);
                js.Append(",lng: ");
                js.Append(point.Longitude);
                js.Append("}");
            }
            js.Append("];");

            js.Append("var bus_stops = [];");

            js.Append("for (var i = 0; i < locations.length; i++)" );
            js.Append("{                                         " );
            js.Append("    addMarker(locations[i]);              " );
            js.Append("}                                        " );

            js.Append("function addMarker(location) {");
            js.Append("    var marker = new google.maps.Marker({ position: location, map: map } );");
            //js.Append("    var marker = new google.maps.Marker({ position: location } );");
            js.Append("    bus_stops.push(marker);");
            js.Append("}");

            return js.ToString();
        }

        private string js_start_stop_markers(double lat0, double lng0, double lat1, double lng1)
        {
            StringBuilder js = new StringBuilder();

            js.Append("var markerOptions = {");
            js.AppendFormat("position: new google.maps.LatLng({0},{1}), title:'Start', label: 'S'{2}", lat0, lng0, "};");
            js.Append("var marker = new google.maps.Marker(markerOptions);");
            js.Append("marker.setMap(map);");

            js.Append("markerOptions = {");
            js.AppendFormat("position: new google.maps.LatLng({0},{1}), title:'Finish', label:'F'{2}", lat1, lng1, "};");
            js.Append("marker = new google.maps.Marker(markerOptions);");
            js.Append("marker.setMap(map);");

            return js.ToString();
        }

        public string html
        {
            get { return _html.ToString(); }
        }
    }
}
