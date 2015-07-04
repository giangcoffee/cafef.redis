<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ucCafef_Screener.ascx.cs" Inherits="CafeF.StockScreener.Front.UserControl.ucCafef_Screener" %>
<%@ Register Src="/UserControl/CafefToolbar.ascx" TagName="Toolbar" TagPrefix="uc1" %>
<link href="http://cafef3.vcmedia.vn/styles/screener/screener.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/jquery-ui-1.7.3.slider.custom.min.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/jquery.hoverIntent.minified.js" charset="utf-8"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/jquery.bgiframe.min.js" charset="utf-8"></script>
<!--[if IE]><script src="http://cafef3.vcmedia.vn/js/screener/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/jquery.bt.min.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/screener.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/screener/compatibility.js"></script>
<%--<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/cometd/jquery/jquery.json-2.2.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/cometd/jquery/jquery.cookie.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/cometd/jquery/jquery.cometd.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/cometd/jquery/jquery.cometd-reload.js"></script>
<script src="/Scripts/THEO_DOI_CHUNG_KHOAN.js" type="text/javascript"></script>--%>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/cometd.framework.js"></script>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/THEO_DOI_CHUNG_KHOAN.js"></script>
<style type="text/css">
.paging {float: left;width: 20%; clear:none; text-align: center; padding: 0;}
#searchresults tr td, #searchresults tr th {
    border-bottom-color: #CCCCCC;
    border-bottom-style: solid;
    border-bottom-width: 1px;
    padding-bottom: 3px;
    padding-top: 2px;
}
</style>
<div style="width: 980px;" class="cf_WCBox">
    <div style="text-align:center;"><uc1:Toolbar runat="server" id="ucToolbar" /></div>
    <%--<div class="cf_BoxHeader">
        <div>
        </div>
    </div>--%>
    <div class="cf_BoxContent">
        <div style="width: 980px;" class="cf_WCBox">
            <div class="cf_BoxContent" style="text-align: left; padding: 0pt 20px;">
                <div style="padding-top: 10px;" class="cf_MBTop">
                    <h1 id="title">
                        <b>Bộ lọc chứng khoán</b> <span class="indicator">
                            <img src="http://cafef3.vcmedia.vn/images/screener_loading.gif" alt="loading ..." height="25" width="25" /></span></h1>
                    <span id="lastupdate"><a href="/ScreenerUserGuide.aspx" target="_blank">Hướng dẫn sử dụng nhanh bộ lọc</a> - Dữ liệu giao dịch thị trường cập nhật đến ngày
                        <asp:Literal runat="server" ID="ltrLastUpdate" /></span>
                        
                    <div style="clear: both;">
                    </div>
                </div>
                <table style="background: none repeat scroll 0% 0% rgb(255, 255, 255); padding-top: 0px;" width="100%" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td class="CafeF_Padding10" style="font-size: 13px; line-height: 120%;" valign="top">
                                <div id="filter">
                                    <div id="cate-filter" class="hdg">
                                        <input type="hidden" value="1" id="page" />
                                        <input type="hidden" value="" id="orderby" />
                                        <input type="hidden" value="" id="orderdir" />
                                        <input type="hidden" value="" id="defaultfilter" />
                                        <input type="hidden" id="pagefilter" />
                                        <input type="hidden" id="charfilter" />
                                        <input type="hidden" id="char" />
                                        <input type="hidden" id="cache" value="true" />
                                        <span>Sàn giao dịch : </span>
                                        <asp:DropDownList runat="server" ID="lsTradeCenter" CssClass="centerfilter">
                                            <asp:ListItem Selected="true" Text="-- HSX & HNX --" Value="-1" />
                                            <asp:ListItem Text="HSX" Value="1" />
                                            <asp:ListItem Text="HNX" Value="2" />
                                            <asp:ListItem Text="OTC" Value="8" />
                                            <asp:ListItem Text="UpCom" Value="9" />
                                        </asp:DropDownList>
                                        <span>Ngành nghề : </span>
                                        <asp:DropDownList runat="server" ID="lsCategory" CssClass="catefilter">
                                            <asp:ListItem Selected="true" Text="-- Tất cả --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div id="criteria_rows">
                                        <asp:Repeater runat="server" ID="repFilter">
                                            <HeaderTemplate>
                                                <table border="0" cellpadding="0" cellspacing="2" id="tblcriterion">
                                                    <thead>
                                                        <tr>
                                                            <th width="200">
                                                                Tiêu chí</th>
                                                            <th>
                                                                Min</th>
                                                            <th align="center">
                                                                Phân bố theo số lượng</th>
                                                            <th>
                                                                Max</th>
                                                            <th>
                                                                &nbsp;</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="criterion-row" criterion="<%#Eval("CriterionId") %>">
                                                    <td>
                                                        <%#Eval("CriterionName") %>
                                                        <img src="http://cafef3.vcmedia.vn/images/screener_icon_info.gif" alt="?" class="cinfo" longdesc="<%#Eval("CriterionId") %>" /></td>
                                                    <td>
                                                        <input type="text" id="min_<%#Eval("CriterionId") %>" value="<%# FormatNumber(Eval("MinValue")) %>" class="txtmin" real="<%# Eval("MinValue") %>" base="<%# Eval("MinValue") %>" /></td>
                                                    <td width="202">
                                                        <div class="sliderback_MarketCap sliderback displaynone" style="display: block;">
                                                            <table style="padding: 0pt;" cellpadding="0" cellspacing="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <div class="slider_MarketCap_left slider" style="left: 0px; top: 0px;" rel="<%#Eval("CriterionId") %>">
                                                                                <div class="sliderimage_MarketCap_left sliderimage_default">
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                        <td valign="top" class="histogram_td">
                                                                            <div class="histogram_MarketCap displayinline">
                                                                                <table cellspacing="0" cellpadding="0" border="0" class="histogram_MarketCap_table">
                                                                                    <tbody>
                                                                                        <asp:Repeater runat="server" ID="repSlider" DataSource='<%# Eval("DistributionValue") %>'>
                                                                                            <HeaderTemplate>
                                                                                                <tr>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <td value="<%# FormatNumber(Eval("Value")) %>" class="step_<%# Eval("Number") %>" real="<%#Eval("Value") %>">
                                                                                                    <div style="height: <%# Eval("Ratio") %>px;" class="valuecol">
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="blankcol">
                                                                                                    </div>
                                                                                                </td>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                </tr></FooterTemplate>
                                                                                        </asp:Repeater>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                        <td valign="middle">
                                                                            <div class="slider_MarketCap_right slider" style="left: 202px; top: 0px;" rel="<%#Eval("CriterionId") %>">
                                                                                <div class="sliderimage_MarketCap_right sliderimage_default">
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <input type="text" id="max_<%# Eval("CriterionId") %>" value="<%# FormatNumber(Eval("MaxValue")) %>" class="txtmax" real="<%# Eval("MaxValue") %>" base="<%# Eval("MaxValue") %>" /></td>
                                                    <td valign="middle">
                                                        <a href="#" class="delfilter" rel="<%# Eval("CriterionId") %>">
                                                            <img src="http://cafef3.vcmedia.vn/images/screener_delete.gif" alt="X" /></a><a href="#" class="resetfilter" rel="<%# Eval("CriterionId") %>"><img src="http://cafef3.vcmedia.vn/images/screener_reset.gif" alt="Reset" /></a></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody> </table></FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div id="filtermanagecompact">
                                        <div>
                                            <img align="bottom" src="http://cafef3.vcmedia.vn/images/screener_cleardot.gif" class="plus openbox" alt="+" />
                                            <a class="openbox">Thêm chỉ tiêu</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" class="reset">Sử dụng bộ chỉ tiêu mặc định</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" class="resetfilter" rel="all">Lấy giá trị mặc định</a> <span class="indicator">&nbsp;&nbsp;<img src="http://cafef3.vcmedia.vn/images/screener_loading_text.gif" alt="loading ..." height="8" /></span>
                                        </div>
                                    </div>
                                    <div id="filtermanagebox" style="display: none; width: 700px;">
                                        <div class="searchtabsheader">
                                            <img align="bottom" src="http://cafef3.vcmedia.vn/images/screener_cleardot.gif" class="minus openbox" alt="-" />
                                            <a class="openbox">Đóng</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a class="reset">Sử dụng bộ chỉ tiêu mặc định</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" class="resetfilter" rel="all">Lấy giá trị mặc định</a> <span class="indicator">&nbsp;&nbsp;<img src="http://cafef3.vcmedia.vn/images/screener_loading_text.gif" alt="loading ..." height="8" /></span>
                                        </div>
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="12%" valign="top">
                                                        <asp:Repeater runat="server" ID="repCategory">
                                                            <HeaderTemplate>
                                                                <table cellspacing="0" cellpadding="0" class="searchtabs">
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="white-space: nowrap;" class="inactive">
                                                                        <a rel="<%# Eval("CategoryId") %>">
                                                                            <%# Eval("CategoryName") %>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr>
                                                                    <td class="inactive last">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                </tbody> </table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td width="30%" valign="top">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr valign="top" width="100%">
                                                                    <td id="criterionlist">
                                                                        <asp:Repeater runat="server" ID="repCriterion">
                                                                            <ItemTemplate>
                                                                                <div style="display: none;" id="category_<%# Eval("CategoryId") %>">
                                                                                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Criteria") %>'>
                                                                                        <ItemTemplate>
                                                                                            <div class="criteriadiv" rel="<%# Eval("CriterionId") %>">
                                                                                                <a rel="<%# Eval("CriterionId") %>" class="inactivelink" code="<%# Eval("CriterionCode") %>">
                                                                                                    <%# Eval("CriterionName") %>
                                                                                                </a>
                                                                                            </div>
                                                                                            <div id="criteriondesc_<%# Eval("CriterionId") %>" style="display: none;">
                                                                                                <%# Eval("CriterionDesc") %>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td width="*" valign="top" style="padding: 0px 10px;" id="criteriondesc">
                                                        <div style="display: none;">
                                                            <div id="criteria_title">
                                                                <h1>
                                                                </h1>
                                                            </div>
                                                            <div id="criteria_definition" valign="top">
                                                            </div>
                                                            <div id="criteria_button" valign="top">
                                                                <button rel="">
                                                                    Thêm chỉ tiêu</button></div>
                                                            <div class="check" style="display: none;" id="criteria_added">
                                                                Đã thêm</div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div id="result">
                                    <div class="hdg" id="searchresultssummary">
                                        Bạn đang xem &nbsp;<b>1</b>&nbsp;-&nbsp;<b>1</b>&nbsp;công ty trên tổng số&nbsp;<b>1</b> công ty.
                                    </div>
                                    <div>
                                        <div class="listpage character">
                                            <span class="list"></span><a href="#" title="">Tất cả</a></div>
                                        <div class="listpage paging">
                                            <%--<a href="#" class="export" rel="xls">Xuất ra excel</a>  --%>Trang : <span class="list"><a rel="1" href="#">1</a><a rel="2" class="selected" href="#">2</a><span> . . . </span><a rel="100" href="#">100</a></span><span class="space">&nbsp;</span></div>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                    <div id="searchresults">
                                        <table width="100%" border="0" cellpadding="4" cellspacing="0">
                                            <thead>
                                                <tr>
                                                    <th width="10" code="stt">
                                                        STT</th>
                                                    <th width="20%" code="name">
                                                        Tên công ty</th>
                                                    <th width="40" code="code">
                                                        <a href="#" rel="-2" class="orderlink">Mã</a></th>
                                                    <th width="40" code="tc">
                                                        <a href="#" rel="-3" class="orderlink">Sàn</a></th>
                                                    <asp:Repeater runat="server" ID="repField">
                                                        <ItemTemplate>
                                                            <th width="70" code="<%# Eval("CriterionCode")%>" cid="<%# Eval("CriterionId") %>" style="text-align: right;">
                                                                <a href="#" rel="<%# Eval("CriterionId") %>" class="orderlink">
                                                                    <%# Eval("CriterionName") %>
                                                                </a>
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <th width="*" code="other">
                                                        &nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div>
                                        &nbsp;</div>
                                    <div>
                                        <div class="listpage character">
                                            <span class="list"></span><a href="#" title="">Tất cả</a></div>
                                        <div class="listpage paging">
                                            Trang : <span class="list"><a rel="1" href="#">1</a><a rel="2" class="selected" href="#">2</a><span> . . . </span><a rel="100" href="#">100</a></span><span class="space">&nbsp;</span></div>
                                    </div>
                                    <div style="clear: both">
                                    </div>
                                    <div id="searchnavigator" class="ftr">
                                        <div style="float: right;">
                                            Số lượng mã / trang
                                            <asp:DropDownList runat="server" ID="lsPageSize" CssClass="pagesize">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="20" Value="20" Selected="true" />
                                                <asp:ListItem Text="30" Value="30" />
                                            </asp:DropDownList>
                                        </div>
                                        Bạn đang xem &nbsp;<b>1</b>&nbsp;-&nbsp;<b>1</b>&nbsp; trên tổng số&nbsp;<b>1</b> công ty.
                                    </div>
                                </div>
                                <div style="display: none;">
                                    <asp:Label runat="server" ID="lblError" /></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <%--<div class="cf_BoxFooter">
        <div>
        </div>
    </div>--%>
</div>
