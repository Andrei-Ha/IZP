<%@ Page Title="Архив Пинских ЭС" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Archive.aspx.cs" Inherits="Archive" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
    function Enab()
    {
        if (!document.getElementById('ctl00_MainContent_RadioButton1').checked) {
            document.getElementById('ctl00_MainContent_DropDownList_Year').setAttribute('disabled', 'disabled');
            document.getElementById('ctl00_MainContent_Panel2').style.display = "inline"; 
        } else {
            document.getElementById('ctl00_MainContent_DropDownList_Year').removeAttribute('disabled');
            document.getElementById('ctl00_MainContent_Panel2').style.display = "none";
        }
    }
    function Func_Panel3() {
    if (document.getElementById('ctl00_MainContent_Button2').value != "скрыть"){
            document.getElementById('ctl00_MainContent_Panel3').style.display = "inline";
            document.getElementById('ctl00_MainContent_Button2').value = "скрыть";
            document.getElementById('ctl00_MainContent_ButtonPDF').style.display = "inline";
        }else{
            document.getElementById('ctl00_MainContent_Panel3').style.display = "none";
            document.getElementById('ctl00_MainContent_Button2').value = "подробнее";
            document.getElementById('ctl00_MainContent_ButtonPDF').style.display = "none";
        }
    }
    function Func_PDF() {
        var params = "menubar=no,location=no,resizable=YES,top=50,left=50,scrollbars=yes,status=no,width=750,height=531"

        window.open("rep_brak_PES.aspx" + document.getElementById('ctl00_MainContent_LabelQstring').value + "&count_str=" + document.getElementById('ctl00_MainContent_HiddenFieldCountString').value, "печать", params);
    }
</script>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"></asp:SqlDataSource>
        <table width="100%">
            <tr>
                <td style="text-align:left">
                    <strong>Архив испытаний средств защиты сторонних организаций</strong>
                </td>
                <td style="text-align:right">
                    <asp:Button ID="ButtonDelArch" runat="server" Text="удаление архива" style="background-color:Teal; color:White" />
                </td>
            </tr>
        </table>  
            
<fieldset style="vertical-align: middle; text-align: center; background-color:#DCDCDC;">
    <legend style="color: #A0522D; font-weight:bold; font-style: italic; font-size: 9px;">фильтр</legend>
        <fieldset style="vertical-align: middle; text-align: left; color:Blue;">
        <legend style="color: gray; font-weight: normal; font-style: italic; font-size: 9px;">временной интервал</legend>
            <asp:RadioButton ID="RadioButton1" Text="" GroupName="term" runat="server" 
                Checked="true" AutoPostBack="true" 
                oncheckedchanged="RadioButton1_CheckedChanged" />
            за <asp:DropDownList ID="DropDownList_Year" runat="server" AutoPostBack="true" 
                onselectedindexchanged="DropDownList_Year_SelectedIndexChanged"/> год&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:RadioButton ID="RadioButton2" Text="" GroupName="term" runat="server" 
                Checked="false" AutoPostBack="true" 
                oncheckedchanged="RadioButton2_CheckedChanged" />
            за период
            <asp:Panel ID="Panel2" runat="server" style="display:none">
            с 
            <radCln:RadDatePicker ID="RadDatePicker_date_s" runat="server"  
                    MaxDate="2050-12-31" MinDate="2007-01-01" Width="110px" Font-Bold="False" 
                    Font-Italic="False" AutoPostBack="true"
                    onselecteddatechanged="RadDatePicker_date_s_SelectedDateChanged">
                                <Calendar ID="Calendar1" runat="server" Skin="WebBlue" >
                                </Calendar>
                                <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium">
                                </DateInput>
                            </radCln:RadDatePicker>
            по
            <radCln:RadDatePicker ID="RadDatePicker_date_po" runat="server"  
                    MaxDate="2050-12-31" MinDate="2007-01-01" Width="110px" Font-Bold="False" 
                    Font-Italic="False" AutoPostBack="true" 
                    onselecteddatechanged="RadDatePicker_date_po_SelectedDateChanged">
                                <Calendar ID="Calendar2" runat="server" Skin="WebBlue" >
                                </Calendar>
                                <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium">
                                </DateInput>
                            </radCln:RadDatePicker>
            </asp:Panel>
        </fieldset>
        <table>
            <tr>
                <td>
        <fieldset style="vertical-align: middle; text-align: left;">
                <legend style="color: gray; font-weight:normal; font-style: italic; font-size: 9px;">название организации и бригады</legend>
                    <asp:DropDownList ID="DropDownListOrg" runat="server" AutoPostBack="true" 
                onselectedindexchanged="DropDownListOrg_SelectedIndexChanged" />
                    <asp:DropDownList ID="DropDownListBrig" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="DropDownListBrig_SelectedIndexChanged"/>
        </fieldset>                
                </td>
            </tr>
            <tr>
                <td>
        <fieldset style="vertical-align: middle; text-align: left;">
                <legend style="color: gray; font-weight:normal; font-style: italic; font-size: 9px;">группа средств защиты</legend>
                    <asp:DropDownList ID="DropDownListGroup" runat="server" AutoPostBack="true"
                    onselectedindexchanged="DropDownListGroup_SelectedIndexChanged">
                    </asp:DropDownList>
        </fieldset>                  
                </td>
            </tr>
        </table>  
        <asp:Button ID="Button1" runat="server" Text="показать" 
        onclick="Button1_Click" ForeColor="Sienna" />
