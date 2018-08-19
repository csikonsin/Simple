<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Simple.Core._Default" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - My ASP.NET Application</title>

    <webopt:BundleReference runat="server" ID="cssBundle" />
    <%: Scripts.Render("~/bundles/CmsScripts") %>


    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />


</head>
<body>
    <asp:PlaceHolder ID="ph" runat="server" />
</body>
</html>