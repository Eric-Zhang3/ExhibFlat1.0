var datatb = new $.Hashtable();

//$("#prcha").parent().mouseleave(function () { $(".num input").blur() });

function changepr() {

    $("#prcha li.num").remove();
    $("#prcha").append(
        "<li class=\"num\">大于等于" +
        "<input type=\"text\" class=\"amountD amountDk\" key=\"i1\" " + ' id="jsc_p1" ' +
        " fv-minvalue=\"1\" fv-minvalue-msg=\"采购量至少为1\"" +
        " fv-datatype='Int32' fv-datatype-msg='必须正整数,最大值: 2147483647' fv-compareto=\">=,StartOrderQuantity|<=,MaxProduction\" fv-compareto-msg=\"数量填写错误,应大于等于起订量且小于等于最大生产量,后序填写递增\" fv-msgpanel=\"msgpr\"" + "fv-empty=\"false\" fv-empty-msg=\"数量不能为空！\"" +
        " onchange=\"ichange(this)\"/>，单价为 <input type=\"text\" class=\"amountD amountDv\"  key=\"i1\" " + ' id="jsc_v1" ' +
        " fv-datatype='UFloat' fv-datatype-msg='输入只能为非负数字' fv-compareto=\"<=,JSCPrice\" fv-compareto-msg=\"单价填写错误,应小于等于原聚单价,后序填写递减\"  fv-msgpanel=\"msgprpp\"" + "fv-empty=\"false\" fv-empty-msg=\"单价不能为空！\"" +
        " onchange=\"ichange(this)\" />" + " <cite class=\"none del\" style=\"display: none\" key=\"i1\" onclick=\"del(this)\">×</cite></li> ");
    datatb.clear();
    FormValidateInit();
}

function ichange(obj) {

    var key = $(obj).attr("key"),
        o = {};
    var k = $("input.amountDk[key='" + key + "']").val().replace(" ", "");
    var v = $("input.amountDv[key='" + key + "']").val().replace(" ", "");

    if (datatb.containsKey(key))
        datatb.remove(key);

    if (!!k && !!v) {
        o[k] = v;
        datatb.add(key, o);
    }
    $('#intervalPr').val(JSON.stringify(datatb.getitems()));
}

$(function() {
    $(".spe").click(function() {
        add();
    })
})

function add() {
    var max = 3;
    $(".del").show();
    var i = $(".pre li.num").length;
    if (i < max &&
            parseFloat($("input[key='i" + i + "'].amountDk").val()) >= 0 &&
            parseFloat($("input[key='i" + i + "'].amountDv").val()) >= 0 &&
            $("input[key='i" + i + "'].amountDk").hasClass("validate-succeed") &&
            $("input[key='i" + i + "'].amountDv").hasClass("validate-succeed")
    ) {
        html = '<li class="num">大于等于<input type="text" key="i' + (i + 1) +
            '" class="amountD amountDk" onchange="ichange(this)" ' + ' id="jsc_p' + (i + 1) + '" ' +
            " fv-minvalue=\"1\" fv-minvalue-msg=\"采购量至少为1\"" +
            " fv-datatype='Int32' fv-datatype-msg='必须正整数,最大值: 2147483647' fv-compareto=\">," + "jsc_p" + i + "|<=,MaxProduction\" fv-compareto-msg=\"数量填写错误,应大于等于起订量且小于等于最大生产量,后序填写递增\" fv-msgpanel=\"msgpr\"" +
            '/>，单价为 <input type="text" key="i' + (i + 1) +
            '"  class="amountD amountDv" onchange="ichange(this)"' + ' id="jsc_v' + (i + 1) + '" ' + " fv-datatype='UFloat' fv-datatype-msg='输入只能为非负数字' fv-compareto=\"<," + "jsc_v" + i + "\" fv-compareto-msg=\"单价填写错误,应小于等于原聚单价,后序填写递减\"  fv-msgpanel=\"msgprpp\"" +
            '/> <cite class="del" key="i' + (i + 1) +
            '" onclick="del(this)">×</cite></li>';
        $(".pre").append(html);
        if (i == max - 1) {
            $(".spe").hide();
        }
        FormValidateInit();
    }
}

