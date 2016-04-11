$(document).ready(function () {
    // 如果默认选中了一个收货地址
    $("input[type='radio'][name='SubmmitOrder$Common_ShippingAddressesRadioButtonList']").each(function (i, obj) {
        if (obj.checked) {
            var shippingId = $(this).attr("value");
            ResetAddress(shippingId);
        }
    });

    // 收获地址列表选择触发事件
    $("input[type='radio'][name='SubmmitOrder$Common_ShippingAddressesRadioButtonList']").bind('click', function () {
        var shippingId = $(this).attr("value");
        ResetAddress(shippingId);
    })

    //3级收货地址选择触发事件
    $("#ddlRegions1").bind("change", function () {
        changeExpress("#SubmmitOrder_ddlExpress");
        CalculateFreight($("#ddlRegions1").val()); 
    })
   

    // 配送方式选择触发事件
    $("input[name='shippButton'][type='radio']").bind('click', function () {
        var regionId = $("#regionSelectorValue").val();
        var shippmodeId = $(this).attr("value");
        $("#SubmmitOrder_inputShippingModeId").val(shippmodeId);
        CalculateFreight(regionId);
    })

    // 支付方式选择触发事件
    $("input[name='paymentMode'][type='radio']").bind('click', function () {
        $('#SubmmitOrder_inputPaymentModeId').val($(this).val());
        var flag = $("#chk_tax").prop("checked") == true ? "1" : "0";
        if (flag == "1") {
            CalculateTotalPrice(eval($("#SubmmitOrder_lblTaxRate").html()));
        }
        else {
            CalculateTotalPrice();
        }
    })

    $("#chk_tax").click(function () {
        $.ajax({
            url: "SubmmitOrderHandler.aspx",
            type: 'post', dataType: 'json', timeout: 10000,
            data: { Action: "GetTax", totalPrice: $("#bundlingpricespan").html(),
                taxRate: $("#chk_tax").prop("checked") == true ? $("#SubmmitOrder_litTaxRate").html() : "0.00"
            },
            cache: false,
            success: function (resultData) {
                if (resultData.Status == "OK") {
                    $("#SubmmitOrder_lblTaxRate").html(resultData.Tax);
                    $("#SubmmitOrder_htmlTax").val(resultData.Tax);
                    CalculateTotalPrice(resultData.Tax);
                }
            }
        });
    });
    // 使用优惠券
    $("#btnCoupon").click(function () {
        if (location.href.indexOf("groupBuy") > 0 || location.href.indexOf("countDown") > 0) {
            alert("团购或限时抢购不能使用优惠券")
            return false;
        }
        var couponCode = $("#SubmmitOrder_CmbCoupCode").combobox("getValue");
        var cartTotal = $("#SubmmitOrder_lblTotalPrice").html();
        $.ajax({
            url: "SubmmitOrderHandler.aspx",
            type: 'post', dataType: 'json', timeout: 10000,
            data: { Action: "ProcessorUseCoupon", CartTotal: cartTotal, CouponCode: couponCode },
            cache: false,
            success: function (resultData) {
                if (resultData.Status == "OK") {
                    $("#SubmitOrder_CouponName").html(resultData.CouponName);
                    $("#SubmmitOrder_litCouponAmout").html("-" + resultData.DiscountValue);
                    $("#SubmmitOrder_htmlCouponCode").val(couponCode);
                    CalculateTotalPrice();
                }
                else {
                    alert("您的优惠券编号无效(可能不在有效期范围内), 或者您的商品金额不够");
                 }
            }
        });
    })

    // 创建订单
    $("#SubmmitOrder_btnCreateOrder").click(function () {

        var str = $("#SubmmitOrder_txtShipTo").val();
        var reg = new RegExp("[\u4e00-\u9fa5a-zA-Z]+[\u4e00-\u9fa5_a-zA-Z0-9]*");
        if (str == "" || !reg.test(str)) {
            alert("请输入正确的收货人姓名");
            return false;
        }
        var address = $("#SubmmitOrder_txtAddress").val();
        if (address.length > 41 || address.length < 3) {
            alert('详细地址文字长度在3至40个字符');
            return false;
        }
        var txtZipcode = $('#SubmmitOrder_txtZipcode').val();
        var regzipcode = /^[0-9][0-9]{5}$/;
        if (txtZipcode==""||!regzipcode.test(txtZipcode)) {
            alert("邮政编码格式不对,长度限制在6位数字");
            return false;
        }
      
        if ($("#SubmmitOrder_txtAddress").val() == ""){
            alert("请输入收货人详细地址");
            return false;
        }
        if ($("#SubmmitOrder_txtTelPhone").val() == "" && $("#SubmmitOrder_txtCellPhone").val() == "") {
            alert("请输入电话号码或手机号码");
            return false;
        }
      
        var selectedRegionId = GetSelectedRegionId();
        var selectedcity = $("#ddlRegions2").val();
        var selectedarea = $("#ddlRegions3").val();
        if (selectedRegionId == null || selectedRegionId.length == "" || selectedRegionId == "0" ) {
            alert("请选择您的收货人地址");
            return false;
        }
        if (selectedcity == "" || selectedarea=="") {
            alert("请详细选择您的收货人地址");
            return false;
        }

        if (!PageIsValid()) {
            alert("部分信息没有通过检验，请查看页面提示");
            return false;
        }

        //if ($("#SubmmitOrder_inputShippingModeId").val() == "") {
        //    alert("请选择配送方式");
        //    return false;
        //}
        //if ($("#SubmmitOrder_inputPaymentModeId").val() == "") {
        //    alert("请选择支付方式");
        //    return false;
        //}

    })
});
    
