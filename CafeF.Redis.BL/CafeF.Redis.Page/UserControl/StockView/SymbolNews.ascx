<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SymbolNews.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.SymbolNews" %>
<div class="tintucsukien" style="padding-top:10px">
    <div style="float:right;"><a href="/tin-doanh-nghiep/<%=__symbol%>/Event.chn" id="aViewMoreLink">Xem tất cả</a></div>
    <h2 class="cattitle noborder">Tin tức - Sự kiện</h2>
    <div id="divTopEvents">
        <asp:Repeater ID="rptTopEvents" runat="server" OnItemDataBound="rptTopEvents_ItemDataBound">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li style="line-height:20px"><asp:Literal runat="server" ID="ltrContent"></asp:Literal></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>        
    </div>
    <div class="xemtiep2 clearfix">
  	        <%--<div class="xemtiep"><a href="/tin-doanh-nghiep/<%=__symbol%>/Event.chn" id="aViewMoreLink">Xem tất cả</a></div>--%>
            <div class="paging">
               <span id="spanPre"  style="font-weight:bold; font-family:Tahoma; font-size:9px; color:#B2B2B2">&lt;&lt; Trước</span>&nbsp;&nbsp;&nbsp;&nbsp;
               <span id="spanNext"><a id="aNext" style="font-weight:bold; font-family:Tahoma; font-size:9px; color:#C00" href="javascript:LoadNext();">Sau &gt;&gt;</a></span>
            </div>
        </div>
    <div class="loctin">
        <strong>Lọc tin</strong>:  
        <a href="javascript:void(0);" name="aLink" id="a0" onclick="LoadEventsRelatedNews('<%=__symbol %>',0,1,6);">Tất cả</a> 
        | <a href="javascript:void(0);" name="aLink" id="a2" onclick="LoadEventsRelatedNews('<%=__symbol %>',2,1,6);">Trả cổ tức - Chốt quyền</a> 
        | <a href="javascript:void(0);" name="aLink" id="a1" onclick="LoadEventsRelatedNews('<%=__symbol %>',1,1,6);">Tình hình SXKD & Phân tích khác</a>  
        | <a name="aLink" id="a4" href="javascript:void(0);" onclick="LoadEventsRelatedNews('<%=__symbol %>',4,1,6);">Tăng vốn - Cổ phiếu quỹ</a>  
        | <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a name="aLink" id="a5" href="javascript:void(0);" onclick="LoadEventsRelatedNews('<%=__symbol %>',5,1,6);"> GD cổ đông lớn & Cổ đông nội bộ</a>  
        | <a name="aLink" id="a3" href="javascript:void(0);" onclick="LoadEventsRelatedNews('<%=__symbol %>',3,1,6);">Thay đổi nhân sự</a>
    </div>
</div>
<input id="hdConfigID" type="hidden" value="0" />
<input id="hdPageIndex" type="hidden" value="1" />
<input id="hdSymbol" type="hidden" value="<% = __symbol %>" />

<script type="text/javascript">
var _symbol = '<% = __symbol %>';
var hdconfigid = document.getElementById('hdConfigID');
var hdpageindex = document.getElementById('hdPageIndex');
var hdsymbol = document.getElementById('hdSymbol');
var anext = document.getElementById('aNext');
var apre = document.getElementById('aPre');     
var spannext = document.getElementById('spanNext');
var spanpre = document.getElementById('spanPre');  
var countTotal; 
function LoadNext()
{
    var index = hdpageindex.value;       
    var configid =  hdconfigid.value;    
    var symbol =     hdsymbol.value;   
    index = eval(eval(index) + 1);      
    LoadEventsRelatedNews(symbol,configid, index,6);
}
function LoadPre()
{
    var index = hdpageindex.value;       
    var configid =  hdconfigid.value;    
    var symbol =     hdsymbol.value;   
    if(index >"1")
    {
        index = eval(eval(index) - 1);
        LoadEventsRelatedNews(symbol,configid, index,6);    
    }        
}
function LoadEventsRelatedNews(symbol, configID, index, size)
{  
    $.ajax({
		type: "GET",
		url: "/Ajax/Events_RelatedNews_New.aspx",
		data: "symbol="+ _symbol + "&configID=" + configID + "&PageIndex=" + index + "&PageSize=" + size + "&Type=1",
		success: function(msg){
		     document.getElementById("divTopEvents").innerHTML=msg;
		     if (msg=="")
		        hdpageindex.value ="1";
		}
	});
//	$.ajax({
//		type: "GET",
//		url: "/Ajax/GetTotalPage.aspx",
//		data: "symbol="+ _symbol + "&configID=" + configID + "&PageIndex=" + index + "&PageSize=" + size + "&Type=1",
//		success: function(msg){
//		     GetTotalCount(msg, index);
//		}
//	});
	GetTotalCount(index);
	GetALink(configID);
    hdpageindex.value = index;
    hdconfigid.value = configID;
    hdsymbol.value = symbol;
    var alink = document.getElementById('aViewMoreLink');
    if(configID!="0")
    {
        alink.href ='/Tin-doanh-nghiep/'+ symbol +'/'+ configID +'/Event.chn'
    }
    else
    {
        alink.href ='/Tin-doanh-nghiep/'+ symbol +'/Event.chn'
    }
} 
function GetALink(id)
{
    var aID = document.getElementById('a' + id);
    var aLink = document.getElementsByName('aLink');
    
    for(i=0; i< aLink.length; i++)
    {
        aLink[i].style.color="";
    }
    aID.style.color = "#CC0001";
}
function GetTotalCount( ide)
{        
//    countTotal = total;
//    if(ide>=countTotal)
//    {
//        spannext.innerHTML = "<span style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#B2B2B2\">Sau >></span>";
//    }
//    else
//    {
//        spannext.innerHTML = "<a style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#C00\" id=\"aNext\" href=\"javascript:LoadNext();\">Sau >></a>";
//    }
    if(ide==1)
    {
        spanpre.innerHTML = "<span style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#B2B2B2\"><< Trước</span>";
    }
    else
    {
        spanpre.innerHTML = "<a style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#C00\" id=\"aPre\" href=\"javascript:LoadPre();\"><< Trước</a>";
    }
}
//LoadEventsRelatedNews(_symbol,0,1,6);
//GetALink(0);
</script>
                        