$(function () {
    /*查询栏点击*/
    $('.sort-item').on('click', function () {
        var index = $(this).index();
        $('.sort-item').eq(index).siblings().find('img').attr('src', '/Content/img/list/all.png');
        $(this).addClass('sort-selected').siblings().removeClass('sort-selected');
        var srcAdr = $(this).find('img').attr('src');

        /*降序*/
        if (flag == 1 && srcAdr == '/Content/img/list/up.png') {
            $(this).find('img').attr('src', '/Content/img/list/down.png');
            switch ($(this).text()) {
                case "价格": GetGoodsList("priceDesc"); break;
                case "折扣": GetGoodsList("discountDesc"); break;
                default: GetGoodsList(); break;
            }
            bind();
        } else {
            /*升序*/
            $(this).find('img').attr('src', '/Content/img/list/up.png');
            switch ($(this).text()) {
                case "价格": GetGoodsList("priceAsc"); break;
                case "折扣": GetGoodsList("discountAsc"); break;
                default: GetGoodsList(); break;
            }
            bind();
        }
        flag = 1;
    });

    /*查询栏触碰*/
    $('.sort-item').hover(function () {
        if (flag == 0)
            $(this).find('img').attr('src', '/Content/img/list/all_hover.png');
    }, function () {
        if (flag == 0)
            $(this).find('img').attr('src', '/Content/img/list/all.png');
    });
})
var flag = 0;

function bind() {
    /*商品背景详情触碰*/
    $('.sort-product-item').hover(function () {
        var index = $(this).index();
        $(this).addClass('item-hover').siblings().removeClass('item-hover');
        $('.sort-product-item').eq(index).children('.side-product-pic').css('display', 'block');
        for (var i = 0; i < $('.sort-product-item').length; i++) {
            if (i != index) {
                $('.sort-product-item').eq(index).children('.side-product-pic').css('display', 'none');
            }
        }
    }, function () {
        var index = $(this).index();
        $(this).removeClass('item-hover');
        $('.side-product-pic').eq(index).children('.side-product-pic').css('display', 'none');
    });

    /*图片触碰切换*/
    var iAdr;
    $('.side-product-pic img').hover(function () {
        var imgAdr = $(this).attr('src');
        iAdr = $(this).parent('.side-product-pic').parent('.sort-product-text').prev().find('img').attr('src');
        $(this).parent('.side-product-pic').parent('.sort-product-text').prev().find('img').attr('src', imgAdr);
    }, function () {
        $(this).parent('.side-product-pic').parent('.sort-product-text').prev().find('img').attr('src', iAdr);
    });

    /*商品logo触碰切换*/
    $('.brand-logo').hover(function () {
        var index = $(this).index();
        $('.brand-logo').eq(index).children('img').css('display', 'none');
        $('.brand-logo').eq(index).children('span').css('display', 'block');
        $('.brand-logo').eq(index).addClass('logo-text').siblings().removeClass('logo-text');
    }, function () {
        var index = $(this).index();
        $('.brand-logo').eq(index).children('img').css('display', 'block');
        $('.brand-logo').eq(index).children('span').css('display', 'none');
        $('.brand-logo').eq(index).removeClass('logo-text');
        });

    $(".sort-product-item").click(function () {
        var goodsid = $(this).attr("data-goodsid");
        location.href = "/Home/ProductDetails?goodsid=" + goodsid;
    })
}

