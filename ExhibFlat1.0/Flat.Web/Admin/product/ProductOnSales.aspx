<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductOnSales.aspx.cs" Inherits="Flat.Web.Admin.ProductOnSales" %>

<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.Common.Controls" Assembly="ExhibFlat.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.ControlPanel.Utility" Assembly="ExhibFlat.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../Utility/jquery-1.8.2.min.js"></script>
    <script src="../../Utility/json2.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <script src="../js/jquery-1.8.2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">

    <div class="optiongroup mainwidth">
        <ul>
            <li class="menucurrent"><a><span>�����е���Ʒ</span></a></li>
            <li><a href="ProductUnSales.aspx"><span>�¼�������Ʒ</span></a></li>
            <li><a href="ProductInStock.aspx"><span>�ֿ��е���Ʒ</span></a></li>
            <li><a href="ProductUnclassified.aspx"><span>δ������Ʒ</span></a></li>
            <li class="optionend"><a href="ProductOnDeleted.aspx"><span>����վ�е���Ʒ</span></a></li>
        </ul>
    </div>
    <!--ѡ�-->

    <div class="dataarea mainwidth">
        <!--����-->
        <div class="searcharea">
            <ul>
                <li><span>��Ʒ���ƣ�</span><span><asp:TextBox ID="txtSearchText" runat="server" CssClass="forminput" /></span></li>
                <li>
                    <abbr class="formselect">
                        <Hi:ProductCategoriesDropDownList ID="dropCategories" runat="server" NullToDisplay="--��ѡ����Ŀ--" />
                    </abbr>
                </li>
                
                <li>
                    <abbr class="formselect">
                        <Hi:ProductTypeDownList ID="dropType" runat="server" NullToDisplay="--��ѡ����Ʒ����--" />
                    </abbr>
                </li>
            </ul>
        </div>
        <div class="searcharea">
            <ul>
                <li><span>�̼ұ��룺</span><span><asp:TextBox ID="txtSKU" runat="server" CssClass="forminput" /></span></li>
                <li>
                    <span>���ʱ�䣺</span><span>
                        <%--<UI:WebCalendar CalendarType="StartDate" ID="calendarStartDate" runat="server" cssclass="forminput" /><span class="Pg_1010">��</span><UI:WebCalendar ID="calendarEndDate" runat="server" CalendarType="EndDate" cssclass="forminput" />--%>
                        <input type="text" class="Wdate" id="calendarStartDate" readonly="readonly" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'calendarEndDate\');}'})" /></span><span class="Pg_1010">��</span>
                    <span>
                        <input type="text" class="Wdate" id="calendarEndDate" readonly="readonly" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'calendarStartDate\');}'})" />
                        <asp:HiddenField ID="hidStartDate" runat="server" />
                        <asp:HiddenField ID="hidEndDate" runat="server" />
                    </span>
                </li>
                <li>
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="searchbutton" /></li>
            </ul>
        </div>
        <!--����-->
        <div class="functionHandleArea">
            <div class="pageNumber">
                <div class="pagination">
                    <UI:Pager runat="server" ShowTotalPages="false" ID="pager1" />
                </div>
            </div>
            <!--����-->
            <div class="batchHandleArea">
                <ul>
                    <li class="batchHandleButton">
                        <span class="signicon"></span>
                        <span class="allSelect"><a href="javascript:void(0)" onclick="SelectAll()">ȫѡ</a></span>
                        <span class="reverseSelect"><a href="javascript:void(0)" onclick="ReverseSelect()">��ѡ</a></span>
                        <span class="delete">
                            <Hi:ImageLinkButton ID="btnDelete" runat="server" Text="ɾ��" IsShow="true" DeleteMsg="ȷ��Ҫ����Ʒ�������վ��" /></span>
                        <span class="submit_btnxiajia"><a href="javascript:void(0)" onclick="javascript:PenetrationStatus();">�¼�</a></span>
                        <span class="submit_btnxiajia"><a href="javascript:void(0)" onclick="javascript:StockStatus();">���</a></span>
                        <span class="downproduct"><a href="javascript:void(0)" onclick="EditBaseInfo()">����������Ϣ</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="EditSaleCounts()">������ʾ��������</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="EditMemberPrices()">����������</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="EditProdcutTag()">������Ʒ������ǩ</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="BatchUpdStocks()">����ͬ�����</a></span>
                    </li>
                </ul>
            </div>
        </div>

        <!--�����б�����-->
        <div class="datalist">
            <UI:Grid runat="server" ID="grdProducts" Width="100%" AllowSorting="true" ShowOrderIcons="true" GridLines="None" DataKeyNames="ProductId"
                SortOrder="Desc" AutoGenerateColumns="false" HeaderStyle-CssClass="table_title">

                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="ѡ��" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("ProductId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="����" DataField="DisplaySequence" ItemStyle-Width="40px" HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField ItemStyle-Width="35%" HeaderText="��Ʒ" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <div style="float: left; margin-right: 10px;">
                                <a href='<%#"../../ProductDetails.aspx?productId="+Eval("ProductId")%>' target="_blank">
                                    <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailUrl40" />
                                </a>
                            </div>
                            <div style="float: left; line-height: 20px;" class="tl">
                                <span class="Name"><a href='<%#"../../ProductDetails.aspx?productId="+Eval("ProductId")%>' target="_blank"><%# Eval("ProductName") %></a></span>
                                <span class="colorC">�̼ұ��룺<%# Eval("ProductCode") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="���" ItemStyle-Width="100" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' Width="25"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <Hi:MoneyColumnForAdmin HeaderText=" �г���" ItemStyle-Width="80" DataField="MarketPrice" HeaderStyle-CssClass="td_right td_left" />
                    
                    
                     <asp:BoundField HeaderText="���ʱ��" DataField="AddedDate" ItemStyle-Width="120" HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField HeaderText="����" ItemStyle-Width="19%" HeaderStyle-CssClass=" td_left td_right_fff">
                        <ItemTemplate>
                            <span class="submit_bianji"><a href="<%#"EditProduct.aspx?productId="+Eval("ProductId")%>" target="_blank">�༭</a></span>
                            
                            <span class="submit_shanchu">
                                <Hi:ImageLinkButton ID="btnDelete" CommandName="Delete" runat="server" Text="ɾ��" IsShow="true" DeleteMsg="ȷ��Ҫ����Ʒ�������վ��" /></span>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>
        </div>
        <div class="page">
            <div class="bottomPageNumber">
                <!--��ҳ����-->
                <div class="pageHandleArea">
                    <ul>
                        <li class="paginalNum"><span>ÿҳ��ʾ������</span><UI:PageSize runat="server" ID="hrefPageSize" />
                        </li>
                    </ul>
                </div>
                <div class="pageNumber">
                    <div class="pagination">
                        <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                    </div>
                </div>
            </div>
        </div>

    </div>

    <%-- �¼���Ʒ--%>
    <div id="UnSaleProduct" style="display: none;">
        <div class="frame-content">
            <%-- ͬʱ�����̻���<asp:CheckBox ID="chkDeleteImage" Text="�����̻�" Checked="true" runat="server" onclick="javascript:SetPenetrationStatus(this)" />
    <p><em>��ѡ�����̻�ʱ��������վ���ڴ���Ʒ��Ϣ�Լ��������Ϣ������ɾ��</em></p>--%>
        ���¼ܣ�������վ���ڴ���Ʒ��������Ϣ�ȶ�����ɾ���������̽����ɼ����µ���֧�����Ƿ�����¼�
        </div>
    </div>

    <%-- �����Ʒ--%>
    <div id="InStockProduct" style="display: none;">
        <div class="frame-content">
            <%--  ͬʱ�����̻���<asp:CheckBox ID="chkInstock" Text="�����̻�" Checked="true" runat="server" onclick="javascript:SetPenetrationStatus(this)" />
        <p><em>��ѡ�����̻�ʱ��������վ���ڴ���Ʒ��Ϣ�Լ��������Ϣ������ɾ����</em></p>--%>
         ����⣬������վ���ڴ���Ʒ��������Ϣ�ȶ�����ɾ���������̽����ɼ����µ���֧�����Ƿ�������
        </div>
    </div>

    
    <div style="display: none">
        
        <Hi:TrimTextBox runat="server" ID="txtProductTag" TextMode="MultiLine"></Hi:TrimTextBox>
        <asp:Button ID="btnStockPentrationStauts" runat="server" Text="�����Ʒ" CssClass="submit_DAqueding" />
        <asp:Button ID="btnOK" runat="server" Text="�¼���Ʒ" CssClass="submit_DAqueding" />
        <input type="hidden" id="hdPenetrationStatus" value="1" runat="server" />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="producttag.helper.js"></script>
    <script type="text/javascript" src="../../Utility/layer/layer.min.js"></script>
    <script src="../../Utility/My97DatePicker/calendar.js"></script>
    <script src="../../Utility/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#calendarStartDate").val($("#contentHolder_hidStartDate").val());
            $("#calendarEndDate").val($("#contentHolder_hidEndDate").val());
            $("#contentHolder_btnSearch").off().on("click", function () {
                var startDate = $("#calendarStartDate").val();
                var endDate = $("#calendarEndDate").val();
                $("#contentHolder_hidStartDate").val(startDate);
                $("#contentHolder_hidEndDate").val(endDate);
                return true;
            });


        });

        var formtype = "tag";
        function BatchUpdStocks() {
            var productIds = GetProductId();
            if (productIds.length > 0) {
                layer.load("ͬ����...", 150);
                $.ajax({
                    type: 'Post',
                    url: '/admin/product/WarehouseInfoOperate.aspx?BatchUpdStocks=' + productIds,
                    success: function (msg) {
                        if (msg == "TRUE") {
                            layer.msg("ͬ���ɹ�", 1, 1);
                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                        else if (msg == "FALSE") {
                            layer.msg("����ʧ��", 1, 0);
                        }
                    }
                })
            }

        }

        function SynchronizeToWarehouse(productId) {
            layer.load("ͬ����...", 15);
            $.ajax({
                type: 'Post',
                url: '/admin/product/SynchronizeProductToWarehouse.aspx?productId=' + productId,
                success: function (msg) {
                    if (msg == "True") {
                        layer.msg("�����ɹ�", 1, 1, function () {
                            layer.closeAll();
                        });
                        //alert("�����ɹ�");
                    }
                    else {
                        layer.msg("ʧ�ܣ�" + msg, 3, 0, function () {
                            layer.closeAll();
                        });
                        //alert("ʧ�ܣ�"+msg);
                    }
                }
            })
        }
        function EditStocks() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditStocks.aspx?ProductIds=" + productIds, "�������", 880, null);
        }

        function EditBaseInfo() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditBaseInfo.aspx?ProductIds=" + productIds, "������Ʒ������Ϣ", null, null);
        }

        function EditSaleCounts() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditSaleCounts.aspx?ProductIds=" + productIds, "����ǰ̨��ʾ����������", null, null);
        }

        function EditMemberPrices() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditMemberPrices.aspx?ProductIds=" + productIds, "����������", 1000, null);
        }

        function EditDistributorPrices() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditDistributorPrices.aspx?ProductIds=" + productIds, "���������̲ɹ���", 1000, null);
        }

        function GetProductId() {
            var v_str = "";

            $("input[type='checkbox'][name='CheckBoxGroup']:checked").each(function (rowIndex, rowItem) {
                v_str += $(rowItem).attr("value") + ",";
            });

            if (v_str.length == 0) {
                alert("��ѡ����Ʒ");
                return "";
            }
            return v_str.substring(0, v_str.length - 1);
        }
        function GetActivityCount() {
            var v_str = "";

            $("input[type='checkbox'][name='CheckBoxGroup']:checked").each(function (rowIndex, rowItem) {
                v_str += $(rowItem).attr("value") + ",";
            });

            if (v_str.length == 0) {
                alert("��ѡ����Ʒ");
                return "";
            }
            return v_str.substring(0, v_str.length - 1);
        }

        function SetPenetrationStatus(checkobj) {
            if (checkobj.checked) {
                $("#contentHolder_hdPenetrationStatus").val("1");
            } else {
                $("#contentHolder_hdPenetrationStatus").val("0");
            }
        }


        //�¼�
        function PenetrationStatus() {

            var productIds = GetProductId();
            if (productIds.length > 0) {
                formtype = "unsale";
                arrytext = null;
                DialogShow("��Ʒ�¼�", "productunsale", "UnSaleProduct", "contentHolder_btnOK");
            }
        }

        //���
        function StockStatus() {
            var productIds = GetProductId();
            if (productIds.length > 0) {
                formtype = "instock";
                arrytext = null;
                DialogShow("��Ʒ���", "productinstock", "InStockProduct", "contentHolder_btnStockPentrationStauts");
            }

        }

        function EditProdcutTag() {
            var productIds = GetProductId();
            if (productIds.length > 0) {
                formtype = "tag";
                arrytext = null;
                setArryText('contentHolder_txtProductTag', "");
                DialogShow("��Ʒ��ǩ", "producttag", "TagsProduct", "contentHolder_btnUpdateProductTags");
            }

        }

        function CollectionProduct(url) {
            DialogFrame("product/" + url, "�����Ʒ");
        }



        function validatorForm() {
            switch (formtype) {
                case "tag":
                    if ($("#contentHolder_txtProductTag").val().replace(/\s/g, "") == "") {
                        alert("��ѡ����Ʒ��ǩ");
                        return false;
                    }
                    break;
                case "unsale":
                    setArryText('contentHolder_hdPenetrationStatus', $("#contentHolder_hdPenetrationStatus").val());
                    break;
                case "instock":
                    setArryText('contentHolder_hdPenetrationStatus', $("#contentHolder_hdPenetrationStatus").val());
                    break;
            };
            return true;
        }

    </script>
</asp:Content>
