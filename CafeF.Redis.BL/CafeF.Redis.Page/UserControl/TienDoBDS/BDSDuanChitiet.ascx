<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BDSDuanChitiet.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.TienDoBDS.BDSDuanChitiet" %>
<%@ Register Src="BDSImages.ascx" TagName="BDSImages" TagPrefix="uc1" %>
<style type="text/css">
    .tiendobdschitiet
    {
    }
    .tiendobdschitiet li
    {
       -moz-background-clip:border;
-moz-background-inline-policy:continuous;
-moz-background-origin:padding;
background:transparent url(/images/sprite_ico_tiendo.png) no-repeat scroll;

padding:5px 10px 4px 15px;
    }
    .tiendobdschitiet li .l
    {
    	list-style:square;
        float: left;
    }
    .tiendobdschitiet li .r
    {
        float: right;
        text-align:left;
        width:150px;
    }
   
.tttiendo {
  font-size: .9em;
  font-style:italic;
  font-weight: normal;
  text-decoration:none;
  text-align: left;
}
.tttiendo span { display: none;}
/*background:; ie hack, something must be changed in a for ie to execute it*/
.tttiendo:link {color : #003366;
text-decoration : none;
font-family : tahoma, verdana, arial;
font-size : 11px;}
.tttiendo:hover {   position:relative;text-decoration:none; z-index:24; color: #aaaaff; background:transparent;color : #003c5e;
text-decoration : none;
font-family : tahoma, verdana, arial;
font-size : 11px;}
.tttiendo:hover span.tooltip {
  color: #111;
  display:block;
  position:absolute;
  top:0px; left:-15px;
  padding: 15px 0 0 0;
  width:459px;
  text-align: left;
  text-decoration:none;
  z-index: -1;
  color : #343434;
font-family : tahoma, verdana, arial;
font-size : 11px;
}
/*.tt.rightEnd:hover span.tooltip {
	right: -20px; left: auto;
}*/

.tttiendo:hover span.top {
  display: block;
  padding: 30px 8px 0;
  background: url(/Style/images/bubble1.png) no-repeat top;
}
/*.tt.rightEnd:hover span.top {
	background: url(images/bubble-right.png) no-repeat top;
}*/
.tttiendo:hover span.middle { /* different middle bg for stretch */
  display: block;
  padding: 0px 12px 10px 8px;
  font-style:normal; 
  text-align:justify;
  background: url(/Style/images/bubble_filler1.png) repeat bottom;
}
.tttiendo:hover span.bottom {
	display: block;
	padding:3px 8px 10px;
	color:#343434;
	font-size:10px;
	font-style:italic;
	font-weight:normal;
  background: url(/Style/images/bubble1.png) no-repeat bottom;
}



.tttiendo div 
{
	display: none;
}
/*background:; ie hack, something must be changed in a for ie to execute it*/

.tttiendo:hover {   position:relative;text-decoration:none; z-index:24; color: #aaaaff; background:transparent;color : #003c5e;
text-decoration : none;
font-family : tahoma, verdana, arial;
font-size : 11px;}
.tttiendo:hover div.tooltip {
  color: #111;
  display:block;
  position:absolute;
  top:0px; left:-10px;
  padding: 15px 0 0 0;
  width:300px;
  text-align: left;
  text-decoration:none;
  z-index: -1;
  color : #343434;
font-family : tahoma, verdana, arial;
font-size : 11px;
}
/*.tt.rightEnd:hover div.tooltip {
	right: -20px; left: auto;
}*/

.tttiendo:hover div.top {
  display: block;
  padding: 30px 8px 0;
  background: url(/Style/images/bubble1.png) no-repeat top;
}
/*.tt.rightEnd:hover div.top {
	background: url(images/bubble-right.png) no-repeat top;
}*/
.tttiendo:hover div.middle { /* different middle bg for stretch */
  display: block;
  padding: 0px 12px 10px 8px;
  font-style:normal; 
  text-align:justify;
  font-size:11px;
  background: url(/Style/images/bubble_filler.png) repeat bottom;
}
.tttiendo:hover div.bottom {
	display: block;
	padding:3px 8px 10px;
	color:#343434;
	font-size:10px;
	font-style:italic;
	font-weight:normal;
  background: url(/Style/images/bubble1.png) no-repeat bottom;
}


