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


  // hero slider slick activation with animation
  function mainSlider() {
    var BasicSlider = $('.hero-slider');
    BasicSlider.on('init', function(e, slick) {
        var $firstAnimatingElements = $('.single-slider:first-child').find('[data-animation]');
        doAnimations($firstAnimatingElements);
    });
    BasicSlider.on('beforeChange', function(e, slick, currentSlide, nextSlide) {
        var $animatingElements = $('.single-slider[data-slick-index="' + nextSlide + '"]').find('[data-animation]');
        doAnimations($animatingElements);
    });
    BasicSlider.slick({
        autoplay: false,
        autoplaySpeed: 1100,
        dots: false,
        fade: true,
        arrows: true,
        prevArrow: '<button type="button" class="slick-prev"><i class="far fa-long-arrow-alt-left"></i></button>',
        nextArrow: '<button type="button" class="slick-next"><i class="far fa-long-arrow-alt-right"></i></button>',
        responsive: [
            { breakpoint: 767, settings: { dots: false, arrows: false } }
        ]
    });

    function doAnimations(elements) {
        var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        elements.each(function() {
            var $this = $(this);
            var $animationDelay = $this.data('delay');
            var $animationType = 'animated ' + $this.data('animation');
            $this.css({
                'animation-delay': $animationDelay,
                '-webkit-animation-delay': $animationDelay
            });
            $this.addClass($animationType).one(animationEndEvents, function() {
                $this.removeClass($animationType);
            });
        });
    }
}
mainSlider();





  $('.portfolio-slider').slick({
    dots: false,
    infinite: false,
    speed: 300,
    // autoplay:true,
    slidesToShow: 1,
    slidesToScroll: 1,
    prevArrow: '<img class="portfolio-slick-prev" src="img/icons/prev-arrow.png">',
    nextArrow: '<img class="portfolio-slick-next" src="img/icons/next-arrow-white.png">',
    responsive: [{
        breakpoint: 1024,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,
          dots: true
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });

  // Portfolio-2

  $('.portfolio-slider-3').slick({
    dots: false,
    infinite: true,
    speed: 300,
    autoplay: true,
    slidesToShow: 3,
    slidesToScroll: 1,
    prevArrow: '<img class="portfolio-3-slick-prev" src="img/icons/prev-arrow-white.png">',
    nextArrow: '<img class="portfolio-3-slick-next" src="img/icons/next-arrow-white.png">',
    responsive: [{
        breakpoint: 1680,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
          infinite: true
        }
      },
      {
        breakpoint: 1200,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 991,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });

  // Testimoniasl

  $('.testimonial-active').slick({
    dots: false,
    infinite: true,
    speed: 300,
    autoplay: true,
    slidesToShow: 1,
    slidesToScroll: 1,
    prevArrow: '<img class="testimonial-slick-prev" src="img/icons/prev-arrow-white.png">',
    nextArrow: '<img class="testimonial-slick-next" src="img/icons/next-arrow-white.png">',
    adaptiveHeight: true,
    responsive: [{
        breakpoint: 1024,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,

        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });
  //popup
  $('.features-video-btn a').magnificPopup({
    type: 'image'
  });

  $('.popup-image').magnificPopup({
    type: 'image'
  });

  $('.popup-video').magnificPopup({
    type: 'iframe'
  });

  $(".growth-progress-bar").circularProgress({
    line_width: 8,
    color: "#3763EC",
    starting_position: 0, // 12.00 o' clock position, 25 stands for 3.00 o'clock (clock-wise)
    percent: 0, // percent starts from
    percentage: true,

  }).circularProgress('animate', 45, 2000);

  $(".growth-progress-bar2").circularProgress({
    line_width: 8,
    color: "#3763EC",
    starting_position: 5, // 12.00 o' clock position, 25 stands for 3.00 o'clock (clock-wise)
    percent: 0, // percent starts from
    percentage: true,

  }).circularProgress('animate', 74, 2000);

  //about hover icons
  $('.hover-items .single-hover').on('mouseenter mouseleave', function (event) {
    $(this).siblings('.active').removeClass('active');
    $(this).addClass('active');
    event.preventDefault();
  });


  // brand-active
  $('.brand-active').slick({
    dots: false,
    arrows: false,
    infinite: true,
    speed: 100,
    autoplay: true,
    slidesToShow: 6,
    slidesToScroll: 6,
    responsive: [{
        breakpoint: 1500,
        settings: {
          slidesToShow: 5,
          slidesToScroll: 3,
          infinite: true,
          dots: false
        }
      },
      {
        breakpoint: 991,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 4
        }
      },
      {
        breakpoint: 575,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 2
        }
      }
    ]
  });

  // counter-up

  $('.counter-up').each(function () {
    $(this).prop('Counter', 0).animate({
      Counter: $(this).text()
    }, {
      duration: 3100,
      easing: 'swing',
      step: function (now) {
        $(this).text(Math.ceil(now));
      }
    });
  });
  // News Active
  $('.news-active').slick({
    dots: false,
    infinite: true,
    speed: 300,
    autoplay: true,
    slidesToShow: 1,
    slidesToScroll: 1,
    nextArrow: '<i class="fa fa-arrow-right news-slick news-slick-next"></i>',
    prevArrow: '<i class="fa fa-arrow-left news-slick news-slick-prv"></i>',
    adaptiveHeight: true,
    responsive: [{
        breakpoint: 1024,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,

        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });

  // Active menu
  $(document).ready(function () {
    $('.pagination .page-item').click(function () {
      $('.page-item').removeClass("active");
      $(this).addClass("active");
    });
  });


  //about page counters
  $('.fact-counter').counterUp();


  //team slider on team page

  $('.team-slider-items .row').slick({
    dots: true,
    arrows: false,
    infinite: true,
    // autoplay: true,
    autoplaySpeed: 3000,
    slidesToShow: 3,
    slidesToScroll: 1,
    responsive: [{
        breakpoint: 1200,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
          infinite: true,
        }
      },
      {
        breakpoint:768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });

  // service details page sidebar contact form select
  if ($('#option').length != 0) {
    $('#option').niceSelect();
  }



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


