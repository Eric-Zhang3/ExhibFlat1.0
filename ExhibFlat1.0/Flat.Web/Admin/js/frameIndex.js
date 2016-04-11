//override  Frame.js;
//选择点击一级菜单显示
//selecttype
function ShowMenuLeftOnlyIndex(role, node, secondnodei, threenodei) {
    if (!node)
        return;
    var firstnode = node; //|'营销管理';\
    var secondnode = secondnodei || null;
    var threenode = threenodei || null;
    var selfrole = role || "common";
    // var admin = !!adminbool;
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
                if ($(this).attr("Title") == "")
                    $menutitle = $('<div ></div>');
                else
                    $menutitle = $('<div class="admin_menutitle"></div>');
                $menuaspan = $("<span onclick='ShowSecond(this)'>" + '<em></em>' + "" + $(this).attr("Title") + " " + '<cite class="active"></cite>' + "</span>"); //获取二级分类名称
                $menutitle.append($menuaspan);
                $(this).find("PageLink").each(function(k) {
                    var a = this.hasAttribute ? this.hasAttribute("act") : this.getAttribute("act") != null;
                    var hasrole = this.getAttribute("role") || "common";
                    if (hasrole != selfrole && hasrole != "common")
                        return true;

                    var link_href = $(this).attr("Link");
                    var link_title = $(this).attr("Title");
                    $alink = $("<a href='" + link_href + "' target='frammain'>" + link_title + "</a>");
                    if (link_title == "短信套餐购买" || link_title == "渠道授权" || link_title == "分销订单") {
                        $alink = $("<a href='" + link_href + "' target='_blank'>" + link_title + "</a>");
                    }
                    if (a) {
                        curenturl = link_href;
                        $alink.addClass("curent");
                    } else if (link_href == curenturl) {
                        $alink.addClass("curent");
                    }
                    $menutitle.append($alink);
                });
                if ($menutitle.find("a").length > 0)
                    $(".mainWp").append($menutitle);
            });
            $("#menu_arrow").css("display", "none");
            $(".admin_content_r").css("left", 52);
            if (threenode != null) {
                curenturl = threenode;
            }
            $("#frammain").attr("src", curenturl);
            $("#frammain").width($(window).width() - 63);
        }
    });
    $(".admin_menu a:contains('" + firstnode + "')").addClass("admin_curent").siblings().removeClass("admin_curent");
}