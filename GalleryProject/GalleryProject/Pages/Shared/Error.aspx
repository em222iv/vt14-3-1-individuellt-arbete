<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="GalleryProject.Pages.Shared.WebForm1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>There was an error</p>
            <asp:HyperLink ID="PicturePageHyperLinkFromError"
                runat="server"
                Text="Back"
                NavigateUrl='<%# GetRouteUrl("PicturePage", null)%>' />
</asp:Content>
