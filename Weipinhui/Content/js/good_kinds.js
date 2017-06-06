// 左侧导航栏位置变换及其背景色改变
$(window).on("scroll",function(){
if($(this).scrollTop()>500){
	$(".left_kinds").css("top","150px");
	$(".kind-nav-list li").eq(0).css("background","");
	$(".kind-nav-list li").eq(1).css("background","#f10180");
}
else{
	$(".left_kinds").css("top","20px");
	$(".kind-nav-list li").eq(1).css("background","");
	$(".kind-nav-list li").eq(0).css("background","#f10180");
}
})
//	li改变颜色
	$(".kind-nav-list li").click(function(){
		for(var i=0;i<$(".kind-nav-list li").length;i++){
			$(".kind-nav-list li").eq(i).css("background","");
		}
		$(this).css("background","#ed4177");
	})

