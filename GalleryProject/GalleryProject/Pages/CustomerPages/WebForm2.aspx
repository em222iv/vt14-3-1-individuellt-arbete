<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.WebForm2" ViewStateMode="Disabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Pictures
    </h1>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
        CssClass="validation-summary-errors" />
    <asp:PlaceHolder ID="insertSuccess" runat="server" Visible="false">
        <p>Your gallery has been added</p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="deleteSuccess" runat="server" Visible="false">
        <p>Your gallery has been deleted</p>
    </asp:PlaceHolder>
    <div id="fileBrowser">
        <asp:FileUpload ID="fileBrowse" runat="server" AllowMultiple="True" />
    </div>
    <asp:ListView ID="GalleryConnectionString" runat="server"
        ItemType="GalleryProject.Model.Picture"
        InsertMethod="PictureListView_InsertItem"
        UpdateMethod="PictureListView_UpdateItem"
        DeleteMethod="PictureListView_DeleteItem"
        SelectMethod="PictureListView_GetData"
        DataKeyNames="PictureID"
        InsertItemPosition="FirstItem">
        <LayoutTemplate>
            <table class="grid">
                <tr>
                    <%-- <th>
                        GalleryID
                    </th>--%>
                    <th>
                        Pictures
                    </th>
                    <th>
                        Category
                    </th>
                </tr>
                <%-- Platshållare för nya rader --%>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <%-- Mall för nya rader. --%>
            <tr>
                <td id="PictureContainerTd">
                    <asp:Image ID="image"
                        runat="server"
                        Width="100"
                        Height="70"
                        ImageUrl='<%#"~/Images/" + Item.PictureName%>' />
                </td>
                <td id="CategoryBox">
                      <asp:DropDownList ID="CategoryDropDownList" runat="server"
                        SelectMethod="CategoryListView"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        SelectedValue='<%# BindItem.CategoryID %>'>
                    </asp:DropDownList>
                </td>
                <td id="DeleteEdittd">
                    <%-- "Kommandknappar" för att ta bort och redigera kunduppgifter. Kommandonamnen är VIKTIGA! --%>
                    <asp:LinkButton ID="Tabort" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick="return confirm('Vill du ta bort galleriet?')" />
                    <asp:LinkButton ID="Redigera" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                </td>
                <td>
                    <asp:HyperLink ID="CommentPageLink" runat="server"
                        Text="kommentarer"
                        NavigateUrl='<%# GetRouteUrl("Comment", new { Item.PictureID})%>' />
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- Detta visas då kunduppgifter saknas i databasen. --%>
            <table class="grid">
                <tr>
                    <td>uppgifter saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <%-- Mall för rad i tabellen för att lägga till nya kunduppgifter. Visas bara om InsertItemPosition 
            har värdet FirstItemPosition eller LasItemPosition.--%>
            <tr>
                <td>
                    <asp:TextBox ID="PictureNameBox" runat="server" MaxLength="50" Text='<%# BindItem.PictureName %>' />
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server"
                        SelectMethod="CategoryListView"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        SelectedValue='<%# BindItem.CategoryID %>'>
                    </asp:DropDownList>
                </td>
                <td>
                    <%-- "Kommandknappar" för att lägga till en ny kunduppgift och rensa texfälten. Kommandonamnen är VIKTIGA! --%>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Lägg till" OnClientClick="return confirm('Vill du lägga till denna bild?')" /></div>
                    <asp:LinkButton ID="CleanTextButton" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />

                </td>
            </tr>
        </InsertItemTemplate>
        <EditItemTemplate>

            <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
            <tr>
                <td id="EditTD">
                    <asp:TextBox ID="EditPictureName" runat="server" MaxLength="50" Width="90" Height="70" Text='<%# BindItem.PictureName %>' />
                </td>
                <td>
                     <asp:DropDownList ID="CategoryDropDownList" runat="server"
                        SelectMethod="CategoryListView"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        SelectedValue='<%# BindItem.CategoryID %>'>
                    </asp:DropDownList>
                </td>
                <td>
                    <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Spara" />
                    <asp:LinkButton ID="CancellButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>


