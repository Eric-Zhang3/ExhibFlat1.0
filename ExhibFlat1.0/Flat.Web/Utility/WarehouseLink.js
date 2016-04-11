

/** 
 
 @Name：laytpl-v1.1 精妙的js模板引擎 
 @Author：贤心 - 2014-08-16
 @Site：http://sentsin.com/layui/laytpl 
 @License：MIT license
 */

;
!function() {
    "use strict";
    var f,
        b = { open: "{{", close: "}}" },
        c = {
            exp: function(a) { return new RegExp(a, "g") },
            query: function(a, c, e) {
                var f = ["#([\\s\\S])+?", "([^{#}])*?"][a || 0];
                return d((c || "") + b.open + f + b.close + (e || ""))
            },
            escape: function(a) { return String(a || "").replace(/&(?!#?[a-zA-Z0-9]+;)/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/'/g, "&#39;").replace(/"/g, "&quot;") },
            error: function(a, b) {
                var c = "Laytpl Error：";
                return "object" == typeof console && console.error(c + a + "\n" + (b || "")), c + a
            }
        },
        d = c.exp,
        e = function(a) { this.tpl = a };
    e.pt = e.prototype, e.pt.parse = function(a, e) {
        var f = this, g = a, h = d("^" + b.open + "#", ""), i = d(b.close + "$", "");
        a = a.replace(/[\r\t\n]/g, " ").replace(d(b.open + "#"), b.open + "# ").replace(d(b.close + "}"), "} " + b.close).replace(/\\/g, "\\\\").replace(/(?="|')/g, "\\").replace(c.query(), function(a) { return a = a.replace(h, "").replace(i, ""), '";' + a.replace(/\\/g, "") + '; view+="' }).replace(c.query(1), function(a) {
            var c = '"+(';
            return a.replace(/\s/g, "") === b.open + b.close ? "" : (a = a.replace(d(b.open + "|" + b.close), ""), /^=/.test(a) && (a = a.replace(/^=/, ""), c = '"+_escape_('), c + a.replace(/\\/g, "") + ')+"')
        }), a = '"use strict";var view = "' + a + '";return view;';
        try {
            return f.cache = a = new Function("d, _escape_", a), a(e, c.escape)
        } catch (j) {
            return delete f.cache, c.error(j, g)
        }
    }, e.pt.render = function(a, b) {
        var e, d = this;
        return a ? (e = d.cache ? d.cache(a, c.escape) : d.parse(d.tpl, a), b ? (b(e), void 0) : e) : c.error("no data")
    }, f = function(a) { return "string" != typeof a ? c.error("Template not found") : new e(a) }, f.config = function(a) {
        a = a || {};
        for (var c in a) b[c] = a[c]
    }, f.v = "1.1", window.laytpl = f
}();
//窗口加载
$(function () { 
    $.ajax({
        url: '/API/CommonHandler.ashx?op=userrole',
        dataType: 'json',
        type: 'GET',
        timeout: 5000,
        error: function (xm, msg) {
            alert(msg);
        },
        async: false,
        success: function (data) {
            var userrole = data.UserRole;
            var url = "";
            if (userrole != "Member") {
                url="/User/js/menu2.html";
            }else{
                url="/User/js/menu.html";
            }

            $.getJSON("/User/js/json.json", function (d) {
                $.get(url, function (tpl) {
                    laytpl(tpl).render(d, function (render) {
                        document.getElementById('view').innerHTML = render;
                        var cur = location.pathname;
                        $("#view > li > ul > li").each(function () {
                            var $this = $(this);
                            var a = $this.children("a[href^=" + cur + "]");
                            if (a.length < 1) {
                                return true;
                            } else {
                                $(this).addClass("active");
                                return false;
                            }
                        })
                    });
                }).fail(function () {
                    console.log("menu.html loading eeror");
                })
            }).fail(function () {
                console.log("json.json loading eeror");
            });
        }
    });
    //LoadWarehouseLink();
    
});
if (!$.layer) {
    document.write("<scri" + "pt src='/utility/layer/layer.min.js'></sc" + "ript>");
}

//var openlayerlist = [];

function openlayer(url, title) {
    var pageii = $.layer({
        type: 2,
        title: title,
        shadeClose: true,
        maxmin: true,
        fix: false,
        area: ['1024px', 500],
        iframe: {
            src: url
        }
    });
    // openlayerlist.unshift(pageii);
}

function getcookie(objname) {//获取指定名称的cookie的值
    var arrstr = document.cookie.split("; ");
    for (var i = 0; i < arrstr.length; i++) {
        var temp = arrstr[i].split("=");
        if (temp[0] == objname) return unescape(temp[1]);
    }
}

function LoadWarehouseLink() {
    var hySupplier = $("#hySupplier");
    if (!!hySupplier) {
        $.ajax({
            url: '/Admin/LoginUser.ashx?action=login',
            dataType: 'json',
            type: 'GET',
            timeout: 5000,
            error: function(xm, msg) {
                alert(msg);
            },
            async: false,
            success: function(siteinfo) {
                window.open(siteinfo.warehouseURL);
            }
        });
    }
}