///////////////////////////////////////////////////////////////////////////////////
// IE Check
///////////////////////////////////////////////////////////////////////////////////
var isIE = (document.all) ? true : false;

///////////////////////////////////////////////////////////////////////////////////
// Image Helper
///////////////////////////////////////////////////////////////////////////////////
var imageObject = null;
var currentObject = null;

function ResizeImage(I, W, H) {
    if (I.length > 0 && imageObject != null && currentObject != I) {
        setTimeout("ResizeImage('" + I + "'," + W + "," + H + ")", 100);
        return;
    }

    var F = null;
    if (I.length > 0) {
        F = document.getElementById(I);
    }

    if (F != null) {
        imageObject = F;
        currentObject = I;
    }

    if (isIE) {
        if (imageObject.readyState != "complete") {
            setTimeout("ResizeImage(''," + W + "," + H + ")", 50);
            return;
        }
    } else if (!imageObject.complete) {
        setTimeout("ResizeImage(''," + W + "," + H + ")", 50);
        return;
    }

    var B = new Image();
    B.src = imageObject.src;
    var A = B.width;
    var C = B.height;
    if (A > W || C > H) {
        var a = A / W;
        var b = C / H;
        if (b > a) {
            a = b;
        }
        A = A / a;
        C = C / a;
    }
    if (A > 0 && C > 0) {
        imageObject.style.width = A + "px";
        imageObject.style.height = C + "px";
    }

    imageObject.style.display = '';
    imageObject = null;
    currentObject = null;
}

///////////////////////////////////////////////////////////////////////////////////
// String Helper
///////////////////////////////////////////////////////////////////////////////////
String.format = function () {
    if (arguments.length == 0)
        return null;

    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

///////////////////////////////////////////////////////////////////////////////////
// URL Helper
///////////////////////////////////////////////////////////////////////////////////
function GetQueryString(key) {
    var url = location.href;
    if (url.indexOf("?") <= 0) {
        return "";
    }

    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};

    for (i1 = 0; j = paraString[i1]; i1++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }

    var returnValue = paraObj[key.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function GetQueryStringKeys() {
    var keys = {};
    var url = location.href;

    if (url.indexOf("?") <= 0) {
        return keys;
    }

    keys = url.substring(url.indexOf("?") + 1, url.length).split("&");
    for (i2 = 0; i2 < keys.length; i2++) {
        if (keys[i2].indexOf("=") >= 0) {
            keys[i2] = keys[i2].substring(0, keys[i2].indexOf("="));
        }
    }

    return keys;
}

function GetCurrentUrl() {
    var url = location.href;

    if (url.indexOf("?") >= 0) {
        return url.substring(0, url.indexOf("?"));
    }

    return url;
}

function AppendParameter(key, pvalue) {
    var reg = /^[0-9]*[1-9][0-9]*$/;
    var url = GetCurrentUrl() + "?";
    var keys = GetQueryStringKeys();

    if (keys.length > 0) {
        for (i3 = 0; i3 < keys.length; i3++) {
            if (keys[i3] != key) {
                url += keys[i3] + "=" + GetQueryString(keys[i3]) + "&";
            }
        }
    }

    if (!reg.test(pvalue)) {
        alert("只能输入正整数");
        return url.substring(0, url.length - 1);
    }

    url += key + "=" + pvalue;
    return url;
}


///////////////////////////////////////////////////////////////////////////////////
// DataList Select Helper
///////////////////////////////////////////////////////////////////////////////////
function SelectAll() {

    var checkbox = document.getElementsByName("CheckBoxGroup");

    if (checkbox == null) {
        return false;
    }
    if (typeof checkbox.length != 'undefined') {
        if (checkbox.length > 0) {
            for (var i = 0; i < checkbox.length; i++) {
                checkbox[i].checked = true;
            }

        }
    } else {
        checkbox.checked = true;
    }


    return false;
}

function CheckIsLoginDetail(url, productId) {
    var quantity = parseInt($("#buyAmount").val());
    var s = document.getElementById("productDetails_Stock");
    var stock = 0;
    //modify by meiyh  2014-9-26
    if (s != null) {
        stock = parseInt(document.getElementById("productDetails_Stock").innerHTML);
    } else {
        stock = parseInt($.trim($("#lblRemain").text()));
    }
    //if (quantity > stock) {
    //    alert("商品库存不足 " + quantity + " 件，暂不能下单，请稍后再试");
    //    return false;
    //}
    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return false;
    } else {
        $.ajax({
            url: '/AjaxPage.aspx?productId=' + productId,
            type: 'post',
            async: true,
            datatype: "text",
            success: function (data) {
                //var d = eval("(" + data + ")");
                if (data == "false") {
                    alert("加入数据包失败");
                } else if (data == "same") {
                    alert("该商品已存在数据包中");
                    return false;
                } else {
                    $("#favDiv").show();
                    $("#favDiv").html("<div><span>加入数据包成功！</span><a href='" + url + "' style='color:red;'>查看数据包中的商品</a></div><div><br/><input type='button' value=' 确 定 ' onclick=\"$('#favDiv').hide()\"></div>");
                }
            },
            error: function () {
                alert("cuowo");
            }
        });


    }
}

function CheckIsLoginFav(url) {
    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return false;
    } else {
        window.location.href = url;
    }
}

