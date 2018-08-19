<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaseModuleWrapper.ascx.cs" Inherits="Simple.Core.Views.BaseModuleWrapper" %>
<asp:Panel ID="pnModule" runat="server">
    <asp:PlaceHolder ID="ph" runat="server" />
    <asp:PlaceHolder runat="server" ID="admin" Visible="false">
        <a runat="server" id="addModule" class="button addmodule">Module settings</a>
    </asp:PlaceHolder>
</asp:Panel>