(function ($) {
  "use strict";
  // Main menu
  jQuery('#mobile-menu').meanmenu({
    meanMenuContainer: '.mobile-menu',
    meanScreenWidth: "991"
  });

  //language menu
  $(".language>ul>li>a").click(function(){
    $(".sub-language").toggle();
  });

  $(window).on('load', function () {
    $('#preloader').delay(350).fadeOut('slow');
    $('body').delay(350).css({ 'overflow': 'visible' });
  })


  //about hover icons
  $('.hover-items .single-hover').on('mouseenter mouseleave', function (event) {
    $(this).siblings('.active').removeClass('active');
    $(this).addClass('active');
    event.preventDefault();
  });


  // Active menu
  $(document).ready(function () {
    $('.pagination .page-item').click(function () {
      $('.page-item').removeClass("active");
      $(this).addClass("active");
    });
  });


  // map
  function basicmap() {
    // Basic options for a simple Google Map
    // For more options see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions
    var mapOptions = {
      // How zoomed in you want the map to start at (always required)
      zoom: 11,
      scrollwheel: false,
      // The latitude and longitude to center the map (always required)
      center: new google.maps.LatLng(23.810331, 90.412521), // New York
      // This is where you would paste any style found on Snazzy Maps.
      styles: [{ "featureType": "landscape", "elementType": "all", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.business", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "poi.business", "elementType": "labels", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "poi.park", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.school", "elementType": "all", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.sports_complex", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit.station.bus", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": "21" }, { "weight": "4.05" }] }]
    };
    // Get the HTML DOM element that will contain your map
    // We are using a div with id="map" seen below in the <body>
    var mapElement = document.getElementById('contact-map');

    // Create the Google Map using our element and options defined above
    var map = new google.maps.Map(mapElement, mapOptions);

    // Let's also add a marker while we're at it
    var marker = new google.maps.Marker({
      position: new google.maps.LatLng(23.810331, 90.412521),
      map: map,
      title: 'Cryptox'
    });
  }
  if ($('#contact-map').length != 0) {
    google.maps.event.addDomListener(window, 'load', basicmap);
  }


})(jQuery);


