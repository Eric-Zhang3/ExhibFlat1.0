requirejs.config({
    baseUrl: '../js/'
});
var sel = {
    'ActivityStatus': '',
    'JSC_CODE': '',
    'ProductName': '',
    'Sku': '',
    'ApplicationTime': '',
    'ActTime': '',
    'days': '',
    'supplierName': '',
    'startp': '1',
    'endp': '10'
};

$(function() {
    $(".selectItem .selStatus").CheckBoxBindFn({
        callback: function($item) {
            var s = '';
            $(".selectItem .selStatus input[type=checkbox]:checked:not([class=all])").each(function() {
                if ($(this).val() != '')
                    s += $(this).val() + ',';
            });
            sel.ActivityStatus = s;
            return false;
        }
    });


    $(".selectItem .selactcode").change(function() {
        sel.JSC_CODE = $(this).val();
    });
    $(".selectItem .selproname").change(function() {
        sel.ProductName = $(this).val();
    });
    $(".selectItem .selsku").change(function() {
        sel.Sku = $(this).val();
    });
    $("#seldatetime").on("click", function(e) {
        e = window.event || e;
        var co = new AddCalendar(e, this, 1, function(v) {
            var a = $('#seldatetime2').val();
            if (!!a && a < v) {
                alert('开始时间应小于结束时间');
                return false;
            } else {
                return true;
            }

        });
    });
    $("#seldatetime2").on("click", function(e) {
        e = window.event || e;
        var o = new AddCalendar(e, this, 1, function(v) {
            var a = $('#seldatetime').val();
            if (!!a && a > v) {
                alert('结束时间应大于等于开始时间');
                return false;
            } else {
                return true;
            }
        })

    });


    $("#selecttodo").click(function() {
        todo();
    });

    function todo() {
        var start = $('#seldatetime').val();
        var end = $('#seldatetime2').val();
        if (end == "")
            sel.ActTime = start;
        else if (start == "")
            sel.ActTime = end;
        else if (end != "" && start != "") {
            sel.ActTime = start + " " + end;
        }

        // $.postWebService("./ActMsgBySupplier.aspx/selectActList", { JsonStr: JSON.stringify(sel) }, function(d) {
        //   $("#selecttodo").data["selecttodo"] = $.parseJSON(d.d);
        listinit();
        //  })
    }
});
$(function() {

    //$.postWebService("./ActMsgBySupplier.aspx/selectActList", { JsonStr: "" }, function(d) {
    //    $("#selecttodo").data["selecttodo"] = $.parseJSON(d.d);
    //    listinit();
    //});
    var paginalNum = $(".pageHandleArea > ul > li.paginalNum > a");
    // $(".pageHandleArea>ul>li.paginalNum>a.selectthis").text();
    paginalNum.each(function() {
        var self = this;
        $(self).on("click", function() {
            paginalNum.removeClass("selectthis");
            $(self).addClass("selectthis");
            //var curpage = $("#curpage");
            //if (curpage != undefined)
            //    curpage.text(1);
            listinit();
        });
    });
    $("#topage").on("change", function() {
        var l = DataType.UInt32.CheckRange($(this).val());
        if (!l) {
            alert("必须正整数");
            return;
        }
    })
});

var arryDictNameCode = {
    1: ".celbtn", //取消按钮  ,待审核
    2: ".seebtn,.rebtn", //查看原因,重新报名 ,未通过
    3: ".paybtn", //	付款  ,已通过未付款
    7: ".seebtn", //查看原因	活动终止
    8: ".promgbtn", //	活动成功 生产管理
    9: '.promgbtn', //	已售罄 生产管理
    4: ".celbtn.red", //已通过已付款付款
    11: ".seebtn"
}
var arrybtncodes = {
    ".celbtn": "<button type=\"button\" class=\"celbtn nored\">取消</button>",
    ".seebtn": "<button type=\"button\" class=\"seebtn\">查看原因</button>",
    ".rebtn": "<button type=\"button\" class=\"rebtn\">重新报名</button>",
    ".seebtn,.rebtn": "<button type=\"button\" class=\"seebtn\">查看原因</button>" + "<button type=\"button\" class=\"rebtn\">重新报名</button>",
    ".paybtn": "<button type=\"button\" class=\"paybtn\">付款</button>",
    ".promgbtn": "<button type=\"button\" class=\"promgbtn\">生产管理</button>",
    ".celbtn.red": "<button type=\"button\" class=\"celbtn red\">取消</button>"
}

