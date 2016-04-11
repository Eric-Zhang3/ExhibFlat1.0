//窗口加载
$(function() {

    $.get("/User/js/loginform.html", function(tpl) {
        $('body').append(tpl);
    }).fail(function() {
        console.log("loginform.html loading eeror");
    })

    $.get("/utility/findreg.ashx", function(d) {
        var str = d.split(';');
        $(".login_tan a:eq(0)").click(function () { 
            location.href = str[0];  
            return false;
        });
        $(".login_tan a:eq(1)").click(function () { 
            location.href = str[1];
            return false;
        });
    })
});