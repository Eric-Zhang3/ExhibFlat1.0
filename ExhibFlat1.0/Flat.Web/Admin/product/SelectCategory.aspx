<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SelectCategory.aspx.cs" Inherits="Flat.Web.Admin.SelectCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <link id="cssLink" rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
    <style type="text/css">
        .results_pos{position:relative;overflow:hidden;background:red;float:left;width:891px;background:#e5f0ff;border:1px solid #c7deff;border-left:0;height:298px;}
        .results_ol{position:absolute;display:block;overflow:hidden;width:2250px;clear:both;left:0px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
<div class="optiongroup mainwidth">
        <ul>
            <li class="menucurrent">
                <a><span>选择产品类目</span></a></li>
        </ul>
</div>
    
<div class="dataarea mainwidth">
<div class="advanceSearchArea clearfix"></div>
  <div class="search_results">

  </div>
    <a></a>
      
    <div class="results">
        <div class="results_main">
            <div class="results_left">
                <label>
                    <input type="button" name="button2" id="button2" value="" class="search_left" />
                </label>
            </div>
            <div class="results_pos">
                <ol class="results_ol">
                </ol>
            </div>
            <div class="results_right">
                <label>
                    <input type="button" name="button2" id="button2" value="" class="search_right" />
                </label>
            </div>
        </div>
    </div>
    <div class="results_img"></div>
    <div class="results_bottom">
        <span class="spanE">你当前选择的是：</span>
        <span id="fullName"></span>
    </div>
 </div>   
 <div class="databottom"></div>
	<div class="bntto">
	  <input type="button" name="button2" id="btnNext" value="下一步" class="submit_DAqueding"/>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="/Admin/script/publish.helper.js"></script>
</asp:Content>