</style>
<div class="tiendobdschitiet">
    <asp:Repeater EnableViewState="false" ID="rpData" runat="server" 
        onitemdatabound="rpData_ItemDataBound" onitemcreated="rpData_ItemCreated">
        <ItemTemplate>
            <h3 class="cattitle noborder">
                <a id="aName" runat="server"></a>
                <asp:Literal ID="ltName" runat="server"></asp:Literal></h3>
            <table>
                <tr>
                    <td style="margin-right: 10px; vertical-align:top; width:290px;">
                        <uc1:BDSImages ID="BDSImages1" runat="server" />
                         <div class="xemtiep clearfix" style="padding:0px; padding-top:15px; cursor:pointer;">
  	                         <a id="aAll" runat="server" target="_blank" >Xem tất cả</a>
                         </div>
                    </td>
                    <td valign=top>
                        <ul>
                            <li class="clearfix">
                                <div class="l">
                                    Tổng vốn đầu tư:</div>
                                <div class="r">
                                    <asp:Literal ID="ltTongVon" runat="server"></asp:Literal></div>
                            </li>
                            <li class="clearfix">
                                <div class="l">
                                    Tỷ lệ góp vốn:</div>
                                <div class="r">
                                    <asp:Literal ID="ltTyLeGhopVon" runat="server"></asp:Literal></div>
                            </li>
                               <asp:Repeater EnableViewState="false" ID="rpLN" runat="server">
                            <ItemTemplate>
                            <li class="clearfix">
                                <div class="l">
                                   <asp:Literal ID="ltHeader" runat="server" Text="Doanh thu/Lợi nhuận dự kiến:"></asp:Literal></div>
                                <div class="r">
                                    <asp:Literal ID="ltLoiNhuanDoanhThu" runat="server" Text='<%# String.Format("{0:#,##0}",Eval("LoiNhuanDoanhThu"))%>'></asp:Literal> tỷ VND</div>
                            </li>
                             </ItemTemplate>
                            </asp:Repeater>
                            <li class="clearfix">
                                <div class="l">
                                    Hình thức kinh doanh:</div>
                                <div class="r">
                                    <asp:Literal ID="ltHinhThucKinhDoanh" runat="server"></asp:Literal></div>
                            </li>
                            <li class="clearfix">
                                <div class="l">
                                    Địa điểm:</div>
                                <div class="r">
                                    <asp:Literal ID="ltDiaDiem" runat="server"></asp:Literal></div>
                            </li>
                            <asp:Repeater EnableViewState="false" ID="rpDT" runat="server">
                            <ItemTemplate>
                                <li class="clearfix">
                                    <div class="l">
                                        <asp:Literal ID="ltHeader" runat="server" Text="Diện tích:"></asp:Literal></div>
                                    <div class="r">
                                        <asp:Literal ID="ltDienTich" runat="server" Text='<%# String.Format("{0:#,##0}",Eval("DienTich")) %> '></asp:Literal> m2</div>
                                </li>
                            </ItemTemplate>
                            </asp:Repeater>
                            <li class="clearfix">
                                <div class="l">
                                    Tiến độ hiện tại:</div>
                                <br />
                                    <asp:Literal ID="ltGhiChu" runat="server"></asp:Literal>
                            </li>
                            <li class="clearfix">
                                <div class="l">
                                    Mô tả khác:</div>
                                <br />
                                    <asp:Literal ID="ltMota" runat="server"></asp:Literal>
                            </li>
                        </ul>
                    </td>
                </tr>
                <tr><td colspan=2 style="border-bottom:1px solid #EEEEEE;"></td></tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    <div class="paging" id="paging" runat="server"></div>
</div>
