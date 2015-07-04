<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TinDoanhNghiep.aspx.cs" Inherits="CafeF.Redis.Page.TinDoanhNghiep" MasterPageFile="~/MasterPage/SoLieu.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    #container {background-image:none;}
</style>
<div class="tintucsukien">
    <h3 style="padding-bottom: 20px;">TIN TỨC DOANH NGHIỆP NIÊM YẾT</h3>
    <div class="loctin" style="padding-bottom:5px">
        <strong>Lọc tin</strong>:  
        <a href="javascript:void(0);" name="aLink" id="a0" onclick="LoadEventsRelatedNews('<%=__symbol %>',0,1,20);">Tất cả</a> 
        | <a href="javascript:void(0);" name="aLink" id="a2" onclick="LoadEventsRelatedNews('<%=__symbol %>',2,1,20);">Trả cổ tức - Chốt quyền</a> 
        | <a href="javascript:void(0);" name="aLink" id="a1" onclick="LoadEventsRelatedNews('<%=__symbol %>',1,1,20);">Tình hình SXKD & Phân tích khác</a>  
        | <a name="aLink" id="a4" href="javascript:void(0);" onclick="LoadEventsRelatedNews('<%=__symbol %>',4,1,20);">Tăng vốn - Cổ phiếu quỹ</a>  
        | <a name="aLink" id="a5" href="javascript:void(0);" onclick="LoadEventsRelatedNews('<%=__symbol %>',5,1,20);"> GD cổ đông lớn & Cổ đông nội bộ</a>  
        | <a name="aLink" id="a3" href="javascript:void(0);" onclick="LoadEventsRelatedNews('<%=__symbol %>',3,1,20);">Thay đổi nhân sự</a>
    </div>
    <div style="padding-left:20px">
    <div id="divTopEvents">
        <div id="divEvents">
            <asp:Repeater ID="rptTopEvents" runat="server" OnItemDataBound="rptTopEvents_ItemDataBound">
                <HeaderTemplate>
                    <ul class="News_Title_Link"></HeaderTemplate>
                <ItemTemplate>
                    <li style="line-height:20px;"><asp:Literal runat="server" ID="ltrContent"></asp:Literal></li></ItemTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    </div>
    <div class="xemtiep2 clearfix">
        <div style="text-align:center">
           <span id="spanPre"><span style="font-weight: bold; font-family: Tahoma; font-size: 9px; color: rgb(178, 178, 178);">&lt;&lt; Trước</span></span>&nbsp;&nbsp;&nbsp;&nbsp;
           <span id="spanNext"><a href="javascript:LoadNext();" id="aNext" style="font-weight: bold; font-family: Tahoma; font-size: 9px; color: rgb(204, 0, 0);">Sau &gt;&gt;</a></span>
        </div>
    </div>
</div>
<input id="hdConfigID" type="hidden" value="<%= (Request["configid"]??"0") %>" />
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
    LoadEventsRelatedNews(symbol,configid, index,20);
}
function LoadPre()
{
    var index = hdpageindex.value;       
    var configid =  hdconfigid.value;    
    var symbol =     hdsymbol.value;   
    if(index >"1")
    {
        index = eval(eval(index) - 1);
        LoadEventsRelatedNews(symbol,configid, index,20);    
    }        
}
function LoadEventsRelatedNews(symbol, configID, index, size)
{  
    $.ajax({
		type: "GET",
		url: "/Ajax/Events_RelatedNews_New.aspx",
		data: "symbol="+ _symbol + "&configID=" + configID + "&PageIndex=" + index + "&PageSize=" + size + "&Type=2",
		success: function(msg){
		     document.getElementById("divTopEvents").innerHTML=msg;
		}
	});
//	$.ajax({
//		type: "GET",
//		url: "/Ajax/GetTotalPage.aspx",
//		data: "symbol="+ _symbol + "&configID=" + configID + "&PageIndex=" + index + "&PageSize=" + size + "&Type=2",
//		success: function(msg){
//		     GetTotalCount(msg, index);
//		}
	//	});
	GetTotalCount(10000, index);
	GetALink(configID);
    hdpageindex.value = index;
    hdconfigid.value = configID;
    hdsymbol.value = symbol;
    
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
function GetTotalCount(total, ide)
{        
    countTotal = total;
    if(ide>=countTotal)
    {
        spannext.innerHTML = "<span style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#B2B2B2\">Sau >></span>";
    }
    else
    {
        spannext.innerHTML = "<a style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#C00\" id=\"aNext\" href=\"javascript:LoadNext();\">Sau >></a>";
    }
    if(ide==1)
    {
        spanpre.innerHTML = "<span style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#B2B2B2\"><< Trước</span>";
    }
    else
    {
        spanpre.innerHTML = "<a style=\"font-weight:bold; font-family:Tahoma; font-size:9px; color:#C00\" id=\"aPre\" href=\"javascript:LoadPre();\"><< Trước</a>";
    }
}
if(<%= (Request["configid"]??"0") %>!=0){
    LoadEventsRelatedNews(_symbol,<%= (Request["configid"]??"0") %>,1,20);
    GetALink(<%= (Request["configid"]??"0") %>);    
}
</script>
</asp:Content>