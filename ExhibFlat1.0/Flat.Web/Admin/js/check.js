
$.fn.CheckBoxBindFn = function(op) {
    var option = {
        Allclass: "all",
        itemclass: "",
        callback: function($item) { return false; }
    };
    $.extend(option, op || {});
    var itemstr = "input[type=checkbox]" + (!!option.itemclass ? "[class=" + option.itemclass + "]" : "");
    var $checkbox = $(this).find(itemstr);
    var $all = $checkbox.filter("." + option.Allclass);
    $checkbox.change(function() {
        var $item = $(this);
        if (!!$item.attr("checked")) {
            $item.attr("checked", true);
        } else {
            $item.removeAttr("checked");
        }
        if ($item.hasClass(option.Allclass)) {
            if (!!$item.attr("checked")) {
                $checkbox.attr("checked", true);
            } else {
                $checkbox.removeAttr("checked");
            }
        } else {
            if (!$item.attr("checked")) {
                $all.removeAttr("checked");
            }
        }
        option.callback($item);
    });
}