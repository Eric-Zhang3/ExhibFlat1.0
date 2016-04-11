//公告
function test() {
    $.ajax({
        //提交数据的类型 POST GET
        type: "POST",
        //提交的网址
        url: "../Service/AjaxApiWeb.aspx",
        //提交的数据
        data: { apiname: "newslist", param1: "5" },
        //返回数据的格式
        datatype: "json",//"xml", "html", "script", "json", "jsonp", "text".
        //在请求之前调用的函数
        beforeSend: function () { $("#msg").html("logining"); },
        //成功返回之后调用的函数             
        success: function (data) {
            //$("#msg").html(decodeURI(data));
            var s = "";
            var newData = JSON.parse(data);
            var arry = newData.newsitem;
            for (var i = 0; i < arry.length; i++) {
                if (arry[i].outlink != null && arry[i].outlink.length > 0)
                    s += "	<li><a href='" + arry[i].outlink + "' target=\"_blank\" Title='" + arry[i].title + "'>" + arry[i].title + "</a></li>";
                else
                    s += "	<li><a href='/affiche/" + arry[i].id + ".htm' target=\"_blank\" Title='" + arry[i].title + "'>" + arry[i].title + "</a></li>";
            }
            $("#affichelist").html(s);
        }
        //调用执行后调用的函数
    });
}
//我的订单
function myOrder() {
    $.ajax({
        //提交数据的类型 POST GET
        type: "POST",
        //提交的网址
        url: "../Service/AjaxApiWeb.aspx",
        //提交的数据
        data: { apiname: "cartlist" },
        //返回数据的格式
        datatype: "json",//"xml", "html", "script", "json", "jsonp", "text".
        //在请求之前调用的函数
        beforeSend: function () { $("#msg").html("logining"); },
        //成功返回之后调用的函数             
        success: function (data) {
            //$("#msg").html(decodeURI(data));
            //  var s =0;
            var newData = JSON.parse(data);
            var s = newData.itemsum;
            //for (var i = 0; i < arry.length; i++) {
            //    s += int.parse(arry[i].num);
            //}
            $("#Mycount").html(s);
        }
        //调用执行后调用的函数
    });
}