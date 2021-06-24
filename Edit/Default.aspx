<%@ Page Title="Параметры рабочего места испытателя СЗ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table style="width: 100%; height: 193px;">
        <tr>
            <td style="background-color:#E6E6FA">
            <div style="color:Black">Параметры рабочего места испытателя СЗ</div>
            <hr />
                <asp:Literal ID="Literal1" runat="server" Mode="PassThrough"></asp:Literal>
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
            <td style="background-color:#FFE4C4">
            <div style="color:Black">Форма ввода новых параметров</div>
            <hr />
                <table style="width: 100%;">
                    <tr>
                        <td >
                            Текущая дата<br />
                            <radCln:RadDatePicker ID="RadDatePicker_date_dok" runat="server"  MaxDate="2050-12-31" MinDate="2007-01-01" Width="110px" Font-Bold="False" Font-Italic="False">
                                <Calendar ID="Calendar1" runat="server" Skin="WebBlue">
                                </Calendar>
                                <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium">
                                </DateInput>
                             </radCln:RadDatePicker>    
                        </td>
                        <td style="background-color:lightgray">
                            Прейскурант цен от<br />
                            <radCln:RadDatePicker ID="RadDatePicker_Price" runat="server"  MaxDate="2050-12-31" MinDate="2007-01-01" Width="110px" Font-Bold="False" Font-Italic="False">
                                <Calendar ID="Calendar2" runat="server" Skin="WebBlue">
                                </Calendar>
                                <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium">
                                </DateInput>
                             </radCln:RadDatePicker>
                        </td>        
                    </tr>
                    <tr>
                        <td>
                Температура воды<br /><span style="color:#FF3300">*</span><asp:TextBox ID="T_WATER" runat="server"></asp:TextBox> 
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                 runat="server" ErrorMessage="Обнаружено не число или превышена разрядность!(температура воды)" 
                    ControlToValidate="T_WATER" ValidationExpression="^\d{1,2}$" 
                    ValidationGroup="Group1" ForeColor="Red">!!</asp:RegularExpressionValidator>  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                   runat="server" ErrorMessage="Не задана температура воды!" ControlToValidate="T_WATER" 
                   ValidationGroup="Group1" ForeColor="Red">!!</asp:RequiredFieldValidator>     
                        </td>
                        <td>
                           Температура воздуха<br /><span style="color:#FF3300">*</span><asp:TextBox ID="T_AIR" runat="server"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                             runat="server" ErrorMessage="Обнаружено не число или превышена разрядность(воздух)!" 
                              ControlToValidate="T_AIR" ValidationExpression="^\d{1,2}$" 
                              ValidationGroup="Group1" ForeColor="Red">!!</asp:RegularExpressionValidator>  
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                             runat="server" ErrorMessage="Не задана температура воздуха!" ControlToValidate="T_AIR" 
                             ValidationGroup="Group1" ForeColor="Red">!!</asp:RequiredFieldValidator>               
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Влажность<br /><span style="color:#FF3300">*</span><asp:TextBox ID="HUMIDITY" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                              runat="server" ErrorMessage="Обнаружено не число или превышена разрядность!(влажность)" 
                               ControlToValidate="HUMIDITY" ValidationExpression="^\d{1,2}$" 
                               ValidationGroup="Group1" ForeColor="Red">!!</asp:RegularExpressionValidator>  
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                              runat="server" ErrorMessage="Не задана влажность!" ControlToValidate="HUMIDITY" 
                              ValidationGroup="Group1" ForeColor="Red">!!</asp:RequiredFieldValidator>  
                        </td>
                         <td>
                            <br />  
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Ф.И.О. испытателя<br /><asp:TextBox ID="FIO_EXEC" runat="server" Enabled="false"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                              runat="server" ErrorMessage="Английский алфавит или число символов больше 50" 
                               ControlToValidate="FIO_EXEC" ValidationExpression="^[а-яА-Я''-.'\s]{1,40}$" 
                               ValidationGroup="Group1" ForeColor="Red">!!</asp:RegularExpressionValidator>  
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                              runat="server" ErrorMessage="Не задано давление!" ControlToValidate="FIO_EXEC" 
                              ValidationGroup="Group1" ForeColor="Red">!!</asp:RequiredFieldValidator>  
                        </td>
                        <td>
                              Ф.И.О. начальника<br /><asp:TextBox ID="FIO_CHIEF" runat="server"></asp:TextBox>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                               runat="server" ErrorMessage="Английский алфавит или число симолов больше 50" 
                                ControlToValidate="FIO_CHIEF" ValidationExpression="^[а-яА-ЯёЁ''-.'\s]{1,50}$" 
                                ValidationGroup="Group1" ForeColor="Red">!!</asp:RegularExpressionValidator>  
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                               runat="server" ErrorMessage="Не задано давление!" ControlToValidate="FIO_CHIEF" 
                               ValidationGroup="Group1" ForeColor="Red">!!</asp:RequiredFieldValidator>  
                        </td>
                    </tr>
                    <tr>
                        <td >
                        </td>
                        <td style="text-align:right">
                              <asp:Button ID="Button" runat="server" Text="Сохранить" 
                               onclick="Button_Click" ValidationGroup="Group1" /> 
                              <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red" Visible="false">
                              </asp:Label>
                        </td>
                 </tr>
                </table >
            </td>
        <tr>
            <td colspan=3 style="text-align:right"><strong>Примечание: звездочкой отмечены поля ,обязательные к заполнению.</strong>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
  HeaderText="Обнаружены следующие ошибки:"
   ShowMessageBox="True" ValidationGroup="Group1" />

</asp:Content>

