var isadd = false;
$("#buyAmount").val("0");
var amountReg = /^[1-9]*[1-9][0-9]*$/;
$(document).ready(function () {
    BeginErweima($("#hiddenProductId").val());
    $.each($(".SKUValueClass"), function () {
        $(this).bind("click", function () { SelectSkus(this); });
    });
    $("#buyAmount").bind("keyup", function () { ChangeBuyAmount(); });
    $("#buyButton").bind("click", function () {
        $("#hidBuy").val("0");
        if ($("#hiddenIsLogin").val() == "nologin") {
            $("#loginForBuy").show();
        } else {
            AddCurrentProductToCart();
        }
    }); //立即购买
    $("#addcartButton").bind("click", function () { AddProductToCart(); }); //加入购物车
    $("#imgCloseLogin").bind("click", function () { $("#loginForBuy").hide(); });
    $("#btnLoginAndBuy").bind("click", function () { LoginAndBuy(); });
    $("#btnAmoBuy").bind("click", function () { AnonymousBuy(); });
    $("#textfieldusername").keydown(function (e) {
        if (e.keyCode == 13) {
            LoginAndBuy();
        }
    });

    $("#textfieldpassword").keydown(function (e) {
        if (e.keyCode == 13) {
            LoginAndBuy();
        }
    });

});

function SelectSkus(clt) {

    // 保存当前选择的规格
    var AttributeId = $(clt).attr("AttributeId");
    var ValueId = $(clt).attr("ValueId");

    $("#skuContent_" + AttributeId).val(AttributeId + ":" + ValueId);
    $("#skuContent_" + AttributeId).attr("ValueStr", $(clt).attr("value"));
    if ($(clt).hasClass("SKUSelectValueClass")) { //原先选中
        $("#skuContent_" + AttributeId).removeAttr("ValueStr").removeAttr("value");
    }

    //重置样式
    ResetSkuRowClass("skuRow_" + AttributeId, "skuValueId_" + AttributeId + "_" + ValueId);

    // 如果全选，则重置SKU
    var allSelected = IsallSelected();
    var selectedOptions = "";
    var skuShows = "";
    var s = "";
    //  if (allSelected) {
    $.each($("input[type='hidden'][name='skuCountname']"), function () {
        if (!!$(this).attr("value"))
            selectedOptions += $(this).attr("value") + ",";
        if (!!($(this).attr("ValueStr")))
            skuShows += $(this).attr("ValueStr") + ",";
    });
    if (!skuShows) {
        $.each($("#productSkuSelector input[id^='skuContent']"), function () {

            s += "\"" + $(this).attr("attributename") + "\" ";
        });
    }
    selectedOptions = selectedOptions.substring(0, selectedOptions.length - 1);
    skuShows = skuShows.substring(0, skuShows.length - 1);
    if (!IsallSelected()) selectedOptions = "";

    $.ajax({
        url: "ShoppingHandler.aspx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: { action: "GetSkuByOptions", productId: $("#hiddenProductId").val(), options: selectedOptions },
        success: function (resultData) {
            if (resultData.Status == "OK") {
                selectShowSkuImg(resultData.SkuId);
                ResetCurrentSku(resultData.SkuId, resultData.SKU, resultData.Weight, resultData.Stock, resultData.AlertStock, resultData.SalePrice, "已选择：" + skuShows);
            }
            else if (resultData.Status == "0") {
                ResetCurrentSku2(resultData.Stock, resultData.AlertStock, "0", "已选择：" + s);
            }
            else {
                ResetCurrentSku("", "", "", "", "", "0", "请选择：" + s); //带服务端返回的结果，函数里可以根据这个结果来显示不同的信息
            }
        }
    });
    // }
}

// 是否所有规格都已选
function IsallSelected() {
    var allSelected = true;
    $.each($("input[type='hidden'][name='skuCountname']"), function () {
        if ($(this).attr("value").length == 0) {
            allSelected = false;
        }
    });
    return allSelected;
}

