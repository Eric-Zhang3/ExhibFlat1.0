$(document).ready(function () {

    var $0 = $("#ctl00_contentHolder_cbAll"); $0.attr("Privilege", "0");

    $0 = $("#ctl00_contentHolder_cbSummary"); $0.attr("Privilege", "99999");

    // 店铺管理
    $0 = $("#ctl00_contentHolder_cbShop"); $0.attr("Privilege", "1");
    $0 = $("#ctl00_contentHolder_cbSiteContent"); $0.attr("Privilege", "11");
    $0 = $("#ctl00_contentHolder_cbSMSSettings"); $0.attr("Privilege", "12");
    $0 = $("#ctl00_contentHolder_cbEmailSettings"); $0.attr("Privilege", "13");
    $0 = $("#ctl00_contentHolder_cbPaymentModes"); $0.attr("Privilege", "14");
    $0 = $("#ctl00_contentHolder_cbShippingModes"); $0.attr("Privilege", "15");
    $0 = $("#ctl00_contentHolder_cbShippingTemplets"); $0.attr("Privilege", "16");
    $0 = $("#ctl00_contentHolder_cbExpressComputerpes"); $0.attr("Privilege", "17");
    $0 = $("#ctl00_contentHolder_cbMessageTemplets"); $0.attr("Privilege", "18");
    $0 = $("#ctl00_contentHolder_cbPictureMange"); $0.attr("Privilege", "19");

    //页面管理
    $0 = $("#ctl00_contentHolder_cbPageManger"); $0.attr("Privilege", "2");
    $0 = $("#ctl00_contentHolder_cbManageThemes"); $0.attr("Privilege", "21");
    $0 = $("#ctl00_contentHolder_cbAfficheList"); $0.attr("Privilege", "22");
    $0 = $("#ctl00_contentHolder_cbHelpCategories"); $0.attr("Privilege", "23");
    $0 = $("#ctl00_contentHolder_cbHelpList"); $0.attr("Privilege", "24");
    $0 = $("#ctl00_contentHolder_cbArticleCategories"); $0.attr("Privilege", "25");
    $0 = $("#ctl00_contentHolder_cbArticleList"); $0.attr("Privilege", "26");
    $0 = $("#ctl00_contentHolder_cbFriendlyLinks"); $0.attr("Privilege", "27");
    $0 = $("#ctl00_contentHolder_cbManageHotKeywords"); $0.attr("Privilege", "28");
    $0 = $("#ctl00_contentHolder_cbVotes"); $0.attr("Privilege", "29");


    //商品管理
    $0 = $("#ctl00_contentHolder_cbProductCatalog"); $0.attr("Privilege", "3");
    $0 = $("#ctl00_contentHolder_cbManageProducts"); $0.attr("Privilege", "31");
    $0 = $("#ctl00_contentHolder_cbManageProductsView"); $0.attr("Privilege", "31_1");
    $0 = $("#ctl00_contentHolder_cbManageProductsAdd"); $0.attr("Privilege", "31_2");
    $0 = $("#ctl00_contentHolder_cbManageProductsEdit"); $0.attr("Privilege", "31_3");
    $0 = $("#ctl00_contentHolder_cbManageProductsDelete"); $0.attr("Privilege", "31_4");
    $0 = $("#ctl00_contentHolder_cbInStock"); $0.attr("Privilege", "31_5");
    $0 = $("#ctl00_contentHolder_cbManageProductsUp"); $0.attr("Privilege", "31_6");
    $0 = $("#ctl00_contentHolder_cbManageProductsDown"); $0.attr("Privilege", "31_7");
    $0 = $("#ctl00_contentHolder_cbPackProduct"); $0.attr("Privilege", "31_8");
    $0 = $("#ctl00_contentHolder_cbUpPackProduct"); $0.attr("Privilege", "31_9");

    $0 = $("#ctl00_contentHolder_cbProductUnclassified"); $0.attr("Privilege", "32");
    $0 = $("#ctl00_contentHolder_cbSubjectProducts"); $0.attr("Privilege", "33");
    $0 = $("#ctl00_contentHolder_cbProductBatchUpload"); $0.attr("Privilege", "34");
    $0 = $("#ctl00_contentHolder_cbProductBatchExport"); $0.attr("Privilege", "35");
    

    $0 = $("#ctl00_contentHolder_cbProductLines"); $0.attr("Privilege", "36");
    $0 = $("#ctl00_contentHolder_cbProductLinesView"); $0.attr("Privilege", "36_1");
    $0 = $("#ctl00_contentHolder_cbAddProductLine"); $0.attr("Privilege", "36_2");
    $0 = $("#ctl00_contentHolder_cbEditProductLine"); $0.attr("Privilege", "36_3");
    $0 = $("#ctl00_contentHolder_cbDeleteProductLine"); $0.attr("Privilege", "36_4");

    $0 = $("#ctl00_contentHolder_cbProductTypes"); $0.attr("Privilege", "37");
    $0 = $("#ctl00_contentHolder_cbProductTypesView"); $0.attr("Privilege", "37_1");
    $0 = $("#ctl00_contentHolder_cbProductTypesAdd"); $0.attr("Privilege", "37_2");
    $0 = $("#ctl00_contentHolder_cbProductTypesEdit"); $0.attr("Privilege", "37_3");
    $0 = $("#ctl00_contentHolder_cbProductTypesDelete"); $0.attr("Privilege", "37_4");

    $0 = $("#ctl00_contentHolder_cbManageCategories"); $0.attr("Privilege", "38");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesView"); $0.attr("Privilege", "38_1");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesAdd"); $0.attr("Privilege", "38_2");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesEdit"); $0.attr("Privilege", "38_3");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesDelete"); $0.attr("Privilege", "38_4");

    $0 = $("#ctl00_contentHolder_cbBrandCategories"); $0.attr("Privilege", "39");


    //订单管理
    $0 = $("#ctl00_contentHolder_cbSales"); $0.attr("Privilege", "4");
    $0 = $("#ctl00_contentHolder_cbManageOrder"); $0.attr("Privilege", "41");
    $0 = $("#ctl00_contentHolder_cbManageOrderView"); $0.attr("Privilege", "41_1");
    $0 = $("#ctl00_contentHolder_cbManageOrderDelete"); $0.attr("Privilege", "41_2");
    $0 = $("#ctl00_contentHolder_cbManageOrderEdit"); $0.attr("Privilege", "41_3");
    $0 = $("#ctl00_contentHolder_cbManageOrderConfirm"); $0.attr("Privilege", "41_4");
    $0 = $("#ctl00_contentHolder_cbManageOrderSendedGoods"); $0.attr("Privilege", "41_5");
    $0 = $("#ctl00_contentHolder_cbExpressPrint"); $0.attr("Privilege", "41_6");
    $0 = $("#ctl00_contentHolder_cbManageOrderRefund"); $0.attr("Privilege", "41_7");
    $0 = $("#ctl00_contentHolder_cbManageOrderRemark"); $0.attr("Privilege", "41_8");

    $0 = $("#ctl00_contentHolder_cbPurchaseOrders"); $0.attr("Privilege", "42");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrdersView"); $0.attr("Privilege", "42_1");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrdersEdit"); $0.attr("Privilege", "42_2");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrdersDelete"); $0.attr("Privilege", "42_3");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrdersSendGoods"); $0.attr("Privilege", "42_4");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrdersRefund"); $0.attr("Privilege", "42_5");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrdersRemark"); $0.attr("Privilege", "42_6");

    $0 = $("#ctl00_contentHolder_cbExpressTemplates"); $0.attr("Privilege", "43");
    $0 = $("#ctl00_contentHolder_cbShipper"); $0.attr("Privilege", "44");


    //分销商管理
    $0 = $("#ctl00_contentHolder_cbManageUsers"); $0.attr("Privilege", "5");
    $0 = $("#ctl00_contentHolder_cbManageMembers"); $0.attr("Privilege", "51");
    $0 = $("#ctl00_contentHolder_cbManageMembersView"); $0.attr("Privilege", "51_1");
    $0 = $("#ctl00_contentHolder_cbManageMembersEdit"); $0.attr("Privilege", "51_2");
    $0 = $("#ctl00_contentHolder_cbManageMembersDelete"); $0.attr("Privilege", "51_3");

    $0 = $("#ctl00_contentHolder_cbMemberRanks"); $0.attr("Privilege", "52");
    $0 = $("#ctl00_contentHolder_cbMemberRanksView"); $0.attr("Privilege", "52_1");
    $0 = $("#ctl00_contentHolder_cbMemberRanksAdd"); $0.attr("Privilege", "52_2");
    $0 = $("#ctl00_contentHolder_cbMemberRanksEdit"); $0.attr("Privilege", "52_3");
    $0 = $("#ctl00_contentHolder_cbMemberRanksDelete"); $0.attr("Privilege", "52_4");

    $0 = $("#ctl00_contentHolder_cbOpenIdServices"); $0.attr("Privilege", "53");
    $0 = $("#ctl00_contentHolder_cbOpenIdSettings"); $0.attr("Privilege", "54");




    //分销管理
    $0 = $("#ctl00_contentHolder_cbDistribution"); $0.attr("Privilege", "6");
    $0 = $("#ctl00_contentHolder_cbDistributorGrades"); $0.attr("Privilege", "61");
    $0 = $("#ctl00_contentHolder_cbDistributorGradesView"); $0.attr("Privilege", "61_1");
    $0 = $("#ctl00_contentHolder_cbDistributorGradesAdd"); $0.attr("Privilege", "61_2");
    $0 = $("#ctl00_contentHolder_cbDistributorGradesEdit"); $0.attr("Privilege", "61_3");
    $0 = $("#ctl00_contentHolder_cbDistributorGradesDelete"); $0.attr("Privilege", "61_4");

    $0 = $("#ctl00_contentHolder_cbDistributors"); $0.attr("Privilege", "62");
    $0 = $("#ctl00_contentHolder_cbDistributorsView"); $0.attr("Privilege", "62_1");
    $0 = $("#ctl00_contentHolder_cbDistributorsEdit"); $0.attr("Privilege", "62_2");
    $0 = $("#ctl00_contentHolder_cbDistributorsDelete"); $0.attr("Privilege", "62_3");

    $0 = $("#ctl00_contentHolder_cbRequests"); $0.attr("Privilege", "63");
    $0 = $("#ctl00_contentHolder_cbDistributorsRequests"); $0.attr("Privilege", "63_1");
    $0 = $("#ctl00_contentHolder_cbDistributorsRequestInstruction"); $0.attr("Privilege", "63_2");

    $0 = $("#ctl00_contentHolder_cbManageDistributorSites"); $0.attr("Privilege", "64");
    $0 = $("#ctl00_contentHolder_cbDistributorSiteRequests"); $0.attr("Privilege", "65")

    $0 = $("#ctl00_contentHolder_cbMakeProductsPack"); $0.attr("Privilege", "66");
    $0 = $("#ctl00_contentHolder_ckTaobaoNote"); $0.attr("Privilege", "67");

    $0 = $("#ctl00_contentHolder_cbDistributorSendedMsg"); $0.attr("Privilege", "68");
    $0 = $("#ctl00_contentHolder_cbDistributorAcceptMsg"); $0.attr("Privilege", "69");
    $0 = $("#ctl00_contentHolder_cbDistributorNewMsg"); $0.attr("Privilege", "610");

    //CRM管理
    $0 = $("#ctl00_contentHolder_cbCRMmanager"); $0.attr("Privilege", "7");
    $0 = $("#ctl00_contentHolder_cbProductConsultationsManage"); $0.attr("Privilege", "71");
    $0 = $("#ctl00_contentHolder_cbProductReviewsManage"); $0.attr("Privilege", "72");
    $0 = $("#ctl00_contentHolder_cbReceivedMessages"); $0.attr("Privilege", "73");
    $0 = $("#ctl00_contentHolder_cbSendedMessages"); $0.attr("Privilege", "74");
    $0 = $("#ctl00_contentHolder_cbSendMessage"); $0.attr("Privilege", "75");
    $0 = $("#ctl00_contentHolder_cbManageLeaveComments"); $0.attr("Privilege", "76");
    $0 = $("#ctl00_contentHolder_cbMemberMarket"); $0.attr("Privilege", "77");
    $0 = $("#ctl00_contentHolder_cbClientGroup"); $0.attr("Privilege", "77_1");
    $0 = $("#ctl00_contentHolder_cbClientNew"); $0.attr("Privilege", "77_2");
    $0 = $("#ctl00_contentHolder_cbClientActivy"); $0.attr("Privilege", "77_3");
    $0 = $("#ctl00_contentHolder_cbClientSleep"); $0.attr("Privilege", "77_4");


    //营销推广
    $0 = $("#ctl00_contentHolder_cbMarketing"); $0.attr("Privilege", "8");
    $0 = $("#ctl00_contentHolder_cbGifts"); $0.attr("Privilege", "81");
    $0 = $("#ctl00_contentHolder_cbProductPromotion"); $0.attr("Privilege", "82");
    $0 = $("#ctl00_contentHolder_cbOrderPromotion"); $0.attr("Privilege", "83");
    $0 = $("#ctl00_contentHolder_cbOrderPromotion"); $0.attr("Privilege", "84");
    $0 = $("#ctl00_contentHolder_cbBundPromotion"); $0.attr("Privilege", "85");
    $0 = $("#ctl00_contentHolder_cbGroupBuy"); $0.attr("Privilege", "86");
    $0 = $("#ctl00_contentHolder_cbCountDown"); $0.attr("Privilege", "87");
    $0 = $("#ctl00_contentHolder_cbCoupons"); $0.attr("Privilege", "88");


    //财务统计
    $0 = $("#ctl00_contentHolder_cbFinancial"); $0.attr("Privilege", "9");
    $0 = $("#ctl00_contentHolder_cbAccountSummary"); $0.attr("Privilege", "91");
    $0 = $("#ctl00_contentHolder_cbReCharge"); $0.attr("Privilege", "92");
    $0 = $("#ctl00_contentHolder_cbBalanceDrawRequest"); $0.attr("Privilege", "93");
    $0 = $("#ctl00_contentHolder_cbDistributorAccount"); $0.attr("Privilege", "94");
    $0 = $("#ctl00_contentHolder_cbDistributorReCharge"); $0.attr("Privilege", "95");
    $0 = $("#ctl00_contentHolder_cbDistributorBalanceDrawRequest"); $0.attr("Privilege", "96");
    $0 = $("#ctl00_contentHolder_cbBalanceDetailsStatistics"); $0.attr("Privilege", "97");
    $0 = $("#ctl00_contentHolder_cbBalanceDrawRequestStatistics"); $0.attr("Privilege", "98");


    //统计报表
    $0 = $("#ctl00_contentHolder_cbTotalReport"); $0.attr("Privilege", "10");
    $0 = $("#ctl00_contentHolder_cbSaleTotalStatistics"); $0.attr("Privilege", "101");
    $0 = $("#ctl00_contentHolder_cbUserOrderStatistics"); $0.attr("Privilege", "102");
    $0 = $("#ctl00_contentHolder_cbSaleList"); $0.attr("Privilege", "103");
    $0 = $("#ctl00_contentHolder_cbSaleTargetAnalyse"); $0.attr("Privilege", "104");

    $0 = $("#ctl00_contentHolder_cbProductSaleRanking"); $0.attr("Privilege", "105");
    $0 = $("#ctl00_contentHolder_cbProductSaleStatistics"); $0.attr("Privilege", "106");
    $0 = $("#ctl00_contentHolder_cbMemberRanking"); $0.attr("Privilege", "107");
    $0 = $("#ctl00_contentHolder_cbMemberArealDistributionStatistics"); $0.attr("Privilege", "108");
    $0 = $("#ctl00_contentHolder_cbUserIncreaseStatistics"); $0.attr("Privilege", "109");

    $0 = $("#ctl00_contentHolder_cbDistributionReport"); $0.attr("Privilege", "1011");
    $0 = $("#ctl00_contentHolder_cbPurchaseOrderStatistics"); $0.attr("Privilege", "1012");
    $0 = $("#ctl00_contentHolder_cbDistributionProductSaleRanking"); $0.attr("Privilege", "1013");
    $0 = $("#ctl00_contentHolder_cbDistributorAchievementsRanking"); $0.attr("Privilege", "1014");



    //    

    //  分销管理


    //    $0 = $("#ctl00_contentHolder_cbBatchProducts"); $0.attr("Privilege", "35_10");
    //    $0 = $("#ctl00_contentHolder_cbBatchInStock"); $0.attr("Privilege", "35_11");
    //    $0 = $("#ctl00_contentHolder_cbBatchMembersPrice"); $0.attr("Privilege", "35_12");
    //    $0 = $("#ctl00_contentHolder_cbBatchDistributor"); $0.attr("Privilege", "35_13");







    // 加载后的全选操作 ------------------------------------------------------------------------------------------------------------------------------------

    showOneLayerOnLoad();

    function showOneLayerOnLoad() {

        var flag = true;

        for (var i = 1; i <= 10; i++) {
            $_Control1 = $("input[type='checkbox'][Privilege='" + i + "']");
            var result1 = showTwoLayerOnLoad(i);
            // 如果当前一级下没有下级则判断自己，如果没有选择设置为 false            
            if (result1 == "no" && !$_Control1.attr("checked"))
                flag = false;
            else if (result1)
                $_Control1.attr("checked", true);
            else
                flag = false;
        }
        if (flag)
            $("input[type='checkbox'][Privilege='0']").attr("checked", true);
    };

    function showTwoLayerOnLoad(one) {

        var flag2 = true;
        for (var j = 1; j <= 15; j++) {

            $_Control2 = $("input[type='checkbox'][Privilege='" + one + j + "']");

            // 如果当前一级下没有下级则返回 no ,告诉上级无下级
            if ($_Control2.attr("id") == undefined && j == 1) {
                flag2 = "no";
                return flag2;
            }
            // 如果已经循环到尽头则返回结果
            else if ($_Control2.attr("id") == undefined) {
                return flag2;
            }
            // 如果有下级且没到尽头继续操作
            else if ($_Control2.attr("id") != undefined) {

                // 判断当前的二级下的三级情况
                var result2 = showTheeLayerOnLoad(one, j);


                // 如果当前二级下没有三级则判断自己,如果没选择设置为 false
                if (result2 == "no" && !$_Control2.attr("checked"))
                    flag2 = false;
                else if (result2)
                    $_Control2.attr("checked", true);
                else
                    flag2 = false;
            }
        }

        return flag2;
    };

    function showTheeLayerOnLoad(one, two) {

        var flag3 = true;
        for (var k = 1; k <= 15; k++) {

            $_Control3 = $("input[type='checkbox'][Privilege$='" + one + two + "_" + k + "']");

            // 如果当前二级下没有下级则返回 no ,告诉上级无下级
            if ($_Control3.attr("id") == undefined && k == 1)
                return "no";
            // 如果已经循环到尽头则返回结果
            else if ($_Control3.attr("id") == undefined)
                return flag3;
            // 如果有下级且没到尽头继续操作
            else if ($_Control3.attr("id") != undefined && !$_Control3.attr("checked"))
                flag3 = false;
        }

        return flag3;
    };

    // 单击触发事件 -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    $("input[type='checkbox']").bind('click', function () {

        var value = this.checked;
        // 全选
        if ($(this).attr("privilege") == "0")
            $("input[type='checkbox']").attr("checked", value);
        // 一层的选择
        else if ($(this).attr("privilege") > 0 && $(this).attr("privilege") <= 10) {
            // 没有被选择时
            if (!value)
                $("input[type='checkbox'][Privilege='0']").attr("checked", false);
            // 选择时
            else {
                if (IsOneLayerAllChecked())
                    $("input[type='checkbox'][Privilege='0']").attr("checked", true);
            }
            $("input[type='checkbox'][Privilege^='" + $(this).attr("privilege") + "']").attr("checked", value);
            if ($(this).attr("privilege").substr(0, 1) == '1' && $(this).attr("privilege").indexOf('10')<0 && value == true) {
                $("input[type='checkbox'][Privilege^='10']").attr("checked", false);
            }
            //showTwoLayer(this.Privilege,value);
        }
        // 二层的选择
        else if ($(this).attr("privilege").length = 2 && $(this).attr("privilege") > 10) {
            // 没有被选择时
            if (!value) {
                $("input[type='checkbox'][Privilege='0']").attr("checked", false);
                $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", false);
            }
            // 选择时
            else {
                if (IsTwoLayerAllCheckedOfOne($(this).attr("privilege").substring(0, 1)))
                    $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", true);
                if (IsOneLayerAllChecked())
                    $("input[type='checkbox'][Privilege='0']").attr("checked", true);
            }
            showTheeLayer2($(this).attr("privilege"), value);
        }
        // 三层的选择
        else {
            // 没有被选择时
            if (!value) {
                $("input[type='checkbox'][Privilege='0']").attr("checked", false);
                $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", false);
                $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, this.Privilege.indexOf("_")) + "']").attr("checked", false);
            }
            // 选择时
            else {
                if (IsThreeLayerAllCheckedOfTwo($(this).attr("privilege").substring(0, $(this).attr("privilege").indexOf("_"))))
                    $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, $(this).attr("privilege").indexOf("_")) + "']").attr("checked", true);
                if (IsTwoLayerAllCheckedOfOne($(this).attr("privilege").substring(0, 1)))
                    $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", true);
                if (IsOneLayerAllChecked())
                    $("input[type='checkbox'][Privilege='0']").attr("checked", true);
            }
        }
    })

    // 选择后判断父类是否应该被选择-----------------------------------------------------------------------------------------------------------------------------------------

    // 判断一层是否都选中了
    var IsOneLayerAllChecked = function () {
        for (var i = 1; i <= 15; i++) {
            if (!$("input[type='checkbox'][Privilege='" + i + "']").attr("checked")) {
                return false;
            }
        }
        return true;
    }

    // 判断某一层下的二层是否都选中了
    var IsTwoLayerAllCheckedOfOne = function (one) {
        for (var i = 1; i <= 15; i++) {
            $_Control2 = $("input[type='checkbox'][Privilege='" + one + i + "']");
            if ($_Control2.attr("id") == undefined) {
                break;
            }

            if (!$_Control2.attr("checked")) {
                return false;
            }
        }
        return true;
    }

    // 判断某二层下的三层是否都选中了
    var IsThreeLayerAllCheckedOfTwo = function (two) {
        for (var i = 1; i <= 15; i++) {
            $_Control3 = $("input[type='checkbox'][Privilege='" + two + "_" + i + "']");

            if ($_Control3.attr("id") == undefined) {
                break;
            }

            if (!$_Control3.attr("checked")) {
                return false;
            }
        }
        return true;
    }

    // 全选反选操作-----------------------------------------------------------------------------------------------------------------------------------------------------------------

    //    var showOneLayer = function(one,value) { 
    //    
    //        for(var i=1;i<=10;i++)
    //        {
    //            
    //            $("input[type='checkbox'][Privilege='"+i+"']).attr("checked",value)"); 
    //                      
    //            showTwoLayer(i,value);
    //        } 
    //    };

    //    var showTwoLayer = function(one,value) { 
    //    
    //        for(var j=1;j<=13;j++)
    //        {
    //            
    //            $_Control1=$("input[type='checkbox'][Privilege='"+one+j+"']"); 
    //                      
    //            if ($_Control1.attr("id")==undefined)
    //            {
    //                break;
    //            }
    //            $_Control1.attr("checked",value);
    //            
    //            showTheeLayer(one,j ,value);
    //        } 
    //    };

    var showTheeLayer = function (one, two, value) {

        for (var k = 1; k <= 15; k++) {

            $_Control2 = $("input[type='checkbox'][Privilege$='" + one + two + "_" + k + "']");

            if ($_Control2.attr("id") == undefined) {
                break;
            }
            $_Control2.attr("checked", value);

        }
    };

    var showTheeLayer2 = function (two, value) {

        for (var k = 1; k <= 15; k++) {

            $_Control2 = $("input[type='checkbox'][Privilege$='" + two + "_" + k + "']");

            if ($_Control2.attr("id") == undefined) {
                break;
            }
            $_Control2.attr("checked", value);

        }
    };

}
);
  