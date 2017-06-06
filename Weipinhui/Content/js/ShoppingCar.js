//var singleTotalPrice = new Array();
//var goodsTotalNum = 0;
//var goodsFreePrice = 0;
//var goodsTotalPrice = 0;
//for (var i = 0; i < $('.num').length; i++) {
//    singleTotalPrice[i] = parseInt($('.num').eq(i).html()) * parseInt($(".single-price").eq(i).html());
//    goodsTotalNum += parseInt($('.num').eq(i).html());
//    $('.single-total').eq(i).html(singleTotalPrice[i]);
//}
//for (var i = 0; i < singleTotalPrice.length; i++) {
//    goodsTotalPrice += singleTotalPrice[i];
//}
//$('.goods-total-num').html(goodsTotalPrice);
//goodsFreePrice = goodsTotalPrice - 10;
//$('.goods-free-num').html(goodsFreePrice);

$(() => {
    $(".plus").click(function () {
        var $price = $(this).parents("tr").find(".single-price");
        var $num = $(this).parent().find("span");
        var $sumRow = $(this).parents("tr").find(".single-total");
        var num = parseInt($num.text());
        num++;
        var price = parseFloat($price.text());
        var newRowSum = num * price;

        $.ajax({
            //配置
            type: "POST",
            url: "/Api/Cart/ChangeNumOfGoods",
            data: { goodsid: $(this).attr("data-goodsid"), method: "add" },
            dataType: 'json',
            async: false,//设置为同步操作就可以给全局变量赋值成功 
            success: function (res) {
                if (res.State == 1) {
                    $num.text(num);
                    $sumRow.text(newRowSum);

                    refreshSum();
                }
            }
        });
    })

    $(".minus").click(function () {
        var $price = $(this).parents("tr").find(".single-price");
        var $num = $(this).parent().find("span");
        var $sumRow = $(this).parents("tr").find(".single-total");
        var num = parseInt($num.text());
        num--;
        var price = parseFloat($price.text());
        var newRowSum = num * price;

        if (num >= 1)
        {
            $.ajax({
                //配置
                type: "POST",
                url: "/Api/Cart/ChangeNumOfGoods",
                data: { goodsid: $(this).attr("data-goodsid"), method: "red" },
                dataType: 'json',
                async: false,//设置为同步操作就可以给全局变量赋值成功 
                success: function (res) {
                    if (res.State == 1) {
                        $num.text(num);
                        $sumRow.text(newRowSum);

                        refreshSum();
                    }
                }
            });
        }
    })
})
function refreshSum()
{
    var sum=0;
    for (var i = 0; i < $(".single-total").length; i++)
    {
        sum += parseFloat($(".single-total").eq(i).text());
    }

    $("#SumPrice").text(sum);
    if (sum - 10 < 0)
    {
        $("#SumPriceDis").text(sum);
    }
    else {
        $("#SumPriceDis").text(sum-10);
    }

    $("#Count").text($(".single-total").length);
}


