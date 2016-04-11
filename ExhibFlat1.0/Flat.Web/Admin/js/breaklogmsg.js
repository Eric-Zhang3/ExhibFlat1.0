
requirejs.config({
    baseUrl: '../js/'
});
var sel = {
    'BreachStatus': '',
    'JSC_CODE': '',
    'OrderId': '',
    'BreachCode': '',
    'BreachUserName': '',
    'VictimsUserName': '',
    'CreatedUserName': '',
    'ActTime': '',
    'days': ''
};

$(function() {
    $(".selectItem .selStatus").CheckBoxBindFn({
        callback: function($item) {
            var s = '';
            $(".selectItem .selStatus input[type=checkbox]:checked:not([class=all])").each(function() {
                if ($(this).val() != '')
                    s += $(this).val() + ',';
            });
            sel.BreachStatus = s;
            return false;
        }
    });

    $(".selectItem .selactcode").change(function() {
        sel.JSC_CODE = $(this).val();
    });
    $(".selectItem .selOrderId").change(function() {
        sel.OrderId = $(this).val();
    });
    $(".selectItem .selwycode").change(function() {
        sel.BreachCode = $(this).val();
    });

    $(".selectItem .BreachUserName").change(function() {
        sel.BreachUserName = $(this).val();
    });
    $(".selectItem .VictimsUserName").change(function() {
        sel.VictimsUserName = $(this).val();
    });
    $(".selectItem .CreatedUserName").change(function() {
        sel.CreatedUserName = $(this).val();
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

    })

    $("input.weiyue_fy").click(function() {
        var me = $(this).attr("rl");
        if (me == "me") {
            $(".selectItem .BreachUserName").parent("li").hide();
            $(".selectItem .VictimsUserName").parent("li").show();
            sel.BreachUserName = "me";
            sel.VictimsUserName = "";
        } else {
            $(".selectItem .BreachUserName").parent("li").show();
            $(".selectItem .VictimsUserName").parent("li").hide();
            sel.BreachUserName = "";
            sel.VictimsUserName = "me";
        }
        todo();
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
        $.postWebService("./breaklogmsg.aspx/selectList", { JsonStr: JSON.stringify(sel) }, function(d) {
            $("#selecttodo").data["selecttodo"] = $.parseJSON(d.d);
            listinit();
        })
    }

    var orderid = $.query.get('orderid');
    if (!!orderid) {
        $("input.selOrderId").val(orderid);
        sel.OrderId = orderid;
    }
    $.postWebService("./breaklogmsg.aspx/selectList", { JsonStr: JSON.stringify(sel) }, function(d) {
        $("#selecttodo").data["selecttodo"] = $.parseJSON(d.d);
        listinit();
    });
});
$(function() {
    var paginalNum = $(".pageHandleArea > ul > li.paginalNum > a");
    paginalNum.each(function() {
        var self = this;
        $(self).on("click", function() {
            paginalNum.removeClass("selectthis");
            $(self).addClass("selectthis");
            var curpage = $("#curpage");
            if (curpage != undefined)
                curpage.text(1);
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
    2: ".dobtn",
    1: ".seebtn"
}
var arrybtncodes = {
    ".dobtn": "<button type=\"button\" class=\"dobtn\">申请赔偿</button>",
    ".seebtn": "<button type=\"button\" class=\"seebtn\">查看退款</button>",
    ".doyibin": "<button type=\"button\" class=\"dobtn\">赔偿意见</button>"
}

//触发返回数据调用
function pageselectCallback(page_index, jq) {
    var user = $("#user_id").val();
    //console.log(user)
    var start = onepcount() * (page_index);
    var end = start + onepcount() - 1;

    var tb = getlists();

    var str = new StringBuilder();
    for (var i = start; i < listCount() && i <= end; i++) {
        if (i % 2 != 0)
            str.append("<tr class=\"odd\" >");
        else {
            str.append("<tr class=\"\" >");
        }
        var xx = 2;
        if (tb[i].VictimsUser == user)
            xx = 1; //
        else if (tb[i].BreachUser == user) {
            xx = 2; //违约人
        }
        // var href = "DisobeyDetail.html?WY_CODE=" + tb[i].BreachCode + "&JSC_CODE="+tb[i].JSC_CODE +"&xx="+xx+"&status="+(tb[i].BreachStatus!=null?tb[i].BreachStatus:"0")+"'";
        str.append("<td class=\"\" > <a class='go' href='DisobeyDetail.html?WY_CODE=" + tb[i].BreachCode + "&JSC_CODE=" + tb[i].JSC_CODE + "&xx=" + xx + "&status=" + (tb[i].BreachStatus != null ? tb[i].BreachStatus : "0") + "'> <span class='goto'>" + tb[i].BreachCode + "<span></a></td>");
        str.append("<td class=\"\"  isOrderId='" + !!tb[i].OrderId + "' >" + (tb[i].OrderId != null ? tb[i].OrderId : "未知") + "</td>");
        str.append("<td class=\"\"  isJsc='" + !!tb[i].JSC_CODE + "' style = \"display:none\">" + (tb[i].JSC_CODE != null ? tb[i].JSC_CODE : "未知") + "</td>");
        str.append("<td class=\"\" >" + (/Date/.test(tb[i].CreatedDate) ? eval('new ' + tb[i].CreatedDate.replace(/\//g, "")).format("yyyy-MM-dd hh:mm:ss") : "未知") + "</td>");
        str.append("<td class=\"\" >" + (tb[i].CreatedUserName != null ? tb[i].CreatedUserName : "未知") + "</td>");
        str.append("<td class=\"\" >" + (tb[i].BreachUserName != null ? tb[i].BreachUserName : "未知") + "</td>");
        str.append("<td class=\"\" >" + (tb[i].VictimsUserName != null ? tb[i].VictimsUserName : "未知") + "</td>");

        str.append("<td class=\"\" >" + (tb[i].BreachAmount != null ? tb[i].BreachAmount.toFixed(2) : "未知") + "</td>");
        str.append("<td class=\"\" >" + (tb[i].BreachReason != null ? tb[i].BreachReason : "未知") + "</td>");
        str.append("<td class=\"\" >" + (tb[i].ActualAmount != null ? tb[i].ActualAmount.toFixed(2) : "未知") + "</td>");
        str.append("<td class=\"\" >" + (tb[i].BreachStatus != null ? tb[i].BreachStatus == "1" ? "处理完成" : "处理中" : "未知") + "</td>");
        var btn = "";
        if (!!(arryDictNameCode[tb[i].DictCode])) {
            if (tb[i].DictCode == 1)
                btn = arrybtncodes[".seebtn"];
            else if (tb[i].DictCode == 2) {
                // console.log(tb[i].VictimsUser + " " + tb[i].BreachUser)
                if (tb[i].VictimsUser == user)
                    btn = arrybtncodes[".dobtn"];
                else if (tb[i].BreachUser == user) {
                    btn = arrybtncodes[".doyibin"];
                }
            }
        }


        str.append("<td class=\"\" in='" + i + "'bval='" + tb[i].BreachCode + "' aval='" + tb[i].JSC_CODE + "'>" +
            btn + "</td>");
        str.append("</tr>");

    }

    $('#Searchresult').empty().append(str.toString());
    randbtn();
    $("#curpage").text(page_index + 1);
    
    return false;
}


//数据
function getlists() {
    return $("#selecttodo").data["selecttodo"].dt;
}

//条数
function listCount() {
    return $("#selecttodo").data["selecttodo"].count;
}

//每页条数
function onepcount() {
    var p = parseInt($(".pageHandleArea > ul > li.paginalNum > a.selectthis").text());
    if (p != undefined && /\d+/img.test(p))
        return parseInt(p);
    return 10;
}

//页数
function pcount(count) {
    var p = onepcount(); // parseInt($("#onePageCount").val());
    return Math.ceil(count / p);
}

//初始化
function listinit() {
    var count = listCount();
    var pcounts = pcount(count) || 1;
    $("#allcount").text(count);
    $("#allpcount").text(pcounts);
    initPagination(pcounts);
    initButtons();
}

//初始翻页设置
function initPagination(count) {
    $("#Pagination, #Pagination2").pagination(count, {
        prev_text: "上一页",
        next_text: "下一页",
        num_display_entries: 6,
        num_edge_entries: 1,
        prev_show_always: false,
        next_show_always: false,
        callback: pageselectCallback,
        items_per_page: 1 //,

        //link_to: "?id=__id__"
    });

}

//按钮事件
function initButtons() {
    $('#btnPrev').click(function() {
        $('#Pagination').trigger('prevPage');
    });
    $('#btnNext').click(function() {
        $('#Pagination').trigger('nextPage');
    });
    $('#btnSet').click(function() {
        // $('#Pagination').trigger('setPage', parseInt($("#topage").val()) - 1);
        var vl = $("#topage").val();
        if (DataType.UInt32.CheckRange(vl))
            $('#Pagination').trigger('setPage', parseInt(vl) - 1);
    });
}

function randbtn() {
    $("button[type=button][class=dobtn]").on("click", function() {
        //console.log($(this).parent().parent().find("a.go"))
        $(this).parent().parent().find("span.goto").click();
        
    })
    $("button[type=button][class=seebtn]").on("click", function() {
        //$(this).parent().parent().find("span.goto").click();
        
        var isOrderId = $(this).parent().parent().children("td[isOrderId]").eq(0);
        var isJsc = $(this).parent().parent().children("td[isJsc]").eq(0);

        var textOrderId, textJsc;
        var url;
        //if (isOrderId != "")
        //{
            textOrderId = isOrderId.text();
            textJsc = isJsc.text();

            //  var isOrderIdbool = isOrderId.attr("isOrderId");
            url = "../Refund/RefundIndex.aspx?OrderId=" + textOrderId + "&JSC_CODE=" + textJsc;
        // window.open(url);
            jQuery("a[href='Refund/RefundIndex.aspx']", window.parent.document).addClass("curent");
            jQuery("a[href='JSC/BreakLogMsg.aspx']", window.parent.document).removeClass("curent");
            location.href = url;
        //}
        //else
        //{
        //    textJsc = isJsc.text();

        //    //  var isOrderIdbool = isOrderId.attr("isOrderId");
        //    url = "../Refund/RefundIndex.aspx?JSC_CODE=" + text;
        //    // window.open(url);
        //    location.href = url;
        //}
        //$(this).ShowMenuLeft('售后管理', '退款管理', '退款记录');
            
    })
    
}