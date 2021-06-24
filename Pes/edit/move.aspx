<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="move.aspx.cs" Inherits="move" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <style type="text/css">
   table {
    width: 90%; /* Ширина таблицы */
    border: 1px solid Yellow; /* Рамка вокруг таблицы */
    margin: auto; /* Выравниваем таблицу по центру окна  */
    background-color:#C0C0C0;
   }
   td {
    text-align: center; /* Выравниваем текст по центру ячейки */
   }
   a 
   {
    font-family: 'Arial';
    color: Black;
    font-style: italic;
   }
   b 
   {
    font-family: 'Arial';
    color: #000090;
    font-style: italic;
    font-size:smaller;
   }   
  </style>
</head>
<body style="background-color:#CEDDFD">
    <form id="form1" runat="server" style="text-align:center">
    <script type="text/javascript">
        function Close(reb) {
            GetRadWindow().Close();
            if (reb) { GetRadWindow().BrowserWindow.refreshGrid2(true); }
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)
            return oWindow;
        }

    </script>
    <asp:Label ID="InjectScript" runat="server"></asp:Label>
    <div>
    Перемещение
        <asp:Label ID="LabelQS" runat="server" />
    <hr />
        <asp:HyperLink ID="HyperLinkProtcl" runat="server" />
    <table>
    <tr>
        <td>
            <asp:DropDownList ID="DropDownListOrg" runat="server" AutoPostBack="true" 
                onselectedindexchanged="DropDownListOrg_SelectedIndexChanged">
                </asp:DropDownList>
            <asp:DropDownList ID="DropDownListBrig" runat="server">
                </asp:DropDownList>
        </td>
    </tr>
    </table> 
    <hr />   
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="переместить" />
    </form>
</body>
</html>
