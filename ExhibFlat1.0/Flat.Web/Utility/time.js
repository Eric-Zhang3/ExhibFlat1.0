﻿//var hiddentiem = $("<input type=\"hidden\" id=\"hdstarttime\"/>");
//$(hiddentiem).appendTo("body");
//倒数时间详细页面
function showTimeList(overdata, showObjId, isStart, startdata) {
    var arr = overdata.split(" ");
    var arr1 = arr[0].split("-");
    var arr2 = arr[1].split(":");
    var endData = new Date(arr1[0], (Number(arr1[1]) - 1), arr1[2], arr2[0], arr2[1], arr2[2]);
    arr = startdata.split(" ");
    arr1 = arr[0].split("-");
    arr2 = arr[1].split(":");
    var StartData = new Date(arr1[0], (Number(arr1[1]) - 1), arr1[2], arr2[0], arr2[1], arr2[2]);

    ///离开始的秒数
    var startTotal = (StartData - new Date()) / 1000;


    // 剩余总秒
    var total = (endData -new Date()) / 1000;
    if (parseInt(total) <= 0) {
        $("#" + showObjId).text("此活动已经结束.");
        clearInterval(showTime);
        return;
    }

    //还没开始 ，倒计时离开始还有多久
    if (parseInt(startTotal) >= 0) {
        // 计算时间
        var day = parseInt(startTotal / 86400);
        //var hour = parseInt(total / 3600);
        var hour = parseInt(startTotal % 86400 / 3600);
        var min = parseInt((startTotal % 3600) / 60);
        var sec = parseInt((startTotal % 3600) % 60);

        if (hour.toString().length == 1)
            hour = "0" + hour;
        if (min.toString().length == 1)
            min = "0" + min;
        if (sec.toString().length == 1)
            sec = "0" + sec;
        var showTime = "<span><i class='hour'>" + day + "</i>天<i class=\"hour\">" + hour + "</i>时<i class=\"min\">" + min + "</i>分<i class=\"sec\">" + sec + "</i>秒</span>";
        //"<strong>" + day + "</strong>天<strong>" + hour + "</strong>时<strong>" + min + "</strong>分<strong>" + sec + "</strong>秒";
        $("#" + showObjId).html(showTime);//"离开始还有：" +
        $("#buyButton").attr("disabled", "disabled");
        $("#buyButton").attr("class", "grey");
    }
    else {
        // 计算时间
        var day = parseInt(total / 86400);
        //var hour = parseInt(total / 3600);
        var hour = parseInt(total % 86400 / 3600);
        var min = parseInt((total % 3600) / 60);
        var sec = parseInt((total % 3600) % 60);

        if (hour.toString().length == 1)
            hour = "0" + hour;
        if (min.toString().length == 1)
            min = "0" + min;
        if (sec.toString().length == 1)
            sec = "0" + sec; 
        var showTime = "<span><i class='hour'>" + day + "</i>天<i class=\"hour\">" + hour + "</i>时<i class=\"min\">" + min + "</i>分<i class=\"sec\">" + sec + "</i>秒</span>";

        // 显示
        if (isStart == "1") {
            $("#" + showObjId).html(showTime);//"离结束还有：" +
            $("#buyButton").show();
        }
        else
            $("#" + showObjId).html("即将开始");
    }
}

function getServerTime() {
    var nowtime = $("#hdstarttime").val();
    if (nowtime != "" && nowtime != null && nowtime != "undefined") {
        nowtime = nowtime - 0 + 1000;
    } else {
        var xmlHttp = false;
        //获取服务器时间 
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e2) {
                xmlHttp = false;
            }
        }

        if (!xmlHttp && typeof XMLHttpRequest != 'undefined') {
            xmlHttp = new XMLHttpRequest();
        }

        xmlHttp.open("GET", "null.txt", false);
        xmlHttp.setRequestHeader("Range", "bytes=-1");
        xmlHttp.send(null);

        severtime = new Date(xmlHttp.getResponseHeader("Date"));
        nowtime = severtime.getTime();
    }
    $("#hdstarttime").val(nowtime);
    return nowtime;
}
getServerTime();
//setInterval(getServerTime, 1000);