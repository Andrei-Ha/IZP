<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="monitoring.aspx.cs" Inherits="Others_monitoring" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">    
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>"/>
<table width="100%" style="background-color:#FFFAF0">
    <tr>
        <td colspan="3" style="text-align:center">
            Контроль за периодичностью испытаний
        </td>
    </tr>
    <tr>
        <td colspan="3" style="text-align:center" >
            <fieldset style="vertical-align: middle; text-align: center;">
                <legend style="color: blue; font-weight:normal; font-style: italic; font-size: 9px;">название организации</legend>
                    <asp:DropDownList ID="DropDownListOrg" runat="server" Width="650px"></asp:DropDownList>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td  style="text-align:center">
            <fieldset style="vertical-align: middle; text-align: center;">
                <legend style="color: blue; font-weight:normal; font-style: italic; font-size: 9px;">группа средств защиты</legend>
                    <asp:DropDownList ID="DropDownListGroup" runat="server">
                    </asp:DropDownList>
            </fieldset>
        </td>
        <td>
            <fieldset style="vertical-align: middle; text-align: center;">
                <legend style="color: blue; font-weight:normal; font-style: italic; font-size: 9px;">статус средств защиты</legend>
                    <asp:DropDownList ID="DropDownListCondition" runat="server">
                    </asp:DropDownList>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="text-align:center">
            <asp:Button ID="ButtonFilter" runat="server" Text="применить" 
                onclick="ButtonFilter_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3" style="text-align:right">
            <span class="styleBlack"><em>в работе</em></span><span class="styleOr"><em>осталось 10 дней</em></span><span class="styleR"><em>подлежат испытанию</em></span><span class="styleG"><em>на испытании</em></span><span class="styleGr"><em>(дата следующего испытания)</em></span>
        </td>
    </tr>
</table>
<radG:RadGrid ID="RadGrid2" runat="server" EnableAJAX="False" GridLines="None" DataSourceID="SqlDataSource2" onitemdatabound="RadGrid2_ItemDataBound"
                    Skin="WebBlue"  AllowPaging="False" 
                    EnableAJAXLoadingTemplate="True">
                <MasterTableView   AutoGenerateColumns="false" GroupLoadMode="Client" DataSourceID="SqlDataSource2" DataKeyNames="NAME_ORG" Width="100%" BorderColor="InactiveCaptionText" BorderWidth="1px" GridLines="Both" ShowFooter="true" NoMasterRecordsText="Нет данных">
                    <GroupByExpressions>
                        <radG:GridGroupByExpression>
                            <SelectFields>
                                <radG:GridGroupByField  FieldAlias="Организация" FieldName="NAME_ORG" ></radG:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <radG:GridGroupByField FieldName="NAME_ORG" SortOrder="Ascending"/> 
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
                        <radG:GridTemplateColumn DataField="DETAIL" DataType="System.String" HeaderText="номера"
                            SortExpression="DETAIL" UniqueName="DETAIL">
                            <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BorderWidth="1px" Wrap="false" HorizontalAlign="left" Font-Size="X-Small" VerticalAlign="Middle"/>
                            <HeaderStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" ForeColor="White" />
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <div><asp:Label ID="Label_Detail" runat="server" Font-Italic="true"/></div>
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
                                              
                    </Columns>
               </MasterTableView>
            <ClientSettings >
                <Selecting AllowRowSelect="false"  />
            </ClientSettings>
                <FooterStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Wrap="True" BorderWidth="0" />
        </radG:RadGrid>
</asp:Content>

