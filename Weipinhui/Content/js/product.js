
$(function(){
	
	//图片放大
	$('.bg-pos').on('mouseenter mousemove mouseleave',function(e){
		if(e.type == 'mouseenter'){
			$('.product-wrap .small-img').css('display','none');
			$('.product-wrap .big-img').css('display','block');
			leftX = -e.offsetX*1.5;
			topY = -e.offsetY*1.5;
			$('.product-wrap .big-img').css({'top':topY + 'px','left':leftX + 'px'});
		}
		if(e.type == 'mousemove'){
			leftX = -e.offsetX*1.5;
			topY = -e.offsetY*1.5;
			$('.product-wrap .big-img').css({'top':topY + 'px','left':leftX + 'px'});
		}
		if(e.type == 'mouseleave'){
			$('.product-wrap .small-img').css('display','block');
			$('.product-wrap .big-img').css('display','none');
		}
	});
	
	//图片选择
	$('.product-item-btn').on('click',function(){
        var index = $(this).attr('index');
        $('.product-wrap .small-img').attr('src', $(this).attr("src"));
        $('.product-wrap .big-img').attr('src', $(this).attr("src"));
	});
	
	//数量加减
	$('.freight-num-minus').on('click',function(){
		var num = parseInt($('.freight-num').html());
		if(num > 0){
			num--;
			$('.freight-num').html(num);
			if(num == 0){
				$(this).css('cursor','not-allowed');
			}
		}
	});
	
	$('.freight-num-plus').on('click',function(){
		var num = parseInt($('.freight-num').html());
		num++;
		$('.freight-num').html(num);
		if(num > 0){
			$('.freight-num-minus').css('cursor','pointer');
		}
	});
	
	//尺码选择
	$('.freight-size').on('click',function(){
		$(this).addClass('freight-size-selected').siblings().removeClass('freight-size-selected');
	});
})