// 重置规格值的样式
function ResetSkuRowClass(skuRowId, skuSelectId) {
    var pvid = skuSelectId.split("_");
    var flog = $("#skuContent_" + pvid[1]).attr("value").length > 0;
    $.ajax({
        url: "ShoppingHandler.aspx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: { action: "UnUpsellingSku", productId: $("#hiddenProductId").val(), AttributeId: pvid[1], ValueId: pvid[2] },
        success: function (resultData) {
            console.log(resultData)
            if (resultData.Status == "OK") {
                $.each($("#productSkuSelector dd input, #productSkuSelector dd img"), function (index, item) {
                    var currentPid = $(this).attr("AttributeId");
                    var currentVid = $(this).attr("ValueId");
                    var currentSkuid = this.id.replace("skuValueId", "skuRow");
                    if (currentSkuid.substr(0, skuRowId.length) != skuRowId) {
                        if (!flog) {
                            if ($(item).attr("class") == "SKUUNSelectValueClass") {
                                $(item).attr("class", "SKUValueClass");
                            }
                            $(item).attr("disabled", false);
                            return true;
                        }
                        // 不同属性选择绑定事件
                        var isBind = false;
                        $.each($(resultData.SkuItems), function () {
                            if (currentPid == this.AttributeId && currentVid == this.ValueId) {
                                isBind = true;
                            }
                        });
                        if (isBind) {
                            if ($(item).attr("class") == "SKUUNSelectValueClass") {
                                $(item).attr("class", "SKUValueClass");
                            }
                            $(item).attr("disabled", false);
                        } else {
                            $(item).attr("class", "SKUUNSelectValueClass");
                            $(item).attr("disabled", true);
                        }
                    }
                });
            } else {
                $("#productSkuSelector dd input, #productSkuSelector dd img").attr("disabled", true);
            }
        }
    });
    $.each($("#" + skuRowId + " dd input,#" + skuRowId + " dd img"), function () {
        var status = $(this).attr("disabled");
        if (status != "disabled") {
            $(this).attr({ "class": "SKUValueClass" });
        }
    });
    if ($("#skuContent_" + pvid[1]).attr("value").length > 0)
        $("#" + skuSelectId).attr({ "class": "SKUSelectValueClass" });
}
function ResetCurrentSku2(stock, alertstock, salePrice, options) {
    $("#productDetails_sku").html("");
    $("#ProductDetails_litWeight").html("无");
    $("#productDetails_Stock").html(stock > 0 ? stock : 0);
    $("#productDetails_AlertStock").val(alertstock);
    if (buyAmount.val() != "" && buyAmount.val() != 0 && document.URL.indexOf("groupbuyproduct_detail") == -1 && document.URL.indexOf("countdownproduct_detail") == -1) {
        if (!amountReg.test(buyAmount.val()))
            buyAmount.val("1");
        var quantity = parseInt($("#buyAmount").val());
        var totalPrice = eval(salePrice) * quantity;
        $("#productDetails_Total").html(totalPrice.toFixed(2));
        $("#productDetails_Total_v").val(totalPrice);
    }
}

    
function ResetCurrentSku(skuId, sku, weight, stock, alertstock, salePrice, options) {
    $("#showSelectSKU").html(options);
    var buyAmount = $("#buyAmount");
    if (!!skuId && IsallSelected()) {
        $("#productDetails_sku").html(sku);
        $("#productDetails_sku_v").val(skuId);
        $("#code").empty();
        var w = document.getElementById("divewm").offsetWidth;//生成二维码
        $("#code").qrcode({
            render: "table", //table方式
            width: w, //宽度
            height: w, //高度
            text: sku //任意内容
        });
        
        if (stock < 0)
        {
            $("#productDetails_Stock").html(0);
        }
        else
        {
            $("#productDetails_Stock").html(stock);
        }
        $("#productDetails_AlertStock").val(alertstock);

        if (weight != "" && weight != "0")
            weight = weight + " g";
        else
            weight = "无";
        $("#ProductDetails_litWeight").html(weight);
        if ($("#hiddenIsLogin").val() == "logined")
            $("#ProductDetails_lblBuyPrice").html(salePrice);
        else {
            $("#ProductDetails_lblBuyPrice").html("登录可见批发价");
        }
    } else {
        $("#productDetails_sku").html("");
        $("#ProductDetails_litWeight").html("无");
    }
    if ($(buyAmount).val() != "" && $(buyAmount).val() != 0 && document.URL.indexOf("groupbuyproduct_detail") == -1 && document.URL.indexOf("countdownproduct_detail") == -1) {
        if (!amountReg.test($(buyAmount).val()))
            $(buyAmount).val("1");
        var quantity = parseInt($("#buyAmount").val());
        var totalPrice = eval(salePrice) * quantity;
        $("#productDetails_Total").html(totalPrice.toFixed(2));
        $("#productDetails_Total_v").val(totalPrice);
    }

}

// 购买数量变化以后的处理
function ChangeBuyAmount() {
    var buyAmount = $("#buyAmount");
    if ($(buyAmount).val() != "" && $(buyAmount).val() != 0) {
        if (!amountReg.test($(buyAmount).val()))
            $(buyAmount).val("1");
        var quantity = parseInt($("#buyAmount").val());
        var oldQuantiy = parseInt($("#oldBuyNumHidden").val());
        var productTotal = eval($("#productDetails_Total").html());
        var totalPrice = productTotal / oldQuantiy * quantity;

        $("#productDetails_Total").html(totalPrice.toFixed(2));
        $("#oldBuyNumHidden").attr("value", quantity);
    }
}

