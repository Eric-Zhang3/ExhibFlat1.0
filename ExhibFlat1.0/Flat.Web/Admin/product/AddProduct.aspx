<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="Flat.Web.Admin.AddProduct" %>

<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.Common.Controls" Assembly="ExhibFlat.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.ControlPanel.Utility" Assembly="ExhibFlat.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.Common.Validator" Assembly="ExhibFlat.UI.Common.Validator" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <link id="cssLink" rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />    
    <Hi:Script ID="Script2" runat="server" Src="/utility/jquery_hashtable.js" />
    <Hi:Script ID="Script1" runat="server" Src="/utility/jquery-powerFloat-min.js" />
    <script src="/Utility/jquery-1.6.4.min.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
     
<div class="optiongroup mainwidth">
        <ul>
            <li class="menucurrent">
                <a><span>添加产品</span></a></li>
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
                    <li><span class="sL">市场价：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMarketPrice" onkeyup="validateprice(1)" />元
                <p id="ctl00_contentHolder_txtMarketPriceTip">本站分销商所看到的商品市场价</p><p id="tip1" style="color:red"></p>
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
                    </ul>
                <ul class="btntf Pa_198 clear">
                    <asp:Button runat="server" ID="btnAdd" Text="保 存" OnClientClick="return doSubmit();" CssClass="submit_DAqueding inbnt" />
                </ul>
            </div>
        </div>
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

            //initValid(new InputValidator('ctl00_contentHolder_txtProductCode', 1, 20, false, null, '商家编码的字符长度在1-20之间'));
            //initValid(new InputValidator('ctl00_contentHolder_txtSubhead', 0, 150, true, null, '副标题的字符长度限制150以内'));
            //initValid(new InputValidator('ctl00_contentHolder_txtProBarCode', 0, 16, true, null, '商品条形码的字符长度限制16以内'));
            //initValid(new InputValidator('ctl00_contentHolder_txtMarketPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMarketPrice', 1, 99999999, '请输入1-99999999范围的数值'));
            //initValid(new InputValidator('ctl00_contentHolder_txtSalePrice', 1, 50, false,  '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtSalePrice', 1, 99999999, '请输入1-99999999范围的数值'));
            //initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0, 99999999, '请输入0-99999999范围的数值'));
            //initValid(new InputValidator('ctl00_contentHolder_txtSku', 0, 20, true, null, '货号的长度不能超过6个字符'));
            //initValid(new InputValidator('ctl00_contentHolder_txtBarCode', 0, 16, true, '^(\d+[a-zA-Z]\w*)|([a-zA-Z]+\d\w*)$', '必须限制在16个字符以内且不能是特殊字符'))
            //initValid(new InputValidator('ctl00_contentHolder_txtStock', 1, 50, false, '[1-9]\\d*|0', '应输入大于0的整数'));
            //appendValid(new NumberRangeValidator('ctl00_contentHolder_txtStock', 0, 9999999, '请输入1-9999999范围的数值'));
            //initValid(new InputValidator('ctl00_contentHolder_txtAlertStock', 1, 50, false, '[1-9]\\d*|0', '应输入大于等于0的整数'));
            //appendValid(new NumberRangeValidator('ctl00_contentHolder_txtAlertStock', 0, 9999999, '请输入0-9999999范围的数值'));
            //initValid(new InputValidator('ctl00_contentHolder_txtUnit', 1, 20, true, '[a-zA-Z\/\u4e00-\u9fa5]*$', '必须限制在20个字符以内且只能是英文和中文例:g/元'));
            //initValid(new InputValidator('ctl00_contentHolder_txtWeight', 0, 50, true, '^[0-9]*[1-9][0-9]*$', '应输入整数型数值'));
            //appendValid(new NumberRangeValidator('ctl00_contentHolder_txtWeight', 1, 9999999, '请输入1-9999999范围的数值'));
            //initValid(new InputValidator('ctl00_contentHolder_txtShortDescription', 0, 300, true, null, '商品简介必须限制在20个字符以内'));
            //initValid(new InputValidator('ctl00_contentHolder_txtTitle', 0, 100, true, null, '详细页标题长度限制在100个字符以内'));
            //initValid(new InputValidator('ctl00_contentHolder_txtMetaDescription', 0, 100, true, null, '详细页描述长度限制在100个字符以内'));
            //initValid(new InputValidator('ctl00_contentHolder_txtMetaKeywords', 0, 100, true, null, '详细页搜索关键字长度限制在100个字符以内'));
             
        }
        function validateprice(con) {
            
        }
        //$(document).ready(function () { InitValidators(); });
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
