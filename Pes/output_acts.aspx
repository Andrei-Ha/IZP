<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="output_acts.aspx.cs" Inherits="output_acts" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        function OpenPrint(arg) {
            var params = "menubar=no,location=no,resizable=YES,scrollbars=yes,status=no,width=750"

            window.open(arg, "печать", params);
            //var InsertWin = window.radopen("edit/protocols/protocol1.aspx", "PrintProtocolDialog");
            return false;
        }
        function ShowInsertForm() {
            if (window.document.all.item("MainContent_Button1") != null) {
                window.document.all.item("MainContent_Button1").style.display = "none";
                window.document.all.item("MainContent_Button_DelAct").style.display = "none";
                window.document.all.item("MainContent_CheckBox_Act").style.display = "none";
                window.document.all.item("MainContent_Label_Act").style.display = "none";
            }
            var InsertWin = window.radopen("edit/add_record.aspx", "InsertListDialog");
            return false;
        }
        function refreshGrid1(arg) {
                //window.document.all.item("MainContent_RadioButton1").checked = "checked";
                window["<%=RadGrid2.ClientID %>"].AjaxRequest('<%= RadGrid2.UniqueID %>', 'Rebind2');
                window["<%=RadGrid1.ClientID %>"].AjaxRequest('<%= RadGrid1.UniqueID %>', 'Rebind1');
        }
    </script>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>">
         <SelectParameters>

         </SelectParameters>
         </asp:SqlDataSource>
        <table>
        <tr>
            <td>
                <asp:Button ID="Button_Add" runat="server" OnClientClick="return ShowInsertForm();" Text="Добавить средства защиты" Width="200px" ForeColor="White" Font-Bold="True" BackColor="DarkGray" />
            </td>
            <td>
                <asp:RadioButton ID="RadioButton1" Text="ввод данных" GroupName="acts" runat="server" Checked="true" AutoPostBack="true" 
                    oncheckedchanged="RadioButton1_CheckedChanged" />
                <asp:Label ID="InjectScript" runat="server"></asp:Label>
                <asp:Label ID="Label1" runat="server" Visible="true"/>
                <asp:HiddenField ID="HiddenField_ORG" runat="server" Value="пусто" />
                <asp:HiddenField ID="HiddenField_NUM_ACT" runat="server" Value="пусто" />
                <asp:Label ID="Label_Array" runat="server" Visible="false" Text="массив" />
            </td>
            <td>
                <asp:RadioButton ID="RadioButton2" Text="все акты за " GroupName="acts" runat="server" Checked="false"
                    oncheckedchanged="RadioButton1_CheckedChanged" AutoPostBack="true" />
                <asp:Label ID = "Label_Year" runat="server" />
            </td>
        </tr>
        <tr>
            <td rowspan="2" style="width:30%">
        <radG:RadGrid ID="RadGrid1" DataSourceID="SqlDataSource1" runat="server" Width="97%" EnableAJAX="false"
            PageSize="10" AllowSorting="False" AllowMultiRowSelection="False" Skin="WebBlue" 
                    onitemdatabound="RadGrid1_ItemDataBound" 
                    OnItemCommand="RadGrid1_ItemCommand" ShowStatusBar="false"
            AllowPaging="True" ShowGroupPanel="False" GridLines="none" 
                    onpageindexchanged="RadGrid1_PageIndexChanged" >
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView Width="100%" DataKeyNames="K_ORGAN,NAME_ORG,NUM_ACT" AutoGenerateColumns="false" BorderColor="InactiveCaptionText" BorderWidth="1px" GridLines="Both" NoMasterRecordsText="Нет данных">
            <Columns>
                        <radg:GridTemplateColumn UniqueName="TemplateEditColumn1" HeaderText="№ п/п">
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />                            
                            <ItemTemplate>
                                <asp:HyperLink ID="EditLink0" runat="server" Font-Italic="true"  Text='<%# Convert.ToString( Container.DataSetIndex + 1 )+"." %>'></asp:HyperLink>
                            </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Font-Italic="True" ForeColor="Black" Font-Size="XX-Small" HorizontalAlign="Center" />                           
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="NUM_ACT" DataType="System.String" HeaderText="№ акта"
                            SortExpression="NUM_ACT" UniqueName="NUM_ACT">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="center" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Num_Act" runat="server" Font-Italic="true"  Text='<%# Bind("NUM_ACT") %>'/> &nbsp;</div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>                                               
                        <radG:GridTemplateColumn HeaderText="Организация (включая все бригады)"
                                    SortExpression="FAC_NUM" UniqueName="FAC_NUM" >
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="X-Small" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:left">
                                    <asp:Label ID="Label_ORG" runat="server" Font-Italic="false" style="color:Black" ></asp:Label>
                                    <asp:Label ID="Label_Quantity" runat="server" Font-Italic="true" style="color:Blue"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>                                                     
            </Columns>
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="true">
                    <Selecting AllowRowSelect="false" />
            </ClientSettings>
            <SelectedItemStyle BackColor="LightBlue" />
        </radG:RadGrid>        
            </td>
            <td colspan="2" style="width:70%">
        <radG:RadGrid ID="RadGrid2" runat="server" EnableAJAX="False" GridLines="None" DataSourceID="SqlDataSource2" onitemdatabound="RadGrid2_ItemDataBound"
                    Skin="WebBlue"  AllowPaging="False" 
                    EnableAJAXLoadingTemplate="True">
                <MasterTableView AutoGenerateColumns="false" DataSourceID="SqlDataSource2" DataKeyNames="K_GROUP" Width="100%" BorderColor="InactiveCaptionText" BorderWidth="1px" GridLines="Both" ShowFooter="true" NoMasterRecordsText="Нет данных" >
                    <Columns>
                        <radg:GridTemplateColumn UniqueName="TemplateEditColumn1" HeaderText="№ п/п">
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                                    <ItemStyle Font-Bold="False" Font-Italic="True" ForeColor="Black" Font-Size="XX-Small" HorizontalAlign="Center" /> 
                                    <FooterStyle ForeColor="Black" />                                                                   
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Italic="true"  Text='<%# Convert.ToString( Container.DataSetIndex + 1 )+"." %>'></asp:HyperLink>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:HyperLink ID="HyperLink_Protcl" runat="server" Font-Bold="false" Font-Size="XX-Small" Font-Italic="true"  Text="-"></asp:HyperLink>
                            </FooterTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="NAME_GROUP" DataType="System.String" HeaderText="Наименование работ"
                            SortExpression="NAME_GROUP" UniqueName="NAME_GROUP">
                            <ItemStyle Font-Bold="False" Font-Italic="True" ForeColor="Black" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Left" ForeColor="Black" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Name_Group" runat="server" Font-Italic="true"  Text='<%# Bind("NAME_GROUP") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_ProtcltName_Group" runat="server" Font-Bold="false" Font-Italic="true" Text="-" />
                            </FooterTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="EDIZM" DataType="System.String" HeaderText="Ед.изм."
                            SortExpression="EDIZM" UniqueName="EDIZM">
                            <ItemStyle Font-Bold="False" Font-Italic="True" ForeColor="Black" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle ForeColor="Black" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Label_Edizm" runat="server" Font-Italic="true"  Text='<%# Bind("EDIZM") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_ProtclEdizm" runat="server" Font-Bold="false" Font-Italic="true" Text="-"/>
                            </FooterTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="QUANTITY" DataType="System.Int16" HeaderText="Кол-во"
                            SortExpression="QUANTITY" UniqueName="QUANTITY">
                            <ItemStyle Font-Bold="False" Font-Italic="True" ForeColor="Black" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle ForeColor="Black" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Label_Quantity" runat="server" Font-Italic="true"  Text='<%# Bind("QUANTITY") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_ProtclQuantity" runat="server" Font-Bold="false" Font-Italic="true" Text="-"/>
                            </FooterTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="TARIFF" DataType="System.Int32" HeaderText="Цена"
                            SortExpression="TARIFF" UniqueName="TARIFF">
                            <ItemStyle Font-Bold="False" Font-Italic="True" ForeColor="Black" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Label_Tariff" runat="server" Font-Italic="true"  Text='<%# Bind("TARIFF") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate >
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_ProtclTariff" runat="server" Font-Bold="false" ForeColor="Black" Font-Italic="true" Text="-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a style="color:Blue">ИТОГО без НДС</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        НДС 20%
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a style="color:Red">ВСЕГО с НДС</a>
                                    </td>
                                </tr>
                            </table>
                            </FooterTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="PRICE" DataType="System.Int32" HeaderText="Сумма"
                            SortExpression="PRICE" UniqueName="PRICE">
                            <ItemStyle Font-Bold="False" Font-Italic="True" ForeColor="Black" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Label_Price" runat="server" Font-Italic="true"  Text='<%# Bind("PRICE") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate >
                            <table>
                                <tr>
                                    <td>
                                       <asp:Label ID="Label_ProtclPrice" runat="server" Font-Bold="false" ForeColor="Black" Font-Italic="true" Text="-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_PriceNoNDS" runat="server" ForeColor="Blue" Font-Italic="true" Text="-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_NDS" runat="server" ForeColor="White" Font-Italic="true" Text="-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_PriceYesNDS" runat="server" ForeColor="Red" Font-Italic="true" Text="-" />
                                    </td>
                                </tr>
                            </table>        
                            </FooterTemplate>
                        </radG:GridTemplateColumn>                          
                    </Columns>
               </MasterTableView>
            <ClientSettings >
                <Selecting AllowRowSelect="true"  />
            </ClientSettings>
                <FooterStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Wrap="True" BorderWidth="0" />
            <PagerStyle Mode="NumericPages" />
        </radG:RadGrid>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                <asp:Button runat="server" ID="Button_DelAct" style="color:Red" OnClientClick="return confirm('Вы уверены, что хотите удалить все входящие в этот акт средства защиты?');" onclick="Button_DelAct_Click" Text="удалить" Visible="false" />
            </td>
            <td style="text-align:right;width:80%">
        <asp:CheckBox ID="CheckBox_Act" runat="server" Checked="false" AutoPostBack="true" Visible="false" oncheckedchanged="CheckBox_Act_CheckedChanged" />
        <asp:Label ID="Label_Act" runat="server" Text="без выдачи номера акта" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="Button1" onclick="Button1_Click" Text="Акт" Visible="false" />
            </td>
        </tr>
        </table>
        <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="RadGrid2">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="Button1" />
                        <radA:AjaxUpdatedControl ControlID="Label1" />
                        <radA:AjaxUpdatedControl ControlID="Label2" />
                        <radA:AjaxUpdatedControl ControlID="InjectScript" />
                        <radA:AjaxUpdatedControl ControlID="HiddenField_ORG" />
                        <radA:AjaxUpdatedControl ControlID="HiddenField_NUM_ACT" />
                        <radA:AjaxUpdatedControl ControlID="Label_Array" />
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="AjaxLoadingPanel1" />
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager> 
            <radw:RadWindowManager ID="RadWindowManager1" runat="server" Skin="WebBlue" Language="ru-RU">
                <Windows>
                    <radw:RadWindow ID="InsertListDialog" runat="server" Title="добавление" OnClientClose = "refreshGrid1(true);"
                         ReloadOnShow="True" Modal="true"  Width="600px" Height="600px"  Behavior="Close" Skin="WebBlue" SkinsPath="~/RadControls/Window/Skins" Left="" NavigateUrl="" Top="" />
                </Windows>
            </radw:RadWindowManager>
</asp:Content>