</fieldset>  
    <asp:Panel ID="Panel1" Width="920px" ScrollBars="Horizontal" runat="server">      
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        AutoGenerateColumns="False" CellPadding="4" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
        AllowPaging="True" PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="50" 
            
            style="font-family: 'Times New Roman', Times, serif; font-size: x-small; text-align:justify;" 
            onpageindexchanged="GridView1_PageIndexChanged">
        <Columns>
                         <asp:BoundField HeaderText="№ пр-ла" ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Right" SortExpression="NUM_P" DataField="NUM_P" />
                          <asp:TemplateField HeaderText="Название организации" SortExpression="NAME_ORG" >
              
                <ItemTemplate>
                    <asp:Label ID="NAME_ORG" runat="server" Text='<%# Bind("NAME_ORG") %>' Width="120px" ></asp:Label>
                </ItemTemplate>
              </asp:TemplateField> 
            
        
            <asp:BoundField DataField="NAME_BRIG" HeaderText="Название бригады" 
                SortExpression="NAME_BRIG"  />
            <asp:BoundField DataField="NAME_GROUP" HeaderText="Название группы" 
                SortExpression="NAME_GROUP" />
            <asp:BoundField DataField="NAME" HeaderText="Наименование средства защиты" 
                SortExpression="NAME" />
            <asp:BoundField DataField="FAC_NUM" 
                HeaderText="Инв. или завод. номер" SortExpression="FAC_NUM" />                
            <asp:BoundField DataField="DATE_EXP" HeaderText="Дата испытания" 
                SortExpression="DATE_EXP" />
                <asp:TemplateField FooterText= "Испытано напряжением кВ">
               
               <HeaderTemplate>
                <table  border="1"  style="border-collapse: collapse; border-spacing: 1px; border-top-style: none; border-bottom-style: none; border-left-style: none; border-right-style: none;">
                <tr>
                <td colspan="4" align="center" style="border-top-style:none; border-left-style:none; border-right-style:none;">Испытано напряжением кВ </td>
                </tr>
                <tr>
                <td style="border-left-style:none; border-bottom-style:none; width:60px;" >Средства защиты</td> <td style="border-bottom-style:none; width:60px;">Изолиру- ющая часть</td> <td style="border-bottom-style:none;">Рабочая часть</td> <td style="border-bottom-style:none; border-right-style:none;">провод</td>
                </tr>
                </table>
               </HeaderTemplate>
                <ItemTemplate>
                <table  border="1" style="border-collapse: collapse; border-spacing: 1px; border-top-style: none; border-bottom-style: none; border-left-style: none; border-right-style: none; border-spacing:1px;">
                
                <tr>
                <td style="border-left-style:none; border-bottom-style:none; border-top-style:none; width:43px; text-align:center;"><asp:Label ID="V_PROT" runat="server"  Text='<%# Bind("V_PROT") %>' ></asp:Label></td> <td style="border-bottom-style:none; border-top-style:none; width:42px; text-align:center"> <asp:Label ID="V_ISOL" runat="server"  Text='<%# Bind("V_ISOL") %>' ></asp:Label></td><td style="border-bottom-style:none; border-top-style:none; width:39px; text-align:center"><asp:Label ID="V_WORK" runat="server"  Text='<%# Bind("V_WORK") %>' ></asp:Label></td> <td style="border-bottom-style:none; border-right-style:none; border-top-style:none; width:45; text-align:center"><asp:Label ID="V_WIRE" runat="server"  Text='<%# Bind("V_WIRE") %>' ></asp:Label></td>
                </tr>
                </table>
                   
                </ItemTemplate>
      
                 </asp:TemplateField>
                  <asp:BoundField DataField="V_INDIC" HeaderText="Напр. инди- кации" 
                SortExpression="V_INDIC" />
               <asp:TemplateField >
               <HeaderTemplate>
                <table  border="1px" cellspacing="1px" style="border-collapse: collapse; border-spacing: 0px; border-top-style: none; border-bottom-style: none; border-left-style: none; border-right-style: none;">
                <tr>
                <td colspan="3" align="center" style="border-top-style:none; border-left-style:none; border-right-style:none;">Ток протекающий через изделие, мА </td>
                </tr>
                <tr>
                <td style="border-left-style:none; border-bottom-style:none; width:43px; text-align:center " >Правое</td> <td style="border-bottom-style:none;width:43px; text-align:center" >Левое</td> <td style="border-bottom-style:none; border-right-style:none;width:43px; text-align:center">Указа- тель</td>
                </tr>
                </table>
               </HeaderTemplate>
                <ItemTemplate>
                 <table  border="1"  style="border-collapse: collapse; border-spacing: 1px; border-top-style: none; border-bottom-style: none; border-left-style: none; border-right-style: none;">
                <tr><td style="border-left-style:none; border-bottom-style:none; border-top-style:none; width:43px; text-align:center;" ><asp:Label ID="C_RIGHT" runat="server"  Text='<%# Bind("C_RIGHT") %>' Width="45px"></asp:Label></td> <td style="border-bottom-style:none; border-top-style:none; width:41px; text-align:center;"> <asp:Label ID="C_LEFT" runat="server"  Text='<%# Bind("C_LEFT") %>' Width="40px"></asp:Label></td><td style="border-bottom-style:none; border-top-style:none; text-align:center;"><asp:Label ID="C_POINTER" runat="server"  Text='<%# Bind("C_POINTER") %>' Width="40px"></asp:Label></td>  </tr>
                </table>
                  
                   
                </ItemTemplate>
      
                 </asp:TemplateField>
            <asp:BoundField DataField="RESULT_EXP" HeaderText="Резуль- тат" 
                SortExpression="RESULT_EXP" ItemStyle-Width="40" >
            <ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>            
            <asp:BoundField DataField="T_WATER" HeaderText="Тем-ра воды" 
                SortExpression="T_WATER" />
            <asp:BoundField DataField="T_AIR" HeaderText="Тем-ра воздуха" SortExpression="T_AIR" />
            <asp:BoundField DataField="HUMIDITY" HeaderText="Влаж- ность" 
                SortExpression="HUMIDITY" />
            <asp:BoundField DataField="PRESSURE" HeaderText="Давле- ние" 
                SortExpression="PRESSURE" />
            <asp:BoundField DataField="DATE_IN" HeaderText="Дата поступления" 
                SortExpression="DATE_IN" />
            <asp:BoundField DataField="DATE_PROT" HeaderText="Дата выдачи" 
                SortExpression="DATE_PROT" />
            <asp:BoundField DataField="NUM_ACT" HeaderText="Номер акта" 
                SortExpression="NUM_ACT" />
            <asp:BoundField DataField="NOTE" HeaderText="Приме- чание" SortExpression="NOTE" />
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
    </asp:GridView>
    </asp:Panel>
    <asp:Label ID="LabelQstring2" runat="server" Visible="false" />
    <asp:HiddenField ID="LabelQstring" runat="server"/>
    <asp:HiddenField ID="HiddenFieldCountString" runat="server"/>
    <fieldset style="vertical-align: middle; text-align: right;">
    <asp:Panel ID="Panel3" runat="server" style="display:none; text-align:center;" Width="455px">
        <strong><em>Количество СЗ испытанных и забракованных за выбранный период</em></strong> 
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
        <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" 
            DataSourceID="SqlDataSource2" onitemdatabound="RadGrid2_ItemDataBound"
                    Skin="WebBlue" AutoGenerateColumns="False" Width="450px">
                <MasterTableView   AutoGenerateColumns="False" DataSourceID="SqlDataSource2" 
                    DataKeyNames="K_GROUP" Width="100%" BorderColor="InactiveCaptionText" 
                    BorderWidth="1px" GridLines="Both" ShowFooter="true" 
                    NoMasterRecordsText="Нет данных">
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <radG:GridBoundColumn DataField="NAME_GROUP" UniqueName="column" 
                            HeaderText="Название группы СЗ">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="ALL_COUNT" UniqueName="column1" 
                            HeaderText="кол-во испытанных">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="BRAK_COUNT" UniqueName="column2" 
                            HeaderText="кол-во забракованных">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </radG:GridBoundColumn>
                    </Columns>
               </MasterTableView>
            <ClientSettings >
                <Selecting AllowRowSelect="false"  />
            </ClientSettings>
                <FooterStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Wrap="True" BorderWidth="0" />
        </radG:RadGrid>
    </asp:Panel>
    <asp:Label ID="Label_Stat" runat="server" ForeColor="Blue" /><br />
    <asp:Button ID="ButtonPDF" runat="server" Text="PDF" 
            OnClientClick="Func_PDF(); return false;" ForeColor="#666666" 
            Visible="false" BorderColor="Gray" />
    <asp:Button ID="Button2" runat="server" Text="подробнее" 
            OnClientClick="Func_Panel3(); return false;" ForeColor="#666666" 
            Visible="false" BorderColor="Gray" /> 
    </fieldset>
</asp:Content>