<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_article.aspx.cs" Inherits="Simple.Core.Views.Article.edit_article" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            ArticleId = <asp:textbox runat="server" ID="ArticleId" />
        </div>
        <div>
            <asp:Button runat="server" Text="Speichern" OnClick="Unnamed_Click" />
        </div>
    </form>
</body>
</html>
