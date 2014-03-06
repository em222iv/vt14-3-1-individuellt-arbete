<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GalleryProject.WebForm1" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Style.css" rel="stylesheet" />
    <title></title>
    <link href="GalleryStyle.css" rel="stylesheet" />
</head>
<body>
    <div id="page">
        <div id="main">
            <form id="theForm" runat="server">
                <h1>Kunder
                </h1>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
                    CssClass="validation-summary-errors" />
                <asp:PlaceHolder ID="placeholder" runat="server" Visible="false">
                    <p>Din kontakt är tillagd</p>
                </asp:PlaceHolder>
                <asp:ListView ID="GalleryConnectionString" runat="server"
                    ItemType="GalleryProject.Model.Gallery"
                    InsertMethod="ContactListView_InsertItem"
                    UpdateMethod="ContactListView_UpdateItem"
                    DeleteMethod="ContactListView_DeleteItem"
                    SelectMethod="GalleryListView_GetData"
                    DataKeyNames="GalleryID"
                    InsertItemPosition="FirstItem">
                    <LayoutTemplate>
                        <table class="grid">
                            <tr>
                                <th>
                                    GalleryID
                                </th>
                                <th>GalleryName
                                </th>
                            </tr>
                            <%-- Platshållare för nya rader --%>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <%-- Mall för nya rader. --%>
                        <tr>
                            <td>
                                <%# Item.GalleryID %>
                            </td>
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
                               
                            </td>
                            
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
                            <td>
                                <asp:TextBox ID="Gallery" runat="server" MaxLength="50" Text='<%# BindItem.GalleryID %>' />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="EditItemTemplate" ControlToValidate="Gallery" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                            </td>
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
            </form>
        </div>
    </div>
</body>
</html>
