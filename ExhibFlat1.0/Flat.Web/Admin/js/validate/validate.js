/**
* validate by Yongjin.C
*/
(function() {
    var reInt = /^\s*[+-]?\d+\s*$/; //整数类型
    var reUInt = /^\s*[+]?\d+\s*$/; //正整数类型
    window.DataType = {
        Int16: { Name: "Int16", MaxValue: 32767, MinValue: -32768, CheckRange: function(val) { return ((isFinite(val)) && (reInt.test(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        Int32: { Name: "Int32", MaxValue: 2147483647, MinValue: -2147483648, CheckRange: function(val) { return ((isFinite(val)) && (reInt.test(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        Int64: { Name: "Int64", MaxValue: 9223372036854776000, MinValue: -9223372036854776000, CheckRange: function(val) { return ((isFinite(val)) && (reInt.test(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        Float: { Name: "Float", MaxValue: 3.402823e+38, MinValue: -3.402823e+38, CheckRange: function(val) { return ((isFinite(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        Double: { Name: "Double", MaxValue: 1.79e+308, MinValue: -1.79e+308, CheckRange: function(val) { return ((isFinite(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        UInt16: { Name: "UInt16", MaxValue: 65535, MinValue: 1, CheckRange: function(val) { return ((isFinite(val)) && (reUInt.test(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        UInt32: { Name: "UInt32", MaxValue: 4294967295, MinValue: 1, CheckRange: function(val) { return ((isFinite(val)) && (reUInt.test(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        UInt64: { Name: "UInt64", MaxValue: 18446744073709552000, MinValue: 1, CheckRange: function(val) { return ((isFinite(val)) && (reUInt.test(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        UFloat: { Name: "UFloat", MaxValue: 3.402823E+38, MinValue: 0, CheckRange: function(val) { return ((isFinite(val)) && (this.MaxValue >= val) && (this.MinValue <= val)); } },
        UDouble: { Name: "Double", MaxValue: 1.79e+308, MinValue: 0, CheckRange: function(val) { return ((isFinite(val)) && (this.MaxValue >= val) && (this.MinValue <= val)) } },
        DateTime: {
            Name: "DateTime",
            MaxValue: new Date(9999, 12, 31, 23, 59, 59),
            MinValue: new Date(1, 1, 1, 0, 0, 0),
            CheckRange: function(val) {
                var temp = Date.parse(val);
                return ((!isNaN(temp)) && (this.MaxValue >= temp) && (this.MinValue <= temp))
            }
        },
        String: {
            Name: "String",
            IsNullOrEmpty: function(val) { return !val; },
            IsNullOrWhiteSpace: function(val) { return (val == null) || /^\s*$/.test(val); },
            Trim: function(val, rep) {
                if (!!val) {
                    if (!!rep) {
                        rep = rep.replace(/\\/g, "\\\\").replace(/\//g, "\/");
                        var reg = new RegExp("(^" + rep + ")|(" + rep + "$)", "g");
                        return val.replace(reg, "");
                    }
                    return val.replace(/(^\s*)|(\s*$)/g, "");
                }
                return val;
            },
            // 返回字符串的实际长度, 一个汉字算2个长度
            Length: function(val) { return val.replace(/[^\x00-\xff]/g, "**").length; },
            coverToUnicode: function(str, cssType) {
                var i = 0,
                    l = str.length,
                    result = [], //转换后的结果数组
                    unicodePrefix //unicode前缀 (example:\1234||\u1234)

                        //如果是css中使用格式为\1234之类
                        //字符串的charCodeAt方法返回的是10进制的unicode，所以我们需要用toString(16)将其转为16进制的，才能在JS及CSS中使用，而CSS中跟JS不同的是少了个U
                        = (cssType && cssType.toLowerCase() === 'css') ? '\\' : '\\u';

                for (; i < l; i++) {
                    var cr = str.charCodeAt(i);
                    if (cr > 255) {
                        result.push(unicodePrefix + cr.toString(16));
                    } else
                        result.push(str.charAt(i));
                }

                return result.join('');
            }
        },
        URL: {
            Name: "URL",
            CheckRange: function(val) {
                var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
                    + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //ftp的user@ 
                    + "(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184   
                    + "|" // 允许IP和DOMAIN（域名） 
                    + "([0-9a-z_!~*'()-]+\.)*" // 域名- www.  
                    + "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // 二级域名 
                    + "[a-z]{2,6})" // first level domain- .com or .museum  
                    + "(:[0-9]{1,4})?" // 端口- :80  
                    + "((/?)|" // a slash isn't required if there is no file name 
                    + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
                var re = new RegExp(strRegex);
                return !!re.test(val);
            }
        },
        Email: {
            Name: 'Email',
            CheckRange: function(val) {
                var f = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(val);
                return !!f;
            }
        },
        ZHpostcode: {
            Name: 'ZHpostcode',
            CheckRange: function(val) {
                return !!(/^[1-9]\d{5}(?!\d)$/.test(val));
            }
        },
        ZHtel: {
            Name: 'ZHtel',
            CheckRange: function(val) {
                return !!(/^((\d{3}-\d{8}|\d{4}-\d{7}))(-\d{1,4}){0,1}$/g.test(val));
            }
        },
        ZHIDCard: {
            Name: 'ZHIDCard',
            CheckRange: function(val) {
                //身份证正则表达式(15位) 
                var isIDCard1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
                //身份证正则表达式(18位)
                var isIDCard2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/;
                //验证身份证，返回结果 
                return (!!isIDCard1.test(val) || !!isIDCard2.test(val));
            }
        }
    };

    $.fn.validate = function() {
        var self = this;
        $(self).change(function() {
            if (!!$(self).attr("datatype")) {
                validateData($(self));
            } else if (!!$(self).attr("url")) {
                validateAjax($(self));
            } else if (!!$(self).attr("reg")) {
                validate($(self));
            }
        });
    }
    //验证数据类型
    var CheckDataType = function(val, type) {
        var chk = false;
        if (!DataType.String.IsNullOrEmpty(val)) {
            if (type == DataType.String.Name) {
                chk = true;
            } else if (DataType.hasOwnProperty(type)) {
                chk = DataType[type].CheckRange(val);
            } else {
                alert("data type not define:" + type);
                throw "dta type not define!";
            }
        } else if (type == DataType.String.Name && val == '') {
            chk = true;
        }
        return chk;
    }

    function validateData(obj) {
        var b = CheckDataType(obj.val(), obj.attr('datatype'));
        if (b)
            changestyle(obj, "suc");
        else
            changestyle(obj, "err");
    }

    function validate(obj) {
        var val = obj.attr("reg");
        var reg;
        if (/^\/[\s\S]*\/$/.test(val)) {
            reg = eval(val);
        } else {
            reg = eval("/" + val + "/");
        }

        var objValue = obj.attr("value");
        if (!reg.test(objValue)) {
            changestyle(obj, "err");
            return false;
        } else {
            changestyle(obj, "suc");
            return true;
        }
    }

    function validateAjax(obj) {

        var url = obj.attr("url"), name = obj.attr("name"), value = obj.val();
        var flog = 0;
        toajax(url, { name: value }, function(r) {
            if (!!r.d) {
                flog = 1;
            }
        }, function() { flog = -1; });

        if (flog == 1) {
            changestyle(obj, "suc");
            return true;
        } else if (flog == 0) {
            changestyle(obj, "err");
            return false;
        } else {
            changestyle(obj, "los");
            return false;
        }
    }

    var toajax = function(url, data, fnSuccess, fnerror) {
        if (!fnSuccess)
            fnSuccess = function() {};
        if (!fnerror)
            fnerror = function() {};
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: url,
            data: JSON.stringify(data), //序列化为json格式的字符串
            dataType: "json",
            cache: false,
            async: false,
            success: fnSuccess,
            error: fnerror
        });
    }

    function changestyle(obj, actbool) {
        obj.removeClass("validate-succeed validate-error validate-info validate-loser");
        if (actbool == "suc") {
            obj.addClass("validate-succeed");
        } else if (actbool == "err") {
            obj.addClass("validate-error");
        } else if (actbool == 'los') {
            obj.addClass("validate-loser");
        } else {
            obj.addClass(" validate-info");
        }
    }

})()