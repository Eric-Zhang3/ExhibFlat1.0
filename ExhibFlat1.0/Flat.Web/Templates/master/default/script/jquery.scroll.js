var lb_cur = 1,obj="";
$(function(){
    $(".brands-list .brand-tabs li").click(function(){				
	var _index = $(".brands-list .brand-tabs li").index(this);
	obj = $(".brand-conts .tab-content").eq(_index).find(".slide-content .clearList");
	$(".brand-conts .tab-content").hide();		
	obj.show();		
	var total = obj.size();
     if(total == 1){
         $(".prev-btn,.next-btn").hide();
      }else{
         $(".prev-btn,.next-btn").show();
     }
	$(".prev-btn s,.next-btn s").html(total);
	lb_cur = 1;
	$(".prev-btn i,.next-btn i").html(lb_cur);
	$(".brand-conts .tab-content").hide().eq(_index).show();
	$(".brands-list .brand-tabs li").removeClass("ks-active").eq(_index).addClass("ks-active");
})	
$(".prev-btn").click(function(){
	showUl();
	lb_cur--;
	showLimitBuyProducts();
});
$(".next-btn").click(function(){
	showUl();
	lb_cur++;
	showLimitBuyProducts();	
});
})
function showUl(){
	$(".slide-content .clearList").show();	
}
function showLimitBuyProducts(){
    var leng =$(".slide-content ul:visible").size();
	$(".prev-btn s,.next-btn s").html(leng);
    if(leng == 1){
        $(".prev-btn,.next-btn").hide();
    }
	if(lb_cur <1){
		lb_cur = leng;		
	}else if(lb_cur>leng){		
		lb_cur = 1;		
	}
	$(".prev-btn i,.next-btn i").html(lb_cur);
	if(obj == ""){ 
		$(".slide-content .clearList").hide().eq(lb_cur-1).show();
	}else{ 
	obj.hide().eq(lb_cur-1).show();
	}
}