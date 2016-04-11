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
                <a><span>�����Ʒ</span></a></li>
        </ul>
</div>
    <div class="dataarea mainwidth">
        <div class="datafrom">
            <div class="formitem">
                <ul>
                    <li>
                        <h2 class="colorE">������Ϣ</h2>
                    </li>
                    <li>
                        <span class="sL">������Ŀ��</span>
                        <span class="colorE float fonts">
                            <asp:Literal runat="server" ID="litCategoryName"></asp:Literal></span>
                        [<asp:HyperLink runat="server" ID="lnkEditCategory" CssClass="a" Text="�༭"></asp:HyperLink>]
                    </li>
                    <li>
                        <span class="sL">��Ʒ���ͣ�<em>*</em></span>
                        <abbr class="formselect">
                            <Hi:ProductTypeDownList runat="server" CssClass="productType" ID="dropProductTypes" NullToDisplay="--��ѡ��--" /></abbr>
                        <label runat="server" id="liSupplier">��Ӧ�̣�<em>*</em><abbr class="formselect"><Hi:SupplierDropDownList runat="server" ID="dropSuppliers" NullToDisplay="--��ѡ��--" AutoPostBack="True" OnSelectedIndexChanged="dropSuppliers_Change"/></abbr></label><p>ѡ����Ʒ�����Ĺ�Ӧ��</p>
                    </li>
                   <%-- <li><span class="sL">������Ʒ�ߣ�<em>*</em></span>
                        <abbr class="formselect">
                            <Hi:ProductLineDropDownList runat="server" ID="dropProductLines" NullToDisplay="--��ѡ��--" />
                        </abbr>
                    </li>--%>
                    <li ><span class="sL">Ʒ�ƣ�</span>
                        <abbr class="formselect">
                            <Hi:BrandCategoriesDropDownList runat="server" ID="dropBrandCategories" NullToDisplay="--��ѡ��--"   />
                        </abbr>
                    </li>
                    <li class=" clearfix"><span class="sL">��Ʒ���ƣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductName" Width="350px" onblur="CheckName()"  />
                        <p id="ctl00_contentHolder_txtProductNameTip">�޶���1-60���ַ�</p>
                        <p id="ctl00_contentHolder_txtProductNameRepeat" style="color:red"></p>
                    </li>
                    <li class=" clearfix"><span class="sL">�����⣺</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSubhead" Width="350px"/>
                        <p id="ctl00_contentHolder_txtSubheadTip">�޶���1-150���ַ�</p>
                        <p id="ctl00_contentHolder_txtSubheadRepeat" style="color:red"></p>
                    </li>
                    <li class=" clearfix"><span class="sL">��Ʒ�����룺 </span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProBarCode" Width="350px"  />
                        <p id="ctl00_contentHolder_txtProBarCodeTip">�޶���1-16���ַ�</p>
                        <p id="ctl00_contentHolder_txtProBarCodeRepeat" style="color:red"></p>
                    </li>
                    <li class=" clearfix"><span class="sL">���ͼƬ�� </span>
                        <div class="uploadimages">
                            <Hi:ImageUploader runat="server" ID="ADImage" />
                        </div>
                        </li>
                        
                    <li class=" clearfix"><span class="sL">��Ʒ��ƣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductShortName" Width="350px" onblur="CheckShortName()" />
                        <p id="ctl00_contentHolder_txtProductShortNameTip">�޶���1-20���ַ�</p>
                        <p id="ctl00_contentHolder_txtProductShortNameRepeat" style="color:red"></p>
                    </li>
                    <li><span class="sL">�̼ұ��룺<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductCode" />
                        <Hi:TrimTextBox runat="server" CssClass="forminput"  Style="display: none;" ID="txtGeneralCode" />
                        <p id="ctl00_contentHolder_txtProductCodeTip">���Ȳ��ܳ���20���ַ�</p>
                         <p id="ctl00_contentHolder_txtProductCodeRepeat" style="color:red"></p>
                    </li>
                    <li><span class="sL">������λ��</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtUnit" />
                        <p id="ctl00_contentHolder_txtUnitTip">����������20����������ֻ����Ӣ�ĺ�������:g/Ԫ</p>
                    </li>
                    <li><span class="sL">�г��ۣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMarketPrice" onkeyup="validateprice(1)" />Ԫ
                <p id="ctl00_contentHolder_txtMarketPriceTip">��վ����������������Ʒ�г���</p><p id="tip1" style="color:red"></p>
                    </li>
                    <li><span class="sL">���Ƽۣ�</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtOriginalPrice"  />
                <p id="ctl00_contentHolder_txtOriginalPricePriceTip">Ԫ</p>
                    </li>
                    <li style="display: none"><span class="sL">��������������ۣ�<em>*</em></span>
                        <span style="float: left">
                            <table style="border: none;" width="150px">
                                <tr>
                                    <td style="border: none;">
                                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtLowestSalePrice" /></td>
                                    <td style="border: none; width: 10px;">Ԫ</td>
                                </tr>
                            </table>
                        </span>
                    </li>
                    <li>
                        <h2 class="colorE">��չ����</h2>
                    </li>
                    <li id="attributeRow" style="display: none;"><span class="sL">��Ʒ���ԣ�</span>
                        <div class="attributeContent" id="attributeContent"></div>
                        <Hi:TrimTextBox runat="server" ID="txtAttributes" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                    </li>
                    <li id="skuCodeRow"><span class="sL">SKU���룺<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSku" Enabled="false" />
                        <p id="ctl00_contentHolder_txtSkuTip">�޶���20���ַ�</p>
                    </li>
                    
                    <li id="salePriceRow"><span class="sL">�����ۣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSalePrice" onkeyup="validateprice(2)"/>Ԫ<input type="button" onclick="editProductMemberPrice();" value="�༭������" />
                        <Hi:TrimTextBox runat="server" ID="txtMemberPrices" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                        <p id="ctl00_contentHolder_txtSalePriceTip">��վ����������������Ʒ������</p>
                        <p id="tip2" style="color:red"></p>
                    </li>
                    <li id="costPriceRow" style="display:none"><span class="sL">�ɱ��ۣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtCostPrice" />Ԫ
	            <p id="ctl00_contentHolder_txtCostPriceTip">��Ʒ�ĳɱ���</p>
                    </li>
                    <li id="purchasePriceRow" style="display:none"><span class="sL">�����̲ɹ��ۣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtPurchasePrice" />Ԫ<input type="button" onclick="editProductDistributorPrice();" value="�༭�����ȼ���" />
                        <Hi:TrimTextBox runat="server" ID="txtDistributorPrices" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                        <p id="ctl00_contentHolder_txtPurchasePriceTip">�����̵Ĳɹ���</p>
                    </li>
                    <li id="qtyRow"><span class="sL">��Ʒ��棺<em>*</em></span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtStock" Text="0"   /></li>
                    <li id="alertQtyRow"><span class="sL">�����棺<em>*</em></span><span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtAlertStock" />
                         <p id="ctl00_contentHolder_txtAlertStockTip"></p></span>
                    </li>
                    <li id="weightRow"><span class="sL">��Ʒ������<em>*</em></span><span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtWeight" />��
                        <p id="ctl00_contentHolder_txtWeightTip" ></p></span>

                    </li>
                    <li id="skuTitle" style="display: none;">
                        <h2 class="colorE">��Ʒ���</h2>
                    </li>
                    <li id="enableSkuRow" style="display: none;"><span class="sL">���</span><input id="btnEnableSku" type="button" value="�������" />
                        �������ǰ����д������Ϣ�����Զ�������Ϣ��ÿ�����</li>
                    <li id="skuRow" style="display: none;">
                        <p id="skuContent">
                            <input type="button" id="btnGenerateAll" value="�������й��" />
                            <input type="button" id="btnshowSkuValue" value="���ɲ��ֹ��" />
                            <input type="button" id="btnAddItem" value="����һ�����" />
                            <input type="button" id="btnCloseSku" value="�رչ��" />
                        </p>
                        <p id="skuFieldHolder" style="margin: 0px auto; display: none;"></p>
                        <div id="skuTableHolder">
                        </div>
                        <Hi:TrimTextBox runat="server" ID="txtSkus" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                        <asp:CheckBox runat="server" ID="chkSkuEnabled" Style="display: none;" />
                    </li>
                    <li>
                        <h2 class="colorE">ͼƬ������</h2>
                    </li>
                    <li><span class="sL">��ƷͼƬ��</span>
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
                        <div>ͼƬӦΪjpg,gif,jpeg,png��bmp��ʽ,���ܳ���500k,
                        �����ϴ�700*700���ϳߴ磬�Ա����Ա���ƽ̨�ı�������ҳ��ͼ�ṩ�Ŵ���</div>
                    </li>
                    <li style="display:none"><span class="sL">��Ʒ��飺</span>
                        <Hi:TrimTextBox runat="server" Rows="6" Columns="76" ID="txtShortDescription" TextMode="MultiLine" />
                        <p>�޶���300���ַ�����</p>
                    </li>
                    <li><span class="sL">��Ʒ������</span>
                        <span class="rWidth">
                        <Kindeditor:KindeditorControl ID="editDescription" runat="server" Height="300" Width="100%" /> 
                        </span>                        
                    </li>

                    <li>
                        <h2 class="colorE clear">�������</h2>
                    </li>
                    <li>
                        <span class="sL">��Ʒ����״̬��</span>
                        <span class="sR">
                        <asp:RadioButton runat="server" ID="radInStock" GroupName="SaleStatus" Checked="true" Text="�ֿ���"></asp:RadioButton>
                        <%--<asp:RadioButton runat="server" ID="radOnSales" GroupName="SaleStatus" Text="������"></asp:RadioButton>--%>
                        <asp:RadioButton runat="server" ID="radUnSales" GroupName="SaleStatus" Text="�¼���"></asp:RadioButton>
                        </span>
                    </li>
                    <li style="display:none">
                        <span class="sL">�̻����ã�</span>
                        <span class="sR">
                        <asp:CheckBox runat="server" ID="chkPenetration" Text="�����̻�" /></span>
                    </li>
                    <li id="l_tags" runat="server">
                        <span class="sL">��Ʒ��ǩ���壺<br />
                             </span>

                        <div id="div_tags" class="sR">
                            <Hi:ProductTagsLiteral ID="litralProductTag" runat="server"></Hi:ProductTagsLiteral></div>
                        <div id="div_addtag" style="display: none;margin-top:5px;">
                            <input type="text" id="txtaddtag" /><input type="button" value="����" onclick="return AddAjaxTags()" /></div>
                        <Hi:TrimTextBox runat="server" ID="txtProductTag" TextMode="MultiLine" Style="display: none;"></Hi:TrimTextBox>
                    </li>
                    <li style="display: none;">
                        <span class="sL">�����̴��ۣ�</span>
                        <Hi:YesNoRadioButtonList runat="server" RepeatLayout="Flow" ID="radlEnableMemberDiscount" YesText="��������̴���" NoText="����������̴���" />
                    </li>
                   
                    <li style="display:none">
                        <h2 class="colorE">�����Ż�</h2>
                    </li>
                    <li style="display:none"><span class="sL">��ϸҳ���⣺</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtTitle" Width="350px" />
                    </li>
                    <li style="display:none" ><span class="sL">��ϸҳ������</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMetaDescription" Width="350px" />
                    </li>
                    <li style="display:none"><span class="sL">��ϸҳ�����ؼ��֣�</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMetaKeywords" Width="350px" />
                    </li>
                    <li style="display:none">
                        <h2 class="colorE clear">��Ʒ��������</h2>
                    </li>
                    <li style="display:none"> 
                        <span class="sL">�Ƿ��ƣ�</span>
                        <span class="sR">
                        <asp:RadioButton runat="server" ID="rdButton1" GroupName="IsCustomize" Checked="true" Text="��"></asp:RadioButton>
                        <asp:RadioButton runat="server" ID="rdButton2" GroupName="IsCustomize" Text="��" ></asp:RadioButton> </span>
                    </li>
                    <li> 
                        <span class="sL">������ת·����</span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtCustomizeURL" Width="350px" />
                    </li>
                </ul>
                <ul class="btntf Pa_198 clear">
                    <asp:Button runat="server" ID="btnAdd" Text="�� ��" OnClientClick="return doSubmit();" CssClass="submit_DAqueding inbnt" />
                </ul>
            </div>
        </div>
    </div>
    <div class="Pop_up" id="priceBox" style="display: none;">
        <h1>
            <span id="popTitle"></span>
        </h1>
        <div class="img_datala">
            <img src="../images/icon_dalata.gif" alt="�ر�" />
        </div>
        <div class="mianform ">
            <div id="priceContent">
            </div>
            <div class="aui_buttons" style="margin-top: 10px; text-align: center;">
                <button type="button" onclick="doneEditPrice();" class="aui_state_highlight">ȷ��</button>
            </div>
        </div>
    </div>

    <div class="Pop_up" id="skuValueBox" style="display: none;">
        <h1>
            <span>ѡ��Ҫ���ɵĹ��</span>
        </h1>
        <div class="img_datala">
            <img src="../images/icon_dalata.gif" alt="�ر�" />
        </div>
        <div class="mianform ">
            <div id="skuItems">
            </div>
            <div class="aui_buttons" style="margin-top: 10px; text-align: center;">
                <button type="button" id="btnGenerate" class="aui_state_highlight">ȷ��</button>
            </div>
        </div>
    </div>
    <div id="divImage" style="display: none; position: absolute">
        <img id="imgbig" style="width:300px;height:400px" alt="Ԥ��" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="attributes.helper.js"></script>
    <script type="text/javascript" src="grade.price.helper.js"></script>
    <script type="text/javascript" src="producttag.helper.js"></script>
    <script type="text/javascript" language="javascript">
        //��дgrade.price.helper.js/checkGradePrice
        function checkGradePrice(inputItems) {
            var validated = true;
            var exp = new RegExp("^(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)$", "i");
          
            $.each(inputItems, function (i, v) {
                var gradeid = $(this).attr("gradeid");
                var dval = $(".gradePriceTr.gradeRow[gradeid=" + gradeid + "]>td:last").text().match(/\d+/)[0];
              
                var val = $(this).val();
                if (val.length > 0) {

                    // ���������Ƿ�����Ч�Ľ��
                    if (!exp.test(val)) {
                        alert("�۸�����������������ȷ�ļ۸�");
                        $(this).focus();
                        validated = false;
                        return false;
                    }

                    // ������Ƿ񳬹���ϵͳ��Χ
                    var num = parseFloat(val);
                    if (!((num >= 0.01) && (num <= 10000000))) {
                        alert("����ļ۸񳬳���ϵͳ��ʾ��Χ��");
                        $(this).focus();
                        validated = false;
                        return false;
                    }
                    if (num > parseFloat(dval)) {
                        alert("����ļ۸񲻵ø���ϵͳĬ�ϼ۸�");
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
                    alert("����ļ۸����󣬵�ǰ�����۲��ø�����һ�������ۣ�");
                    $(this).focus();
                    validated = false;
                    return false;
                }
                }
            });
           
            return validated;
        }
        function InitValidators() {

            initValid(new InputValidator('ctl00_contentHolder_txtProductCode', 1, 20, false, null, '�̼ұ�����ַ�������1-20֮��'));
            initValid(new InputValidator('ctl00_contentHolder_txtSubhead', 0, 150, true, null, '��������ַ���������150����'));
            initValid(new InputValidator('ctl00_contentHolder_txtProBarCode', 0, 16, true, null, '��Ʒ��������ַ���������16����'));
            initValid(new InputValidator('ctl00_contentHolder_txtMarketPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMarketPrice', 1, 99999999, '������1-99999999��Χ����ֵ'));
            initValid(new InputValidator('ctl00_contentHolder_txtSalePrice', 1, 50, false,  '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtSalePrice', 1, 99999999, '������1-99999999��Χ����ֵ'));

            //initValid(new InputValidator('ctl00_contentHolder_txtPurchasePrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtPurchasePrice', 0.01, 10000000, '�������ֵ������ϵͳ��ʾ��Χ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtLowestSalePrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtLowestSalePrice', 0.01, 10000000, '�������ֵ������ϵͳ��ʾ��Χ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 10, true, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0, 99999999, '�������ֵ������ϵͳ��ʾ��Χ'));
            initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0, 99999999, '������0-99999999��Χ����ֵ'));
            initValid(new InputValidator('ctl00_contentHolder_txtSku', 0, 20, true, null, '���ŵĳ��Ȳ��ܳ���6���ַ�'));
            initValid(new InputValidator('ctl00_contentHolder_txtBarCode', 0, 16, true, '^(\d+[a-zA-Z]\w*)|([a-zA-Z]+\d\w*)$', '����������16���ַ������Ҳ����������ַ�'))
            initValid(new InputValidator('ctl00_contentHolder_txtStock', 1, 50, false, '[1-9]\\d*|0', 'Ӧ�������0������'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtStock', 0, 9999999, '������1-9999999��Χ����ֵ'));
            initValid(new InputValidator('ctl00_contentHolder_txtAlertStock', 1, 50, false, '[1-9]\\d*|0', 'Ӧ������ڵ���0������'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtAlertStock', 0, 9999999, '������0-9999999��Χ����ֵ'));
            initValid(new InputValidator('ctl00_contentHolder_txtUnit', 1, 20, true, '[a-zA-Z\/\u4e00-\u9fa5]*$', '����������20���ַ�������ֻ����Ӣ�ĺ�������:g/Ԫ'));
            initValid(new InputValidator('ctl00_contentHolder_txtWeight', 0, 50, true, '^[0-9]*[1-9][0-9]*$', 'Ӧ������������ֵ'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtWeight', 1, 9999999, '������1-9999999��Χ����ֵ'));
            initValid(new InputValidator('ctl00_contentHolder_txtShortDescription', 0, 300, true, null, '��Ʒ������������20���ַ�����'));
            initValid(new InputValidator('ctl00_contentHolder_txtTitle', 0, 100, true, null, '��ϸҳ���ⳤ��������100���ַ�����'));
            initValid(new InputValidator('ctl00_contentHolder_txtMetaDescription', 0, 100, true, null, '��ϸҳ��������������100���ַ�����'));
            initValid(new InputValidator('ctl00_contentHolder_txtMetaKeywords', 0, 100, true, null, '��ϸҳ�����ؼ��ֳ���������100���ַ�����'));
             
        }
        function validateprice(con) {
            if ($("#ctl00_contentHolder_txtSalePrice").val() != "" && $("#ctl00_contentHolder_txtMarketPrice").val()!=""){
                if (parseFloat($("#ctl00_contentHolder_txtSalePrice").val()) > parseFloat($("#ctl00_contentHolder_txtMarketPrice").val())) {
                    $("#tip" + con).text("�����۲��ܴ����г���.");
                } else {
                    $("#tip1").text("");
                    $("#tip2").text("");
                }
            }
        }
        $(document).ready(function () { InitValidators(); });
        /*��ȡ�ַ����ֽ���*/
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
        /*��Ʒ������֤*/
        function CheckName() {
            var length = GetCharLength(document.getElementById('ctl00_contentHolder_txtProductName').value);
            if (length == 0 || document.getElementById('ctl00_contentHolder_txtProductName').value == "") {
                $("#ctl00_contentHolder_txtProductNameTip").text("��������Ʒ���ƣ�");
                document.getElementById('ctl00_contentHolder_txtProductName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductNameTip').className = "msgError";
                return false;
            }
            else if (length > 60) {
                $("#ctl00_contentHolder_txtProductNameTip").text("��Ʒ����Ӧ������60���ֽ����ڣ�");
                document.getElementById('ctl00_contentHolder_txtProductName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductNameTip').className = "msgError";
                return false;
            }
            else {
                $("#ctl00_contentHolder_txtProductNameTip").text("��Ʒ�����޶���1-60���ֽ�");
                document.getElementById('ctl00_contentHolder_txtProductName').className = "forminput";
                document.getElementById('ctl00_contentHolder_txtProductNameTip').className = "";
                //$("#ctl00_contentHolder_txtProductShortName").val($("#ctl00_contentHolder_txtProductName").val().substr(0, 5));
                return true;
            }
        }
        /*��Ʒ�����֤*/
        function CheckShortName() {
            var length = GetCharLength(document.getElementById('ctl00_contentHolder_txtProductShortName').value);
            if (length == 0 || document.getElementById('ctl00_contentHolder_txtProductShortName').value == "") {
                $("#ctl00_contentHolder_txtProductShortNameTip").text("��������Ʒ��ƣ�");
                document.getElementById('ctl00_contentHolder_txtProductShortName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductShortNameTip').className = "msgError";
                return false;
            }
            else if (length > 20) {
                $("#ctl00_contentHolder_txtProductShortNameTip").text("��Ʒ���Ӧ������20���ֽ����ڣ�");
                document.getElementById('ctl00_contentHolder_txtProductShortName').className = "errorFocus";
                document.getElementById('ctl00_contentHolder_txtProductShortNameTip').className = "msgError";
                return false;
            }
            else {
                $("#ctl00_contentHolder_txtProductShortNameTip").text("��Ʒ����޶���1-20���ֽ�");
                document.getElementById('ctl00_contentHolder_txtProductShortName').className = "forminput";
                document.getElementById('ctl00_contentHolder_txtProductShortNameTip').className = "";
                return true;
            }
        }

    </script>
</asp:Content>
