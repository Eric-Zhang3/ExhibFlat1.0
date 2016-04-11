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
    $(".selectItem .selsup").change(function() {
        sel.supplierName = $(this).val();
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
    $("#Aseldatetime").on("click", function(e) {
        e = window.event || e;
        var co = new AddCalendar(e, this, 1, function(v) {
            var a = $('#Aseldatetime2').val();
            if (!!a && a < v) {
                alert('开始时间应小于结束时间');
                return false;
            } else {
                return true;
            }

        });
    });
    $("#Aseldatetime2").on("click", function(e) {
        e = window.event || e;
        var o = new AddCalendar(e, this, 1, function(v) {
            var a = $('#Aseldatetime').val();
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

        var start2 = $('#Aseldatetime').val();
        var end2 = $('#Aseldatetime2').val();
        if (end2 == "")
            sel.ActApplicationTimeTime = start2;
        else if (start2 == "")
            sel.ApplicationTime = end2;
        else if (end2 != "" && start2 != "") {
            sel.ApplicationTime = start2 + " " + end2;
        }
        listinit();
    }
})

$(function() {
    // listinit();
    //$.postWebService("./ActMsgBySupplier.aspx/selectActList", { JsonStr: "" }, function(d) {

    //    $("#selecttodo").data["selecttodo"] = $.parseJSON(d.d);
    //    listinit();

    //});
    var paginalNum = $(".pageHandleArea > ul > li.paginalNum > a");
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
})

var arryDictNameCode = {
    3: ".upTodo.disable", //已通过未付款 disable
    4: ".upTodo", //已付款
    1: ".tocheck", //	
    5: ".downTodo", //预售中	
    6: ".downTodo" //活动中	
}
var arrybtncodes = {
    ".upTodo": "<button type=\"button\" class=\"upTodo abled\">上架</button>",
    ".upTodo.disable": "<button type=\"button\" class=\"upTodo disable\" disabled=\"\">上架</button>",
    ".tocheck": "<button type=\"button\" class=\"tocheck\">审核</button>",
    ".downTodo": "<button type=\"button\" class=\"downTodo\">下架</button>"
}
var arrybtncodesimg = {
    ".upTodo": "<img src=\"../images/uimg.png\" class=\"img_no\" /><img src=\"../images/uimg.png\" class=\"img_no\" /><a class=\"upTodo shangq\"  href='javascript:;'>点击上传</a>",
    ".upTodo.disable": "<a  class=\"upTodo disable noborder\" href='javascript:;' onclick='javascript:;'>点击上传</a>",
    ".tocheck": "<a class=\"tocheck noborder\"  href='javascript:;' onclick='javascript:;'>点击上传</a>",
    ".downTodo": " <img src=\"../images/nimg.png\" class=\"img_has\"/> <img src=\"../images/nimg.png\" class=\"img_has\"/><a  class=\"downTodo noborder\"  href='javascript:;' onclick='javascript:;'>点击上传</a>"
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

function additem(obj, i) {
    var str = new StringBuilder();
    if (i % 2 != 0)
        str.append("<tr class=\"odd\" >");
    else {
        str.append("<tr class=\"\" >");
    }
    str.append("<td class=\"\" ><a href='./ActivitiesDetail.aspx?JSC_CODE=" + obj.JSC_CODE + "' >" + obj.JSC_CODE + "</a></td>");
    str.append("<td class=\"\" >" + obj.ProductName + "</td>");
    str.append("<td class=\"\" >" + obj.SKU + "</td>");
    str.append("<td class=\"\" >" + (obj.OrderCountSum == null ? 0 : obj.OrderCountSum) + "</td>");
    str.append("<td class=\"\" >" + (obj.CountSum == null ? 0 : obj.CountSum) + "</td>");
    str.append("<td class=\"\" >" + (obj.OrderPriceSum == null ? 0 : obj.OrderPriceSum) + "</td>");
    str.append("<td class=\"\" >" + obj.CompanyName + "</td>");
    str.append("<td class=\"\" >" + obj.DictName + "</td>");
    str.append("<td class=\"\" >" + (/Date/.test(obj.ApplicationTime) ? eval('new ' +
        obj.ApplicationTime.replace(/\//g, "")).format("yyyy-MM-dd hh:mm:ss") : '0000-00-00 00:00:00') + "</td>");

    str.append("<td class=\"\" > "
        + "<div class=\"input-group\"> <span class=\"input-group-addon xia\"><i class='xia'></i></span>"
        + " <input  type=\"text\" disabled='' class=\"form-control sortval\" in='" + +i + "' val='" + obj.JSC_CODE + "'" +
        " value='" + (obj.PositionSort == null ? 0 : obj.PositionSort) + "'> <span class=\"input-group-addon shang\"><i class='shang'></i></span> </div>" + "</td>");
    str.append("<td class=\"" + (obj.DictCode <= 4 ? "edit" : "") + " datepicker\" val='" + obj.JSC_CODE + "' >" + (/Date/.test(obj.ActivityStartTime) ? eval('new ' + obj.ActivityStartTime.replace(/\//g, "")).format("yyyy-MM-dd") : '未开始') + "</td>");
    str.append("<td class=\"editdate\" >" + (/Date/.test(obj.ActivityEndTime) ? eval('new ' + obj.ActivityEndTime.replace(/\//g, "")).format("yyyy-MM-dd") : '') + "</td>");
    str.append("<td class=\"" + (obj.DictCode <= 3 ? "edit" : "") + " text\" ty='A' val='" + obj.JSC_CODE + "'>" + (obj.ServiceCharge == null ? 0 : obj.ServiceCharge) + "</td>"); // ServiceCharge
    str.append("<td class=\"" + (obj.DictCode <= 3 ? "edit" : "") + " text\" ty='B' val='" + obj.JSC_CODE + "'>" + (obj.SupplierBreach == null ? 0 : obj.SupplierBreach) + "</td>");
    str.append("<td class=\"\" >" + (obj.BreachRate == null ? 0 : obj.BreachRate) + '%' + "</td>");
    str.append("<td class=\"\" >" + (obj.CashDeposit == null ? 0 : obj.CashDeposit) + "</td>");
    if ((!!(obj.ImageBig) || !!(obj.ImageSmall)) && obj.DictCode == 4) {
        str.append("<td class=\"\" in='" + i + "' val='" + obj.JSC_CODE + "'>" +
            "<img src=\"" + obj.ImageSmall + "\" width=\"20\" height=\"20\" val='" + obj.ImageSmall +
            "' class=\"img_has\" onerror=\"javascript: this.src = '../images/nimg.png';\" /><img src=\""
            + obj.ImageSmall + "\" width=\"20\" height=\"20\" val=\"" + obj.ImageBig +
            "\" class=\"img_has\" onerror=\"javascript: this.src = '../images/nimg.png';\"/><a class=\"upTodo shangq\"  href='javascript:;'>点击修改</a>" + "</td>");
    } else if (obj.DictCode == 5 || obj.DictCode == 6) {
        str.append("<td class=\"\" in='" + i + "' val='" + obj.JSC_CODE + "'>" +
            "<img src=\"" + obj.ImageSmall + "\"  width=\"20\" height=\"20\"  onerror=\"javascript: this.src = '../images/nimg.png';\" val='" + obj.ImageSmall +
            "' class=\"img_has\" /><img src=\"" + obj.ImageBig + "\"  width=\"20\" height=\"20\" onerror=\"javascript: this.src = '../images/nimg.png';\" val=\"" + obj.ImageBig +
            "\" class=\"img_has\" /><a class=\"downTodo noborder\"  href='javascript:;' onclick='javascript:;'>点击上传</a>" + "</td>");
    } else {
        str.append("<td class=\"\" in='" + i + "' val='" + obj.JSC_CODE + "'>" +
        (!(arryDictNameCode[obj.DictCode]) ? "" : arrybtncodesimg[arryDictNameCode[obj.DictCode]]) + "</td>");
    }
    str.append("<td class=\"\" in='" + i + "' val='" + obj.JSC_CODE + "' >" +
    (!(arryDictNameCode[obj.DictCode]) ? "" : arrybtncodes[arryDictNameCode[obj.DictCode]]) + "</td>");
    str.append("</tr>");
    return str.toString();
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
    // console.log(count);
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
    });
}

listinit();
var imgup;

function At() {
    if (!!$("a.upTodo.shangq"))
        $("a.upTodo.shangq").parent().removeClass("isact");
    imgup.close();
}

function randbtn() {
    var id;
    // var imgup;
    requirejs(['dialog/js/dialog.min'], function() {
        id = new $.ui.Dialog('button.downTodo', {
            elem: '#xiajia',
            drag: true,
            dragHandle: 'h2',
            overlayClose: true
        });

        imgup = new $.ui.Dialog('a.upTodo.shangq', {
            elem: '#up',

            drag: true,
            dragHandle: 'h2',
            overlayClose: true
        });
    });
    $("#xiajia input[type=button].cancel").off().on('click', function() {
        id.close();
    });
    //下架
    var downTodo = $("button.downTodo");
    var tb = getlists();
    if (downTodo.length > 0) {
        downTodo.each(function() {
            var p = $(this).parent();
            var index = p.attr('in');
            var JSC_CODE = p.attr('val');
            $(this).click(function() {
                $("#xiajia input[type=button].ok").off();
                if (JSC_CODE != undefined && JSC_CODE.length > 0 && tb[index]['JSC_CODE'] == JSC_CODE) {
                    //  console.log(0)
                    $.postWebService("./ActMsgOperator.aspx/getJSC_WY", { 'jsc_code': JSC_CODE }, function(d) {
                        // console.log(1)
                        var dd = JSON.parse(d.d);
                        $("#xiajia .WY_CODE").text(dd.JSC_WYCODE);
                        $("#xiajia .WY_Name").text(dd.ByUser);
                        $("#xiajia .WY_PM").text(dd.SupToB);
                        $("#xiajia .WY_MM").text(dd.SupToUser);
                        $("#xiajia .WY_HJ").text(dd.HJ);

                        $("#xiajia input[type=button].ok").on('click', function() {
                            //todo 下架违约金操作   to_WY
                            id.close();
                            todoWy(JSC_CODE, p, index);
                        });
                    })
                }
            });

        });
    }

    //上架
    var upTodo = $("button.upTodo.abled");
    if (upTodo.length > 0) {
        upTodo.each(function(i) {
            // console.log(i)
            var self = this;
            var p = $(self).parent();
            var index = p.attr('in');
            var JSC_CODE = p.attr('val');
            var onimg = p.prev().find("img.img_no").length;
            $(self).off().on('click', function() {
                if (onimg > 0) {
                    alert("尚未上传宣传图，请先上传图片");
                } else {
                    if (JSC_CODE != undefined && JSC_CODE.length > 0 && tb[index]['JSC_CODE'] == JSC_CODE) {
                        $.postWebService("./ActMsgOperator.aspx/Uptodo", { 'JSC_CODE': JSC_CODE }, function(d) {
                            if (d.d != "F") {
                                var y = parseInt(d.d);
                                tb[index].DictCode = y;
                                if (y == 5)
                                    tb[index].DictName = "预售中";
                                else if (y == 6)
                                    tb[index].DictName = "活动中";
                                else if (y == 10)
                                    tb[index].DictName = "活动失败";
                                alert("上架成功");
                                var code = tb[index].DictCode + '';
                                var b = true;
                                if (sel.ActivityStatus != '') {
                                    var arry = sel.ActivityStatus.split(",");
                                    for (var i in arry) {
                                        if (arry[i] == code) {
                                            b = false; //存在
                                            break;
                                        }
                                    }
                                }
                                if (b) {
                                    if (index == 0) {
                                        p.parent().next().before(additem(tb[index], index));
                                    } else {
                                        p.parent().prev().after(additem(tb[index], index));
                                    }
                                }
                                p.parent().remove();
                                randbtn();
                            } else {
                                alert("上架失败~");
                            }
                        });
                    }
                }
            });
        })
    }

    function todoWy(JSC_CODE, p, index) {
        var AbendReason = $("#xiajia select.WY_YO").val();
        var meno = $("#xiajia textarea.WY_MEMO").val();
        //todo 下架违约金操作   to_WY
        $.postWebService("./ActMsgOperator.aspx/Downtodo", { 'JSC_CODE': JSC_CODE, 'AbendReason': AbendReason, 'meno': meno }, function(d) {
            if (d.d == "F") {
                alert('sorry~');
            } else {
                tb[index].DictCode = 7;
                tb[index].DictName = "活动终止";
                var code = tb[index].DictCode + '';
                var b = true;
                if (sel.ActivityStatus != '') {
                    var arry = sel.ActivityStatus.split(",");
                    for (var i in arry) {
                        if (arry[i] == code) {
                            b = false; //存在
                            break;
                        }
                    }
                }
                if (b) {
                    // console.log(index);
                    if (index == 0) {
                        //  console.log('111')
                        p.parent().next().before(additem(tb[index], index));
                    } else {
                        p.parent().prev().after(additem(tb[index], index));
                    }
                }
                p.parent().remove();
                randbtn();
                alert("产生违约单");
            }
        });
    }

    ///-----------------------------------------------
    //上传图片
    var shangq = $("a.upTodo.shangq");
    if (shangq.length > 0) {
        shangq.each(function() {
            $(this).click(function() {
                //$("#upimg").attr('src', "handler/upimg.aspx?time=" + Date.now());
                //弹出上传窗口
                var self = this;
                var p = $(self).parent();
                p.addClass("isact");

                var index = p.attr('in');
                var JSC_CODE = p.attr('val');
                $('#upimg').attr({ 'in': index, 'val': JSC_CODE });
                $("#jsc_code_up").val(JSC_CODE);

                var h = $('#flUpload').parent().html();
                $('#flUpload').parent().empty().html(h);
                var h2 = $('#FileUpload2').parent().html();
                $('#FileUpload2').parent().empty().html(h2);
                cle2();
                cle();
            });
        });
    }
    ///-----------------------------------------------

    //排位
    // paixun();


    //审核
    var tocheck = $("button.tocheck");
    if (tocheck.length > 0) {
        tocheck.each(function() {
            $(this).click(function() {
                var self = this;
                var p = $(self).parent();
                var index = p.attr('in');
                var JSC_CODE = p.attr('val');
                location.href = "./ActivityExamine.aspx?JSC_CODE=" + JSC_CODE;
            });
        });
    }

    ///图片预览
    showimg();
    if ($("#ableedit").hasClass("initEdit")) {
        initedit();
    }
}

//排位
function paixun() {
    //排位
    var shang = $(".input-group-addon.shang");
    var xia = $(".input-group-addon.xia");
    var sortval = $("input.form-control.sortval");
    sortval.removeAttr('disabled');
    shang.each(function() {
        var self = this;
        $(self).off().on('click', function() {
            //console.log("shang")
            var p = $(self).prev('input');
            var vs = p.val();
            var v = parseInt(vs) - 1;
            if (v >= 0) {
                p.val(v);
                p.change()
            }
        })
    });
    xia.each(function() {
        var self = this;
        $(self).off().on('click', function() {
            var p = $(self).next('input');
            var vs = p.val();
            var v = parseInt(vs) + 1;
            if (v >= 0) {
                p.val(v);
                p.change()
            }
        })

    });
    sortval.each(function() {
        $(this).on('keydown', function(event) {
            var e = event || window.event;
            if (!e.ctrlKey && e.keyCode == 13) {
                $(this).blur()
            }
        });
        $(this).change(function() {

            var code = $(this).attr('val');
            var Sort = $(this).val();
            //  console.log(Sort)
            if (code != undefined && /^JSC/.test(code) && Sort != undefined && /^\d+$/img.test(Sort))
                $.postWebService("./ActMsgOperator.aspx/setSort", { JSC_CODE: code, PositionSort: Sort, p: 0 }, function(d) {
                    //  console.log(d.d);
                })
            else {
                alert('格式错误');
            }
        })
    });
}

//取消排位
function upaixun() {
    $(".input-group-addon.shang").off();
    $(".input-group-addon.xia").off();
    $("input.form-control.sortval").off().attr('disabled', '');
}


function showimg() {
    ///图片预览
    var x = -100;
    var y = 14;
    // var sizeX = 100;
    /*提示图的宽*/
    var sizeY = 100;
    /*提示图的高*/
    $("img.img_has").hover(function(e) {
            var tooltip = "<div id='tooltip'><img src='"
                + $(this).attr("val") + "' alt='" + $(this).attr("title") +
                "预览图' height='" + sizeY + "'/></div>";
            $("body").append(tooltip);
            $("#tooltip").css({
                "position": 'absolute',
                "top": e.pageY + y,
                "left": e.pageX + x
            })
        },
        function(e) {
            $("#tooltip").remove()
        });
    $("img.img_has").mousemove(function(e) {
        $("#tooltip").css({
            "top": e.pageY + y,
            "left": e.pageX + x
        })
    });

}