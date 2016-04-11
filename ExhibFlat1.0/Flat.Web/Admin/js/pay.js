function StandardPost(url, args) {
    var form = $("<form method='post' enctype='multipart/form-data' ></form>");
    form.attr({ "action": url });
    for (arg in args) {
        var input = $("<input type='hidden'>");
        input.attr({ "name": arg });
        input.val(args[arg]);
        form.append(input);
    }
    top.$('body').append(form);
    form.submit();
}

function pay() {
    var code = $.query.get("JSC_CODE");
    if (!!code)
        $.post('/API/CommonHandler.ashx?op=Deposit', { 'JSC_CODE': code }, function(d) {

            var data = eval('(' + d + ')');
            $.getJSON(data.SiteUrl + '/WebApi/PaymentAPI.ashx?op=CheckBalance&callback=?', { 'UserID': data.UserID }, function(d) {
                if (d.res == 'false') {
                    top.window.open('/OrderApply.aspx?sign=JSC');
                } else {
                    StandardPost(data.SiteUrl + '/TransferPayment/TransferPayment?JSC_Code=' + code + '&SessionKey=' + data.SessionKey, data);
                }
            });
        })
}