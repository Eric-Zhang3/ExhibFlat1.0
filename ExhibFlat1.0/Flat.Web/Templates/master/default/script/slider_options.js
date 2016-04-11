jQuery.noConflict();
	jQuery(window).bind('load', function(){
		jQuery().prepare_slider(); 
		jQuery('#slider_list > li').over();
		//Download by http://www.codefans.net
		//=======intro================
		var slider_link = jQuery('.right-btn');
		var slider_link_index = 1;
		var slider_count = jQuery('#slider_list > li').size();	

		function slider_intro(){
			if(slider_link_index <= slider_count){
				slider_link.trigger('click');
				slider_link_index++;
				setTimeout(function(){slider_intro()}, 2000); //select change time
			}
		}
		setTimeout(function(){slider_intro()}, 2000)
	  //===============
		
		jQuery('.left-btn').hover(
		   function () {
			 jQuery(this).addClass("over");
		   },
		   function () {
			 jQuery(this).removeClass("over");
		   })
		
		jQuery('.right-btn').hover(
		   function () {
			 jQuery(this).addClass("over");
		   },
		   function () {
			 jQuery(this).removeClass("over");
		   })
		
//		jQuery('.cms-home .products-grid li').hover(
//		   function () {
//			 jQuery(this).find('.product-name').stop(true, true).slideDown("slow");
//		   },
//		   function () {
//			 jQuery(this).find('.product-name').hide("slow");
//		   })
	});	