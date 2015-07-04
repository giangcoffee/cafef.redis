<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnalyzeReportList.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Report.AnalyzeReportList" %>
<style type="text/css">
    .phantichle {margin-top:4px;}
    a.Black_Bold_Link:link, a.Black_Bold_Link:active, a.Black_Bold_Link:visited {color:#000000; font-weight:bold;    text-decoration:none;}
    a.Black_Bold_Link:hover { color:#CC0001;font-weight:bold; text-decoration:none;}
    #container {background-image:none;}
</style>
<div class="ListCateDiv1" align="center" >
        <%-- <div style="overflow: hidden">
            <div class="floatLeft">
                <img alt="" src="http://cafef3.vcmedia.vn/images/images/conner_top_left.gif" /></div>
            <div style="float: right;">
                <img alt="" src="http://cafef3.vcmedia.vn/images/images/conner_top_right.gif" /></div>
        </div>--%>
        <div align="left" class="ListCateDiv2" style="height: 15px;padding-left:5px; margin-top:5px;">
            <a href="/phan-tich-bao-cao.chn" class="Black_Bold_Link" style="font-family:Arial; font-size:16px;">BÁO CÁO PHÂN TÍCH</a>
        </div>    
        <div>
            <table cellspacing="4" width="98%" >
                <tr>
                    <td colspan="4" height="5px"></td>
                </tr>
                <tr>
                    <td width="30%" style="text-align:left;font-weight:bold;">Mã CK<br />
                        <asp:TextBox ID="txtSymbol" runat="server" CssClass="phantichle" Width="150px"></asp:TextBox>
                        <%--<asp:DropDownList ID="cboStockRelation" runat="server" CssClass="phantichle" Width="150px"></asp:DropDownList>--%>
                         </td>
                    <td width="30%" style="text-align:left;font-weight:bold;">Nguồn báo cáo<br />
                        <asp:DropDownList ID="cboSource" runat="server" CssClass="phantichle" Width="180px"></asp:DropDownList> </td>
                    <td width="30%" style="text-align:left;font-weight:bold;">Ngành<br />
                        <asp:DropDownList ID="cboCategory" runat="server" CssClass="phantichle" Width="180px"></asp:DropDownList> </td>
                    <td width="10%" valign="bottom">&nbsp;<asp:ImageButton  runat="server" ID="ibtnSearch" ImageUrl="http://cafef3.vcmedia.vn/images/images/timkiem.gif" OnClick="ibtnSearch_Click"  /><br /></td>    
                </tr> 
            </table>
            <table cellspacing="0" width="98%" border="0" runat="server" id="tblDetail" style="margin-bottom:10px;margin-top:10px;">
                <tr>
                    <td style="border: solid 1px #e6e6e6;padding:0px 0px 0px 10px;">
                        <table  cellspacing="10" width="99%" border="0" style="text-align:left;">
                            <tr>
                                <td colspan="2" style="text-align:left;font-size:20px;font-weight:bold;padding-bottom:10px;"><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></td>
                            </tr>
                             <tr>
                                <td style="font-weight:bold;width:20%;">Nguồn báo cáo:</td>
                                <td><asp:Literal ID="ltrSource" runat="server"></asp:Literal>&nbsp;</td>
                            </tr>
                            <tr id="tblTrCompany" runat="server">
                                <td style="font-weight:bold;vertical-align:top;">Doanh nghiệp:</td>
                                <td><asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td style="font-weight:bold;">Chi tiết:</td>
                                <td><asp:Literal ID="ltrDetail" runat="server"></asp:Literal>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top:10px; line-height: 18px;" id="reportdetail"><asp:Literal ID="ltrDes" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top:10px;text-align:right;">
                                <div style="overflow:hidden;float:right;">
                                    <div style="float:left;">
                                        <asp:Literal ID="ltrDownload" runat="server"></asp:Literal>
                                    </div>
                                    <div style="float:left; overflow: hidden; background-color: White;height: 30px;padding-top: 5px;padding-left:10px;">
                                        <div id='menuShow_' style="float:left;">
                                                        <img style="cursor: pointer;" border="0" width="25" onclick="ShareToWebShare(1)" src="http://cafef3.vcmedia.vn/images/I_icons/facebook_48.png" alt='' title='' />
                                                        <img style="cursor: pointer;" border="0" width="25" onclick="ShareToWebShare(2)" src="http://cafef3.vcmedia.vn/images/I_icons/twitter_48.png" alt='' title='' />
                                        </div>
                                    </div>
                                </div>
                                
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:left;">
                                    <div style="padding-top: 10px; font-size: 11px;" class="Note">
<b><font color="red">Khuyến cáo</font></b>: Các báo cáo phân tích được CafeF thu thập từ những nguồn tin cậy; tuy nhiên tất cả các quan điểm, luận điểm, khuyến nghị mua/bán trong báo cáo là do tổ chức/cá nhân thực hiện báo cáo đưa ra, hoàn toàn không thể hiện ý chí của CafeF; CafeF không chịu trách nhiệm đối với bất kỳ khoản thua lỗ từ đầu tư nào do sử dụng các báo cáo phân tích này.</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" border="0" width="98%">
                <tr>
                    <td style="text-align:left;">
                         <span style="color:#919191;font-size:11px;font-style:italic;">(*) Báo cáo phân tích, thông tin chia sẻ xin vui lòng gửi tới </span><span class="footerInformDivBoldRed">info@cafef.vn</span>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" width="98%" style="border-top: solid 1px #e6e6e6;"
                        id="tblGridData">
                        
                        <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                            font-weight: bold; color: #004276; background-color: #FFF" >
                            <td class="Header_DateItem" style="width:80px;color:#003466;">
                                Ngày</td>
                            <td class="Header_Price1" style="color:#003466;">
                                Tiêu đề</td>

                           <td class="Header_Price1"  style="width:90px;color:#003466;">
                                Nguồn</td>

                            <td class="Header_ChangeItem"  style="width:90px;color:#003466;">
                                Mã CK <br />liên quan</td>
                            <td class="Header_Price1"  style="width:60px;color:#003466;">
                                Tải về
                                
                                </td>
                        </tr>     
            <asp:Repeater runat="server" ID="rptData">                   
                <ItemTemplate>
                    <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #FFF;"
                        runat="server" id="itemTR">
                        <td class="Item_DateItem" style="padding-bottom:7px;padding-top:7px;">
                           <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateDeploy")).ToString("dd/MM/yyyy")%> 
                        </td>
                        <td class="Item_Price1" style="text-align:left;padding-left:5px;font-weight:bold">
                            <a href="/phan-tich-bao-cao<%#DataBinder.Eval(Container.DataItem, "ID")%>.chn"> <%#DataBinder.Eval(Container.DataItem, "Title")%></a>
                        </td>
                        <td class="Item_Price1" style="text-align:center;">
                            <%#DataBinder.Eval(Container.DataItem, "ResourceCode")%>&nbsp;
                        </td>
                        <td class="Item_Price1" style="text-align:center;">
                            <%#DataBinder.Eval(Container.DataItem, "Symbol")%>&nbsp;
                        </td>
                        <td class="Item_Price1" style="text-align:center;">
                            <%# GetDownload(DataBinder.Eval(Container.DataItem, "file"), DataBinder.Eval(Container.DataItem, "ID"))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="font-family: Arial; font-weight: normal; background-color: #f2f2f2"
                        runat="server" id="altitemTR">
                        <td class="Item_DateItem" style="padding-bottom:7px;padding-top:7px;">
                            <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateDeploy")).ToString("dd/MM/yyyy")%>
                        </td>
                        <td class="Item_Price1" style="text-align:left;padding-left:5px;font-weight:bold" >
                            <a href="/phan-tich-bao-cao<%#DataBinder.Eval(Container.DataItem, "ID")%>.chn"><%#DataBinder.Eval(Container.DataItem, "Title")%></a> 
                        </td>
                        <td class="Item_Price1" style="text-align:center;" >
                            <%#DataBinder.Eval(Container.DataItem, "ResourceCode")%>&nbsp;
                        </td>
                        <td class="Item_Price1" style="text-align:center;">
                            <%#DataBinder.Eval(Container.DataItem, "Symbol")%>&nbsp;
                        </td>
                        <td class="Item_Price1" style="text-align:center;">
                            <%# GetDownload(DataBinder.Eval(Container.DataItem, "file"), DataBinder.Eval(Container.DataItem, "ID"))%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
            <div style="width: 500px; padding-right: 20px; border-bottom: solid 0px #dadada;
                padding-bottom: 10px; text-align: right; float: right">
                <div style="float: right;">
                    <asp:Repeater  ID="rptPage" runat="server" OnItemDataBound="rptPage_ItemDataBound" OnItemCommand="rptPage_ItemCommand">
                        <HeaderTemplate>
                            <table class="CafeF_Paging" border="0" cellpadding="3" cellspacing="3">
                                <tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td align="center" style="width: 20px;">
                                <asp:Button runat="server" ID="btnpage"  CommandName="paging" />
                            </td>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tr> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
         </div>
         
     </div>
     <div class="ListCateDiv1" style="height:30px;">&nbsp;</div>
<asp:HiddenField runat="server" ID="hdPageIndex" />
<script type="text/javascript">
function ShareToWebShare(type)
{
    var url='';
    switch(type)
    {
    case 1:
      url = "http://www.facebook.com/sharer.php?u="+window.location.href + "&link_title=<%=ltrTitle.Text %>";
      break;
    case 2:
      url = "http://twitthis.com/twit?url="+window.location.href + "&link_title=<%=ltrTitle.Text %>" ;
      break;
    case 3:
      url = "http://linkhay.com/submitExt?link_url="+window.location.href+"&link_title=<%=ltrTitle.Text %>";
      break;
    }
    var newWindow = window.open(url,'','_blank,resizable=yes');
    newWindow.focus();
    return false;
}


var TextBox_KeywordId = '<%=txtSymbol.ClientID%>';
    $().ready(function() {
        $('#' + TextBox_KeywordId).autocomplete(oc, {
            minChars: 1,
            delay: 10,
            width: 400,
            matchContains: true,
            autoFill: false,
            Portfolio:false,
            LSK:true,
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
                //return row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c;
                //return row.m;
            }
        });
    });
  function DownloadBaoCao(id,file,bl)
  {
        var img = new Image();
        img.src = "/ARDownload.aspx?IDID=" + id;
        var aBCPT;
        var pathFile="";        
        if(bl==0)
        {
            aBCPT = document.getElementById("aBCPT_" + id);            
        }
        else
            aBCPT = document.getElementById("aBCPT1_" + id);
        if(aBCPT!=null)
        {
            pathFile ="http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/PhanTichBaoCao/" + file;            
            aBCPT.href = pathFile;
        }
  }
</script>