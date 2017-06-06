/*设置sidebar长度*/
$(document).ready(function(){
    var sidebarHeight = $(window).height();
	$('#sidebar').height(sidebarHeight);
});

/* sidebar start */
$('.sidebar-item').click(function () {
	var i = $(this).index();
	if(i == 1)
	{
		$(window).scrollTop(0);
	}
});
/* sidebar end */


/* nav start */
$('.nav-addr').hover(function(){
	$('.nav-addrs').show();
	$('.nav-addrs-hide').show();
	$('.nav-addr img').attr('src','/Content/img/index/nav/arrow_hover.png');
	$(this).css({'color':'#f10180','background': '#fff','border-left':'1px solid #cdcdcd','border-right':'1px solid #cdcdcd'});
},function(){
	$('.nav-addrs').hide();
	$('.nav-addrs-hide').hide();
	$('.nav-addr img').attr('src','/Content/img/index/nav/arrow.png');
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
		$(this).css({'border-left':'1px solid #f5f5f5','border-right':'1px solid #f5f5f5'});
	}
});

/* nav end */

/* menu start */
$('.more-flag').hover(function(){
	$('.more-flag>img').attr('src','/Content/img/index/menu/arrow_hover.png');
	$('.menu-more').show();
},function(){
	$('.more-flag>img').attr('src','/Content/img/index/menu/arrow.png');
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

/* banner start */
/*slide设置计时器*/
var nowImg = 0;
var underlineWidth = $('.item-underline').innerWidth();
var n = $('.slide-image').length;
timer = setInterval("slide()",2000);

/*hover切换*/
$('.banner-list-item').hover(function(){
	clearInterval(timer);
	if($(this).index() == nowImg){
		return;
	}
	$('.slide-image').eq(nowImg).stop().fadeOut(400);
	nowImg = $(this).index();
	$('.item-underline').stop().animate({'left':nowImg*underlineWidth + 'px'},400);
	$('.slide-image').eq(nowImg).stop().fadeIn(400);
},function(){
	timer = setInterval("slide()",2000);
});

/*图片切换函数*/
function slide(){
	$('.slide-image').eq(nowImg).stop().fadeOut(400);
	
	nowImg++;
	
	if(nowImg == n){
		nowImg = 0;
	}
	$('.item-underline').stop().animate({'left':nowImg * underlineWidth + 'px'},400);
	$('.slide-image').eq(nowImg).stop().fadeIn(400);
	

}

/*左右箭头出现*/
$('.banner-image').hover(function(){
	$('.banner-left').stop().animate({'left':'0px'},300);
	$('.banner-right').stop().animate({'right':'0px'},300);
},function(){
	$('.banner-left').stop().animate({'left':'-32px'},300);
	$('.banner-right').stop().animate({'right':'-32px'},300);
});

/*触碰左箭头暂停计时器*/
$('.banner-left').hover(function(){
	clearInterval(timer);
},function(){
	timer = setInterval("slide()",2000);
});

/*触碰右箭头暂停计时器*/
$('.banner-right').hover(function(){
	clearInterval(timer);
},function(){
	timer = setInterval("slide()",2000);
});

/*上一张*/
$('.banner-left').on('click',function(){
	$('.slide-image').eq(nowImg).stop().fadeOut(400);
	nowImg--;
	if(nowImg == -1)
	{
		nowImg = n-1;
	}
	$('.slide-image').eq(nowImg).stop().fadeIn(400);
	$('.item-underline').stop().animate({'left':nowImg*underlineWidth + 'px'},400);
});

/*下一张*/
$('.banner-right').on('click',function(){
	$('.slide-image').eq(nowImg).fadeOut(400);
	nowImg++;
	if(nowImg == n)
	{
		nowImg = 0;
	}
	$('.slide-image').eq(nowImg).stop().fadeIn(400);
	$('.item-underline').stop().animate({'left':nowImg*underlineWidth + 'px'},400);
});

/* banner end */

/* leftSideBar start */

window.onscroll = function(){
	
	for(var i = 0;i < 12;i++)
	{
		if($(window).scrollTop() > 640 + 640*i && $(window).scrollTop() < 640 + 640*(i+1))
		{
			/*给leftSideBar项目切换*/
			$('.leftSideBar-list a').eq(i).css({'background':'#f10180','border-radius':'4px','color':'#fff','font-weight':'bold'});
			$('.leftSideBar-list a').eq(i).siblings().css({'background':'#fff','border-radius':'0px','color':'#666','font-weight':'200'});
			for(var j = 0;j < 12;j++)
			{
				if(i != j)
					$('.leftSideBar-list a img').eq(j).attr({'src':'/Content/img/index/leftSideBar/star.png'});
				else
					$('.leftSideBar-list a img').eq(j).attr({'src':'/Content/img/index/leftSideBar/star_selected.png'});
			}
			
			/*给leftSideBar添加一个hover事件*/
			$('.leftSideBar-list a').eq(i).off('mouseover');
			$('.leftSideBar-list a').eq(i).off('mouseout');
			$('.leftSideBar-list a').eq(i).siblings().on('mouseover mouseout',function(e){
				if(e.type == 'mouseover')
				{
					var i = $(this).index();
					$('.leftSideBar-list a img').eq(i).attr({'src':'/Content/img/index/leftSideBar/star_hover.png'});
					$(this).css({'color':'#f10180'});
				}
				else
				{
					var i = $(this).index();
					$('.leftSideBar-list a img').eq(i).attr({'src':'/Content/img/index/leftSideBar/star.png'});
					$(this).css({'color':'#666'});						
				}
			});
		}
	}
	
	if($(window).scrollTop() > 850)
	{
		$('#leftSideBar').css({'position':'fixed','top':'112px'});
	}
	else{
		$('#leftSideBar').css({'position':'absolute','top':'854px'});
	}
}
/* leftSideBar end */

/* footer start */
$('.footer-middle-dl dd').hover(function(){
	$(this).stop().animate({'padding-left':'2px'},200);
},function(){
	$(this).stop().animate({'padding-left':'0px'},200);
});
/* footer end */