/**
 * Calendar  by Yongjin.C 修改
 * @param gtToday 1(可以大于小于今天)|0(小于今天)|2(大于今天)
 * @param   beginYear           1990
 * @param   endYear             2020
 * @param   language            0(zh_cn)|1(en_us)|2(en_en)|3(zh_tw)|4(jp)
 * @param   patternDelimiter    "-"
 * @param   date2StringPattern  "yyyy-MM-dd"
 * @param   string2DatePattern  "ymd"
 * @version V20060401
 * @version V20061217
 * @version V20080809 add to google project
 * @version V20081226 add language support for japanese 
 * @version V20090104 add fix some bugs in help.html
					  use style.display replace the style.visibility
					  some enhancements and changes
 * @author  KimSoft (jinqinghua [at] gmail.com)
 * NOTE!    you can use it free, but keep the copyright please
 * IMPORTANT:you must include this script file inner html body elment 
 * @see http://code.google.com/p/kimsoft-jscalendar/
 * how to use: AddCalendar(event,this) or AddCalendar(event,this.form.control_date);
 */

function Calendar(e, gtToday, callback, beginYear, endYear, language, patternDelimiter, date2StringPattern, string2DatePattern) {
    e = window.event || e;
    this.eventSrc = e.srcElement || e.target;
    this.gtToday = gtToday || 0; //by Yongjin.C
    this.callback = callback || function() {};
    this.beginYear = beginYear || 1990;
    this.endYear = endYear || 2020;
    this.language = language || 0;
    this.patternDelimiter = patternDelimiter || "-";
    this.date2StringPattern = date2StringPattern || Calendar.language["date2StringPattern"][this.language].replace(/\-/g, this.patternDelimiter);
    this.string2DatePattern = string2DatePattern || Calendar.language["string2DatePattern"][this.language];

    this.dateControl = null;
    this.panel = this.getElementById("__calendarPanel");
    this.iframe = window.frames["__calendarIframe"];
    this.form = null;

    this.date = new Date();
    this.year = this.date.getFullYear();
    this.month = this.date.getMonth();
    this.day = this.date.getDate();

    this.colors = { "bg_cur_day": "#00CC33", "bg_over": "#EFEFEF", "bg_out": "#FFCC00" }
};

