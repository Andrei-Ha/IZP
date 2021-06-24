<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="logistics.aspx.cs" ValidateRequest="false"  Inherits="logistics" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server"> 
    <style type="text/css">
    .Hid{
        visibility:hidden;
    }    
    </style>
    <script type="text/javascript">
        function GetParam(id_cell) {
            if (id_cell>0) {
                document.getElementById("ctl00_MainContent_Label_Temp").innerText = id_cell;
                var nodes = document.getElementsByTagName("INPUT");
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].type == "checkbox" && nodes[i].checked == true) {
                        //nodes[i].checked = bool_var;
                        let str = elem = nodes[i].id;
                        let pos1 = str.indexOf("ctl00_MainContent");
                        let pos2 = str.indexOf("_check_Bind_Cell");
                        let str_id = str.slice(pos1, pos2);
                        document.getElementById(str_id + "_TextBox_CELL").value = id_cell;
                        document.getElementById(str_id + "_TextBox_CELL").setAttribute('readonly', true);
                    }
                }
                //document.getElementById("ctl00_MainContent_Label_Temp").innerText = id_cell;
                document.getElementById("ctl00_MainContent_ButtonSave").click();
            }
        }
        function CheckAll() {
            var bool_var = false;
            var i_count = 0;
        if (document.getElementById('ctl00_MainContent_Button_CheckAll').value == "отметить все") {
            document.getElementById('ctl00_MainContent_Button_CheckAll').value = "снять все";
            bool_var = true;
        } else {
            document.getElementById('ctl00_MainContent_Button_CheckAll').value = "отметить все";
            bool_var = false;
        }
            var nodes = document.getElementsByTagName("INPUT");
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].type == "checkbox") {
                    nodes[i].checked = bool_var;
                    i_count++;
                }
            }
            document.getElementById("Label_count_check").innerText = i_count;
        }
        function OnCheck(elem) {
            //<input id="ctl00_MainContent_RadGrid2_ctl01_ctl06_check_Bind_Cell" type="checkbox">
            //name="ctl00$ctl00_MainContent$RadGrid2$ctl01$ctl06$check_Bind_Cell">
            //ctl00_MainContent_RadGrid2_ctl01_ctl06_Label_CELL
            /*let str = elem.innerHTML;
            let pos1 = str.indexOf("ctl00_MainContent");
            let pos2 = str.indexOf("_check_Bind_Cell");
            let str_id = str.slice(pos1, pos2);
            //document.getElementById("ctl00_MainContent_Label_Temp").innerText = str_id;
            //document.getElementById(str_id + "_Label_CELL").innerText = str_id;
            document.getElementById(str_id + "_TextBox_CELL").value = "4";
            document.getElementById(str_id + "_TextBox_CELL").setAttribute('readonly', true);*/
        }
        function ShowCabinetsForm() {
            var InsertWin = window.radopen("../Edit/Cabinets.aspx", "InsertListDialog");
            return false;
        }        
    </script>
    <asp:Label ID="Label_Temp" runat="server" ></asp:Label>
    <asp:HiddenField ID="HiddenFieldSDS2" runat="server" />      
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
            <radw:RadWindowManager ID="RadWindowManager1" runat="server" Skin="WebBlue" Language="ru-RU">
                <Windows>
                    <radw:RadWindow ID="InsertListDialog" runat="server" Title="выбор ячейки" OnClientClose = "GetParam();"
                         ReloadOnShow="True" Modal="true"  Width="600px" Height="600px"  Behavior="Close" Skin="WebBlue" SkinsPath="~/RadControls/Window/Skins" Left="" NavigateUrl="" Top="" />
                    
                </Windows>
            </radw:RadWindowManager>
<div style="text-align:center"><span style="font-weight: normal"><strong>Определение местонахождения СЗ переданных лаборатории СИЗП</strong></span></div>
<fieldset style="vertical-align: middle; text-align: center; color:Olive">
    <legend style="color: olive; font-weight:normal; font-style: italic; font-size: 9px;">фильтр для отображения информации</legend>
<table width="100%" style="background-color:lightskyblue">
    <tr>
        <td colspan="3" style="text-align:center" >
            <fieldset style="vertical-align: middle; text-align: center;">
                <legend style="color: blue; font-weight:normal; font-style: italic; font-size: 9px;">название организации и бригады</legend>
                    <asp:DropDownList ID="DropDownListOrg" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListOrg_SelectedIndexChanged" />
                <br />
                    <asp:DropDownList ID="DropDownListBrig" runat="server"/>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3"  style="text-align:center">
            <fieldset style="vertical-align: middle; text-align: center;">
                <legend style="color: blue; font-weight:normal; font-style: italic; font-size: 9px;">группа средств защиты</legend>
                    <asp:DropDownList ID="DropDownListGroup" runat="server">
                    </asp:DropDownList>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td style="width:300px"> </td>
        <td colspan="2" style="color:white; font-weight:600;">
            <div>
            <asp:RadioButtonList AutoPostBack="false" ID="RadioButtonList1" runat="server"  RepeatDirection="Horizontal" Width="400px" TextAlign="right">
                <asp:ListItem Text="не привязанные к ячейке" Value="1" Selected="True" />
                <asp:ListItem Text="привязанные к ячейке" Value="2" />
            </asp:RadioButtonList>
                
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="text-align:center">
            <asp:Button ID="ButtonFilter" runat="server" Text="применить" onclick="ButtonFilter_Click" />
        </td>
    </tr>
