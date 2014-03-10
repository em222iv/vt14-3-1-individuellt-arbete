<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ListView ID="GalleryConnectionString" runat="server"
        ItemType="GalleryProject.Model.Comment"
        InsertMethod="CommentListView_InsertItem"
        UpdateMethod="CommentListView_UpdateItem"
        DeleteMethod="CommentListView_DeleteItem"
        SelectMethod="CommentListView_GetData"
        DataKeyNames="PictureID, CommentID"
        InsertItemPosition="FirstItem">
          <LayoutTemplate>
            <table class="grid">
                <tr>
                    <th>
                        Comment
                    </th>
                      <th>
                        Commentator
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
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" OnClientClick="return confirm('Vill du lägga till denna kommentar?')" /></div>
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

                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Spara" />
                    <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
         <ItemTemplate>
   
            <tr>
               
                <td>
                    Comment:<%# Item.CommentInput %>
                </td>
                <td>
                   Commentator:  <%# Item.Commentator %>
                </td>
                <td>
                <asp:LinkButton ID="Redigera" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                <asp:LinkButton ID="Tabort" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick="return confirm('Vill du ta bort galleriet?')" />
              </td>
            </ItemTemplate>

     </asp:ListView>



   <%--  <asp:HyperLink ID="PicturePageHyperLink" 
            runat="server"
            Text="Till bildsidan"
            NavigateUrl='<%# GetRouteUrl("PicturePage", new { })%>' />--%>
</asp:Content>
