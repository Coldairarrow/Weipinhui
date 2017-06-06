/* nav start */

$('.nav-addr').hover(function(){
	$('.nav-addrs').show();
	$('.nav-addrs-hide').show();
	$('.nav-addr img').attr('src','/Content/img/nav/arrow_hover.png');
	$(this).css({'color':'#f10180','background': '#fff','border-left':'1px solid #cdcdcd','border-right':'1px solid #cdcdcd'});
},function(){
	$('.nav-addrs').hide();
	$('.nav-addrs-hide').hide();
	$('.nav-addr img').attr('src','/Content/img/nav/arrow.png');
	$(this).css({'color':'#777','background': '#f5f5f5','border-left':'1px solid #f5f5f5','border-right':'1px solid #f5f5f5'});
});

$('.nav-addrs table tr td').click(function(){
	$('.nav-choice').html($(this).html());
	$('.nav-addrs').hide();
});

$('.nav-list-item').hover(function(){
	var i = $(this).index();
	if(i==0)
	{
		$('.nav-login').show();
		$('.nav-login-hide').show();
		$(this).css({'background': '#fff','border-left':'1px solid #cdcdcd','border-right':'1px solid #cdcdcd'});
	}
},function(){
	var i = $(this).index();
	if(i==0)
	{
		$('.nav-login').hide();
		$('.nav-login-hide').hide();
		$(this).css({'background': '#f5f5f5','border-left':'1px solid #f5f5f5','border-right':'1px solid #f5f5f5'});
	}
});

$('.menu-item').on('click', function () {
	if(!$(this).hasClass('menu-active')){
		$(this).addClass('menu-active').siblings().removeClass('menu-active');
		$(this).removeClass('menu-hover');
		for(var i = 0;i < $('.menu-sort-item').length;i++){
			$('.menu-sort-item').eq(i).removeClass('menu-active');
		}
	}
});

$('.menu-item').hover(function(){
	if(!$(this).hasClass('menu-active')){
		$(this).addClass('menu-hover').siblings().removeClass('menu-hover');
		$(this).find('a').addClass('menu-hover-a').siblings().removeClass('menu-hover-a');
	}
},function(){
	$(this).removeClass('menu-hover');
	$(this).removeClass('menu-hover-a');
});

$('.menu-sort-item').on('click',function(){
	if(!$(this).hasClass('menu-active')){
		$(this).addClass('menu-active').siblings().removeClass('menu-active');
		$(this).removeClass('menu-hover');
	}
	for(var i = 0;i < $('.menu-item').length;i++){
		$('.menu-item').eq(i).removeClass('menu-active');
	}
});

$('.menu-sort-item').hover(function(){
	if(!$(this).hasClass('menu-active')){
		$(this).addClass('menu-hover').siblings().removeClass('menu-hover');
		$(this).find('a').addClass('menu-hover-a').siblings().removeClass('menu-hover-a');
	}
},function(){
	$(this).removeClass('menu-hover');
	$(this).removeClass('menu-hover-a');
});

/* nav end */

/* menu start */

$('.more-flag').hover(function(){
	$('.more-flag>img').attr('src','/Content/img/menu/arrow_hover.png');
	$('.menu-more').show();
},function(){
	$('.more-flag>img').attr('src','/Content/img/menu/arrow.png');
	$('.menu-more').hide();
});

$('.menu-more-item').hover(function(){
	var i = $(this).index();
	$('.shadow').eq(i).stop().animate({'top':'0px','line-height':'96px'},400);
},function(){
	var i = $(this).index();
	$('.shadow').eq(i).stop().animate({'top':'38px','line-height':'80px'},400);
});
/* menu end */

//window.onscroll = function () {
//    alert("sss");
//}
$(document).on('scroll', function () {
    /* menu start */
    alert("ss");
    if ($(window).scrollTop() > 168) {
        if (!$('#menu').hasClass('menu-fix')) {
            $('#menu').addClass('menu-fix');
            $('#menu').animate({ 'top': '0px' }, 500);
        }
    }
    else {
        if ($('#menu').hasClass('menu-fix')) {
            $('#menu').css({ 'top': '-40px' });
            $('#menu').removeClass('menu-fix');
        }

    }
	/* menu end */

})
//alert(window.onscroll);

