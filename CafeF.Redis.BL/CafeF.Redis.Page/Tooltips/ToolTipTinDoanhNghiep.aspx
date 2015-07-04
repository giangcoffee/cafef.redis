<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToolTipTinDoanhNghiep.aspx.cs" Inherits="Portal.ToolTips.ToolTipTinDoanhNghiep" %>
<%@ Import Namespace="CafeF.Redis.BL"%>

<%@ Register Src="Controls/Events.ascx" TagName="Events" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="http://cafef3.vcmedia.vn/styles/cafef.css" type="text/css" />
   <link rel="stylesheet" href="http://cafef3.vcmedia.vn/styles/datacenter.css" type="text/css" />
   <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.js?123"></script>
      <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/jqDnR.js"></script>
      <script type="text/javascript" src="http://cafef4.vcmedia.vn/kby<%= Utils.GetKbyFolder() %>/kby.js"></script>
      <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.bgiframe.min.js"></script>
      <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.dimensions.js"></script>

   <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.autocomplete2.js"></script>
   <script type="text/javascript">    
            function changeFrameHeight()
            {
                if(navigator.appName != "Microsoft Internet Explorer")
                {
                   window.parent.resizeFrame('frmNews', document.body.scrollHeight+10 , '0');
                }
                else //IE
                {
                    window.parent.resizeFrame('frmNews', document.body.scrollHeight+30 , '0');
                }
            }
    </script>
</head>
<body onload="changeFrameHeight();" style="padding:0px 0px 0px 0px;margin:0px 0px 0px 0px;background-color:#fff" >
    <form id="form1" runat="server">
    <div>
        <uc1:Events ID="Events1" runat="server" />    
    </div>
    </form>
    <script type="text/javascript">
        function opennewlink(url)
         {
            newWindowObj=window.open(url);
            //newWindowObj.blur();
            //window.focus();
        }
        $("a").click(function(e) {
            e.preventDefault();
            window.parent.location = $(this).attr('href');
        });
    </script>
</body>
</html>