// 购买按钮单击事件
function AddCurrentProductToCart() {
    isadd = false;
    if (!ValidateBuyAmount()) {
        return false;
    }
    if (!IsallSelected()) {
        alert("请选择规格");
        return false;
    }
    var quantity = parseInt($("#buyAmount").val());
    var maxcount = parseInt($("#maxcount").html());
    var s = document.getElementById("productDetails_Stock");

    var stock = 0;
    //modify by meiyh  2014-9-26
    if (s != null) {
        stock = parseInt(document.getElementById("productDetails_Stock").innerHTML);
    } else {
        stock = parseInt($.trim($("#lblRemain").text()));
    }
    var ckRead = document.getElementById("ckRead");
    if (ckRead != undefined && ckRead != null && ckRead.checked == false) {
        alert("请先阅读聚生产活动服务协议并且同意后,才可采购。");
        return false;
    }
    var alertstock = parseInt($("#productDetails_AlertStock").val());
    if (quantity > stock) {
        alert("商品库存不足 " + quantity + " 件，暂不能下单，请稍后再试");
        return;
    }
    if (maxcount != "" && maxcount != null) {
        if (quantity > maxcount) {
            alert("此为限购商品，每人限购" + maxcount + "件");
            return false;
        }
    }
    if (subsiteuserId != "0" && alertstock >= stock) {
        alert("商品库存低于警戒库存，无法采购！");
        return false;
    }
    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return false;
    }
    BuyProduct();
}

// 登录后再购买
function LoginAndBuy() {
    var username = $("#textfieldusername").val();
    var password = $("#textfieldpassword").val();
    var thisURL = document.URL;

    if (username.length == 0 || password.length == 0) {
        alert("请输入您的用户名和密码!");
        return;
    }
    $.ajax({
        url: "/SSO.aspx/layerlogin",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 10000,
        data: JSON.stringify({ 'name': username, 'pwd': password }),
        async: false,
        success: function (data) {
            var d = JSON.parse(data.d);
            if (d.msg == "ok") {
                $("#loginForBuy").hide('hide');
                $("#hiddenIsLogin").val('logined');
                window.location.reload();
            } else {
                alert(d.msg);
            }
        },
        error: function () {
            alert("发生异常");
        }
    });

}

// 购买商品
function BuyProduct() {
    var thisURL = document.URL.toLowerCase();
    if ($("#productDetails_sku_v").val().replace(/\s/g, "") == "") {
        alert("此商品已经不存在(可能库存不足或被删除或被下架)，暂时不能采购");
        return false;
    }
    if (thisURL.indexOf("groupbuyproductdetails") != -1) {
        location.href = applicationPath + "/SubmitJSCOrder.aspx?buyAmount=" + $("#buyAmount").val() + "&productSku=" + $("#productDetails_sku_v").val() + "&from=groupBuy&jsccode=" + $("input[id$='hiddenJSCCode']").val();
    } else if (thisURL.indexOf("countdownproduct_detail") != -1) {
        location.href = applicationPath + "/SubmmitOrder.aspx?buyAmount=" + $("#buyAmount").val() + "&productSku=" + $("#productDetails_sku_v").val() + "&from=countDown";
    } else {
        if ($("#buyAmount").val().replace(/\s/g, "") == "" || parseInt($("#buyAmount").val().replace(/\s/g, "")) <= 0) {
            alert("商品库存不足 " + parseInt($("#buyAmount").val()) + " 件，暂不能下单，请稍后再试.");
            return false;
        }
        location.href = applicationPath + "/SubmmitOrder.aspx?buyAmount=" + $("#buyAmount").val() + "&productSku=" + $("#productDetails_sku_v").val() + "&from=signBuy";
    }
}

// 验证数量输入
function ValidateBuyAmount() {
    var buyAmount = $("#buyAmount");
    if ($(buyAmount).val() == "") {
        alert("请先填写采购数量!");
        $(buyAmount).val("1");
        return false;
    } else if ($(buyAmount).val() == 0) {
        alert("采购数量为0，请先填写正确的采购数量!");
        $(buyAmount).val("1");
        return false;
    } else if (!amountReg.test($(buyAmount).val())) {
        $(buyAmount).val("1");
        return false;
    } else {
        return true;
    }
}

//*************匿名购买**********************************//
function AnonymousBuy() {
    if (isadd) {
        BuyProductToCart();
    } else {
        BuyProduct();
    }
    $("#loginForBuy").hide();
}