function CheckIsLogin() {
    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return false;
    }
    return true;
}

///下载数据包时验证会员等级
function CheckIsLoginDB() {
    if ($("#hiddenIsLogin").val() == "nologin") {
        $("#loginForBuy").show();
        return false;
    }
    else {
        var userInfo = decodeURI($.cookie("SessionUserInfo"));
        userInfo = userInfo.split("]&SessionKey")[0].substring(1, userInfo.split("]&2")[0].length);
        var obj = JSON.parse(userInfo);
        if (obj.id != null) {
            var temp;
            $.ajax({
                url: '/AjaxPage.aspx?userId=' + obj.id,
                type: 'post',
                async: false,
                datatype: "text",
                success: function (data) {
                    if (data == 1) {
                        temp = false;

                    } else if (data > 1) {
                        temp = true;
                    } else if (data == "false") {
                        temp = true;
                    }
                },
                error: function () {
                    alert("出现异常，请稍后重试！");
                    return false;
                }
            });
            if (temp)
                return true;
            else {
                $("#favDiv").show();
                $("#favDiv").html("<div><span>您尚未成为授权分销商不能操作此功能，请联系在线客服申请</span></div><div><br/><input type='button' value=' 确 定 ' onclick=\"$('#favDiv').hide()\"></div>");
                return false;
            }
        }
        else {
            $("#loginForBuy").show();
            return false;
        }
    }
}

///用户为一级分销商时显示价格提示内容
function ShowPriceTips() {
    var userInfo = decodeURI($.cookie("SessionUserInfo"));
    userInfo = userInfo.split("]&SessionKey")[0].substring(1, userInfo.split("]&2")[0].length);
    var obj = JSON.parse(userInfo);
    if (obj.id != null) {
        $.ajax({
            url: '/AjaxPage.aspx?userId=' + obj.id,
            type: 'post',
            async: false,
            datatype: "text",
            success: function (data) {
                if (data == 1) {
                    $("#price_tips").show();
                }
            },
            error: function () {
            }
        });
    }
}
function ReverseSelect() {

    var checkbox = document.getElementsByName("CheckBoxGroup");

    if (checkbox == null) {
        return false;
    }
    if (typeof checkbox.length != 'undefined') {
        if (checkbox.length > 0) {
            for (var i = 0; i < checkbox.length; i++) {
                checkbox[i].checked = !checkbox[i].checked;
            }
        }
    } else {
        checkbox.checked = !checkbox.checked;
    }
    return false;

}

function StandardPost(url, args) {
    var form = $("<form method='post' enctype='multipart/form-data' ></form>");
    form.attr({ "action": url });
    for (arg in args) {
        var input = $("<input type='hidden'>");
        input.attr({ "name": arg });
        input.val(args[arg]);
        form.append(input);
    }
    top.$('body').append(form);
    form.submit();
}

function DeleteShoppingCart(skuid) {
    $.post("/API/CommonHandler.ashx?op=DeleteShopping&SkuID=" + skuid, {}, function (d) { });
    getShoppingCart();
}

$(document).ready(function () {
    //我的订单
    $(".myOrder").hover(function () {
        getShoppingCart();
        $(this).find(".cartList").show();
        $(this).find("b").show();
        $(".cartInner").hide();
    }, function () {
        $(this).find(".cartList").hide();
        $(this).find("b").hide();
        $(".cartInner").show();
    });
    //分页回车
    var txtGo = $("#txtGoto");
    if (!!txtGo) {
        $("#txtGoto").bind("keypress", function (e) {
            var e = e || window.event;
            if (e.keyCode == 13) {
                var url = AppendParameter('pageindex', $.trim($('#txtGoto').val()));
                window.location.href = url;
                return false;
            }
        });
        $("#txtGoto").bind("keydown", function (e) {
            var e = e || window.event;
            if (e.keyCode == 13) {
                window.location.href = AppendParameter('pageindex', $.trim($('#txtGoto').val()));
                return false;
            }
        });
    }

});

