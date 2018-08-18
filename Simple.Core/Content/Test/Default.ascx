<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Simple.Core.Content.Default" %>
<%@ Register Src="~/Views/ModuleLoader.ascx" TagPrefix="simple" TagName="module" %>

<div class="container body-content">
    <simple:module runat="server" identity="M" id="m" />
</div>
