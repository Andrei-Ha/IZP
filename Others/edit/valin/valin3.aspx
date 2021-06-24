<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="valin3.aspx.cs" Inherits="valin3" %>
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
        function CloseAndRebind(reb) {
            GetRadWindow().Close();
            if (reb) { GetRadWindow().BrowserWindow.refreshGrid2(null); }
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)
            return oWindow;
        }
        function validate(inp) {
            inp.value = inp.value.replace(/[^\d,.]*/g, '').replace(/([,.])[,.]+/g, '$1').replace(/^[^\d]*(\d+([.,]\d{0,5})?).*$/g, '$1');
            //inp.value = inp.value.replace(/[^0-9\-\.]/gi, '');
        }

    </script>
        <asp:Label ID="InjectScript" runat="server"></asp:Label> 
    <div>
    <hr />
    <asp:Label ID="Label_Name" runat="server" style="font-size:medium; font-style:italic; color:Black"/><br/>
    <asp:Label ID="Label_Organ" runat="server" style="font-size:x-small; font-style:italic; color:Gray"/>
    <asp:Label ID="Label_Info" runat="server" style="font-size:small; font-style:italic; color:Orange" /><hr />
    <asp:Label ID="lab" runat="server" style="font-size:xX-small;color:Aqua" visible="false"/>
    <table>
    <tr>
        <td colspan="2">
            <fieldset style="vertical-align: middle; text-align: center; width:150px; margin:auto">
            <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">дата испытания</legend>
                <radCln:RadDatePicker ID="RadDatePicker_date_exp" runat="server"  MaxDate="2050-12-31" MinDate="2007-01-01" Width="110px" Font-Bold="False" Font-Italic="False">
                    <Calendar ID="Calendar1" runat="server" Skin="WebBlue" ></Calendar>
                    <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium"></DateInput>
                </radCln:RadDatePicker> 
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset style="vertical-align: middle; text-align: center;">
            <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">Указатель до 1000В</legend>
                <asp:RadioButton ID="RadioButton3" Text="однополюсный" style="font-size:large" 
                    GroupName="polus" runat="server"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton4" Text="двухполюсный" style="font-size:large" 
                    GroupName="polus" runat="server" Checked="true" />
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:HiddenField ID="HiddenField_v" Value="2" runat="server" />
            <fieldset style="vertical-align: middle; text-align: center;">
            <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">Изолирующая часть(однополюсн.) или Провод(двухполюсн.). Испытано напряжением:</legend>
                <asp:RadioButton ID="RadioButton1" Text="2кВ" style="font-size:large" 
                    GroupName="pointers" runat="server" Checked="true" AutoPostBack="true" 
                    oncheckedchanged="RadioButton1_CheckedChanged" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton2" Text="-" style="font-size:large" 
                    GroupName="pointers" runat="server" 
                    oncheckedchanged="RadioButton2_CheckedChanged" AutoPostBack="true" />
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset style="vertical-align: middle; text-align: center;">
            <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">Напряжение индикации, В</legend>
                <asp:TextBox ID="v_indic" runat="server" Font-Italic="True" MaxLength="20" Width="70px" onkeyup="validate(this)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFV_v_indic" ControlToValidate="v_indic" runat="server" ErrorMessage="поле не должно быть пустым" Display="Dynamic"><img src="../../../Pictures/Warning.png" alt="!" title="поле не должно быть пустым" /></asp:RequiredFieldValidator>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset style="vertical-align: middle; text-align: center;">
            <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">Ток протекающий через изделие(указатель), мА</legend>
                <asp:TextBox ID="c_pointer" runat="server" Font-Italic="True" MaxLength="20" Width="70px" onkeyup="validate(this)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFV_c_pointer" ControlToValidate="c_pointer" runat="server" ErrorMessage="поле не должно быть пустым" Display="Dynamic"><img src="../../../Pictures/Warning.png" alt="!" title="поле не должно быть пустым" /></asp:RequiredFieldValidator>
            </fieldset>        
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td style="width:50%">
            <asp:Button ID="Save" runat="server" CausesValidation="true" Text="Сохранить" BackColor="DarkGray" 
                BorderStyle="Outset" Font-Bold="True"
            Font-Italic="True" ForeColor="White" onclick="Save_Click" />
        </td>
        <td style="width:50%">
            <asp:Button ID="Cancel" runat="server" Text="Отмена" BackColor="DarkGray" 
                BorderStyle="Outset" Font-Bold="True"
            Font-Italic="True" ForeColor="White"  OnClientClick="CloseAndRebind(false)" />
        </td>
    </tr>
    </table>    
    </div>
    </form>
</body>
</html>
