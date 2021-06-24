<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Spr_org.aspx.cs" Inherits="Spr_org" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>" 
        SelectCommand="SELECT CODE, NAME_ORG,UNN, HIDDEN FROM IZP_SPR_ORG ORDER BY NAME_ORG"
        UpdateCommand="UPDATE IZP_SPR_ORG SET NAME_ORG=:NAME_ORG,UNN=:UNN, HIDDEN=:HIDDEN WHERE (CODE=:CODE)"
        DeleteCommand = "UPDATE IZP_SPR_ORG SET HIDDEN=1 WHERE (CODE=:CODE)"
        InsertCommand = "INSERT INTO IZP_SPR_ORG (CODE,NAME_ORG,UNN,HIDDEN) VALUES (IZP_SPR_ORG_SEQ.NEXTVAL,:NAME_ORG,:UNN,:HIDDEN)">
        <UpdateParameters>
        <asp:FormParameter FormField="CODE" Name="CODE" />
        </UpdateParameters>
        <DeleteParameters>
        <asp:FormParameter FormField="CODE" Name="CODE" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <table style="width:100%">
    <tr>
    <td style=" width:630px">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="CODE" DataSourceID="SqlDataSource1" 
        ondatabound="GridView1_DataBound" Width="100%" CellPadding="4" 
            ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="№ п/п">
                    <HeaderStyle />
                    <ItemTemplate>    <%# Convert.ToString( Container.DataItemIndex + 1 ) %>  </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CODE" HeaderText="Код" ReadOnly="True" SortExpression="CODE" />
            
            <asp:TemplateField HeaderText="Название организации" SortExpression="NAME_ORG" >
                <EditItemTemplate>
                    <asp:TextBox ID="NAME_ORG" runat="server" Text='<%# Bind("NAME_ORG") %>' Width="200px" Height="50px" TextMode="MultiLine" ></asp:TextBox>
                    <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" ErrorMessage="Введите название организации (пустое поле)"
                            ControlToValidate="NAME_ORG" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NAME_ORG"
                ErrorMessage=" Название организации (недопустимый символ)" ValidationExpression="^[0-9а-яА-Яa-zA-Z№_''-.'\s]{1,60}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>-->
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="NAME_ORG" runat="server" Text='<%# Bind("NAME_ORG") %>' Width="200px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="УНН" SortExpression="UNN" >
                <EditItemTemplate>
                    <asp:TextBox ID="UNN" runat="server" Text='<%# Bind("UNN") %>' Width="80px"  TextMode="SingleLine" ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="UNN" runat="server" Text='<%# Bind("UNN") %>' Width="80px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Признак" SortExpression="HIDDEN" >
                <EditItemTemplate>
                    <asp:TextBox ID="HIDDEN" runat="server" Text='<%# Bind("HIDDEN") %>' Width="20px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" ErrorMessage="Введите признак(пустое поле)"
                            ControlToValidate="HIDDEN" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="HIDDEN"
                ErrorMessage="Признак(недопустимый символ)" ValidationExpression="^\d{1,1}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="HIDDEN" runat="server" Text='<%# Bind("HIDDEN") %>' Width="20px" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>    

            <asp:CommandField ButtonType="Image" HeaderText="Править" ShowEditButton="True" ShowHeader="True" 
                CancelImageUrl="~/Pictures/Cancel.gif"  
                EditImageUrl="~/Pictures/edit.gif" 
                UpdateImageUrl="~/Pictures/Update.gif" /> 
                  <asp:TemplateField>
               <ItemTemplate>
               <asp:ImageButton ImageUrl="~/Pictures/delete.gif" CommandName="Delete" ID="DelImageBtn"  runat="server" OnClientClick="return confirm('Вы хотите удалить эту запись');" Visible="false" />  
               </ItemTemplate>
                </asp:TemplateField>
        </Columns>

        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>    
    </td>
    <td style="text-align: left; vertical-align:top">
    <asp:Button ID="Button1" runat="server" Text="Добавить новую запись" 
        onclick="Button1_Click" />
    <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
        AutoGenerateRows="False" CellPadding="4" DataKeyNames="CODE" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" 
        Height="50px" Width="125px" Visible="false" DefaultMode="Insert" 
        onitemcommand="DetailsView1_ItemCommand">
        <AlternatingRowStyle BackColor="White" />
        <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
        <EditRowStyle BackColor="#7C6F57" />
        <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="CODE" HeaderText="CODE" ReadOnly="True" 
                SortExpression="CODE" InsertVisible="False" />
           
    <asp:TemplateField HeaderText="Название организации" SortExpression="NAME_ORG">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertNAME_ORG" runat="server" Text='<%# Bind("NAME_ORG") %>' Height="50"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="InsertNAME_ORG"
                ErrorMessage="Введите наименование организации (пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="InsertNAME_ORG"
                ErrorMessage=" Название организации (недопустимый символ)" ValidationExpression="^[0-9а-яА-Яa-zA-Z№_''-.'\s]{1,60}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="УНН" SortExpression="UNN">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertUNN" runat="server" Text='<%# Bind("UNN") %>' Height="50"></asp:TextBox>
         </InsertItemTemplate>
    </asp:TemplateField>            
                <asp:TemplateField HeaderText="Признак" SortExpression="HIDDEN">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertHIDDEN" runat="server" Text='<%# Bind("HIDDEN") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="InsertHIDDEN"
                ErrorMessage="Введите признак"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="InsertHIDDEN"
                ErrorMessage="Признак(недопустимый символ)" ValidationExpression="^\d{1,1}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                ShowEditButton="True" ShowInsertButton="True" ValidationGroup="Gr1" />
        </Fields>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
    </asp:DetailsView>    
    </td>
    </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
  HeaderText="Обнаружены следующие ошибки при редактировании:"
   ShowMessageBox="True" ValidationGroup="Group1"  />
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
  HeaderText="Обнаружены следующие ошибки при вставке:"
   ShowMessageBox="True" ValidationGroup="Gr1"  />
</asp:Content>

