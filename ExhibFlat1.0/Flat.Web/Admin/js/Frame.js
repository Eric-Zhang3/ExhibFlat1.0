// JavaScript Document

//选择点击一级菜单显示
//selecttype
function ShowMenuLeft(firstnode, secondnode, threenode) {
    $.ajax({
        url: "Menu.xml?date=" + new Date(),
        dataType: "xml",
        type: "GET",
        async: false,
        timeout: 10000,
        error: function(xm, msg) {
            alert("loading xml error");
        },
        success: function (xml) {

            $(".mainWp").html('');
            var curenturl = null;
            //根据单机的顶级项名称加载该模块对应的 链接。
            curenturl = $(xml).find("Module[Title='" + firstnode.replace(/\s/g, "") + "']").attr("Link");
            if (secondnode != null) {
                curenturl = secondnode;
            }
            //循环二级列表。
            $(xml).find("Module[Title='" + firstnode.replace(/\s/g, "") + "'] Item").each(function (i) {
                //创建Moudel节点每个ITEM项目详细操作列表。
                $menutitle = $('<div class="admin_menutitle"></div>');
                $aBox = $('<div class="aBox"></div>');
                $menuaspan = $("<span onclick='ShowSecond(this)'>" + '<em></em>' + "" + $(this).attr("Title") + " " + '<cite class="active"></cite>' + "</span>"); //获取二级分类名称
                $menutitle.append($menuaspan);
                //回调每个 pagelink 节点。
                $(this).find("PageLink").each(function(k) {
                    var link_href = $(this).attr("Link");
                    var link_title = $(this).attr("Title");
                    //为每个pageLink生成超链接。
                    $alink = $("<a href='" + link_href + "' target='frammain'>" + link_title + "</a>");
                    //打开新窗口或者
                    if (link_title == "短信套餐购买" || link_title == "渠道授权" || link_title == "分销订单") {
                        $alink = $("<a href='" + link_href + "' target='_blank'>" + link_title + "</a>");
                    }
                   
                    if (link_href == curenturl) {
                        $alink.addClass("curent");
                    }
                    $menutitle.append($alink);
                    //$menutitle.append('<div class="clean"></div>');
                });//对PageLink回调结束。
                $(".mainWp").append($menutitle);
            });//对ITEM回调结束。
            $(".mainWp").append('<i class="lastArrow"></i>');
            //箭头
            $("#menu_arrow").attr("class", "open_arrow");
            $("#menu_arrow").css("display", "block");
            $(".admin_menu_scroll").css("display", "block");
            $(".admin_content_r").css("left", 199);
            if (threenode != null) {
                curenturl = threenode;
            }
            //模块默认加载文档。
            $("#frammain").attr("src", curenturl);
            $("#frammain").width($(window).width() - 210);

        }
    });
    $(".admin_menu a:contains('" + firstnode + "')").addClass("admin_curent").siblings().removeClass("admin_curent");
}


//点击左右关闭树
function ExpendMenuLeft() {
    var clientwidth = $(window).width() - 7;
    if ($(".admin_menu_scroll").is(":hidden")) { //点击展开
        $("#menu_arrow").attr("class", "open_arrow");
        $(".admin_menu_scroll").css("display", "block");
        $(".admin_content_r").css("left", 199);
        $("#frammain").width(clientwidth - 203);
        $(window).resize(function () {
            $("#frammain").width($(window).width() - 210);
        })
    } else { //点击隐藏
        $("#menu_arrow").attr("class", "close_arrow");
        $(".admin_menu_scroll").css("display", "none");
        $(".admin_content_r").css("left", 7);
        $("#frammain").width(clientwidth - 11);
        $(window).resize(function () {
            $("#frammain").width($(window).width() - 20);
        })
    }

}

//点击二级菜单
function ShowSecond(sencond) {
    if ($(sencond).siblings("a:hidden") != null && $(sencond).siblings("a:hidden").length > 0) {
        $(sencond).siblings("a").css("display", "block");
    } else {
        $(sencond).siblings("a").css("display", "none");
    }
    //点击时切换箭头
    if ($(sencond).find("cite").hasClass("active")) {
        $(sencond).find("cite").removeClass("active").addClass("normal");
    } else {
        $(sencond).find("cite").removeClass("normal").addClass("active");
    }
}

//自适应高度
function AutoHeight() {
    var clientheight = $(this).height();
    var clientwidth = $(this).width();
    $(".admin_content_r").height(clientheight - 50);
    if (!$(".admin_menu_scroll").is(":hidden")) {
        clientwidth = clientwidth - 210;
    }
    $("#frammain").width(clientwidth);
}


//窗口变化
$(window).resize(function() {
    AutoHeight();
});

//窗口加载
$(function() {
    AutoHeight();
    $(".admin_menutitle a").live("click", function() {
        $(".admin_menutitle a").removeClass("curent");
        $(this).addClass("curent");
    });
    LoadTopLink();
});


function LoadTopLink() {
    $.ajax({
        url: '/API/LoginUser.ashx?action=login&time=' + Date.now(),
        dataType: 'json',
        type: 'GET',
        timeout: 5000,
        async: false,
        error: function(xm, msg) {
            alert(msg);
        },
        success: function(siteinfo) {
            document.title = "商家中心-"+siteinfo.sitename;
            $(".admin_banneritem a:eq(0)").text(siteinfo.sitename);
            $(".userImg a:eq(0)").text(siteinfo.username);
            //$(".admin_banneritem a:eq(3)").attr("href", siteinfo.accountURL);
            $(".admin_logo img:eq(0)").attr("src", siteinfo.AdminLogoUrl); 
            $('iframe').contents().find("legend").text(siteinfo.SiteDescription);
        }
    });
}