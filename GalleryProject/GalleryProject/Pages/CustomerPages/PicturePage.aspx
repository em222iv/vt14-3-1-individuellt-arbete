<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PicturePage.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.PicturePage" ViewStateMode="Disabled" %>

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
                    <th>Pictures
                    </th>
                    <th>Category
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
                        ImageUrl='<%#"~/Images/thumbImg/" + Item.PictureName%>' />
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
                    <asp:LinkButton ID="Tabort" runat="server" CommandName="Delete" Text="Delete" CausesValidation="false" OnClientClick="return confirm('Do you want to delete this picture?')" />
                    <asp:LinkButton ID="Redigera" runat="server" CommandName="Edit" Text="Edit" CausesValidation="false" />
                </td>
                <td>
                    <asp:HyperLink ID="CommentPageLink" runat="server"
                        Text="Comments"
                        NavigateUrl='<%# GetRouteUrl("CommentPage", new { Item.PictureID})%>' />
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
                    <asp:TextBox ID="PictureNameBox" runat="server" MaxLength="50" Text='<%#: BindItem.PictureName %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="*Please, give the picture a name" ControlToValidate="PictureNameBox" ValidationGroup="insert"></asp:RequiredFieldValidator> 
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
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Add" OnClientClick="return confirm('Do you want to add this picture?')" /></div>
                    <asp:LinkButton ID="CleanTextButton" runat="server" CommandName="Cancel" Text="Clean" CausesValidation="false" />

                </td>
            </tr>
        </InsertItemTemplate>
        <EditItemTemplate>

            <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
            <tr>
                <td id="EditTD">
                    <asp:TextBox ID="EditPictureName" runat="server" MaxLength="50" Width="90" Height="70" Text='<%# BindItem.PictureName %>' />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*You must declare correct filetype. JPG/JPEG/GIF" ValidationExpression="^.+\.(([jJ][pP][eE]?[gG])|([gG][iI][fF])|([pP][nN][gG]))$"  ControlToValidate="EditPictureName"></asp:RegularExpressionValidator>
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
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Spara" OnClientClick="return confirm('Do you want to make the update?')" />
                    <asp:LinkButton ID="CancellButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>


