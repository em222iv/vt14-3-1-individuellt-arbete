<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.WebForm1" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Style.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="ValidationSummary: zup. much bugg, very no" />
    <div>
        <div id="picture"  >
            <div id="bigImg">
            <asp:Image ID="Image" runat="server" Width="550" Height="375" Visible="false" />
                </div>
        </div>
         <div><asp:Label ID="successLabel" runat="server" Text="Uppladdningen lyckades" Visible="false"></asp:Label></div>
         <div id="thumbnails">
              <asp:Repeater ID="repeater" runat="server" ItemType="System.String" SelectMethod="repeater_GetData">
                <ItemTemplate>
                    <asp:HyperLink ID="FileHyperLink" runat="server"
                        Text='<%#Item %>'
                        ImageUrl='<%#"Content/thumbImg/" + Item%>'
                        NavigateUrl='<%#"?" + Item %>'/>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="hejhopp">
            <div id="fileBrowser"><asp:FileUpload ID="fileBrowse" runat="server" AllowMultiple="True" /></div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Choose a picture to upload" ControlToValidate="fileBrowse"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*JPG/JPEG/GIF files only" ValidationExpression="^.+\.(([jJ][pP][eE]?[gG])|([gG][iI][fF])|([pP][nN][gG]))$"  ControlToValidate="fileBrowse"></asp:RegularExpressionValidator>
            <div id="uploadButton"><asp:Button ID="Button" runat="server" Text="uploadPic" OnClick="Button_Click" /></div>
            <div id="deleteButton"><asp:Button ID="Button1" runat="server" Text="deletePic" OnClick="deleteButton_Click" causesValidation="false"/></div>
        </div>
    </div>
    </form>
</body>
</html>