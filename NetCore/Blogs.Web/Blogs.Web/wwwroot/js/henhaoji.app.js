var _hmt = _hmt || [];

var rator = {
	registerRatingButtons: function () {
		$(".btn-ratings").click(function () {
			var n = $(this).data("postid");
			$.getJSON("/like/" + n, function (n) {
				if (n.isSuccess) {
					var t = parseInt($(".likehits-num").text(), 10);
					$(".likehits-num").html(++t);
					$(".btn-ratings").attr("disabled", "disabled");
					$(".ratemessage").show()
				} else alert(n.message)
			})
		})
	}
};

(function () {
	//baidu
	var hm = document.createElement("script");
	hm.src = "https://hm.baidu.com/hm.js?80fb7adcf09e179855b314a2f2c794de";
	var s = document.getElementsByTagName("script")[0];
	s.parentNode.insertBefore(hm, s);

	var bp = document.createElement('script');
	var curProtocol = window.location.protocol.split(':')[0];
	if (curProtocol === 'https') {
		bp.src = 'https://zz.bdstatic.com/linksubmit/push.js';
	}
	else {
		bp.src = 'http://push.zhanzhang.baidu.com/push.js';
	}
	var s = document.getElementsByTagName("script")[0];
	s.parentNode.insertBefore(bp, s);
	//360
	//var src = (document.location.protocol == "http:") ? "http://js.passport.qihucdn.com/11.0.1.js?fec170ad4760d4f9221b8542112c3156" : "https://jspassport.ssl.qhimg.com/11.0.1.js?fec170ad4760d4f9221b8542112c3156";
	//document.write('<script src="'+src+'" id="sozz"><\/script>');

	$("img").lazyload({
		effect: "fadeIn"
	});

	$.ajax({
		url: "/Blog/GetCategories",
		type: "get",
		success: function (res) {
			var html = '';//'<li><a href="/blog/index">ËùÓÐÎÄµµ</a></li>';
			for (var i = 0; i < res.length; i++) {
				html += '<li><a href="/blog/category/' + res[i].categoryID + '">' + res[i].categoryName + '</a></li>';
			}

			$("#categories").append(html);
		}
	})

	rator.registerRatingButtons();
})();


window.dataLayer = window.dataLayer || [];
function gtag() { dataLayer.push(arguments); }
gtag('js', new Date());

gtag('config', 'UA-145345348-1');

//×ÉÑ¯
var trackOutboundLink = function (category, action, label, value) {
	if (!value) {
		value = window.location.href;
	}
	_hmt.push(['_trackEvent', category, action, 'ÍøÖ·', label]);
	gtag('event', category, {
		'event_category': action,
		'event_label': 'ÍøÖ·',
		'value': value
	});
}
//ËÑË÷
var searchContent = function (type) {
	var content;
	if (type == 'mobile')
		content = $("#search-mobile").val();
	else
		content = $("#search").val();

	gtag('event', "search", {
		'search_term': content
	});

	_hmt.push(['_trackEvent', 'search', content]);

	return true;
}