Calendar.language = {
    "year": ["\u5e74", "", "", "\u5e74", "\u5e74"],
    "months": [
        ["\u4e00\u6708", "\u4e8c\u6708", "\u4e09\u6708", "\u56db\u6708", "\u4e94\u6708", "\u516d\u6708", "\u4e03\u6708", "\u516b\u6708", "\u4e5d\u6708", "\u5341\u6708", "\u5341\u4e00\u6708", "\u5341\u4e8c\u6708"],
        ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"],
        ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"],
        ["\u4e00\u6708", "\u4e8c\u6708", "\u4e09\u6708", "\u56db\u6708", "\u4e94\u6708", "\u516d\u6708", "\u4e03\u6708", "\u516b\u6708", "\u4e5d\u6708", "\u5341\u6708", "\u5341\u4e00\u6708", "\u5341\u4e8c\u6708"],
        ["\u4e00\u6708", "\u4e8c\u6708", "\u4e09\u6708", "\u56db\u6708", "\u4e94\u6708", "\u516d\u6708", "\u4e03\u6708", "\u516b\u6708", "\u4e5d\u6708", "\u5341\u6708", "\u5341\u4e00\u6708", "\u5341\u4e8c\u6708"]
    ],
    "weeks": [
        ["\u65e5", "\u4e00", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d"],
        ["Sun", "Mon", "Tur", "Wed", "Thu", "Fri", "Sat"],
        ["Sun", "Mon", "Tur", "Wed", "Thu", "Fri", "Sat"],
        ["\u65e5", "\u4e00", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d"],
        ["\u65e5", "\u6708", "\u706b", "\u6c34", "\u6728", "\u91d1", "\u571f"],
    ],
    "confirm": ["\u786E\u5B9A", "Confirm", "Confirm", "\u6e05\u7a7a", "\u524a\u9664"],
    "today": ["\u4eca\u5929", "Today", "Today", "\u4eca\u5929", "\u4eca\u65e5"],
    "close": ["\u53D6\u6D88", "Close", "Close", "\u95dc\u9589", "\u623b\u308b"],
    "date2StringPattern": ["yyyy-MM-dd", "yyyy-MM-dd", "yyyy-MM-dd", "yyyy-MM-dd", "yyyy-MM-dd"],
    "string2DatePattern": ["ymd", "ymd", "ymd", "ymd", "ymd"]
};

Calendar.prototype.draw = function() {
    calendar = this;

    var _cs = [];
    _cs[_cs.length] = '<form id="__calendarForm" name="__calendarForm" method="post">';
    _cs[_cs.length] = '<table id="__calendarTable" width="100%" border="0" cellpadding="3" cellspacing="1" align="center">';
    _cs[_cs.length] = ' <tr>';
    _cs[_cs.length] = '  <th><input class="l" name="goPrevMonthButton" type="button" id="goPrevMonthButton" value="&lt;" \/><\/th>';
    _cs[_cs.length] = '  <th colspan="5"><select class="year" name="yearSelect" id="yearSelect"><\/select><select class="month" name="monthSelect" id="monthSelect"><\/select><\/th>';
    _cs[_cs.length] = '  <th><input class="r" name="goNextMonthButton" type="button" id="goNextMonthButton" value="&gt;" \/><\/th>';
    _cs[_cs.length] = ' <\/tr>';
    _cs[_cs.length] = ' <tr>';
    for (var i = 0; i < 7; i++) {
        _cs[_cs.length] = '<th class="theader">';
        _cs[_cs.length] = Calendar.language["weeks"][this.language][i];
        _cs[_cs.length] = '<\/th>';
    }
    _cs[_cs.length] = '<\/tr>';
    for (var i = 0; i < 6; i++) {
        _cs[_cs.length] = '<tr align="center">';
        for (var j = 0; j < 7; j++) {
            switch (j) {
            case 0:
                _cs[_cs.length] = '<td class="sun">&nbsp;<\/td>';
                break;
            case 6:
                _cs[_cs.length] = '<td class="sat">&nbsp;<\/td>';
                break;
            default:
                _cs[_cs.length] = '<td class="normal">&nbsp;<\/td>';
                break;
            }
        }
        _cs[_cs.length] = '<\/tr>';
    }
    _cs[_cs.length] = ' <tr>';
    _cs[_cs.length] = '  <th colspan="3"><input type="button" class="b" name="selectTodayButton" id="selectTodayButton" \/><\/th>';
    _cs[_cs.length] = '  <th colspan="2"><input type="button" class="b" name="confirmButton" id="confirmButton" \/><\/th>';
    _cs[_cs.length] = '  <th colspan="2"><input type="button" class="b" name="closeButton" id="closeButton" \/><\/th>';
    _cs[_cs.length] = ' <\/tr>';
    _cs[_cs.length] = '<\/table>';
    _cs[_cs.length] = '<\/form>';

    this.iframe.document.body.innerHTML = _cs.join("");
    this.form = this.iframe.document.forms["__calendarForm"];

    this.form.confirmButton.value = Calendar.language["confirm"][this.language];
    this.form.selectTodayButton.value = Calendar.language["today"][this.language];
    this.form.closeButton.value = Calendar.language["close"][this.language];

    this.form.goPrevMonthButton.onclick = function() { calendar.goPrevMonth(this); };
    this.form.goNextMonthButton.onclick = function() { calendar.goNextMonth(this); };
    this.form.yearSelect.onchange = function() { calendar.update(this); };
    this.form.monthSelect.onchange = function() { calendar.update(this); };

    this.form.confirmButton.onclick = function() {
        calendar.subData(calendar);
        calendar.hide();
    };
    this.form.closeButton.onclick = function() {
        calendar.dateControl.value = '';
        calendar.hide();
    };
    this.form.selectTodayButton.onclick = function() {
        var today = new Date();
        calendar.date = today;
        calendar.year = today.getFullYear();
        calendar.month = today.getMonth();
        calendar.dateControl.value = today.format(calendar.date2StringPattern);
        calendar.hide();
    };
};

Calendar.prototype.bindYear = function() {
    var ys = this.form.yearSelect;
    ys.length = 0;
    for (var i = this.beginYear; i <= this.endYear; i++) {
        ys.options[ys.length] = new Option(i + Calendar.language["year"][this.language], i);
    }
};

Calendar.prototype.bindMonth = function() {
    var ms = this.form.monthSelect;
    ms.length = 0;
    for (var i = 0; i < 12; i++) {
        ms.options[ms.length] = new Option(Calendar.language["months"][this.language][i], i);
    }
};

Calendar.prototype.goPrevMonth = function(e) {
    if (this.year == this.beginYear && this.month == 0) {
        return;
    }
    this.month--;
    if (this.month == -1) {
        this.year--;
        this.month = 11;
    }
    this.date = new Date(this.year, this.month, this.day);
    this.changeSelect();
    this.bindData();
};

Calendar.prototype.goNextMonth = function(e) {
    if (this.year == this.endYear && this.month == 11) {
        return;
    }
    this.month++;
    if (this.month == 12) {
        this.year++;
        this.month = 0;
    }
    this.date = new Date(this.year, this.month, this.day);
    this.changeSelect();
    this.bindData();
};

Calendar.prototype.changeSelect = function() {
    var ys = this.form.yearSelect;
    var ms = this.form.monthSelect;
    for (var i = 0; i < ys.length; i++) {
        if (ys.options[i].value == this.date.getFullYear()) {
            ys[i].selected = true;
            break;
        }
    }
    for (var i = 0; i < ms.length; i++) {
        if (ms.options[i].value == this.date.getMonth()) {
            ms[i].selected = true;
            break;
        }
    }
};

Calendar.prototype.subData = function(e) { //add
    if (new Date(this.year, this.month, this.day) > new Date())
        calendar.dateControl.value = new Date().format(calendar.date2StringPattern);
    else
        calendar.dateControl.value = new Date(this.year, this.month, this.day).format(calendar.date2StringPattern);
    calendar.hide();
};

Calendar.prototype.update = function(e) {
    this.year = e.form.yearSelect.options[e.form.yearSelect.selectedIndex].value;
    this.month = parseInt(e.form.monthSelect.options[e.form.monthSelect.selectedIndex].value);
    if (this.day > new Date(this.year, this.month + 1, 0).getDate())
        this.day = new Date(this.year, this.month + 1, 0).getDate();
    this.date = new Date(this.year, this.month, this.day);
    this.changeSelect();
    this.bindData();
};

Calendar.prototype.bindData = function() {
    var calendar = this;
    var dateArray = this.getMonthViewDateArray(this.date.getFullYear(), this.date.getMonth());
    var tds = this.getElementsByTagName("td", this.getElementById("__calendarTable", this.iframe.document));
    for (var i = 0; i < tds.length; i++) {
        tds[i].style.backgroundColor = calendar.colors["bg_over"];
        tds[i].onclick = null;
        tds[i].onmouseover = null;
        tds[i].onmouseout = null;
        tds[i].innerHTML = dateArray[i] || "&nbsp;";
        if (i > dateArray.length - 1) continue;
        if (dateArray[i]) {
            tds[i].onclick = function() {
                if (calendar.dateControl) {
                    var dd = new Date();
                    if (calendar.gtToday == 0 && new Date(calendar.date.getFullYear(), calendar.date.getMonth(), this.innerHTML) > dd) {
                        dd.setDate(dd.getDate() - 1); //by Yongjin.C
                        calendar.dateControl.value = dd.format(calendar.date2StringPattern);
                    } else if (calendar.gtToday == 2 && new Date(calendar.date.getFullYear(), calendar.date.getMonth(), this.innerHTML) <= dd) {
                        dd.setDate(dd.getDate() + 1);
                        calendar.dateControl.value = dd.format(calendar.date2StringPattern);
                    } else
                        calendar.dateControl.value = new Date(calendar.date.getFullYear(), calendar.date.getMonth(), this.innerHTML).format(calendar.date2StringPattern);
                }
                calendar.hide();
            };
            tds[i].onmouseover = function() { this.style.backgroundColor = calendar.colors["bg_out"]; };
            tds[i].onmouseout = function() { this.style.backgroundColor = calendar.colors["bg_over"]; };
            if (calendar.day == dateArray[i]) {
                tds[i].style.backgroundColor = calendar.colors["bg_cur_day"];
                tds[i].onmouseover = function() { this.style.backgroundColor = calendar.colors["bg_out"]; };
                tds[i].onmouseout = function() { this.style.backgroundColor = calendar.colors["bg_cur_day"]; };
            }
        } //end if
    } //end for
};

Calendar.prototype.getMonthViewDateArray = function(y, m) {
    var dateArray = new Array(42);
    var dayOfFirstDate = new Date(y, m, 1).getDay();
    var dateCountOfMonth = new Date(y, m + 1, 0).getDate();
    for (var i = 0; i < dateCountOfMonth; i++) {
        dateArray[i + dayOfFirstDate] = i + 1;
    }
    return dateArray;
};

Calendar.prototype.show = function(dateControl, popuControl) {
    if (this.panel.style.display == "block") {
        this.panel.style.display = "none";
    }
    if (!dateControl) {
        throw new Error("arguments[0] is necessary!")
    }
    this.dateControl = dateControl;
    popuControl = popuControl || dateControl;

    this.draw();
    this.bindYear();
    this.bindMonth();
    if (dateControl.value.length > 0) {
        this.date = new Date(dateControl.value.toDate(this.patternDelimiter, this.string2DatePattern));
        this.year = this.date.getFullYear();
        this.month = this.date.getMonth();
        this.day = this.date.getDate();
    }
    this.changeSelect();
    this.bindData();

    var xy = this.getAbsPoint(popuControl);
    this.panel.style.left = xy.x + "px";
    this.panel.style.top = (xy.y + dateControl.offsetHeight) + "px";
    this.panel.style.display = "block";
    this.panel.style.zIndex = 999;
};

Calendar.prototype.hide = function() {
    this.panel.style.display = "none";
    this.callback();
};

Calendar.prototype.getElementById = function(id, object) {
    object = object || document;
    return document.getElementById ? object.getElementById(id) : document.all(id);
};

Calendar.prototype.getElementsByTagName = function(tagName, object) {
    object = object || document;
    return document.getElementsByTagName ? object.getElementsByTagName(tagName) : document.all.tags(tagName);
};

Calendar.prototype.getAbsPoint = function(e) {
    var x = e.offsetLeft;
    var y = e.offsetTop;
    while (e = e.offsetParent) {
        if (e.style.position == 'absolute' || e.style.position == 'relative' || (e.style.overflow != 'visible' && e.style.overflow != '')) {
            if (e.style.overflow == 'auto' || e.style.overflowX == 'auto' || e.style.overflowY == 'auto') { //修复overflow auto 移位问题
                x += -e.scrollLeft + e.offsetLeft + e.clientLeft;
                y += -e.scrollTop + e.offsetTop + e.clientTop;
                continue; //by Yongjin.C
            }
            break;
        }
        x += e.offsetLeft;
        y += e.offsetTop;
    }
    return { "x": x, "y": y };
};

/**
 * @param   d the delimiter
 * @param   p the pattern of your date
 * @author  meizz
 * @author  kimsoft add w+ pattern
 */
Date.prototype.format = function(style) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(), //day
        "h+": this.getHours(), //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "w+": "\u65e5\u4e00\u4e8c\u4e09\u56db\u4e94\u516d".charAt(this.getDay()), //week
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(style)) {
        style = style.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(style)) {
            style = style.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return style;
};

/**
 * @param d the delimiter
 * @param p the pattern of your date
 * @rebuilder kimsoft
 * @version build 2006.12.15
 */
String.prototype.toDate = function(delimiter, pattern) {
    delimiter = delimiter || "-";
    pattern = pattern || "ymd";
    //字符串中是否有[未知格式出错不对]中某个字,返回现在时间
    if (/[\u672a\u77e5\u683c\u5f0f\u51fa\u9519\u4e0d\u5bf9]/.test(this))
        return new Date();
    var a = this.split(delimiter);
    var y = parseInt(a[pattern.indexOf("y")], 10);
    //remember to change this next century ;)
    if (y.toString().length <= 2) y += 2000;
    if (isNaN(y)) y = new Date().getFullYear();
    var m = parseInt(a[pattern.indexOf("m")], 10) - 1;
    var d = parseInt(a[pattern.indexOf("d")], 10);
    if (isNaN(d)) d = 1;
    return new Date(y, m, d);
};
String.prototype.toDate1 = function() {
    var val = Date.parse(this);
    return new Date(val);
}
var divs = [];
divs[divs.length] = '<div id="__calendarPanel" style="position:absolute;display:none;background-color:#FFFFFF;border:1px solid #666666;width:200px;height:208px;">';
divs[divs.length] = '<iframe name="__calendarIframe" id="__calendarIframe" width="100%" height="100%" scrolling="no" frameborder="0" style="margin:0px;"><\/iframe>';
divs[divs.length] = '<\/div>';
document.write(divs.join(""));

var __ci = window.frames['__calendarIframe'];
var cis = [];
cis[cis.length] = '<!DOCTYPE html PUBLIC "-\/\/W3C\/\/DTD XHTML 1.0 Transitional\/\/EN" "http:\/\/www.w3.org\/TR\/xhtml1\/DTD\/xhtml1-transitional.dtd">';
cis[cis.length] = '<html xmlns="http:\/\/www.w3.org\/1999\/xhtml">';
cis[cis.length] = '<head>';
cis[cis.length] = '<meta http-equiv="Content-Type" content="text\/html; charset=utf-8" \/>';
cis[cis.length] = '<title>Web Calendar(UTF-8) Written By KimSoft<\/title>';
cis[cis.length] = '<style type="text\/css">';
cis[cis.length] = '<!--';
cis[cis.length] = 'body {font-size:12px;margin:0px;text-align:center;}';
cis[cis.length] = 'form {margin:0px;}';
cis[cis.length] = 'select {font-size:12px;background-color:#EFEFEF;}';
cis[cis.length] = 'table {border:0px solid #CCCCCC;background-color:#FFFFFF}';
cis[cis.length] = 'th {font-size:12px;font-weight:normal;background-color:#FFFFFF;}';
cis[cis.length] = 'th.theader {font-weight:normal;background-color:#666666;color:#FFFFFF;width:24px;}';
cis[cis.length] = 'select.year {width:64px;}';
cis[cis.length] = 'select.month {width:60px;}';
cis[cis.length] = 'td {font-size:12px;text-align:center;}';
cis[cis.length] = 'td.sat {color:#0000FF;background-color:#EFEFEF;cursor:default;}';
cis[cis.length] = 'td.sun {color:#FF0000;background-color:#EFEFEF;cursor:default;}';
cis[cis.length] = 'td.normal {background-color:#EFEFEF;cursor:default;}';
cis[cis.length] = 'input.l {border: 1px solid #CCCCCC;background-color:#EFEFEF;width:20px;height:20px;}';
cis[cis.length] = 'input.r {border: 1px solid #CCCCCC;background-color:#EFEFEF;width:20px;height:20px;}';
cis[cis.length] = 'input.b {border: 1px solid #CCCCCC;background-color:#EFEFEF;width:100%;height:20px;cursor:pointer;}';
cis[cis.length] = '-->';
cis[cis.length] = '<\/style>';
cis[cis.length] = '<\/head>';
cis[cis.length] = '<body>';
cis[cis.length] = '<\/body>';
cis[cis.length] = '<\/html>';
__ci.document.writeln(cis.join(""));
__ci.document.close();
var calendar;
document.onclick = function(e) {
    e = window.event || e;
    var srcElement = e.srcElement || e.target;
    if (calendar && calendar.eventSrc != srcElement && calendar.panel.style.display != "none") {
        calendar.subData(calendar);
    }
};

///0,1,2
function AddCalendar(e, obj, gtorlttoday, callback) {
    var b = 1;
    if (!gtorlttoday)
        b = 1;
    else //By yongjin.C
        b = gtorlttoday;
    calendar = new Calendar(e, b, callback, '2007', '2020', 0, '-', 'yyyy-MM-dd');
    calendar.show(obj, obj);
}