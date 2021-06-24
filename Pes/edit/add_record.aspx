<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add_record.aspx.cs" Inherits="add_record" %>
<%@ Register Assembly="RadComboBox.Net2" Namespace="Telerik.WebControls" TagPrefix="radC" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <style type="text/css">
      table {
        width: 90%; /* Ширина таблицы */
        border: 1px solid yellow; /* Рамка вокруг таблицы */
        margin: auto;
           }
   #col1 {
    width: 25%; /* Ширина первой колонки */
    background: #f0f0f0; /* Цвет фона первой колонки */
   }
   #col2 {
    width: 75%; /* Ширина второй колонки */
    background: #fc5; /* Цвет фона второй колонки */
    text-align: left; /* Выравнивание по правому краю */
   }
   #col1, #col2 {
    vertical-align: top; /* Выравнивание по верхнему краю */
    padding: 5px; /* Поля вокруг содержимого ячеек */
   }
  </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color:LightGrey">
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
<div><br/></div>    
    <div>
    <script type="text/javascript">
            function CloseAndRebind(reb) {
                GetRadWindow().Close();
                if (reb) { GetRadWindow().BrowserWindow.refreshGrid1(null); }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)
                return oWindow;
            }
            function fnHide(str,str2) {
                window.document.all.item("Label_Temp").innerHTML = str;
                window.document.all.item("Label_Temp").style.display = "inline";
                window.document.all.item("Label_Temp2").innerHTML = str2;
                window.document.all.item("Label_Temp2").style.display = "inline";
                window.setTimeout(fnHide2, 4000);
            }
            function fnHide2() {
                window.document.all.item("Label_Temp").style.display = "none";
                window.document.all.item("Label_Temp2").style.display = "none";
        }
        window.onload = function () {
            
            //document.getElementById("TextBoxNew").setAttribute('visibility', 'hidden');
            //window.alert("dsfdsf");
        }
    </script>
        <asp:Label ID="InjectScript" runat="server"></asp:Label> 
    <asp:SqlDataSource ID="SqlDataSourceGroup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>" 
        SelectCommand="SELECT ID_, NAME_GROUP, NUM_PROTCL FROM IZP_SPR_PERIOD ORDER BY NUM_PROTCL" />
    <table>
    <tr>
        <td colspan="3">
    <fieldset style="vertical-align: middle; text-align: center;">
    <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">дата поступления</legend>
                        <radCln:RadDatePicker ID="RadDatePicker_date_in" runat="server"  MaxDate="2050-12-31" MinDate="2007-01-01" Width="110px" Font-Bold="False" Font-Italic="False">
                            <Calendar ID="Calendar1" runat="server" Skin="WebBlue" >
                            </Calendar>
                            <DateInput Skin="" Font-Bold="True" Font-Italic="True" Font-Size="Medium">
                            </DateInput>
                        </radCln:RadDatePicker> 
    </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3">
    <fieldset style="vertical-align: middle; text-align: center;">
    <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">название организации</legend>    
    <table>
    <tr>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Width="70px" Visible="false"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server"  Visible="false"
                ImageUrl="~/Pictures/find.jpg" onclick="ImageButton1_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="DropDownListOrg" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListOrg_SelectedIndexChanged"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="TextBoxNew" runat="server" Width="350px" TextMode="MultiLine" Visible="true" />
            <asp:Button ID="ButtonNew" runat="server" Width="73px" 
                Text="Добавить" Visible="true" onclick="ButtonNew_Click" />
        </td>
    </tr>        
    </table>    
     </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        <fieldset style="vertical-align: middle; text-align: center;">
            <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">название бригады</legend> 
        <table>
        <tr>
            <td>
                <asp:DropDownList ID="DropDownListBrig" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="DropDownListBrig_SelectedIndexChanged"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBoxNewBrig" runat="server" Width="350px" TextMode="MultiLine" Visible="true" />
                <asp:Button ID="ButtonNewBrig" runat="server" Width="73px"  
                    Text="Добавить" Visible="true" onclick="ButtonNewBrig_Click" />                            
            </td>
        </tr>
        </table>
        </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3">
    <fieldset style="vertical-align: middle; text-align: center;">
    <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">группа средств защиты</legend>  
    <table>
    <tr>
        <td>
            <asp:DropDownList ID="DropDownListGroup" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownListGroup_SelectedIndexChanged"/>
        </td>
    </tr>
    </table>       
    </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3">
    <fieldset style="vertical-align: middle; text-align: center;">
    <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">средство защиты</legend>
        <table>
        <tr>
            <td>
         <asp:DropDownList ID="DropDownListProt" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="DropDownListProt_SelectedIndexChanged"/>            
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBoxNewProt" runat="server" Width="350px" TextMode="MultiLine" Visible="true" />
                <asp:Button ID="ButtonNewProt" runat="server" Width="73px"
                    Text="Добавить" Visible="true" onclick="ButtonNewProt_Click" />                            
            </td>
        </tr>
        </table>
    </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3">
    <fieldset style="vertical-align: middle; text-align: center;">
    <legend style="color: blue; font-weight: bold; font-style: italic; font-size: 9px;">инвентарный или заводской номер</legend>
        <table>
        <tr>
            <td id="col1">
            <asp:Label ID="LabelFacNum" runat="server" style="color:Red; font-size:x-small"/><br/>
                <asp:TextBox ID="TextBoxFacNum" runat="server"></asp:TextBox> 
                <asp:Button ID="ButtonFacNum" runat="server" Text="-->" Width="30px" OnClick="ButtonFacNum_Click" />    
            </td>
            <td id="col2">
                <asp:CheckBoxList ID="CheckBoxListFacNum" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black" RepeatColumns="2" Font-Size="X-Small" Width="100%">
                    <asp:ListItem Text="" Enabled="false" style="display:inline"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        </table>
    </fieldset>
        </td>
    </tr>
  <tr>
    <td style="text-align: right"><br /><asp:Button ID="Button1" runat="server" BackColor="DarkGray" BorderStyle="Outset" Font-Bold="True"
            Font-Italic="True" ForeColor="White" Text="СОХРАНИТЬ" CausesValidation="true" OnClick="Button1_Click" /></td>  
    <td style="text-align: center">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Pictures/loading4.gif" Visible="true" />
        <label ID="Label_Temp" runat="server" style="display:none; color:Red; font-size:medium" >---</label><br />
        <label ID="Label_Temp2" runat="server" style="display:none; color:Blue; font-size:medium" >---</label><br />
        <asp:Button ID="Button2" runat="server" BackColor="DarkGray" BorderStyle="Outset" Font-Bold="True"
            Font-Italic="True" ForeColor="White" Text="СОХРАНИТЬ И ПРОДОЛЖИТЬ" CausesValidation="true" OnClick="Button2_Click" /></td>
    <td><br /><asp:Button id="Button3" runat="server" ForeColor="White" Font-Italic="True" Font-Bold="True" Text="  ОТМЕНА  " OnClientClick="CloseAndRebind(true)" BorderStyle="Outset" BackColor="DarkGray"></asp:Button></td>
  </tr>
    </table>
        <radG:RadGrid ID="RadGrid2" runat="server" EnableAJAX="False" GridLines="None" DataSourceID="SqlDataSource2" onitemdatabound="RadGrid2_ItemDataBound"
                    Skin="WebBlue"  AllowPaging="False" 
                    EnableAJAXLoadingTemplate="True">
                <MasterTableView   AutoGenerateColumns="false" GroupLoadMode="Client" DataSourceID="SqlDataSource2" DataKeyNames="K_GROUP" Width="100%" BorderColor="InactiveCaptionText" BorderWidth="1px" GridLines="Both" ShowFooter="true" NoMasterRecordsText="Нет данных">
                    <GroupByExpressions>
                        <radG:GridGroupByExpression>
                            <SelectFields>
                                <radG:GridGroupByField  FieldAlias="группа" FieldName="NAME_GROUP" SortOrder="None"></radG:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <radG:GridGroupByField FieldName="K_GROUP" /> 
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
                        <radG:GridTemplateColumn DataField="NAME" DataType="System.String" HeaderText="название СЗ"
                            SortExpression="NAME" UniqueName="NAME">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="left" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Name" runat="server" Font-Italic="true"  Text='<%# Bind("NAME") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_SumName" runat="server" Font-Bold="True" ForeColor="Red" Font-Italic="true"  Text="общее количество:"></asp:Label>
                            </FooterTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="QUANTITY" DataType="System.String" HeaderText="кол-во"
                            SortExpression="QUANTITY" UniqueName="QUANTITY">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="center" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Quantity" runat="server" Font-Italic="true"  Text='<%# Bind("QUANTITY") %>'/></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_SumQuantity" runat="server" Font-Bold="true" ForeColor="Red" Font-Italic="true" Text="-"/>
                            </FooterTemplate>
                        </radG:GridTemplateColumn> 
                        <radG:GridTemplateColumn DataField="EDIZM" DataType="System.String" HeaderText="ед. изм."
                            SortExpression="EDIZM" UniqueName="EDIZM">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="center" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Edizm" runat="server" Font-Italic="true"  Text='<%# Bind("EDIZM") %>'/></div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn DataField="NUM_P" DataType="System.String" HeaderText="№ проткл."
                            SortExpression="NUM_P" UniqueName="NUM_P">
                            <ItemStyle Font-Bold="True" ForeColor="Blue" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="center" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Num_p" runat="server" Font-Italic="true"  /></div>
                            </ItemTemplate>
                        </radG:GridTemplateColumn >                        
                    </Columns>
               </MasterTableView>
            <ClientSettings >
                <Selecting AllowRowSelect="false"  />
            </ClientSettings>
                <FooterStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Wrap="True" BorderWidth="0" />
        </radG:RadGrid>
    <asp:Label ID="InjectScript_Temp" runat="server"></asp:Label>
        <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="DropDownListOrg">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="TextBoxNew"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonNew"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListBrig"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListGroup"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListProt"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="RadGrid2"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="ButtonNew">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="DropDownListOrg"/>
                        <radA:AjaxUpdatedControl ControlID="TextBoxNew"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonNew"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListBrig"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListGroup"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListProt"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="RadGrid2"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="DropDownListBrig">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="TextBoxNewBrig"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonNewBrig"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="RadGrid2"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="ButtonNewBrig">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="TextBoxNewBrig"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonNewBrig"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListBrig"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListGroup"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListProt"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="RadGrid2"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="DropDownListGroup">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="DropDownListProt"/>
                        <radA:AjaxUpdatedControl ControlID="LabelFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonFacNum"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="DropDownListProt">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="TextBoxNewProt"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonNewProt"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="ButtonNewProt">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="TextBoxNewProt"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonNewProt"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListProt"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="RadGrid2"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="ButtonFacNum">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="TextBoxFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="InjectScript_Temp"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="Button1">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="Button1"/>
                        <radA:AjaxUpdatedControl ControlID="Button2"/>
                        <radA:AjaxUpdatedControl ControlID="Button3"/>
                        <radA:AjaxUpdatedControl ControlID="InjectScript"/>
                        <radA:AjaxUpdatedControl ControlID="InjectScript_Temp"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="Button2">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="InjectScript_Temp"/>
                        <radA:AjaxUpdatedControl ControlID="TextBoxFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="ButtonFacNum"/>

                        <radA:AjaxUpdatedControl ControlID="DropDownListGroup"/>
                        <radA:AjaxUpdatedControl ControlID="DropDownListProt"/>
                        <radA:AjaxUpdatedControl ControlID="CheckBoxListFacNum"/>
                        <radA:AjaxUpdatedControl ControlID="RadGrid2"/>
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>

        <script type="text/javascript">
            document.getElementById("TextBoxNew").hidden = true;
            document.getElementById("ButtonNew").hidden = true;
            document.getElementById("TextBoxNewBrig").hidden = true;
            document.getElementById("ButtonNewBrig").hidden = true;
            document.getElementById("TextBoxNewProt").hidden = true;
            document.getElementById("ButtonNewProt").hidden = true;
            document.getElementById("CheckBoxListFacNum").hidden = true;
            document.getElementById("Image1").hidden = true;
        </script>
    </div>
<div><br/></div>
    </form>
</body>  
</html>



