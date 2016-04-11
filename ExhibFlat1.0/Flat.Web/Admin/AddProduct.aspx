<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="Hidistro.UI.Web.Admin.AddProduct" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Validator" Assembly="Hidistro.UI.Common.Validator" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <link id="cssLink" rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />    
    <Hi:Script ID="Script2" runat="server" Src="/utility/jquery_hashtable.js" />
    <Hi:Script ID="Script1" runat="server" Src="/utility/jquery-powerFloat-min.js" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
     
<div class="optiongroup mainwidth">
        <ul>
            <li class="menucurrent">
                <a><span>添加商品</span></a></li>
        </ul>
</div>
    <div class="dataarea mainwidth">
        <div class="datafrom">
            <div class="formitem">
                <ul>
                    <li>
                        <h2 class="colorE">基本信息</h2>
                    </li>
                    <li>
                        <span class="sL">所属类目：</span>
                        <span class="colorE float fonts">
                            <asp:Literal runat="server" ID="litCategoryName"></asp:Literal></span>
                        [<asp:HyperLink runat="server" ID="lnkEditCategory" CssClass="a" Text="编辑"></asp:HyperLink>]
                    </li>
                    <li>
                        <span class="sL">商品类型：<em>*</em></span>
                        <abbr class="formselect">
                            <Hi:ProductTypeDownList runat="server" CssClass="productType" ID="dropProductTypes" NullToDisplay="--请选择--" /></abbr>
                        <label runat="server" id="liSupplier">供应商：<em>*</em><abbr class="formselect"><Hi:SupplierDropDownList runat="server" ID="dropSuppliers" NullToDisplay="--请选择--" AutoPostBack="True" OnSelectedIndexChanged="dropSuppliers_Change"/></abbr></label><p>选择商品所属的供应商</p>
                    </li>
                   <%-- <li><span class="sL">所属产品线：<em>*</em></span>
                        <abbr class="formselect">
                            <Hi:ProductLineDropDownList runat="server" ID="dropProductLines" NullToDisplay="--请选择--" />
                        </abbr>
                    </li>--%>
                    <li ><span class="sL">品牌：</span>
                        <abbr class="formselect">
                            <Hi:BrandCategoriesDropDownList runat="server" ID="dropBrandCategories" NullToDisplay="--请选择--"   />
                        </abbr>
                    </li>
                    <li class=" clearfix"><span class="sL">商品名称：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductName" Width="350px" onblur="CheckName()"  />
                        <p id="ctl00_contentHolder_txtProductNameTip">限定在1-60个字符</p>
                        <p id="ctl00_contentHolder_txtProductNameRepeat" style="color:red"></p>
                    </li>
                    <li class=" clearfix"><span class="sL">副标题：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSubhead" Width="350px"/>
                        <p id="ctl00_contentHolder_txtSubheadTip">限定在1-150个字符</p>
                        <p id="ctl00_contentHolder_txtSubheadRepeat" style="color:red"></p>
                    </li>
                    <li class=" clearfix"><span class="sL">商品条形码： </span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProBarCode" Width="350px"  />
                        <p id="ctl00_contentHolder_txtProBarCodeTip">限定在1-16个字符</p>
                        <p id="ctl00_contentHolder_txtProBarCodeRepeat" style="color:red"></p>
                    </li>
                    <li class=" clearfix"><span class="sL">广告图片： </span>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="ADImage" />
                        </div>
                        </li>
                        
                    <li class=" clearfix"><span class="sL">商品简称：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductShortName" Width="350px" onblur="CheckShortName()" />
                        <p id="ctl00_contentHolder_txtProductShortNameTip">限定在1-20个字符</p>
                        <p id="ctl00_contentHolder_txtProductShortNameRepeat" style="color:red"></p>
                    </li>
                    <li><span class="sL">商家编码：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductCode" />
                        <Hi:TrimTextBox runat="server" CssClass="forminput"  Style="display: none;" ID="txtGeneralCode" />
                        <p id="ctl00_contentHolder_txtProductCodeTip">长度不能超过20个字符</p>
                         <p id="ctl00_contentHolder_txtProductCodeRepeat" style="color:red"></p>
                    </li>
                    <li><span class="sL">计量单位：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtUnit" />
                        <p id="ctl00_contentHolder_txtUnitTip">必须限制在20个字以内且只能是英文和中文例:g/元</p>
                    </li>
                    <li><span class="sL">市场价：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMarketPrice" onkeyup="validateprice(1)" />元
                <p id="ctl00_contentHolder_txtMarketPriceTip">本站分销商所看到的商品市场价</p><p id="tip1" style="color:red"></p>
                    </li>
                    <li><span class="sL">吊牌价：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtOriginalPrice"  />
                <p id="ctl00_contentHolder_txtOriginalPricePriceTip">元</p>
                    </li>
                    <li style="display: none"><span class="sL">分销商最低批发价：<em>*</em></span>
                        <span style="float: left">
                            <table style="border: none;" width="150px">
                                <tr>
                                    <td style="border: none;">
                                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtLowestSalePrice" /></td>
                                    <td style="border: none; width: 10px;">元</td>
                                </tr>
                            </table>
                        </span>
                    </li>
                    <li>
                        <h2 class="colorE">扩展属性</h2>
                    </li>
                    <li id="attributeRow" style="display: none;"><span class="sL">商品属性：</span>
                        <div class="attributeContent" id="attributeContent"></div>
                        <Hi:TrimTextBox runat="server" ID="txtAttributes" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                    </li>
                    <li id="skuCodeRow"><span class="sL">SKU编码：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSku" Enabled="false" />
                        <p id="ctl00_contentHolder_txtSkuTip">限定在20个字符</p>
                    </li>
                    
                    <li id="salePriceRow"><span class="sL">批发价：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSalePrice" onkeyup="validateprice(2)"/>元<input type="button" onclick="editProductMemberPrice();" value="编辑分销价" />
                        <Hi:TrimTextBox runat="server" ID="txtMemberPrices" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                        <p id="ctl00_contentHolder_txtSalePriceTip">本站分销商所看到的商品批发价</p>
                        <p id="tip2" style="color:red"></p>
                    </li>
                    <li id="costPriceRow" style="display:none"><span class="sL">成本价：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtCostPrice" />元
	            <p id="ctl00_contentHolder_txtCostPriceTip">商品的成本价</p>
                    </li>
                    <li id="purchasePriceRow" style="display:none"><span class="sL">分销商采购价：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtPurchasePrice" />元<input type="button" onclick="editProductDistributorPrice();" value="编辑分销等级价" />
                        <Hi:TrimTextBox runat="server" ID="txtDistributorPrices" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                        <p id="ctl00_contentHolder_txtPurchasePriceTip">分销商的采购价</p>
                    </li>
                    <li id="qtyRow"><span class="sL">商品库存：<em>*</em></span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtStock" Text="0"   /></li>
                    <li id="alertQtyRow"><span class="sL">警戒库存：<em>*</em></span><span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtAlertStock" />
                         <p id="ctl00_contentHolder_txtAlertStockTip"></p></span>
                    </li>
                    <li id="weightRow"><span class="sL">商品重量：<em>*</em></span><span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtWeight" />克
                        <p id="ctl00_contentHolder_txtWeightTip" ></p></span>

                    </li>
                    <li id="skuTitle" style="display: none;">
                        <h2 class="colorE">商品规格</h2>
                    </li>
                    <li id="enableSkuRow" style="display: none;"><span class="sL">规格：</span><input id="btnEnableSku" type="button" value="开启规格" />
                        开启规格前先填写以上信息，可自动复制信息到每个规格</li>
                    <li id="skuRow" style="display: none;">
                        <p id="skuContent">
                            <input type="button" id="btnGenerateAll" value="生成所有规格" />
                            <input type="button" id="btnshowSkuValue" value="生成部分规格" />
                            <input type="button" id="btnAddItem" value="增加一个规格" />
                            <input type="button" id="btnCloseSku" value="关闭规格" />
                        </p>
                        <p id="skuFieldHolder" style="margin: 0px auto; display: none;"></p>
                        <div id="skuTableHolder">
                        </div>
                        <Hi:TrimTextBox runat="server" ID="txtSkus" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                        <asp:CheckBox runat="server" ID="chkSkuEnabled" Style="display: none;" />
                    </li>
                    <li>
                        <h2 class="colorE">图片和描述</h2>
                    </li>
                    <li><span class="sL">商品图片：</span>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="uploader1" />
                        </div>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="uploader2" />
                        </div>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="uploader3" />
                        </div>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="uploader4" />
                        </div>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="uploader5" />
                        </div>
                        <div>图片应为jpg,gif,jpeg,png或bmp格式,不能超过500k,
                        建议上传700*700以上尺寸，以便在淘宝等平台的宝贝详情页主图提供放大功能</div>
                    </li>
                    <li style="display:none"><span class="sL">商品简介：</span>
                        <Hi:TrimTextBox runat="server" Rows="6" Columns="76" ID="txtShortDescription" TextMode="MultiLine" />
                        <p>限定在300个字符以内</p>
                    </li>
                    <li><span class="sL">商品描述：</span>
                        <span class="rWidth">
                        <Kindeditor:KindeditorControl ID="editDescription" runat="server" Height="300" Width="100%" /> 
                        </span>                        
                    </li>

                    <li>
                        <h2 class="colorE clear">相关设置</h2>
                    </li>
                    <li>
                        <span class="sL">商品销售状态：</span>
                        <span class="sR">
                        <asp:RadioButton runat="server" ID="radInStock" GroupName="SaleStatus" Checked="true" Text="仓库中"></asp:RadioButton>
                        <%--<asp:RadioButton runat="server" ID="radOnSales" GroupName="SaleStatus" Text="出售中"></asp:RadioButton>--%>
                        <asp:RadioButton runat="server" ID="radUnSales" GroupName="SaleStatus" Text="下架区"></asp:RadioButton>
                        </span>
                    </li>
                    <li style="display:none">
                        <span class="sL">铺货设置：</span>
                        <span class="sR">
                        <asp:CheckBox runat="server" ID="chkPenetration" Text="立刻铺货" /></span>
                    </li>
                    <li id="l_tags" runat="server">
                        <span class="sL">商品标签定义：<br />
                             </span>

                        <div id="div_tags" class="sR">
                            <Hi:ProductTagsLiteral ID="litralProductTag" runat="server"></Hi:ProductTagsLiteral></div>
                        <div id="div_addtag" style="display: none;margin-top:5px;">
                            <input type="text" id="txtaddtag" /><input type="button" value="保存" onclick="return AddAjaxTags()" /></div>
                        <Hi:TrimTextBox runat="server" ID="txtProductTag" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                    </li>
                    <li style="display: none;">
                        <span class="sL">分销商打折：</span>
                        <Hi:YesNoRadioButtonList runat="server" RepeatLayout="Flow" ID="radlEnableMemberDiscount" YesText="参与分销商打折" NoText="不参与分销商打折" />
                    </li>
                   
                    <li style="display:none">
                        <h2 class="colorE">搜索优化</h2>
                    </li>
                    <li style="display:none"><span class="sL">详细页标题：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtTitle" Width="350px" />
                    </li>
                    <li style="display:none" ><span class="sL">详细页描述：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMetaDescription" Width="350px" />
                    </li>
                    <li style="display:none"><span class="sL">详细页搜索关键字：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMetaKeywords" Width="350px" />
                    </li>
                    <li style="display:none">
                        <h2 class="colorE clear">产品定制设置</h2>
                    </li>
                    <li style="display:none"> 
                        <span class="sL">是否定制：</span>
                        <span class="sR">
                        <asp:RadioButton runat="server" ID="rdButton1" GroupName="IsCustomize" Checked="true" Text="否"></asp:RadioButton>
                        <asp:RadioButton runat="server" ID="rdButton2" GroupName="IsCustomize" Text="是" ></asp:RadioButton> </span>
                    </li>
                    <li> 
                        <span class="sL">定制跳转路径：</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtCustomizeURL" Width="350px" />
                    </li>
                </ul>
                <ul class="btntf Pa_198 clear">
                    <asp:Button runat="server" ID="btnAdd" Text="保 存" OnClientClick="return doSubmit();" CssClass="submit_DAqueding inbnt" />
                </ul>
            </div>
        </div>
    </div>
    <div class="Pop_up" id="priceBox" style="display: none;">
        <h1>
            <span id="popTitle"></span>
        </h1>
        <div class="img_datala">
            <img src="../images/icon_dalata.gif" alt="关闭" />
        </div>
        <div class="mianform ">
            <div id="priceContent">
            </div>
            <div class="aui_buttons" style="margin-top: 10px; text-align: center;">
                <button type="button" onclick="doneEditPrice();" class="aui_state_highlight">确定</button>
            </div>
        </div>
    </div>

    <div class="Pop_up" id="skuValueBox" style="display: none;">
        <h1>
            <span>选择要生成的规格</span>
        </h1>
        <div class="img_datala">
            <img src="../images/icon_dalata.gif" alt="关闭" />
        </div>
        <div class="mianform ">
            <div id="skuItems">
            </div>
            <div class="aui_buttons" style="margin-top: 10px; text-align: center;">
                <button type="button" id="btnGenerate" class="aui_state_highlight">确定</button>
            </div>
        </div>
    </div>
    <div id="divImage" style="display: none; position: absolute">
        <img id="imgbig" style="width:300px;height:400px" alt="预览" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="attributes.helper.js"></script>
    <script type="text/javascript" src="grade.price.helper.js"></script>
    <script type="text/javascript" src="producttag.helper.js"></script>
    <script type="text/javascript" language="javascript">
        //重写grade.price.helper.js/checkGradePrice
        function checkGradePrice(inputItems) {
            var validated = true;
            var exp = new RegExp("^(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)$", "i");
          
            $.each(inputItems, function (i, v) {
                var gradeid = $(this).attr("gradeid");
                var dval = $(".gradePriceTr.gradeRow[gradeid=" + gradeid + "]>td:last").text().match(/\d+/)[0];
              
                var val = $(this).val();
                if (val.length > 0) {

                    // 检查输入的是否是有效的金额
                    if (!exp.test(val)) {
                        alert("价格输入有误，请输入正确的价格");
                        $(this).focus();
                        validated = false;
                        return false;
                    }

                    // 检查金额是否超过了系统范围
                    var num = parseFloat(val);
                    if (!((num >= 0.01) && (num <= 10000000))) {
                        alert("输入的价格超出了系统表示范围！");
                        $(this).focus();
                        validated = false;
                        return false;
                    }
                    if (num > parseFloat(dval)) {
                        alert("输入的价格不得高于系统默认价格");
                        $(this).focus();
                        validated = false;
                        return false;
                    }
                } else {
                    val = dval;
                }
                if (i > 0) {
               
                var prv = inputItems.eq(i - 1);
                var pgrade = prv.attr("gradeid");
                var prvdval = $(".gradePriceTr.gradeRow[gradeid=" + pgrade + "]>td:last").text().match(/\d+/)[0];
                var pval = prv.val() || prvdval;

                if (pval < val) {
                    alert("输入的价格有误，当前分销价不得高于上一级分销价！");
                    $(this).focus();
                    validated = false;
                    return false;
                }
                }
            });
           
            return validated;
        }
        function InitValidators() {

            initValid(new InputValidator('ctl00_contentHolder_txtProductCode', 1, 20, false, null, '商家编码的字符长度在1-20之间'));
            initValid(new InputValidator('ctl00_contentHolder_txtSubhead', 0, 150, true, null, '副标题的字符长度限制150以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtProBarCode', 0, 16, true, null, '商品条形码的字符长度限制16以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtMarketPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMarketPrice', 1, 99999999, '请输入1-99999999范围的数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtSalePrice', 1, 50, false,  '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtSalePrice', 1, 99999999, '请输入1-99999999范围的数值'));

            //initValid(new InputValidator('ctl00_contentHolder_txtPurchasePrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtPurchasePrice', 0.01, 10000000, '输入的数值超出了系统表示范围'));
            //initValid(new InputValidator('ctl00_contentHolder_txtLowestSalePrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtLowestSalePrice', 0.01, 10000000, '输入的数值超出了系统表示范围'));
            //initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 10, true, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0, 99999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0, 99999999, '请输入0-99999999范围的数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtSku', 0, 20, true, null, '货号的长度不能超过6个字符'));
            initValid(new InputValidator('ctl00_contentHolder_txtBarCode', 0, 16, true, '^(\d+[a-zA-Z]\w*)|([a-zA-Z]+\d\w*)$', '必须限制在16个字符以内且不能是特殊字符'))
            initValid(new InputValidator('ctl00_contentHolder_txtStock', 1, 50, false, '[1-9]\\d*|0', '应输入大于0的整数'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtStock', 0, 9999999, '请输入1-9999999范围的数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtAlertStock', 1, 50, false, '[1-9]\\d*|0', '应输入大于等于0的整数'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtAlertStock', 0, 9999999, '请输入0-9999999范围的数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtUnit', 1, 20, true, '[a-zA-Z\/\u4e00-\u9fa5]*$', '必须限制在20个字符以内且只能是英文和中文例:g/元'));
            initValid(new InputValidator('ctl00_contentHolder_txtWeight', 0, 50, true, '^[0-9]*[1-9][0-9]*$', '应输入整数型数值'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtWeight', 1, 9999999, '请输入1-9999999范围的数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtShortDescription', 0, 300, true, null, '商品简介必须限制在20个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtTitle', 0, 100, true, null, '详细页标题长度限制在100个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtMetaDescription', 0, 100, true, null, '详细页描述长度限制在100个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtMetaKeywords', 0, 100, true, null, '详细页搜索关键字长度限制在100个字符以内'));
             
        }
        function validateprice(con) {
            if ($("#ctl00_contentHolder_txtSalePrice").val() != "" && $("#ctl00_contentHolder_txtMarketPrice").val()!=""){
                if (parseFloat($("#ctl00_contentHolder_txtSalePrice").val()) > parseFloat($("#ctl00_contentHolder_txtMarketPrice").val())) {
                    $("#tip" + con).text("批发价不能大于市场价.");
                } else {
                    $("#tip1").text("");
                    $("#tip2").text("");
                }
            }
        }
        $(document).ready(function () { InitValidators(); });
        /*获取字符串字节数*/
        function GetCharLength(str) {
            var iLength = 0;
            for (var i = 0; i < str.length; i++) {
                if (str.charCodeAt(i) > 255) {
                    iLength += 2;
                }
                else {
                    iLength += 1;
                }
            }
            return iLength;
        }
        /*商品名称验证*/
        function CheckName() {
            var length = GetCharLength(document.getElementById('ctl00_contentHolder_txtProductName').value);
            if (length == 0 || document.getElementById('ctl00_contentHolder_txtProductName').value == "") {
                $("#ctl00_contentHolder_txtProductNameTip").text("请输入商品名称！");
                document.getElementById('ctl00_contentHolder_txtProductName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductNameTip').className = "msgError";
                return false;
            }
            else if (length > 60) {
                $("#ctl00_contentHolder_txtProductNameTip").text("商品名称应限制在60个字节以内！");
                document.getElementById('ctl00_contentHolder_txtProductName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductNameTip').className = "msgError";
                return false;
            }
            else {
                $("#ctl00_contentHolder_txtProductNameTip").text("商品名称限定在1-60个字节");
                document.getElementById('ctl00_contentHolder_txtProductName').className = "forminput";
                document.getElementById('ctl00_contentHolder_txtProductNameTip').className = "";
                //$("#ctl00_contentHolder_txtProductShortName").val($("#ctl00_contentHolder_txtProductName").val().substr(0, 5));
                return true;
            }
        }
        /*商品简称验证*/
        function CheckShortName() {
            var length = GetCharLength(document.getElementById('ctl00_contentHolder_txtProductShortName').value);
            if (length == 0 || document.getElementById('ctl00_contentHolder_txtProductShortName').value == "") {
                $("#ctl00_contentHolder_txtProductShortNameTip").text("请输入商品简称！");
                document.getElementById('ctl00_contentHolder_txtProductShortName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductShortNameTip').className = "msgError";
                return false;
            }
            else if (length > 20) {
                $("#ctl00_contentHolder_txtProductShortNameTip").text("商品简称应限制在20个字节以内！");
                document.getElementById('ctl00_contentHolder_txtProductShortName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductShortNameTip').className = "msgError";
                return false;
            }
            else {
                $("#ctl00_contentHolder_txtProductShortNameTip").text("商品简称限定在1-20个字节");
                document.getElementById('ctl00_contentHolder_txtProductShortName').className = "forminput";
                document.getElementById('ctl00_contentHolder_txtProductShortNameTip').className = "";
                return true;
            }
        }

    </script>
</asp:Content>
