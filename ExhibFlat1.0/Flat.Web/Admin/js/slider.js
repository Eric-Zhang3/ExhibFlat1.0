function slider() {
  //图标闪出  
  $(function(){
    var o = new Object();
    o['start']  = {'left':340,'top':250};
    o[0] = { 'left': 0, 'top': 139};
    o[1] = { 'left': 101, 'top': 63};
    o[2] = { 'left': 218, 'top': 20};
    o[3] = { 'left': 333, 'top': 0};
    o[4] = { 'left': 460, 'top': 30};
    o[5] = { 'left': 587, 'top': 82};
    var start = 1;
          if( start )
          {
            $('.indexL ul .list1').stop().animate(o[0],1000);
            $('.indexL ul .list2').stop().animate(o[1],1000);
            $('.indexL ul .list3').stop().animate(o[2],1000);
            $('.indexL ul .list4').stop().animate(o[3],1000);
            $('.indexL ul .list5').stop().animate(o[4],1000);
            $('.indexL ul .list6').stop().animate(o[5],1000);            
            start = 0;
          }else{
            $('.indexL ul li').stop().animate(o['start'],1000);
            start = 1;
          }
          return false;
  });
    //点击切换对应内容
          $(".rightWp").find("div").eq(0).show();
           $(".indexL ul li").hover(function() {
               var nIndex = $(".indexL ul li").index(this);
               $(".rightWp div").eq(nIndex).show().siblings(".rightWp div").hide();
           })
//    $("#topMenu ul li").each(function () {
//        $(this).hover(function () {
//            var _top = $(this).position().top;
//            var _left = $(this).position().left;
//            console.log(_top, _left)
//            if (_left < 670) {
//                $(this).stop().css({
//                    left: _left - 10 + "px",
//                    top: _top - 10 + "px"
//                }, 1000)
//            } else {
//                $(this).stop().css({
//                    left: _left + 10 + "px",
//                    top: _top - 10 + "px"
//                }, 1000)
//            }
//            $(this).find("img").stop().css({ width: '100px', height: '98px' }, 1000)
//        }, function () {
//            var _top = $(this).position().top;
//            var _left = $(this).position().left;
//            if (_left < 670) {
//                $(this).stop().css({
//                    left: _left + 10 + "px",
//                    top: _top + 10 + "px"
//                }, 1000)
//                $(this).find("img").stop().css({ width: '84px', height: '82px' }, 1000)
//                $(this).find("p").stop().css({ fontSize: '15px' }, 1000);
//            } else {
//                $(this).stop().css({
//                    left: _left - 10 + "px",
//                    top: _top + 10 + "px"
//                }, 1000)
//            }
//        })
//    })
}
