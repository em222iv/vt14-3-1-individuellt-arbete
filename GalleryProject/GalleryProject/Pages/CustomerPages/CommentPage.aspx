<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CommentPage.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.CommentPage" ViewStateMode="Disabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:FormView ID="PresentImageView" runat="server" ItemType="GalleryProject.Model.Picture" DataKeyNames="PictureID">
        <ItemTemplate>
            <asp:Image ID="PresentImage"
                runat="server"
                ImageUrl='<%#"~/Images/" + Item.PictureName%>' />
        </ItemTemplate>
    </asp:FormView>

    <asp:ListView ID="GalleryConnectionString" runat="server"
        ItemType="GalleryProject.Model.Comment"
        InsertMethod="CommentListView_InsertItem"
        UpdateMethod="CommentListView_UpdateItem"
        DeleteMethod="CommentListView_DeleteItem"
        SelectMethod="CommentListView_GetData"
        DataKeyNames="CommentID"
        InsertItemPosition="FirstItem">
        <LayoutTemplate>

            <table class="grid">
                <tr>
                    <th>Comment
                    </th>
                    <th>Commentator
                    </th>
                </tr>
                <%-- Platshållare för nya rader --%>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <InsertItemTemplate>
            <%-- Mall för rad i tabellen för att lägga till nya kunduppgifter. Visas bara om InsertItemPosition 
            har värdet FirstItemPosition eller LasItemPosition.--%>
            <tr>
                <td>
                    <asp:TextBox ID="CommentBox" runat="server" MaxLength="300" Text='<%# BindItem.CommentInput %>' />
                </td>
                <td>
                    <asp:TextBox ID="ComentatorBoxz" runat="server" MaxLength="30" Text='<%# BindItem.Commentator %>' />
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Comment" OnClientClick="return confirm('Do you want to make this comment?')" /></div>
                </td>
                <td>
                    <asp:HyperLink ID="PicturePageHyperLink"
                        runat="server"
                        Text="Back"
                        NavigateUrl='<%# GetRouteUrl("PicturePage", null)%>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="EditComment" runat="server" MaxLength="50" Text='<%# BindItem.CommentInput %>' />
                </td>
                <td>
                    <asp:TextBox ID="EditCommentator" runat="server" MaxLength="50" Text='<%# BindItem.Commentator %>' />
                </td>
                <td>
                    <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>

                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Update" OnClientClick="return confirm('Do you want to update this comment?')"/>
                    <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Item.CommentInput %>
                </td>
                <td><%# Item.Commentator %>
                </td>
                <td>
                    <asp:LinkButton ID="Redigera" runat="server" CommandName="Edit" Text="Edit" CausesValidation="false" />
                    <asp:LinkButton ID="Tabort" runat="server" CommandName="Delete" Text="Delete" CausesValidation="false" OnClientClick="return confirm('Do you want to delete the comment?')" />
                </td>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