//获取购物车信息
function getShoppingCart() {
    $.post("/API/CommonHandler.ashx?op=ShoppingCar", {}, function (d) {
        var data = eval(d);
        $(".cartInner").empty();
        if (!!data) {
            $.each(data, function (i, content) {
                if (i == 0) {
                    $(".cartInner").append($("<p style=\"line-height:37px\">最近加入的商品：</p>"));
                }
                var productname = content.Name.length > 2 ? content.Name.substring(0, 2) + "..." : content.Name;
                var item = $("<dl> <dt> <a href=/product_detail-" + content.ProductId + ".htm><img src='" + content.ThumbnailUrl60 + "' style='width:40px;height:40px'/></a> <em>×" + content.Quantity + "</em> </dt> <dd> <strong><em>￥</em>" + content.MemberPrice + "/件</strong><p><a href='' onclick='DeleteShoppingCart(\"" + content.SkuId + "\");return false'>删除</a></p> </dd> </dl>");
                $(".cartInner").append(item);
            });
            $(".cartList h2").hide();
            $(".cartInner").show();
            $(".cartInner").append($("<p class='checkOrder'><a href='/ShoppingCart.aspx'>查看我的采购单</a></p>"));
        } else {
            $(".cartList h2").show();
            $(".cartInner").hide();
        }
    });
}

//计算坐标方法,得到某obj的x,y坐标,兼容浏览器
function getWinElementPos(obj) {
    var ua = navigator.userAgent.toLowerCase();
    var isOpera = (ua.indexOf('opera') != -1);
    var isIE = (ua.indexOf('msie') != -1 && !isOpera); // not opera spoof
    var el = obj;
    if (el.parentNode === null || el.style.display == 'none') {
        return false;
    }
    var parent = null;
    var pos = [];
    var box;
    if (el.getBoundingClientRect) //IE
    {
        box = el.getBoundingClientRect();
        var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop);
        var scrollLeft = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft);
        return { x: box.left + scrollLeft, y: box.top + scrollTop };
    } else if (document.getBoxObjectFor) {
        box = document.getBoxObjectFor(el);
        var borderLeft = (el.style.borderLeftWidth) ? parseInt(el.style.borderLeftWidth) : 0;
        var borderTop = (el.style.borderTopWidth) ? parseInt(el.style.borderTopWidth) : 0;
        pos = [box.x - borderLeft, box.y - borderTop];
    } else // safari & opera
    {
        pos = [el.offsetLeft, el.offsetTop];
        parent = el.offsetParent;
        if (parent != el) {
            while (parent) {
                pos[0] += parent.offsetLeft;
                pos[1] += parent.offsetTop;
                parent = parent.offsetParent;
            }
        }
        if (ua.indexOf('opera') != -1
            || (ua.indexOf('safari') != -1 && el.style.position == 'absolute')) {
            pos[0] -= document.body.offsetLeft;
            pos[1] -= document.body.offsetTop;
        }
    }
    if (el.parentNode) {
        parent = el.parentNode;
    } else {
        parent = null;
    }
    while (parent && parent.tagName != 'BODY' && parent.tagName != 'HTML') { // account for any scrolled ancestors
        pos[0] -= parent.scrollLeft;
        pos[1] -= parent.scrollTop;

        if (parent.parentNode) {
            parent = parent.parentNode;
        } else {
            parent = null;
        }
    }
    return { x: pos[0], y: pos[1] };
}


//隐藏登录框
function hideLogin() {
    $("#hidBuy").val("0");
    $("#loginForBuy").hide();
}

