<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Articles.ascx.cs" Inherits="Simple.Core.Views.ArticlesView" %>
<asp:Panel ID="pnArticles" runat="server">
    <asp:Repeater ID="repArticles" runat="server">
        <ItemTemplate>
            <h1><%#Eval("Heading") %></h1>
            <div><%#Eval("Text") %></div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>