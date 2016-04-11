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
                <a><span>��Ӳ�Ʒ</span></a></li>
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
                    <li><span class="sL">�г��ۣ�<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMarketPrice" onkeyup="validateprice(1)" />Ԫ
                <p id="ctl00_contentHolder_txtMarketPriceTip">��վ����������������Ʒ�г���</p><p id="tip1" style="color:red"></p>
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
                    </ul>
                <ul class="btntf Pa_198 clear">
                    <asp:Button runat="server" ID="btnAdd" Text="�� ��" OnClientClick="return doSubmit();" CssClass="submit_DAqueding inbnt" />
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

            //initValid(new InputValidator('ctl00_contentHolder_txtProductCode', 1, 20, false, null, '�̼ұ�����ַ�������1-20֮��'));
            //initValid(new InputValidator('ctl00_contentHolder_txtSubhead', 0, 150, true, null, '��������ַ���������150����'));
            //initValid(new InputValidator('ctl00_contentHolder_txtProBarCode', 0, 16, true, null, '��Ʒ��������ַ���������16����'));
            //initValid(new InputValidator('ctl00_contentHolder_txtMarketPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMarketPrice', 1, 99999999, '������1-99999999��Χ����ֵ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtSalePrice', 1, 50, false,  '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'));
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtSalePrice', 1, 99999999, '������1-99999999��Χ����ֵ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 50, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '�������ʹ���ֻ������ʵ������ֵ'))
            //appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0, 99999999, '������0-99999999��Χ����ֵ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtSku', 0, 20, true, null, '���ŵĳ��Ȳ��ܳ���6���ַ�'));
            //initValid(new InputValidator('ctl00_contentHolder_txtBarCode', 0, 16, true, '^(\d+[a-zA-Z]\w*)|([a-zA-Z]+\d\w*)$', '����������16���ַ������Ҳ����������ַ�'))
            //initValid(new InputValidator('ctl00_contentHolder_txtStock', 1, 50, false, '[1-9]\\d*|0', 'Ӧ�������0������'));
            //appendValid(new NumberRangeValidator('ctl00_contentHolder_txtStock', 0, 9999999, '������1-9999999��Χ����ֵ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtAlertStock', 1, 50, false, '[1-9]\\d*|0', 'Ӧ������ڵ���0������'));
            //appendValid(new NumberRangeValidator('ctl00_contentHolder_txtAlertStock', 0, 9999999, '������0-9999999��Χ����ֵ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtUnit', 1, 20, true, '[a-zA-Z\/\u4e00-\u9fa5]*$', '����������20���ַ�������ֻ����Ӣ�ĺ�������:g/Ԫ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtWeight', 0, 50, true, '^[0-9]*[1-9][0-9]*$', 'Ӧ������������ֵ'));
            //appendValid(new NumberRangeValidator('ctl00_contentHolder_txtWeight', 1, 9999999, '������1-9999999��Χ����ֵ'));
            //initValid(new InputValidator('ctl00_contentHolder_txtShortDescription', 0, 300, true, null, '��Ʒ������������20���ַ�����'));
            //initValid(new InputValidator('ctl00_contentHolder_txtTitle', 0, 100, true, null, '��ϸҳ���ⳤ��������100���ַ�����'));
            //initValid(new InputValidator('ctl00_contentHolder_txtMetaDescription', 0, 100, true, null, '��ϸҳ��������������100���ַ�����'));
            //initValid(new InputValidator('ctl00_contentHolder_txtMetaKeywords', 0, 100, true, null, '��ϸҳ�����ؼ��ֳ���������100���ַ�����'));
             
        }
        function validateprice(con) {
            
        }
        //$(document).ready(function () { InitValidators(); });
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
