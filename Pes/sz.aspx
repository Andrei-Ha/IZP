<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="sz.aspx.cs" Inherits="sz" %>
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
    <div>
        <asp:Label ID="InjectScript" runat="server"></asp:Label>    
        <asp:Label ID="LabelQS" runat="server" />
        <asp:Button ID="Button1" runat="server" BackColor="DarkGray" BorderStyle="Outset" Font-Bold="True" Enabled="false" Visible="false"
            Font-Italic="True" ForeColor="White" Text="  УДАЛИТЬ СЗ " OnClientClick="return confirm('Вы уверены, что хотите удалить это средство защиты?');" OnClick="Button1_Click"/>
    <hr />
        <asp:HyperLink ID="HyperLinkProtcl" runat="server" />
    <hr />
        <asp:HyperLink ID="HyperLinkProtclSpisan" runat="server" />
    <hr />
    <br />
    <table>
    <tr>
        <td>
            <asp:HyperLink ID="HyperLinkMove" runat="server" />   
        </td>
    </tr>
    </table> 
    <hr />
        <asp:Button id="Button3" runat="server" ForeColor="White" Font-Italic="True" Font-Bold="True" Text="  закрыть  " OnClientClick="Close(false)" BorderStyle="Outset" BackColor="DarkGray"></asp:Button>
    <hr />
    </div>
    </form>
</body>
</html>
