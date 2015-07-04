<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BDSImages.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.TienDoBDS.BDSImages" %>

<div class="block hinhanhlq" style="border:1px solid #EEEEEE;padding:2px;">  
    <div class="blcontent clearfix" >
        <div class="hatop">
            <img id="imgMain<%= imageid %>" src="<%= mainSrc %>" width="290" height="200" />
            <%--<div class="hloverlay"></div>
            <div class="halink"><a href="#"><%= title %></a></div>--%>
        </div>
        <ul class="clearfix" style="padding-top:2px;">
            <asp:Repeater ID="rptImages" runat="server">
                <ItemTemplate>
                    <li1 style="list-style-image:none;list-style-position:outside;list-style-type:none;padding-right:2px;"><img style="cursor:pointer;border:0px" alt="" onclick="ImageClick('imgMain<%= imageid %>',this);" src='<%# Container.DataItem %>' width="38" height="38" /></li1>
                </ItemTemplate>
            </asp:Repeater>
        </ul>       
    </div>
</div><!-- / Hình ảnh liên quan -->

<script type="text/javascript">
var zoom = '<%= sZoomTienDoBDSPath %>';
var zoommain = '<%= sZoomMain %>';

    function ImageClick(id, img)
    {
        document.getElementById(id).src = img.src.replace(zoom,zoommain);
    }
</script>