// 重置收货地址
function ResetAddress(shippingId)
{
    var ConsigneeName = $("#SubmmitOrder_txtShipTo");
    var ConsigneeAddress = $("#SubmmitOrder_txtAddress");
    var ConsigneePostCode = $("#SubmmitOrder_txtZipcode");
    var ConsigneeTel = $("#SubmmitOrder_txtTelPhone");
    var ConsigneeHandSet = $("#SubmmitOrder_txtCellPhone");

    $.ajax({
        url: "SubmmitOrderHandler.aspx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { Action: "GetUserShippingAddress", ShippingId: shippingId },
        async: true,
        success: function(resultData) {
            if (resultData.Status == "OK") {
                $(ConsigneeName).val(resultData.ShipTo);
                ConsigneeName.focus();

                $(ConsigneeAddress).val(resultData.Address);
                ConsigneeAddress.focus();

                $(ConsigneePostCode).val(resultData.Zipcode);
                ConsigneePostCode.focus();

                $(ConsigneeTel).val(resultData.TelPhone);
                ConsigneeTel.focus();

                $(ConsigneeHandSet).val(resultData.CellPhone);
                ConsigneeHandSet.focus();
                ResetSelectedRegion(resultData.RegionId);
                CalculateFreight(resultData.RegionId);
              
            }
            else {
                alert("收货地址选择出错，请重试!");
            }
        }
    });
}
//税金
function CalculateTax() {
    var cartTotalPrice = $("#bundlingpricespan").html();
    $.ajax({
        url: "SubmmitOrderHandler.aspx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { Action: 'GetTax', ModeId: paymentModeId, CartTotalPrice: cartTotalPrice},
        success: function(resultData) {
            if (resultData.Status == "OK") {
                $("#SubmmitOrder_lblTaxRate").html(resultData.Tax);
            }
        }
    }); 
}                
// 总金额
function CalculateTotalPrice(tax) {
    if(!tax)
    {
      tax=0.00;
    }
    var cartTotalPrice = $("#bundlingpricespan").html();
    var shippmodePrice = $("#SubmmitOrder_lblShippModePrice").html();
    var couponPrice = $("#SubmmitOrder_litCouponAmout").html();
        
    var total = eval(cartTotalPrice) + eval(couponPrice);    
    
    if ($("#SubmmitOrder_hlkFeeFreight").html() == "")
        total = total + eval(shippmodePrice);
    // 计算支付手续费
    var paymentModeId = $('#SubmmitOrder_inputPaymentModeId').val();
    var flag = $("#chk_tax").prop("checked") == true ? "1" : "0";    
    $.ajax({
            url: "SubmmitOrderHandler.aspx",
            type: 'post', dataType: 'json', timeout: 10000, async: true,
            data: { Action: 'ProcessorPaymentMode', ModeId: paymentModeId, CartTotalPrice: total, TotalPrice: cartTotalPrice, Taxes: tax },
            success: function(resultData) {
                if (resultData.Status == "OK") {
                    $("#SubmmitOrder_lblPaymentPrice").html(resultData.Charge);  
                    total = total + eval(resultData.Charge);
                    if(tax>0)
                    {
                      total=Math.round((total+eval(tax))*100)/100;
                    }
                  $("#SubmmitOrder_lblOrderTotal").html(total.toFixed(2));      
                }
            }
        });     
}

// 重新计算运费
function CalculateFreight(regionId) {
    var weight = $("#SubmmitOrder_litAllWeight").html();
    var shippingModeId = $("#SubmmitOrder_ddlExpress").val();
    //alert(shippingModeId+"____"+weight+"======="+regionId);
    $.ajax({
        url: "SubmmitOrderHandler.aspx",
        type: 'post', dataType: 'json', timeout: 10000,
        async:true,
        data: { Action: 'CalculateFreight', ModeId: shippingModeId, Weight: weight, RegionId: regionId },
        success: function(resultData) {
            if (resultData.Status == "OK") {
               
                 
                $("#SubmmitOrder_lblShippModePrice").html(resultData.Price);
              

                var flag = $("#chk_tax").prop("checked") == true ? "1" : "0";  
                if(flag=="1")
                {
                CalculateTotalPrice(eval($("#SubmmitOrder_lblTaxRate").html()));
                }
                else
                {
                CalculateTotalPrice();
                }
                var s = parseFloat($('#SubmmitOrder_lblShippModePrice').text());
                $("#SubmmitOrder_lblTotalPrice").text((parseFloat($('#SubmmitOrder_lblOrderTotal').text()) + parseFloat($('#SubmmitOrder_lblShippModePrice').text())).toFixed(2));

                //changeExpress("#SubmmitOrder_ddlExpress");
            }
            
        }
        //, error: function() { alert("Error"); }
    });
}

function ShowConsignment(obj){
    if($("#tr_pates").css("display")=="block"){
        $("#tr_pates").css("display","none");
        $(obj).text("切换到代销模式");
    }else{
         $("#tr_pates").css("display","block");
         $(obj).text("切换到普通模式");
    }
}