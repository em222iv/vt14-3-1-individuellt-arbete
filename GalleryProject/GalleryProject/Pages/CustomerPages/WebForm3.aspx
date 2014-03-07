<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.WebForm3" viewStateMode="Disabled"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <h1>Gallerier
    </h1>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
        CssClass="validation-summary-errors" />
    <asp:PlaceHolder ID="insertSuccess" runat="server" Visible="false">
        <p>Your gallery has been added</p>
    </asp:PlaceHolder>
        <asp:PlaceHolder ID="deleteSuccess" runat="server" Visible="false">
        <p>Your gallery has been deleted</p>
    </asp:PlaceHolder>
    <asp:ListView ID="GalleryConnectionString" runat="server"
        ItemType="GalleryProject.Model.Gallery"
        InsertMethod="GalleryListView_InsertItem"
        UpdateMethod="GalleryListView_UpdateItem"
        DeleteMethod="GalleryListView_DeleteItem"
        SelectMethod="GalleryListView_GetData"
        DataKeyNames="GalleryID"
        InsertItemPosition="FirstItem">
        <LayoutTemplate>
            <table class="grid">
                <tr>
                    <%-- <th>
                        GalleryID
                    </th>--%>
                    <th>
                        GalleryName
                    </th>
                </tr>
                <%-- Platshållare för nya rader --%>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <%-- Mall för nya rader. --%>
            <tr>
                <%-- <td>
                    <%# Item.GalleryID %>
                </td>--%>
                <td>
                    <%# Item.GalleryName %>
                </td>
                <td class="command">
                    <%-- "Kommandknappar" för att ta bort och redigera kunduppgifter. Kommandonamnen är VIKTIGA! --%>

                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick="return confirm('Vill du ta bort galleriet?')" />
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />

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
                    <asp:TextBox ID="GalleryNameBox" runat="server" MaxLength="50" Text='<%# BindItem.GalleryName %>' />
                </td>
                <td>
                        <%-- "Kommandknappar" för att lägga till en ny kunduppgift och rensa texfälten. Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" OnClientClick="return confirm('Vill du lägga till dettta galleri?')" />
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                </td>
            </tr>
        </InsertItemTemplate>
        <EditItemTemplate>
            <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
            <tr>
                <%-- <td>
                    <asp:TextBox ID="Gallery" runat="server" MaxLength="50" Text='<%# BindItem.GalleryID %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="EditItemTemplate" ControlToValidate="Gallery" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>--%>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Text='<%# BindItem.GalleryName %>' />
                </td>
                <td>
                    <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>

                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Spara" />
                    <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>

</asp:Content>
