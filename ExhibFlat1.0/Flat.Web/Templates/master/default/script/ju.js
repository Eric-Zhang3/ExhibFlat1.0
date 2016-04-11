function Timer() {
    var now = new Date();
    now.setMilliseconds(now.getMilliseconds() + _Timespan);
    //var end_time = document.getElementById("timer").getAttribute("end-time");

   // $(".ju_time").show();
    $.each($(".ju_item"), function (i, element) {
        var ju_time = $(element).find(".ju_time")[0];
        var ju_over = $(element).find(".ju_over")[0];
        var end_time = ju_time.getAttribute("end-time");
        var _EndTime = new Date(end_time);
        var diffDate = _EndTime - now;

        //alert(ju_time.innerText);
        //return false;

        if (diffDate <= 0) {
            $(ju_over).show();
            $(ju_time).hide();
        }
        else {
            $(ju_over).hide();
            var diffDay = parseInt(diffDate / 1000 / 60 / 60 / 24);
            var diffHour = parseInt((diffDate - diffDay * 1000 * 60 * 60 * 24) / 1000 / 60 / 60);
            var diffMinute = parseInt((diffDate - diffDay * 1000 * 60 * 60 * 24 - diffHour * 1000 * 60 * 60) / 1000 / 60);
            var diffSecond = parseInt((diffDate - diffDay * 1000 * 60 * 60 * 24 - diffHour * 1000 * 60 * 60 - diffMinute * 1000 * 60) / 1000);

            ju_time.innerText = diffDay + "Ìì" + FormatTime(diffHour) + "Ê±" + FormatTime(diffMinute) + "·Ö" + FormatTime(diffSecond) + "Ãë";
        }
      }
   )
    //$(".ju_time").show().text();
    setTimeout(Timer, 1000);
}

function FormatTime(iTime)
{
    if (iTime < 10)
    {
        return "0" + iTime;
    }
    else
    {
        return iTime;
    }
}

function Go(skuId)
{
    //document.location.href = "/site/item/show/?sku_id="+skuId;
    window.open("/site/item/show/?sku_id=" + skuId);
}

$(document).ready(function ()
{
    Timer();
});
