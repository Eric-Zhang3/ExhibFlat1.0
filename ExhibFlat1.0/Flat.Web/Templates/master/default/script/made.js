/* 
 商品定制及旋转
 */
var oCode = "0",//商品编号
	oView = 1,//当前视角
	oMaxView = 3,//最大视角
	oArr = new Array(1,2,3), //各模块所代替的颜色
	nCode = "",
	oUrl = "/templates/master/default/images/dingzhi/"; //图片所在路径
 //选择颜色
function changeColor(mCode,mColor){
	oArr[mCode] = mColor;
	madeImg(mCode);
}
//生成图片
function madeImg(mCode){
	var ImgName = oCode + "_" +oView + "_" + mCode + "_" + oArr[mCode] + ".png", //图片命名规则 商品编号+ 视图 + 模块化+ 颜色
		DivId = "picBox" + mCode;
		document.getElementById(DivId).src = oUrl + ImgName;
}
//切视图
function getView(oVim){
	if(oVim == 1){
		if(oView  == oMaxView){
			oView = 1;
		}else{
			oView++;
		}
	}
	else{
		if(oView == 1){
			oView = 3;
		}
		else{
			oView--;
		}
	}
	for(var i = 0; i< oArr.length;i++){
		madeImg(i);
	}
}
//使选中的模块图片透明度变弱
function Show(mCode){
	var DivId = "picBox" + mCode;
		$("#" + DivId).attr("class","over");
}
//透明度默认
function Default(mCode){
	var DivId = "picBox" + mCode;
		$("#" + DivId).attr("class","out");
}