//*************2011-07-25  添加到购物车按钮单击事件****************//
function AddProductToCart() {
    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return false;
    }
    if (!ValidateBuyAmount()) {
        return false;
    }

    if (!IsallSelected()) {
        alert("请选择规格");
        return false;
    }

    var quantity = parseInt($("#buyAmount").val());
    var stock = parseInt(document.getElementById("productDetails_Stock").innerHTML);
    var alertstock = parseInt($("#productDetails_AlertStock").val());
    if (quantity > stock) {
        alert("商品库存不足 " + quantity + " 件，暂不能下单，请稍后再试.");
        return false;
    }
    if (subsiteuserId != "0" && alertstock >= stock) {
        alert("商品库存低于警戒库存，无法采购！");
        return false;
    }
    var spcounttype = parseInt($("#spcounttype").text());
    if (quantity + spcounttype > stock) {
        alert("商品库存不足!");
        return false;
    }
    BuyProductToCart(); //添加到购物车
}

function BuyProductToCart() {

    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return;
    }
    var xtarget = $("#addcartButton").offset().left;
    var ytarget = $("#addcartButton").offset().top;
    $("#divshow, #divbefore").css("top", parseInt(ytarget + 40) + "px");

    $("#divshow, #divbefore").css("left", parseInt(xtarget) + "px");
    if ($(document).scrollTop() <= 145) {
        $("#divshow, #divbefore").css("top", parseInt(ytarget - 125) + "px");
    }
    $(".dialog_title_r, .btn-continue").bind("click", function () {
        $("#divshow").css('display', 'none');
        window.location.reload();
    });
    $(".btn-viewcart").attr("href", applicationPath + "/ShoppingCart.aspx");

    $.ajax({
        url: "ShoppingHandler.aspx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: { action: "AddToCartBySkus", quantity: parseInt($("#buyAmount").val()), productSkuId: $("#productDetails_sku_v").val(), Stock: $("#productDetails_Stock").text() },
        async: false,
        beforeSend: function () {
            $("#divbefore").css('display', 'block');
        },
        complete: function () {
            // setTimeout("if($('#divshow').css('display')=='block'){$('#divshow').css('display','none')}",6000);
            //$("#divshow").blur(function(){alert('aaaa')});
        },
        success: function (resultData) {

            if (resultData.Status == "OK") {
                $("#divbefore").css('display', 'none');
                $("#divshow").css('display', 'block'); //显示添加购物成功
                $("#spcounttype").text(resultData.Quantity);
                $("#sptotal").text(resultData.TotalMoney);

                $("#spcartNum").text(resultData.Quantity);
                $("#ProductDetails_ctl03___cartMoney").text(resultData.TotalMoney);
            } else if (resultData.Status == "0") {
                // 商品已经下架
                $("#divbefore").css('display', 'none');
                alert("此商品已经不存在(可能被删除或被下架)，暂时不能采购");
            } else if (resultData.Status == "1") {
                // 商品库存不足
                $("#divbefore").css('display', 'none');
                alert("商品库存不足 " + parseInt($("#buyAmount").val()) + " 件，暂不能下单，请稍后再试.");
            } else if (resultData.Status == "up") {
                // 商品库存不足
                $("#divbefore").css('display', 'none');
                alert("当前规格已购买数量为(" + resultData.Quantity + ") ,超额" + resultData.OutCount + " 件，暂不能下单，请稍后再试.");
            } else {
                // 抛出异常消息
                $("#divbefore").css('display', 'none');
                alert(resultData.Status + '66');
            }
        }
    });
}

function selectShowSkuImg(skuid) {
    $.ajax({
        url: "/api/GetProductImgHandler.ashx",
        data: { skuid: skuid },
        success: function (data) {
            if (data != "") {
                var img = document.createElement("img");
                img.setAttribute("bimg310", data);
                img.setAttribute("bimg", data);
                if($(".displayP_pic_jqzoom").html()==""){
                    $(".displayP_pic_jqzoom").html('<img id="ProductDetails_common_ProductImages___imgBig" jqimg="" src="" style="border-width:0px;" alt="">');
                }
                preview(img);
                //$(".displayP_pic-scroll .items ul li a img").each(function (index, ele) {
                //    if (index + 1 == parseInt(data)) {
                //        preview(this);
                //    }
                //});
            }
            return;
        }
    });
}

function BeginErweima(productid) {
    $.ajax({
        url: "/ShoppingHandler.aspx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: { action: "GetSkuByOptions", productId: productid, options: "null" },
        success: function (resultData) {
            $("#code").empty();
            var w = document.getElementById("divewm").offsetWidth;
            $("#code").qrcode({
                render: "table", //table方式
                width: w, //宽度
                height: w, //高度
                text: resultData.SKU //任意内容
            });

        }
    });
}