</table>
</fieldset>
<br />
<div style="text-align:right">
    <asp:Button ID="Button_Add" runat="server" Visible="false" OnClientClick="return ShowCabinetsForm();" Text="Привязать к ячейке отмеченные" Width="250px" ForeColor="White" Font-Bold="True" BackColor="DarkGray" />
    <asp:Button ID="Button_CheckAll" runat="server" Visible="false"  OnClientClick="CheckAll(); return false;" Text="отметить все" Width="200px" ForeColor="White" Font-Bold="True" BackColor="LightGreen" />
    <label id="Label_count_check"></label>
</div>
<br />
<radG:RadGrid ID="RadGrid2" runat="server" EnableAJAX="False" GridLines="None" 
        DataSourceID="SqlDataSource2" Skin="WebBlue"  AllowPaging="False"  
        AutoGenerateColumns="false" onitemdatabound="RadGrid2_ItemDataBound" 
        ondatabound="RadGrid2_DataBound">
        <MasterTableView  DataKeyNames="ID" NoMasterRecordsText="Нет записей соответствующих выбранному фильтру" >
                    <GroupByExpressions>
                        <radG:GridGroupByExpression>
                            <SelectFields>
                                <radG:GridGroupByField  FieldAlias="Организация" FieldName="NAME_ORG" ></radG:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <radG:GridGroupByField FieldName="NAME_ORG" SortOrder="Ascending"/> 
                            </GroupByFields>
                        </radG:GridGroupByExpression>
                        <radG:GridGroupByExpression>
                            <SelectFields>
                                <radG:GridGroupByField  FieldAlias="бригада" FieldName="NAME_BRIG" ></radG:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <radG:GridGroupByField FieldName="NAME_BRIG" SortOrder="Ascending"/> 
                            </GroupByFields>
                        </radG:GridGroupByExpression>
                    </GroupByExpressions>
            <Columns>
                        <radg:GridTemplateColumn UniqueName="TemplateEditColumn1" HeaderText="№ п/п">
                                    <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                                    <ItemStyle Font-Bold="True" Font-Italic="True" ForeColor="Black" Font-Size="XX-Small" HorizontalAlign="Center" /> 
                                    <FooterStyle ForeColor="Black" />                                                                   
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Italic="true"  Text='<%# Convert.ToString( Container.DataSetIndex + 1 )+"." %>'></asp:HyperLink>
                            </ItemTemplate>
                        </radg:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="NAME_GROUP" DataType="System.String" HeaderText="группа СЗ"
                            SortExpression="NAME_GROUP" UniqueName="NAME_GROUP">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="left" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Name_Group" runat="server" Font-Italic="true" /></div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="NAME" DataType="System.String" HeaderText="название СЗ"
                            SortExpression="NAME" UniqueName="NAME">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="left" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Name" runat="server" Font-Italic="true"  Text='<%# Bind("NAME") %>'/></div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="FAC_NUM" DataType="System.String" HeaderText="зав. ном.(дата выд. прот-ла)"
                            SortExpression="FAC_NUM" UniqueName="FAC_NUM">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="left" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Size="X-Small"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Fac_Num" runat="server" Font-Italic="true" /></div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                <radG:GridTemplateColumn DataField="CELL" DataType="System.String" HeaderText="номер ячейки"
                            SortExpression="CELL" UniqueName="CELL">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="left" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <div><asp:TextBox ID="TextBox_CELL" runat="server" Width="18" Font-Italic="true"  Text='<%# Bind("CELL") %>'/></div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                <radG:GridTemplateColumn DataField="BIND_CELL" DataType="System.Char" HeaderText="отметить"
                             UniqueName="BIND_CELL">
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="LightGreen" />
                            <ItemTemplate>
                                <div><asp:CheckBox ID="check_Bind_Cell" runat="server" onchange="OnCheck(this)" style="background-color:LightGreen;" />
                                </div>
                            </ItemTemplate>
                </radG:GridTemplateColumn> 
            </Columns>
        </MasterTableView>
</radG:RadGrid>
        <radA:RadAjaxManager ID="RadAjaxManager2" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="DropDownListOrg">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="DropDownListBrig"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>
        <radA:AjaxLoadingPanel ID="AjaxLoadingPanel2" runat="server">
            <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/LoadingProgressBar.gif" />
        </radA:AjaxLoadingPanel>
 <div style="text-align:right">
     <asp:Button ID="ButtonSave" CssClass="Hid" runat="server"  Visible="true" onclick="ButtonSave_Click"  ForeColor="White" Font-Bold="True" BackColor="DarkGray"/>
 </div>
</asp:Content>

