<%@ Page Title="" Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="PaidTimeOffUI.Administration.User" %>
<%@ MasterType VirtualPath="~/PaidTimeOffEditPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <table>
        <tr>
            <td>Window Account Name:</td>
            <td>
                <asp:TextBox runat="server" ID="txtWindowAccountName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td>
                <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>Last Name:</td>
            <td>
                <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>Email Address:</td>
            <td>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Active:</td>
            <td>
                <asp:CheckBox runat="server" ID="chkActive" />
            </td>
        </tr>
    </table>
</asp:Content>
