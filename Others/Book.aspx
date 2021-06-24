<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Book.aspx.cs" Inherits="Book" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server"> 
    <script type="text/javascript">
        function OpenPrint(arg) {
            var params = "menubar=no,location=no,resizable=YES,top=50,left=50,scrollbars=yes,status=no,width=400,height=566"

            window.open(arg,"печать",params);
            //var InsertWin = window.radopen("edit/protocols/protocol1.aspx", "PrintProtocolDialog");
            return false;
        }
        function ShowInsertValForm(formin, id, k_group, rowIndex)
            {
             var grid = window["<%= RadGrid1.ClientID %>"];
                
             //var rowControl = grid.MasterTableView.Rows[rowIndex].Control; 
             //grid.MasterTableView.SelectRow(rowControl, true);

             window.radopen("" + formin + id+"&k_group="+k_group, "InsertValListDialog");
                return false; 
            }
            function refreshGrid1(arg) {
                if (window.document.all.item("MainContent_Button_Protcl") != null) {
                    window.document.all.item("MainContent_Button_Protcl").style.display = "none";
                }
                window["<%=RadGrid1.ClientID %>"].AjaxRequest('<%= RadGrid1.UniqueID %>', 'Rebind1');
                window["<%=RadGrid2.ClientID %>"].AjaxRequest('<%= RadGrid2.UniqueID %>', 'Rebind2');
            }
            function refreshGrid2(arg) {
                window["<%=RadGrid2.ClientID %>"].AjaxRequest('<%= RadGrid2.UniqueID %>', 'RebindAndNavigate2');
            }            
            function CloseWin(){
                window.close();
                return false;
            }
            function fnHide() {
                window.document.all.item("MainContent_LabLab").style.display = "inline";
                window.setTimeout(fnHide2, 3000);
            }
            function fnHide2() {
                window.document.all.item("MainContent_LabLab").style.display = "none";
            }
            
    </script>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>">
         </asp:SqlDataSource>
        <table>
        <tr>
            <td rowspan="2">
                <asp:Label ID="Label1" runat="server" />
            </td>
            <td style=" border-width:thin; border-color:Gray">
                    <asp:RadioButton ID="RadioButton1" Text="ввод данных" GroupName="protcls" runat="server" Checked="true" AutoPostBack="true" oncheckedchanged="RadioButton1_CheckedChanged" />
                    <asp:RadioButton ID="RadioButton2" Text="выданные протоколы" GroupName="protcls" runat="server" Checked="false" oncheckedchanged="RadioButton1_CheckedChanged" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="LabelOrg" runat="server" style="font-size:x-small; font-style:italic; color:Blue" />&nbsp;
                <asp:HiddenField ID="HiddenFieldNP" runat="server" Value="пусто" />
                <asp:HiddenField ID="HiddenFieldYEAR" runat="server" Value="пусто" />
                <asp:Label ID="InjectScript" runat="server"></asp:Label>            
            </td>
        </tr>
        <tr>
            <td rowspan="2" style="width:30%">
        <radG:RadGrid ID="RadGrid1" DataSourceID="SqlDataSource1" runat="server" Width="97%" EnableAJAX="false"
            PageSize="10" AllowSorting="False" AllowMultiRowSelection="False" Skin="WebBlue" 
                    onitemdatabound="RadGrid1_ItemDataBound" ShowStatusBar="false"
            AllowPaging="True" ShowGroupPanel="False" GridLines="none" 
                    OnItemCommand="RadGrid1_ItemCommand" 
                    onpageindexchanged="RadGrid1_PageIndexChanged">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView Width="100%" DataKeyNames="YEAR,NUM_P,NAME_BRIG,NAME_ORG,K_ORGAN,K_BRIG,PERIOD" AutoGenerateColumns="false" BorderColor="InactiveCaptionText" BorderWidth="1px" GridLines="Both" NoMasterRecordsText="Нет данных">
            <Columns>
                        <radG:GridTemplateColumn HeaderText="№ прот." UniqueName="NUM_P" >
                                    <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="Medium" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center">
                                    <asp:Label ID="Label_Num_p" runat="server" />
                                </div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn HeaderText="Организация(бригада), группа СЗ (дата выд. прот.)"
                                    SortExpression="FAC_NUM" UniqueName="FAC_NUM" >
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="X-Small" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center">
                                    <asp:Label ID="Label_ORG" runat="server" Font-Italic="true" style="color:Blue" ></asp:Label><br />
                                    <asp:Label ID="Label_GROUP" runat="server" Font-Italic="true" style="color:Gray"></asp:Label><br />
                                    <asp:Label ID="Label_Date_prot" runat="server" Font-Italic="true" Font-Size="XX-Small" style="color:Green" />
                                </div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn HeaderText="кол-во ед." UniqueName="QUANTITY" >
                                    <ItemStyle Font-Bold="False" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="X-Small" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center">
                                    <asp:Label ID="Label_Quantity" runat="server" />
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
            <td style="width:70%">
        <radG:RadGrid ID="RadGrid2" runat="server" EnableAJAX="False" GridLines="None" DataSourceID="SqlDataSource2" 
                    Skin="WebBlue" onitemdatabound="RadGrid2_ItemDataBound" AllowPaging="False" 
                    EnableAJAXLoadingTemplate="True">
                <MasterTableView AutoGenerateColumns="false" DataSourceID="SqlDataSource2" DataKeyNames="ID_,K_GROUP,NUM_P,YEAR,PERIOD" Width="100%" BorderColor="InactiveCaptionText" BorderWidth="1px" GridLines="Both" ShowFooter="false"  NoMasterRecordsText="Нет данных">
                    <Columns>
                        <radg:GridTemplateColumn UniqueName="TemplateEditColumn1" HeaderText="№ п/п">
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />                            
                            <ItemTemplate>
                                <asp:HyperLink ID="EditLink0" runat="server" Font-Italic="true"  Text='<%# Convert.ToString( Container.DataSetIndex + 1 )+"." %>'></asp:HyperLink>
                            </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Font-Italic="True" ForeColor="Black" Font-Size="XX-Small" HorizontalAlign="Center" />                           
                        </radg:GridTemplateColumn> 
                        <radG:GridBoundColumn DataField="NAME" DataType="System.Char" HeaderText="средство защиты"
                            SortExpression="NAME" UniqueName="NAME" ReadOnly="True">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="X-Small" HorizontalAlign="Center"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                        </radG:GridBoundColumn>
                        <radG:GridTemplateColumn HeaderText="заводской № / (№ ячейки)" SortExpression="FAC_NUM" UniqueName="FAC_NUM" >
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="X-Small" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:HyperLink ID="ValLink" runat="server" Font-Italic="true"  Text='<%# Bind("FAC_NUM") %>'></asp:HyperLink>&nbsp;&nbsp;/&nbsp;&nbsp;   
                                    <asp:Label ID="Label_Cell" runat="server" Font-Italic="true" Text ='<%# Bind("CELL") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="DATE_IN" DataType="System.DateTime" HeaderText="дата поступлен."
                            SortExpression="DATE_IN" UniqueName="DATE_IN" ReadOnly="True" >
                            <HeaderStyle Width="75px" Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemStyle Font-Bold="True" Font-Italic="True" ForeColor="Black" HorizontalAlign="Center" BorderWidth="1px" Font-Size="XX-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="DATE_EXP" DataType="System.DateTime" HeaderText="дата испыт."
                            SortExpression="DATE_EXP" UniqueName="DATE_EXP" ReadOnly="True" >
                            <HeaderStyle Width="75px" Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemStyle Font-Bold="True" Font-Italic="True" ForeColor="Black" HorizontalAlign="Center" BorderWidth="1px" Font-Size="XX-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridTemplateColumn HeaderText="рез-ты измерений">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="XX-Small" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" Width="105" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Values" runat="server" Font-Italic="true"></asp:Label></div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn HeaderText="раб.параметры">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="XX-Small" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" Width="105" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Params" runat="server" Font-Italic="true"></asp:Label></div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn HeaderText="рез-т">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="True" Font-Size="XX-Small" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:Label ID="Resul" runat="server" Font-Italic="true"></asp:Label></div>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                    </Columns>
               </MasterTableView>
            <ClientSettings >
                <Selecting AllowRowSelect="true"  />
            </ClientSettings>
                <FooterStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Wrap="False" BorderWidth="0" />
            <PagerStyle Mode="NumericPages" />
        </radG:RadGrid>
        <asp:Label ID="Label_Warning" runat="server" style="color:Red"/>
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                <asp:Label ID="Label_Protcl" runat="server" Visible="false"  /> 
                <asp:Button ID="Button_Protcl" runat="server" Text="выдать протокол" 
                    Visible="false" onclick="Button_Protcl_Click" />
                <asp:Button ID="Button_Admin" runat="server" Text="<-! сформировать заново !->" 
                    Visible="false" onclick="Button_Protcl_Click" ForeColor="Red" />
                    <asp:Label ID="Label2" runat="server" Visible="true"  />
            </td>
        </tr>
        </table>
        <asp:Literal ID="Literal1" runat="server" />
            <radw:RadWindowManager ID="RadWindowManager1" runat="server" Skin="WebBlue" Language="ru-RU">
                <Windows>
                    <radw:RadWindow ID="InsertListDialog" runat="server" Title="добавление"
                         ReloadOnShow="True" Modal="True"  Width="550px" Height="600px"  Behavior="Close" Skin="WebBlue" SkinsPath="~/RadControls/Window/Skins" Left="" NavigateUrl="" Top="" />
                    <radw:RadWindow ID="InsertValListDialog" runat="server" Title="ввод данных испытания"
                         ReloadOnShow="True" Modal="True"  Width="550px" Height="450px"  Behavior="Close" Skin="WebBlue" SkinsPath="~/RadControls/Window/Skins" Left="" NavigateUrl="" Top="" />
                    <radw:RadWindow ID="PrintProtocolDialog" runat="server" Title="печать протокола"
                         ReloadOnShow="True" Modal="True"  Width="550px" Height="450px"  Behavior="Close" Skin="WebBlue" SkinsPath="~/RadControls/Window/Skins" Left="" NavigateUrl="" Top="" />
                </Windows>
            </radw:RadWindowManager>
        <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="RadGrid2">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="LabelOrg" />
                        <radA:AjaxUpdatedControl ControlID="Button_Protcl" />
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="AjaxLoadingPanel1" />
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>                     
</asp:Content>

