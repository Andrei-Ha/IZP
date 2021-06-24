<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DelArch_PES.aspx.cs" Inherits="DelArch_PES" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    Удаление архива Пинских электрических сетей
    <asp:Panel ID="PanelFilter" runat="server">
    <fieldset style="vertical-align: middle; text-align: center;">
        <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">Выберите крайнюю дату удаляемого архива(включительно)</legend>
                            <radCln:RadDatePicker ID="RadDatePicker_date" runat="server" MinDate="2007-01-01" Width="110px" Font-Bold="False" Font-Italic="False">
                                <Calendar ID="Calendar1" runat="server" Skin="WebBlue" >
                                </Calendar>
                                <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium" Enabled="false">
                                </DateInput>
                            </radCln:RadDatePicker>
                            <asp:Button ID="ButtonForm" runat="server" Text="сформировать запрос на удаление" ForeColor="White" BackColor="Teal" onclick="ButtonForm_Click" /> 
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="PanelCancel" runat="server">
    <fieldset style="vertical-align: middle; text-align: center;">
        <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;"></legend>
                            <asp:Button ID="ButtonCansel" runat="server" Text="отменить запрос на удаление" ForeColor="Yellow" BackColor="Teal" OnClientClick="return confirm('Вы уверены, что хотите отменить запрос на удаление архива?');" onclick="ButtonCansel_Click" /> 
                            <asp:Button ID="ButtonDelete" runat="server" Text="удалить архив до выбранной даты" ForeColor="Blue" BackColor="Teal" OnClientClick="return confirm('Вы уверены, что хотите удалить архив до выбранной даты?');" onclick="ButtonDelete_Click" />
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="PanelConfirm1" runat="server">
    <fieldset style="vertical-align: middle; text-align: center;">
        <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;"></legend>
                            <asp:Label ID="LabelConfirm" runat="server" ForeColor="Blue" Font-Italic="true" />
                            <asp:Button ID="ButtonConfirmYes" runat="server" Text="разрешить удаление архива" ForeColor="Yellow" BackColor="Teal" onclick="ButtonConfirmYes_Click" /> 
                            <asp:Button ID="ButtonConfirmNo" runat="server" Text="запретить удаление архива" ForeColor="Red" BackColor="Teal" onclick="ButtonConfirmNo_Click" />
        </fieldset>
    </asp:Panel>
    <asp:Label ID="LabelContent" runat="server" />
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
            ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
        <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" 
            DataSourceID="SqlDataSource2" Skin="WebBlue" AutoGenerateColumns="False" Width="100%">
                <MasterTableView   AutoGenerateColumns="False" DataSourceID="SqlDataSource2" 
                    DataKeyNames="ID" Width="100%" BorderColor="InactiveCaptionText" 
                    BorderWidth="1px" GridLines="Both" ShowFooter="true" 
                    NoMasterRecordsText="Нет данных">
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <radG:GridBoundColumn DataField="ID" UniqueName="column" 
                            HeaderText="№">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="MIN_DATE" UniqueName="column1" 
                            HeaderText="мин. дата">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="MAX_DATE" UniqueName="column2" 
                            HeaderText="макс. дата">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="PROT_COUNT" UniqueName="column3" 
                            HeaderText="кол-во протоколов">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="REC_COUNT" UniqueName="column4" 
                            HeaderText="кол-во записей">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="EXEC_INFO" UniqueName="column5" 
                            HeaderText="дата; инициатор; IP">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="CONFIRM1" UniqueName="column6" 
                            HeaderText="решение ЛПР1">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="CONFIRM1_INFO" UniqueName="column7" 
                            HeaderText="дата; ЛПР1; IP">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="CONFIRM2" UniqueName="column8" 
                            HeaderText="решение ЛПР2">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="CONFIRM2_INFO" UniqueName="column9" 
                            HeaderText="дата; ЛПР2; IP">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="DEL" UniqueName="column10" 
                            HeaderText="статус">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="DEL_INFO" UniqueName="column11" 
                            HeaderText="дата; исполнитель; IP">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </radG:GridBoundColumn>
                    </Columns>
               </MasterTableView>
            <ClientSettings >
                <Selecting AllowRowSelect="false"  />
            </ClientSettings>
                <FooterStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Wrap="True" BorderWidth="0" />
        </radG:RadGrid>
</asp:Content>

