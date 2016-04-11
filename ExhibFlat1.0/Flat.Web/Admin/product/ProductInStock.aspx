<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductOnSales.aspx.cs" Inherits="Flat.Web.Admin.ProductInStock" %>

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
            <li><a href="ProductOnSales.aspx"><span>出售中的商品</span></a></li>
            <li><a href="ProductUnSales.aspx"><span>下架区的商品</span></a></li>
            <li class="menucurrent"><a href="ProductInStock.aspx"><span>仓库中的商品</span></a></li>
            <li><a href="ProductUnclassified.aspx"><span>未分类商品</span></a></li>
            <li class="optionend"><a href="ProductOnDeleted.aspx"><span>回收站中的商品</span></a></li>
        </ul>
    </div>
    <!--选项卡-->

    <div class="dataarea mainwidth">
        <!--搜索-->
        <div class="searcharea">
            <ul>
                <li><span>商品名称：</span><span><asp:TextBox ID="txtSearchText" runat="server" CssClass="forminput" /></span></li>
                <li>
                    <abbr class="formselect">
                        <Hi:ProductCategoriesDropDownList ID="dropCategories" runat="server" NullToDisplay="--请选择类目--" />
                    </abbr>
                </li>
                
                <li>
                    <abbr class="formselect">
                        <Hi:ProductTypeDownList ID="dropType" runat="server" NullToDisplay="--请选择商品类型--" />
                    </abbr>
                </li>
            </ul>
        </div>
        <div class="searcharea">
            <ul>
                <li><span>商家编码：</span><span><asp:TextBox ID="txtSKU" runat="server" CssClass="forminput" /></span></li>
                <li>
                    <span>添加时间：</span><span>
                        <%--<UI:WebCalendar CalendarType="StartDate" ID="calendarStartDate" runat="server" cssclass="forminput" /><span class="Pg_1010">至</span><UI:WebCalendar ID="calendarEndDate" runat="server" CalendarType="EndDate" cssclass="forminput" />--%>
                        <input type="text" class="Wdate" id="calendarStartDate" readonly="readonly" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'calendarEndDate\');}'})" /></span><span class="Pg_1010">至</span>
                    <span>
                        <input type="text" class="Wdate" id="calendarEndDate" readonly="readonly" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'calendarStartDate\');}'})" />
                        <asp:HiddenField ID="hidStartDate" runat="server" />
                        <asp:HiddenField ID="hidEndDate" runat="server" />
                    </span>
                </li>
                <li>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="searchbutton" /></li>
            </ul>
        </div>
        <!--结束-->
        <div class="functionHandleArea">
            <div class="pageNumber">
                <div class="pagination">
                    <UI:Pager runat="server" ShowTotalPages="false" ID="pager1" />
                </div>
            </div>
            <!--结束-->
            <div class="batchHandleArea">
                <ul>
                    <li class="batchHandleButton">
                        <span class="signicon"></span>
                        <span class="allSelect"><a href="javascript:void(0)" onclick="SelectAll()">全选</a></span>
                        <span class="reverseSelect"><a href="javascript:void(0)" onclick="ReverseSelect()">反选</a></span>
                        <span class="delete">
                            <Hi:ImageLinkButton ID="btnDelete" runat="server" Text="删除" IsShow="true" DeleteMsg="确定要把商品移入回收站吗？" /></span>
                        <span class="submit_btnxiajia"><a href="javascript:void(0)" onclick="javascript:PenetrationStatus();">下架</a></span>
                        <span class="submit_btnxiajia"><a href="javascript:void(0)" onclick="javascript:StockStatus();">入库</a></span>
                        <span class="downproduct"><a href="javascript:void(0)" onclick="EditBaseInfo()">调整基本信息</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="EditSaleCounts()">调整显示销售数量</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="EditMemberPrices()">调整批发价</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="EditProdcutTag()">调整商品关联标签</a></span>
                        <span class="printorder"><a href="javascript:void(0)" onclick="BatchUpdStocks()">批量同步库存</a></span>
                    </li>
                </ul>
            </div>
        </div>

        <!--数据列表区域-->
        <div class="datalist">
            <UI:Grid runat="server" ID="grdProducts" Width="100%" AllowSorting="true" ShowOrderIcons="true" GridLines="None" DataKeyNames="ProductId"
                SortOrder="Desc" AutoGenerateColumns="false" HeaderStyle-CssClass="table_title">

                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="选择" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("ProductId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="排序" DataField="DisplaySequence" ItemStyle-Width="40px" HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField ItemStyle-Width="35%" HeaderText="商品" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <div style="float: left; margin-right: 10px;">
                                <a href='<%#"../../ProductDetails.aspx?productId="+Eval("ProductId")%>' target="_blank">
                                    <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailUrl40" />
                                </a>
                            </div>
                            <div style="float: left; line-height: 20px;" class="tl">
                                <span class="Name"><a href='<%#"../../ProductDetails.aspx?productId="+Eval("ProductId")%>' target="_blank"><%# Eval("ProductName") %></a></span>
                                <span class="colorC">商家编码：<%# Eval("ProductCode") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库存" ItemStyle-Width="100" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' Width="25"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <Hi:MoneyColumnForAdmin HeaderText=" 市场价" ItemStyle-Width="80" DataField="MarketPrice" HeaderStyle-CssClass="td_right td_left" />
                    
                    
                     <asp:BoundField HeaderText="添加时间" DataField="AddedDate" ItemStyle-Width="120" HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField HeaderText="操作" ItemStyle-Width="19%" HeaderStyle-CssClass=" td_left td_right_fff">
                        <ItemTemplate>
                            <span class="submit_bianji"><a href="<%#"EditProduct.aspx?productId="+Eval("ProductId")%>" target="_blank">编辑</a></span>
                            
                            <span class="submit_shanchu">
                                <Hi:ImageLinkButton ID="btnDelete" CommandName="Delete" runat="server" Text="删除" IsShow="true" DeleteMsg="确定要把商品移入回收站吗？" /></span>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>
        </div>
        <div class="page">
            <div class="bottomPageNumber">
                <!--分页功能-->
                <div class="pageHandleArea">
                    <ul>
                        <li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" />
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

    <%-- 下架商品--%>
    <div id="UnSaleProduct" style="display: none;">
        <div class="frame-content">
            <%-- 同时撤销铺货：<asp:CheckBox ID="chkDeleteImage" Text="撤销铺货" Checked="true" runat="server" onclick="javascript:SetPenetrationStatus(this)" />
    <p><em>当选择撤销铺货时，所有子站关于此商品信息以及促销活动信息都将被删除</em></p>--%>
        若下架，所有子站关于此商品及促销信息等都将被删除，分销商将不可继续下单或支付，是否继续下架
        </div>
    </div>

    <%-- 入库商品--%>
    <div id="InStockProduct" style="display: none;">
        <div class="frame-content">
            <%--  同时撤销铺货：<asp:CheckBox ID="chkInstock" Text="撤销铺货" Checked="true" runat="server" onclick="javascript:SetPenetrationStatus(this)" />
        <p><em>当选择撤销铺货时，所有子站关于此商品信息以及促销活动信息都将被删除。</em></p>--%>
         若入库，所有子站关于此商品及促销信息等都将被删除，分销商将不可继续下单或支付，是否继续入库
        </div>
    </div>

    
    <div style="display: none">
        
        <Hi:TrimTextBox runat="server" ID="txtProductTag" TextMode="MultiLine"></Hi:TrimTextBox>
        <asp:Button ID="btnStockPentrationStauts" runat="server" Text="入库商品" CssClass="submit_DAqueding" />
        <asp:Button ID="btnOK" runat="server" Text="下架商品" CssClass="submit_DAqueding" />
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
            $("#calendarStartDate").val($("#ctl00_contentHolder_hidStartDate").val());
            $("#calendarEndDate").val($("#ctl00_contentHolder_hidEndDate").val());
            $("#ctl00_contentHolder_btnSearch").off().on("click", function () {
                var startDate = $("#calendarStartDate").val();
                var endDate = $("#calendarEndDate").val();
                $("#ctl00_contentHolder_hidStartDate").val(startDate);
                $("#ctl00_contentHolder_hidEndDate").val(endDate);
                return true;
            });


        });

        var formtype = "tag";
        function BatchUpdStocks() {
            var productIds = GetProductId();
            if (productIds.length > 0) {
                layer.load("同步中...", 150);
                $.ajax({
                    type: 'Post',
                    url: '/admin/product/WarehouseInfoOperate.aspx?BatchUpdStocks=' + productIds,
                    success: function (msg) {
                        if (msg == "TRUE") {
                            layer.msg("同步成功", 1, 1);
                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                        else if (msg == "FALSE") {
                            layer.msg("操作失败", 1, 0);
                        }
                    }
                })
            }

        }

        function SynchronizeToWarehouse(productId) {
            layer.load("同步中...", 15);
            $.ajax({
                type: 'Post',
                url: '/admin/product/SynchronizeProductToWarehouse.aspx?productId=' + productId,
                success: function (msg) {
                    if (msg == "True") {
                        layer.msg("操作成功", 1, 1, function () {
                            layer.closeAll();
                        });
                        //alert("操作成功");
                    }
                    else {
                        layer.msg("失败：" + msg, 3, 0, function () {
                            layer.closeAll();
                        });
                        //alert("失败："+msg);
                    }
                }
            })
        }
        function EditStocks() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditStocks.aspx?ProductIds=" + productIds, "调整库存", 880, null);
        }

        function EditBaseInfo() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditBaseInfo.aspx?ProductIds=" + productIds, "调整商品基本信息", null, null);
        }

        function EditSaleCounts() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditSaleCounts.aspx?ProductIds=" + productIds, "调整前台显示的销售数量", null, null);
        }

        function EditMemberPrices() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditMemberPrices.aspx?ProductIds=" + productIds, "调整分销价", 1000, null);
        }

        function EditDistributorPrices() {
            var productIds = GetProductId();
            if (productIds.length > 0)
                DialogFrame("product/EditDistributorPrices.aspx?ProductIds=" + productIds, "调整分销商采购价", 1000, null);
        }

        function GetProductId() {
            var v_str = "";

            $("input[type='checkbox'][name='CheckBoxGroup']:checked").each(function (rowIndex, rowItem) {
                v_str += $(rowItem).attr("value") + ",";
            });

            if (v_str.length == 0) {
                alert("请选择商品");
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
                alert("请选择商品");
                return "";
            }
            return v_str.substring(0, v_str.length - 1);
        }

        function SetPenetrationStatus(checkobj) {
            if (checkobj.checked) {
                $("#ctl00_contentHolder_hdPenetrationStatus").val("1");
            } else {
                $("#ctl00_contentHolder_hdPenetrationStatus").val("0");
            }
        }


        //下架
        function PenetrationStatus() {

            var productIds = GetProductId();
            if (productIds.length > 0) {
                formtype = "unsale";
                arrytext = null;
                DialogShow("商品下架", "productunsale", "UnSaleProduct", "ctl00_contentHolder_btnOK");
            }
        }

        //入库
        function StockStatus() {
            var productIds = GetProductId();
            if (productIds.length > 0) {
                formtype = "instock";
                arrytext = null;
                DialogShow("商品入库", "productinstock", "InStockProduct", "ctl00_contentHolder_btnStockPentrationStauts");
            }

        }

        function EditProdcutTag() {
            var productIds = GetProductId();
            if (productIds.length > 0) {
                formtype = "tag";
                arrytext = null;
                setArryText('ctl00_contentHolder_txtProductTag', "");
                DialogShow("商品标签", "producttag", "TagsProduct", "ctl00_contentHolder_btnUpdateProductTags");
            }

        }

        function CollectionProduct(url) {
            DialogFrame("product/" + url, "相关商品");
        }



        function validatorForm() {
            switch (formtype) {
                case "tag":
                    if ($("#ctl00_contentHolder_txtProductTag").val().replace(/\s/g, "") == "") {
                        alert("请选择商品标签");
                        return false;
                    }
                    break;
                case "unsale":
                    setArryText('ctl00_contentHolder_hdPenetrationStatus', $("#ctl00_contentHolder_hdPenetrationStatus").val());
                    break;
                case "instock":
                    setArryText('ctl00_contentHolder_hdPenetrationStatus', $("#ctl00_contentHolder_hdPenetrationStatus").val());
                    break;
            };
            return true;
        }

    </script>
</asp:Content>