function del(a) {
    num = $(".pre li.num").length
    if (num > 1) {
        var key = $(a).attr("key");
        if (datatb.containsKey(key))
            datatb.remove(key);
        $(a).parent().remove();
        num = $(".pre li.num").length
        if (num == 1) {
            $(".del").hide();
        }
    } else {
        datatb.clear();
        $(a).parent().find("input").val("");
        $(a).parent().find(".del").hide();
        $(".del").hide();
    }
    $(".spe").css({
        'display': ''
    });
    $('#intervalPr').val(JSON.stringify(datatb.getitems()));
}

requirejs.config({
    baseUrl: '../js/'
});

requirejs(['dialog/js/dialog.min'], function() {
    var di = new $.ui.Dialog('#selectOneProductid', {
        elem: '#testBox',
        overlayClose: true,
        drag: true,
        dragHandle: 'h2'
    });
    //商品未选择,input readonly
    if (!$('#ProductID').val() || !$('#SkuId').val() || !$('#ProductName').val()) {
        $("input[class!=jsc_readonly][type!=button][type!=radio][id!=Pname][id!=Pcode]").attr("readonly", "readonly").on('click', qxz);
    }

    function qxz() {
        alert("请选择商品");
    }

    $("#testBox .dataList input[type=button].ok").on('click', function Tclose() {
        var sel = $('#testBox .dataList .innerTable input[type=radio][name=selp]:checked');
        if (sel.length > 0) {
            var i = sel.attr('in');
            var o = sel.data[i + '-item'];
            //SkuId: "1340_70_74", Productid: 1340, SKU: "h32927-12", ProductName: 
            $('#SkuId').val(o.SkuId);
            $('#ProductID').val(o.Productid);
            $('#ProductName').val(o.ProductName);
            $("#yuanJSCPrice").val(o.SalePrice);
            if (!!o.Unit)
                $('i.jsc_unit').text(o.Unit);
            else {
                $('i.jsc_unit').text('件');
            }
            $('#yuanJSCPriceshow').text("原价: " + o.SalePrice + "元");
            // console.log(o)
            //商品选择,input 可输入
            $("#msgnc").removeClass().addClass("validate-info").empty();

            $("input[class!=jsc_readonly][type!=button][type!=radio][id!=Pname][id!=Pcode]").removeAttr("readonly").off('click', qxz);
        } else {
            $('#SkuId').val("");
            $('#ProductID').val("");
            $('#ProductName').val("");
            $("input[class!=jsc_readonly][type!=button][type!=radio][id!=Pname][id!=Pcode]").attr("readonly", "readonly").on('click', qxz);
        }
        di.close();

    });
    $("#testBox .dataList input[type=button].cancel").on('click', function Tclose() {
        di.close();
    });
    $("#selbtn").on('click', function() {
        var productName = $('#Pname').val();
        var sku = $('#Pcode').val();
        if (sku != undefined && productName != undefined) {

            $.postWebService("ActivityRegister.aspx/SelectProductList", { "sku": sku, "productName": productName }, function(rl) {
                try {
                    var ar = JSON.parse(rl.d);
                    // console.log(ar)
                    if (ar.length > 0) {
                        var html = $("<table></table>");
                        for (var item in ar) {
                            //ProductName: "h32927-15 奥康皮鞋 鞋类 鞋类 39 红色"
                            //Productid: 1340
                            //SKU: "h32927-15"
                            //SkuId: "1340_70_73"
                            var itemhtml = $((item % 2 != 0 ? "<tr class=\"odd\" >" : "<tr class=\"\" >") + "<td width=\"30\">" +
                                "<input type=\"radio\" name='selp' in='" + item + "'/>" + "</td> <td width=\"190\">" +
                                ar[item]['SKU'] + "</td>" + "<td width=\"190\">" + ar[item]['ProductName'] + "</td>" +
                                "<td width=\"190\">" + ar[item]['Stock'] + "</td> <td width=\"190\"> </td><td width=\"115\">"
                                + "<a href=\"../product/EditProduct.aspx?productId=" + ar[item]['Productid'] + "\">查看</a></td></tr>");
                            itemhtml.children('input[type=radio]').data[item + '-item'] = ar[item];
                            html.append(itemhtml);

                        }
                        $('#testBox .dataList .innerTable').empty().append(html);
                    } else {
                        $('#testBox .dataList .innerTable').empty();
                    }
                } catch (e) {
                    $('#testBox .dataList .innerTable').empty();
                }

            });
        }
    });
    $("#selbtn").click();
});