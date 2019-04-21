<%@ Page Title="" Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="PaidTimeOffUI.Administration.Roles" %>

<%@ Register Assembly="V2.FrameworkControls" Namespace="V2.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/PaidTimeOffEditGrid.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <cc1:CustomGridView ID="cgvRoles" runat="server" OnRowDataBound="cgvRoles_RowDataBound" ></cc1:CustomGridView>
</asp:Content>