function LoginAndBuy2() {
    var username = $("#textfieldusername").val();
    var password = $("#textfieldpassword").val();
    //  var thisURL = document.URL;

    if (username.length == 0 || password.length == 0) {
        alert("请输入您的用户名和密码!");
        return;
    }
    var loginhtml = "您好，{0}<a href='/user/UserDefault.aspx'>商家中心</a> <b><a onclick='return todoout()' href='/logout.aspx'>退出</a></b>";
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
                top.$(".siteMenu .userState").html(String.format(loginhtml, username));

                $("#loginForBuy").hide('hide');
                $("#hiddenIsLogin").val('logined');
                // var url = GetCurrentUrl();
                var buy = $("#hidBuy").val();
                //if (buy == "1") {
                //    //$("#hidBuy").val("0");
                //    //BuyProduct();
                //    AddProductToCart();
                //} else 
                if (buy == "2") {
                    location.href = location.href;
                    return true;
                }
                /*未找到AddProductToCart()，暂时注释掉下列引用函数代码(ly)*/
                //else {
                //    AddProductToCart();
                //      location.href = location.href;
                //}
            } else {
                alert(d.msg);
            }
        },
        error: function () {
            alert("发生异常");
        }
    });
    location.reload();
}

function supisapply_Yi() {
    var flog = false;
    $.ajax({
        type: "POST",
        url: "/UserApplyVerification.aspx",
        async: false,
        dataType: "json",
        success: (function (d) {
            switch (parseInt(d, 0)) {
                case 1:
                    location.href = "/ApplicationSucceed.aspx";
                    flog = true;
                    break;
                case 2:
                    location.href = "/UserApply.aspx";
                    flog = true;
                    break;
                case 3:
                    $.layer({
                        shade: [0],
                        area: ['auto', 'auto'],
                        dialog: {
                            msg: '申请不通过,是否重新申请？',
                            btns: 2,
                            type: -1,
                            btn: ['确定', '取消'],
                            yes: function () {
                                location.href = "/UserApply.aspx";
                                flog = true;
                            },
                            no: function () {
                                flog = false;
                            }
                        }
                    });
                    break;
                case 4:
                    $.layer({
                        area: ['auto', 'auto'],
                        dialog: { msg: '您已是高级用户，无需再申请！', type: -1 }
                    });
                    flog = true;
                    break;
                case 5:
                    location.href = "/ApplicationSucceed.aspx";
                    //location.href = "/Application.aspx";
                    flog = true;
                    break;
                default:
                    flog = false;
                    break;
            }
        }),
        error: (function (d) {
            flog = false;
        })
    });
    return flog;
}

function supisapply() {
    var flog = false;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/ApplicationSucc.aspx/isApplied",
        data: JSON.stringify({}), //序列化为json格式的字符串
        async: false,
        dataType: "json"
    }).done(function (d) {
        switch (parseInt(d.d, 0)) {
            case 1:
                location.href = "/ApplicationOK.aspx";
                flog = true;
                break;
            case 2:
                location.href = "/Application.aspx";
                break;
            case 3:
                $.layer({
                    shade: [0],
                    area: ['auto', 'auto'],
                    dialog: {
                        msg: '申请不通过,是否重新申请？',
                        btns: 2,
                        type: -1,
                        btn: ['确定', '取消'],
                        yes: function () {
                            flog = true;
                            location.href = "/Application.aspx";
                        },
                        no: function () {
                            flog = false;
                        }
                    }
                });
                break;
            default:
                flog = false;
                break;
        }

    }).fail(function () { flog = false; });
    return flog;
}

//登出
function todoout() {
    var flog = false;
    $.post("/loginout.ashx").done(function (d) {
        location.href = location.href;
        flog = false;
        $("<img src='" + d + "' style='display:none'/>").appendTo("body");
    })
        .fail(function () {
            flog = true;
        })
    return flog;
}

//头部公告滚动
function AutoScroll(obj) {
    $(obj).find("ul:first").animate({
        marginTop: "-25px"
    }, 500, function () {
        $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
    });
}

$(document).ready(function () {
    setInterval('AutoScroll("#s1")', 3000);
});

window.onerror = function () { return true; }
//window.onerror = function (message, url, line) {
//    var temp = "message:" + message + ";\n" + "url:" + url + ";\n" + "line:" + line + ";\n";
//    alert(temp);
//    return true;
//}

$(function () {
    $("#ques_left").find("li").eq(0).addClass("select");
    $("#content .partBox").hide();
    $("#content").find(".partBox").eq(0).show();
    $("#ques_left li").hover(function () {
        var nIndex = $("#ques_left li").index(this);
        $(this).addClass("select").siblings().removeClass("select");
        $(".partBox").eq(nIndex).show().siblings(".partBox").hide();
    })
})

//返回顶部
$(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() > 500) {
            $(".goTop").fadeIn(1500)
        } else {
            $(".goTop").fadeOut(1500);
        }
    })
    $('.goTop').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 200);
    });

})