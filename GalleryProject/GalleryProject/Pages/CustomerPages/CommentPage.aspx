<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CommentPage.aspx.cs" Inherits="GalleryProject.Pages.CustomerPages.CommentPage" ViewStateMode="Disabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Comments</h1>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
        HeaderText="Correct your Error to proceed"
        CssClass="validation-summary-errors" />
    <asp:ListView ID="GalleryConnectionString" runat="server"
        ItemType="GalleryProject.Model.Comment"
        InsertMethod="CommentListView_InsertItem"
        UpdateMethod="CommentListView_UpdateItem"
        DeleteMethod="CommentListView_DeleteItem"
        SelectMethod="CommentListView_GetData"
        DataKeyNames="CommentID"
        InsertItemPosition="FirstItem" OnSelectedIndexChanged="GalleryConnectionString_SelectedIndexChanged">
        <LayoutTemplate>
            <%--visar columner för bilder och katerogi--%>
            <table class="grid">
                <tr>
                    <th>
                        Comment
                    </th>
                    <th>
                        Commentator
                    </th>
                </tr>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate> 
            <%-- radar ut alla kommentarerna som tillhör bildID:et och knappar för att ta bort eller editera--%>
            <tr>
                <td><%# Item.CommentInput %>
                </td>
                <td><%# Item.Commentator %>
                </td>
                <td>
                    <asp:LinkButton ID="Redigera" runat="server" CommandName="Edit" Text="Edit" CausesValidation="false" />
                    <asp:LinkButton ID="Tabort" runat="server" CommandName="Delete" Text="Delete" CausesValidation="false" OnClientClick="return confirm('Do you want to delete the comment?')" />
                </td>
            </tr>
        </ItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td>
                    <%--visar textfält fär att lägga till kommentar och kommentatorsalias --%>
                    <%-- kommentern får max innehålla 300 tecken--%>
                  <%-- Jag har föröskt att ändra storlek med columns på ett textfält men det funkar inte? kan asp css:en stoppa det?--%>
                    <asp:TextBox ID="CommentBox" runat="server" MaxLength="300" Text='<%# BindItem.CommentInput %>' ValidationGroup="InsertComment" />
                    <asp:RequiredFieldValidator ID="RequiredCommentValidator" runat="server" ErrorMessage="Please, write down your comment" ControlToValidate="CommentBox" ValidationGroup="InsertComment"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <%-- namnet får max vara 30 bokstäver--%>
                    <asp:TextBox ID="ComentatorBox" runat="server" MaxLength="30" Text='<%# BindItem.Commentator %>' ValidationGroup="InsertComment" />
                    <asp:RequiredFieldValidator ID="RequiredCommentatorValidator" runat="server" ErrorMessage="Please, write give a alias" ControlToValidate="ComentatorBox" ValidationGroup="InsertComment"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <%--  knapp för att lägga till kommentaren. --%>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Comment" ValidationGroup="InsertComment" OnClientClick="return confirm('Do you want to make this comment?')" /></div>
                </td>
                <td>
                    <td>
                        <asp:HyperLink ID="PicturePageHyperLink"
                            runat="server"
                            Text="Back"
                            NavigateUrl='<%# GetRouteUrl("PicturePage", null)%>' />
                    </td>
                </td>
            </tr>
        </InsertItemTemplate>

        <EditItemTemplate>
            <tr>
                <td>
                    <%--Boxar och knappar för att redigera kommentar och kommentator--%>
                   <%-- max 300 tecken på textboxen--%>
                    <asp:TextBox ID="EditComment" runat="server" MaxLength="300" Text='<%# BindItem.CommentInput %>' Columns="300" />
                    <asp:RequiredFieldValidator ID="RequiredCommentValidator2" runat="server" ErrorMessage="*Please, write down your comment" ControlToValidate="EditComment"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="EditCommentator" runat="server" MaxLength="30" Text='<%# BindItem.Commentator %>' />
                       <asp:RequiredFieldValidator ID="RequiredCommentatorValidator2" runat="server" ErrorMessage="*Please, write down your comment" ControlToValidate="EditCommentator"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Update" OnClientClick="return confirm('Do you want to update this comment?')" />
                    <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
