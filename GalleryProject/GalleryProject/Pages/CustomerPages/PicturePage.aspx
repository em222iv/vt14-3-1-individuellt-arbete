<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PicturePage.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.PicturePage" ViewStateMode="Disabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Pictures
    </h1>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Correct your Error to proceed"
        CssClass="validation-summary-errors" />
    <asp:PlaceHolder ID="insertSuccess" runat="server" Visible="false">
        <p>Your gallery has been added</p>
    </asp:PlaceHolder>
    <div id="fileBrowser">
        <asp:FileUpload ID="fileBrowse" runat="server" AllowMultiple="True" />
    </div>
    <%--Listview objekt med Picture som itemtype--%>
    <asp:ListView ID="GalleryConnectionString" runat="server"
        ItemType="GalleryProject.Model.Picture"
        InsertMethod="PictureListView_InsertItem"
        UpdateMethod="PictureListView_UpdateItem"
        DeleteMethod="PictureListView_DeleteItem"
        SelectMethod="PictureListView_GetData"
        DataKeyNames="PictureID"
        InsertItemPosition="FirstItem">
        <LayoutTemplate>
            <%--visar columner för bilder och katerogi--%>
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
         <%--  Radar ut alla tumnagelbilder och dropdownlistor som visar fär vilken kategori de tillhör--%>
            <tr>
                <td id="PictureContainerTd">
                    <asp:Image ID="image"
                        runat="server"
                        ImageUrl='<%#"~/Images/thumbImg/" + Item.PictureName%>' />
                </td>
                <%--visar vilken kategori bilden tilhlör. enabled=false så för att rundgå missförstånd om att man kan ändra kateogri utanför editläget--%>
                <td id="CategoryBox">
                    <asp:DropDownList ID="CategoryDropDownList" runat="server"
                        SelectMethod="CategoryListView"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        SelectedValue='<%# BindItem.CategoryID %>'
                        enabled="false">
                    </asp:DropDownList>
                </td>
                <td id="DeleteEdittd">
                <%--   knappar för att ta bort eller reidgera--%>
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
        <InsertItemTemplate>
        <%-- Visar en textbox för att ge namn till en bild och dropdown för att ange dens kategori--%>
            <tr>
                <td>
                    <asp:TextBox ID="PictureNameBox" runat="server" MaxLength="30" ValidationGroup="insert" Text='<%#: BindItem.PictureName %>' />
                     <%--Validering--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Please, give the picture a name" ControlToValidate="PictureNameBox" ValidationGroup="insert"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server"
                        SelectMethod="CategoryListView"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        SelectedValue='<%#: BindItem.CategoryID %>'>
                    </asp:DropDownList>
                </td>
                <td>
           <%--  Knapar för att läga till eller rensa textfältet för bilder--%>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Add"  /></div>
                    <asp:LinkButton ID="CleanTextButton" runat="server" CommandName="Cancel" Text="Clean" CausesValidation="false" />

                </td>
            </tr>
        </InsertItemTemplate>
        <EditItemTemplate>
     <%--       Visas när man vill redigera bild
            Ändra bildnamn, man måste skriva in filformat på bilden. formatet visas när man vill redigera och får ändras mellan PNG,JPG och GIF. men det bör förbli samma filändelse--%>
            <tr>
                <td id="EditTD">
                    <asp:TextBox ID="EditPictureName" runat="server" MaxLength="30" Width="150" Height="40" Text='<%# BindItem.PictureName %>' />
                  <%--Validering--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*You must declare correct filetype. JPG/GIF/PNG" ValidationExpression="^.+\.(([jJ][pP][eE]?[gG])|([gG][iI][fF])|([pP][nN][gG]))$" ControlToValidate="EditPictureName"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server"
                        SelectMethod="CategoryListView"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        SelectedValue='<%#: BindItem.CategoryID %>'>
                    </asp:DropDownList>
                </td>
                <td>
                <%--  knappar för att redigeringen eller genomföra den--%>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Spara" OnClientClick="return confirm('Do you want to make the update?')" />
                    <asp:LinkButton ID="CancellButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>


