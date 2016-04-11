// JavaScript Document

//ѡ����һ���˵���ʾ
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
        success: function(xml) {
            $(".mainWp").html('');
            var curenturl = null;
            curenturl = $(xml).find("Module[Title='" + firstnode.replace(/\s/g, "") + "']").attr("Link");
            if (secondnode != null) {
                curenturl = secondnode;
            }

            $(xml).find("Module[Title='" + firstnode.replace(/\s/g, "") + "'] Item").each(function(i) {
                $menutitle = $('<div class="admin_menutitle"></div>');
                $menuaspan = $("<span onclick='ShowSecond(this)'>" + '<em></em>' + "" + $(this).attr("Title") + " " + '<cite class="active"></cite>' + "</span>"); //��ȡ������������
                $menutitle.append($menuaspan);
                $(this).find("PageLink").each(function(k) {
                    var link_href = $(this).attr("Link");
                    var link_title = $(this).attr("Title");
                    $alink = $("<a href='" + link_href + "' target='frammain'>" + link_title + "</a>");
                    if (link_title == "�����ײ͹���") {
                        $alink = $("<a href='" + link_href + "' target='_blank'>" + link_title + "</a>");
                    }
                    if (link_href == curenturl) {
                        $alink.addClass("curent");
                    }
                    $menutitle.append($alink);
                    //$menutitle.append('<div class="clean"></div>');
                });
                $(".mainWp").append($menutitle);
            });
            $(".mainWp").append('<i class="lastArrow"></i>');
            $("#menu_arrow").attr("class", "open_arrow");
            $("#menu_arrow").css("display", "block");
            $(".admin_menu_scroll").css("display", "block");
            $(".admin_content_r").css("left", 192);
            if (threenode != null) {
                curenturl = threenode;
            }
            $("#frammain").attr("src", curenturl);
            $("#frammain").width($(window).width() - 185);
        }
    });
    $(".admin_menu a:contains('" + firstnode + "')").addClass("admin_curent").siblings().removeClass("admin_curent");
}


//������ҹر���
function ExpendMenuLeft() {
    var clientwidth = $(window).width() - 7;
    if ($(".admin_menu_scroll").is(":hidden")) { //���չ��
        $("#menu_arrow").attr("class", "open_arrow");
        $(".admin_menu_scroll").css("display", "block");
        $(".admin_content_r").css("left", 192);
        $("#frammain").width(clientwidth - 185);
    } else { //�������
        $("#menu_arrow").attr("class", "close_arrow");
        $(".admin_menu_scroll").css("display", "none");
        $(".admin_content_r").css("left", 7);
        $("#frammain").width(clientwidth)
    }

}

//��������˵�
function ShowSecond(sencond) {
    if ($(sencond).siblings("a:hidden") != null && $(sencond).siblings("a:hidden").length > 0) {
        $(sencond).siblings("a").css("display", "block");
    } else {
        $(sencond).siblings("a").css("display", "none");
    }
    //���ʱ�л���ͷ
    if ($(sencond).find("cite").hasClass("active")) {
        $(sencond).find("cite").removeClass("active").addClass("normal");
    } else {
        $(sencond).find("cite").removeClass("normal").addClass("active");
    }
}

//����Ӧ�߶�
function AutoHeight() {
    var clientheight = $(this).height();
    var clientwidth = $(this).width() - 15;
    $(".admin_menu_scroll").height(clientheight);
    $(".admin_content_r").height(clientheight);
    if (!$(".admin_menu_scroll").is(":hidden")) {
        clientwidth = clientwidth - 168;
    }
    $("#frammain").width(clientwidth);
}


//���ڱ仯
$(window).resize(function() {
    AutoHeight();
});

//���ڼ���
$(function() {
    AutoHeight();
    $("#menu_left a").live("click", function() {
        $("#menu_left a").removeClass("curent");
        $(this).addClass("curent");
    });
    LoadTopLink();

    //if ($.cookie("guide") == null || $.cookie("guide") == "undefined" || $.cookie("guide") != 1) {
    //    DialogFrame('help/index.html', '������', 750, null);
    //}
});


function LoadTopLink() {
    $.ajax({
        url: '/Admin/LoginUser.ashx?action=login&time=' + Date.now(),
        dataType: 'json',
        type: 'GET',
        timeout: 5000,
        error: function(xm, msg) {
            alert(msg);
        },
        success: function(siteinfo) {
            document.title = siteinfo.sitename;  
            $(".admin_banneritem a:eq(0)").text(siteinfo.sitename);
            $(".userImg a:eq(0)").text(siteinfo.username);
            $(".admin_logo img:eq(0)").attr("src", siteinfo.AdminLogoUrl);
            $('iframe').contents().find("legend").text(siteinfo.SiteDescription);
        }
    });
}