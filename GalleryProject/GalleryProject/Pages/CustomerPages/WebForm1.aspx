<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="picture"  >
            <div id="bigImg">
            <asp:Image ID="Image" runat="server" Width="550" Height="375" Visible="false" />
                </div>
        </div>
</asp:Content>