function pageselectCallback(page_index, jq) {
    if (first) {
        first = false;
    } else {
        var start = onepcount() * (page_index) + 1;
        var end = start + onepcount() - 1;
        sel.startp = start;
        sel.endp = end;
        $.postWebServiceNoAsync("./ActMsgBySupplier.aspx/selectActList", { JsonStr: JSON.stringify(sel) }, function(d) {
            $("#selecttodo").data["selecttodo"] = $.parseJSON(d.d);

            var tb = getlists();
            //  console.log(tb)
            var str = new StringBuilder();
            for (var i in tb) {
                str.append(additem(tb[i], i));
            }

            $('#Searchresult').empty().append(str.toString());
            randbtn();
            var count = listCount();
            var pcounts = pcount(count) || 1;
            $("#allcount").text(count);
            $("#allpcount").text(pcounts);
            $("#curpage").text(page_index + 1);
        });
    }
    return false;
}

var additem = function(obj, i) {
    var str = new StringBuilder();
    if (i % 2 != 0)
        str.append("<tr class=\"odd\" >");
    else {
        str.append("<tr class=\"\" >");
    }
    str.append("<td class=\"\" > <a href='./ActivitiesDetail.aspx?JSC_CODE=" + obj.JSC_CODE + "' >" + obj.JSC_CODE + "</a></td>");
    str.append("<td class=\"\" >" + (/Date/.test(obj.ActivityStartTime) ? eval('new ' + obj.ActivityStartTime.replace(/\//g, "")).format("yyyy-MM-dd") : '未开始') + "</td>");
    str.append("<td class=\"\" >" + (/Date/.test(obj.ActivityEndTime) ? eval('new ' + obj.ActivityEndTime.replace(/\//g, "")).format("yyyy-MM-dd") : '') + "</td>");
    str.append("<td class=\"\" >" + obj.ProductName + "</td>");
    str.append("<td class=\"\" >" + obj.SKU + "</td>");
    str.append("<td class=\"DictName\" >" + obj.DictName + "</td>");
    str.append("<td class=\"\" >" + (obj.CashDeposit == null ? 0 : obj.CashDeposit) + "</td>");
    str.append("<td class=\"\" >" + (obj.GotBreachSum == null ? 0 : obj.GotBreachSum) + "</td>");
    str.append("<td class=\"\" >" + (obj.PayBreachSum == null ? 0 : obj.PayBreachSum) + "</td>");


    str.append("<td class=\"\" >" + (obj.OrderPriceSum == null ? 0 : obj.OrderPriceSum) + "</td>");
    str.append("<td class=\"\" >" + (obj.OrderCountSum == null ? 0 : obj.OrderCountSum) + "</td>");
    str.append("<td class=\"\" >" + (obj.CountSum == null ? 0 : obj.CountSum) + "</td>");

    str.append("<td class=\"\" >" + (obj.CancelSum == null ? 0 : obj.CancelSum) + "</td>");
    str.append("<td class=\"\" >" + (obj.CloseSum == null ? 0 : obj.CloseSum) + "</td>");

    str.append("<td class=\"jsc_jindu\" >" + (obj.ProductProgress == null ? 0 : obj.ProductProgress) + "%" + "</td>");
    str.append("<td class=\"jsc_zl\" >" + (obj.ProductSum == null ? 0 : obj.ProductSum) + "</td>");

    str.append("<td class=\"\" in='" + i + "' val='" + obj.JSC_CODE + "'>" + (!(arryDictNameCode[obj.DictCode]) ? "" : arrybtncodes[arryDictNameCode[obj.DictCode]]) + "</td>");
    str.append("</tr>");
    return str.toString();
}

function randbtn() {
    //  1: ".celbtn",//取消按钮  ,待审核
    //2: ".seebtn,.rebtn",//查看原因,重新报名 ,未通过
    //3: ".paybtn" ,//	付款  ,已通过未付款
    //7: ".seebtn",//查看原因	活动终止
    //8: ".promgbtn" //	活动成功 生产管理
    //取消按钮  ,待审核

    var id, qux;
    var celbtn = $(".celbtn");
    var promgbtn = $(".promgbtn");
    requirejs(['dialog/js/dialog.min'], function() {
        if (celbtn.length > 0)
            qux = new $.ui.Dialog('.celbtn', {
                elem: '#quxiao',
                overlayClose: true,
                drag: true,
                dragHandle: 'h4'
            });
        if (promgbtn.length > 0)
            id = new $.ui.Dialog('.promgbtn', {
                elem: '#guanli',
                overlayClose: false,
                drag: true,
                dragHandle: 'h2'
            });
    });
    if (celbtn.length > 0) {

        $("#quxiao input[type=button].cancel").off().on('click', function() {

            qux.close();
        });
        $(".celbtn").each(function() {
            var p = $(this).parent();
            var JSC_CODE = p.attr('val');

            $(this).click(function() {
                $("#quxiao input[type=button].ok").off();
                if (JSC_CODE != undefined && JSC_CODE.length > 0) {
                    $("#quxiao input[type=button].ok").on('click', function() {
                        var re = $("#quxiao .selyy").val();
                        $.postWebService("./ActMsgBySupplier.aspx/CancelAct", { 'JSC_CODE': JSC_CODE, 're': re }, function(d) {
                            if (d.d == "T") {

                                p.siblings('.DictName').html("已取消");
                                p.empty().html("<button type=\"button\" class=\"seebtn\">查看原因</button>");
                                qux.close();
                                p.find(".seebtn").click(function() {
                                    if (JSC_CODE != undefined && JSC_CODE.length > 0) {
                                        $.postWebService("./ActMsgBySupplier.aspx/bec", { 'JSC_CODE': JSC_CODE }, function(d) {
                                            if (d.d != "") {
                                                alert(d.d);
                                            }
                                        })
                                    }
                                })
                            } else {
                                alert("sorry");
                                // alert(JSC_CODE + re)
                            }
                        })
                    })

                }
            })

        });
    }
    //查看原因
    $('.seebtn').each(function() {
        var p = $(this).parent();
        var JSC_CODE = p.attr('val');
        $(this).click(function() {
            if (JSC_CODE != undefined && JSC_CODE.length > 0) {
                $.postWebService("./ActMsgBySupplier.aspx/bec", { 'JSC_CODE': JSC_CODE }, function(d) {
                    if (d.d != "") {
                        alert(d.d);
                    }
                })
            }
        });
    });

    $('.rebtn').each(function() {
        var p = $(this).parent();
        var JSC_CODE = p.attr('val');
        $(this).click(function() {
            if (JSC_CODE != undefined && JSC_CODE.length > 0) {
                $.postWebService("./ActMsgBySupplier.aspx/gotoRegister", { 'JSC_CODE': JSC_CODE }, function(d) {
                    // console.log(d.d)
                    if (d.d != "F")
                        location.href = d.d;
                })
            }
        });
    });
    //todo 付款 
    $(".paybtn").each(function() {
        var p = $(this).parent();
        var JSC_CODE = p.attr('val');
        $(this).click(function() {
            if (JSC_CODE != undefined && JSC_CODE.length > 0) {
                $.post("/API/CommonHandler.ashx?op=Deposit", { 'JSC_CODE': JSC_CODE }, function(d) {

                    var data = eval("(" + d + ")");
                    $.getJSON(data.SiteUrl + "/WebApi/PaymentAPI.ashx?op=CheckBalance&callback=?", { 'UserID': data.UserID }, function(d) {
                        if (d.res == "false") {
                            //没有开通预付款账户
                            top.window.open("/OrderApply.aspx?sign=JSC");
                        } else {
                            //跳转到支付页面 
                            StandardPost(data.SiteUrl + "/TransferPayment/TransferPayment?JSC_Code=" + JSC_CODE + "&SessionKey=" + data.SessionKey, data);
                        }
                    });
                })
            }
        });
    });

    if (promgbtn.length > 0) {

        $("#guanli input[type=button].cancel").off().on('click', function() {
            $("#guanli table").empty();
            id.close();
        });

        var tb = getlists();
        promgbtn.each(function() {
            var p = $(this).parent();
            var index = p.attr('in');
            var JSC_CODE = p.attr('val');
            $(this).click(function() {
                // console.log(3);
                $("#guanli input[type=button].ok").off();
                if (JSC_CODE != undefined && JSC_CODE.length > 0) {
                    if (tb[index]['JSC_CODE'] == JSC_CODE) {
                        var t = tb[index]['ProductProgress'] == null ? 0 : tb[index]['ProductProgress'];
                        var str = new StringBuilder();
                        var s;
                        str.append("<tr><td class='tt'>订单总数：</td><td>" + (tb[index]['OrderCountSum'] == null ? 0 : tb[index]['OrderCountSum']) + "单</td> </tr>");
                        str.append("<tr><td class='tt'>取消订单：</td><td>" + (tb[index]['CancelSum'] == null ? 0 : tb[index]['CancelSum']) + "单</td> </tr>");
                        str.append("<tr><td class='tt'>订购总数：</td><td>" + (tb[index]['CountSum'] == null ? 0 : tb[index]['CountSum']) + (!!tb[index]['Unit'] ? tb[index]['Unit'] : "件") + "</td> </tr>");
                        str.append("<tr><td class='tt'>交易关闭：</td><td>" + (tb[index]['CloseSum'] == null ? 0 : tb[index]['CloseSum']) + "单</td> </tr>");


                        str.append("<tr><td class='tt'>生产总量：</td><td class='tl'>  <input type=\"number\" " + (t == 100 ? "checked readonly='true' disabled" : '') + " class=\"ProductSum\" datatype='UInt32' value='" + (tb[index]['ProductSum'] == null ? '' : tb[index]['ProductSum']) + "'/>" + (!!tb[index]['Unit'] ? tb[index]['Unit'] : "件") + "</td> </tr>");
                        str.append("<tr><td class='tt'>生产环节描述：</td><td class='tl'> <input type=\"text\" " + (t == 100 ? "checked readonly='true' disabled" : '') + " class=\"ProgressDesc\" datatype='String' multiple=\"2\" value='" + (tb[index]['ProductProgressDesc'] == null ? "" : tb[index]['ProductProgressDesc']) + "'/></td> </tr>");
                        s = "<input type=\"radio\" name=\"r\" value=\"0\" " + (t == 0 ? "checked" : t > 0 ? "readonly='true' disabled" : '') + "/>0%" +
                            "<input type=\"radio\" name=\"r\" value=\"20\" " + (t == 20 ? "checked" : t > 20 ? "readonly='true' disabled" : '') + "/>20%" +
                            "<input type=\"radio\" name=\"r\" value=\"50\" " + (t == 50 ? "checked" : t > 50 ? "readonly='true' disabled" : '') + "/>50%" +
                            "<input type=\"radio\" name=\"r\" value=\"80\" " + (t == 80 ? "checked" : t > 80 ? "readonly='true' disabled" : '') + "/>80%" +
                            "<input type=\"radio\" name=\"r\" value=\"100\" " + (t == 100 ? "checked readonly='true' disabled" : '') + "/>100%";

                        str.append("<tr><td class='tt'>本期活动订单生产进度：</td><td> "
                            +
                            s
                            + "</td> </tr>");

                        $("#guanli table").empty().append(str.toString());
                        $("input.ProductSum").validate();
                        $("input.ProgressDesc").validate();
                        $("#guanli input[type=button].ok").on('click', function() {
                            if (t == 100) {
                                id.close();
                                return false;
                            }
                            var remark = $("#guanli .ProgressDesc").val();

                            var jindu = $("#guanli input[type=radio]:checked").val();
                            if (jindu == undefined)
                                jindu = 0;
                            var zl = $("#guanli .ProductSum").val();
                            if (!zl) {

                                alert("生产总量不可为空");
                                return false;
                            }
                            if (!DataType.UInt32.CheckRange(zl))
                                return false;

                            $.postWebService('./ActMsgBySupplier.aspx/productionControl', {
                                'JSC_CODE': JSC_CODE,
                                'zliang': zl,
                                'remark': remark,
                                'jindu': jindu
                            }, function(d) {
                                if (d.d == "T") {
                                    tb[index]['ProductSum'] = zl;
                                    tb[index]['ProductProgress'] = jindu;
                                    tb[index]['ProductProgressDesc'] = remark;
                                    p.prev(".jsc_zl").text(zl);
                                    p.prev(".jsc_zl").prev(".jsc_jindu").text(jindu + "%");
                                    id.close();
                                } else {
                                    alert("sorry");
                                }
                            });

                        });
                        $("#guanli input[type=button].cancel").on('click', function() {
                            id.close();
                        })

                    } else {
                        return;
                    }
                }
            });

        });
    }
}

var first = false;

function listinit() {
    pageselectCallback(0);
    first = true;
    var count = listCount();
    var pcounts = pcount(count) || 1;

    //$("#allcount").text(count);
    //$("#allpcount").text(pcounts);
    initPagination(pcounts);
    initButtons();
}

function onepcount() {
    return parseInt($(".pageHandleArea > ul > li.paginalNum > a.selectthis").text());
}

function pcount(count) {
    var p = onepcount();
    return Math.ceil(count / p);
}

function getlists() {
    return $("#selecttodo").data["selecttodo"].dt;
}

function listCount() {
    return $("#selecttodo").data["selecttodo"].count;
}

function initPagination(count) {
    // console.log("initPagination");
    $("#Pagination, #Pagination2").pagination(count, {
        prev_text: "上一页",
        next_text: "下一页",
        num_display_entries: 6,
        num_edge_entries: 1,
        prev_show_always: false,
        next_show_always: false,
        callback: pageselectCallback,
        items_per_page: 1
    });
}

listinit();

//按钮事件
function initButtons() {

    $('#btnPrev').click(function() {
        $('#Pagination').trigger('prevPage');
    });
    $('#btnNext').click(function() {
        $('#Pagination').trigger('nextPage');
    });
    $('#btnSet').click(function() {
        var vl = $("#topage").val();
        if (DataType.UInt32.CheckRange(vl))
            $('#Pagination').trigger('setPage', parseInt(vl) - 1);
        // $('#Pagination').trigger('setPage', parseInt($("#topage").val()) - 1);
    });
}