<%@ Control Language="C#" %>
<%@ Import Namespace="ExhibFlat.Core" %>
<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.Common.Controls" Assembly="ExhibFlat.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="ExhibFlat.UI.Flat.Tags" Assembly="ExhibFlat.UI.Flat.Tags" %>
<li>
    <div class="proWp">
        <div class="proItem">
            <Hi:ProductDetailsLink ID="ProductDetailsLink2" runat="server" ProductName='<%# Eval("ProductName") %>' ProductId='<%# Eval("ProductId") %>' ImageLink="true" title='<%# Eval("ProductName") %>'>
            <Hi:ListImage ID="Common_ProductThumbnail1" runat="server" DataField="ThumbnailUrl220" CustomToolTip="ProductName" /></Hi:ProductDetailsLink>
            <div class="proPrice">
                <strong>￥<Hi:FormatedMoneyLabel ID="FormatedMoneyLabel2" Money='<%# Eval("SalePrice")%>' runat="server"></Hi:FormatedMoneyLabel></strong>
                <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&amp;uin=210662846&amp;site=qq&amp;menu=yes" style="display: inline-block;">
                    <em class="details-hint" style="display: none;">申请低价
                    </em>
                </a>
                
            </div>
            <div class="proTitle">
                <Hi:ProductDetailsLink ID="ProductDetailsLink1" runat="server" ProductName='<%# Eval("ProductName") %>' ProductId='<%# Eval("ProductId") %>'></Hi:ProductDetailsLink>
            </div>
        </div>
        
        <div class="dealN">
            <span>出售<em><%# Eval("SaleCounts") %></em>件</span>
            <Hi:ProductDetailsLink ID="ProductDetailsLink3" runat="server" ProductName='<%# "立即采购＞" %>' ProductId='<%# Eval("ProductId") %>'></Hi:ProductDetailsLink>
        </div>
        <div class="transTop transTime">
            <span><%# Eval("GroundingDate") %><p>新品</p>
            </span>
        </div>
    </div>
</li>
