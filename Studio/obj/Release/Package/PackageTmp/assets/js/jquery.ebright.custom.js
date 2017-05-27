$(document).ready(function () {

    // Fancybox
    $(".theater").fancybox();
    // Fancybox	
    //	$(".ext-source").fancybox({
    //		'transitionIn'		: 'none',
    //		'transitionOut'		: 'none',
    //		'autoScale'     	: false,
    //		'type'				: 'iframe',
    //		'width'				: '50%',
    //		'height'			: '60%',
    //		'scrolling'   		: 'no'
    //	});
    // Scroll to top
    $().UItoTop({ easingType: 'easeOutQuart' });
    //Animate hover slide
    $(".animate-hover-slide figure").each(function () {
        var animateImgHeight = $(this).find("img").height();
        $(this).find("figcaption").css({ "height": animateImgHeight + "px" });
    });
    // Search function
    $("#cmdSearch, #cmdSearchCollapse").click(function () {
        $("#divSearch").fadeIn(300);
        return false;
    });
    $("#cmdCloseSearch").click(function () {
        $("#divSearch").fadeOut(300);
    });

    // Keyboard shortcuts
    $('html').keyup(function (e) {
        if (e.keyCode == 27)
            if ($("#divSearch").is(':visible'))
                $("#divSearch").fadeOut(300);
    });

//    var date = new Date();
//    date.setTime(date.getTime() + (5 * 60 * 1000));
});

$(window).resize(function () {
    //Animate hover slide
    $(".animate-hover-slide figure").each(function () {
        var animateImgHeight = $(this).find("img").height();
        $(this).find("figcaption").css({ "height": animateImgHeight + "px" });
    });
});