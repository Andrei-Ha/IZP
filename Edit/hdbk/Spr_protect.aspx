<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Spr_protect.aspx.cs" Inherits="Spr_protect" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>" 
        SelectCommand="SELECT ID_, NAME, FABRIC, K_GROUP, HIDDEN FROM IZP_SPR_PROTECT ORDER BY NAME"
        UpdateCommand="UPDATE IZP_SPR_PROTECT SET NAME=:NAME, FABRIC=:FABRIC, K_GROUP=:K_GROUP, HIDDEN=:HIDDEN WHERE (ID_=:ID_)"
        DeleteCommand = "DELETE FROM IZP_SPR_PROTECT WHERE (ID_= :ID_)"
        InsertCommand = "INSERT INTO IZP_SPR_PROTECT (ID_,NAME,FABRIC,K_GROUP,HIDDEN) VALUES (IZP_SPR_PROTECT_SEQ.NEXTVAL,:NAME,:FABRIC,:K_GROUP,:HIDDEN)">
        <UpdateParameters>
        <asp:FormParameter FormField="ID_" Name="ID_" />
        </UpdateParameters>
        <DeleteParameters>
        <asp:FormParameter FormField="ID_" Name="ID_" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <table style="width:100%">
    <tr>
    <td style=" width:630px">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID_" DataSourceID="SqlDataSource1" 
        ondatabound="GridView1_DataBound" Width="100%" CellPadding="4" 
            ForeColor="#333333" GridLines="None"  style="text-align: left">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="№ п/п">
                    <HeaderStyle />
                    <ItemTemplate>    <%# Convert.ToString( Container.DataItemIndex + 1 ) %>  </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID_" HeaderText="КОД" ReadOnly="True" Visible="TRUE" 
                SortExpression="ID_" />
            
                <asp:TemplateField HeaderText="Наименование" SortExpression="NAME" >
                <EditItemTemplate>
                    <asp:TextBox ID="NAME" runat="server" Text='<%# Bind("NAME") %>' Width="250px" TextMode="MultiLine" Height="50px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" ErrorMessage="Введите наименование(пустое поле)"
                            ControlToValidate="NAME" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NAME"
                ErrorMessage="Введите название(недопустимый символ)" ValidationExpression='^[0-9а-яА-Яa-zA-Z№_"-."\s]{1,60}$'  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="NAME" runat="server" Text='<%# Bind("NAME") %>' Width="250px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>
           
               
                <asp:TemplateField HeaderText="Производитель" SortExpression="FABRIC" >
                <EditItemTemplate>
                    <asp:TextBox ID="FABRIC" runat="server" Text='<%# Bind("FABRIC") %>' Width="200px" TextMode="MultiLine" Height="50px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" ErrorMessage="Введите Производителя(пустое поле)"
                            ControlToValidate="FABRIC" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FABRIC"
                ErrorMessage="Введите производителя(недопустимй символ)" ValidationExpression='^[а-яА-Яa-zA-Z_0-9\W]{1,60}$' ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="FABRIC" runat="server" Text='<%# Bind("FABRIC") %>' Width="200px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Группа СЗ" SortExpression="K_GROUP" >
                <EditItemTemplate>
                    <asp:TextBox ID="K_GROUP" runat="server" Text='<%# Bind("K_GROUP") %>' Width="20px"  ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" ErrorMessage="Введите номер группы(пустое поле)"
                            ControlToValidate="K_GROUP" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="K_GROUP"
                ErrorMessage="Номер группы(недопустимый символ)" ValidationExpression="^\d{1,3}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="K_GROUP" runat="server" Text='<%# Bind("K_GROUP") %>' Width="20px" ></asp:Label>
                </ItemTemplate>
                
            </asp:TemplateField>   
            
            <asp:TemplateField HeaderText="Признак" SortExpression="HIDDEN" >
                <EditItemTemplate>
                    <asp:TextBox ID="HIDDEN" runat="server" Text='<%# Bind("HIDDEN") %>' Width="20px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Runat="server" ErrorMessage="Введите признак(пустое поле)"
                            ControlToValidate="HIDDEN" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="HIDDEN"
                ErrorMessage="Признак(недопустимый символ)" ValidationExpression="^\d{1,3}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="HIDDEN" runat="server" Text='<%# Bind("HIDDEN") %>' Width="20px" ></asp:Label>
                </ItemTemplate>
                
            </asp:TemplateField>  
            <asp:CommandField ButtonType="Image" HeaderText="Править" ShowEditButton="True" ShowHeader="True" 
                CancelImageUrl="~/Pictures/Cancel.gif" DeleteImageUrl="~/Pictures/delete.gif" 
                EditImageUrl="~/Pictures/edit.gif" 
                UpdateImageUrl="~/Pictures/Update.gif" ValidationGroup="Group1" />
              <asp:TemplateField>
               <ItemTemplate>
               <asp:ImageButton ImageUrl="~/Pictures/delete.gif" CommandName="Delete"
  ID="DelImageBtn"  runat="server" OnClientClick="return confirm('Вы хотите удалить эту запись');" Visible="false" />  
               </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" HorizontalAlign="Left" 
            VerticalAlign="Middle" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" 
            VerticalAlign="Middle" />
    </asp:GridView>    
    </td>
    <td style="text-align: left; vertical-align:top">
    <asp:Button ID="Button1" runat="server" Text="Добавить новую запись" 
        onclick="Button1_Click" />
    <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
        AutoGenerateRows="False" CellPadding="4" DataKeyNames="ID_" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" 
        Height="50px" Width="125px" Visible="false" DefaultMode="Insert" 
        onitemcommand="DetailsView1_ItemCommand">
        <AlternatingRowStyle BackColor="White" />
        <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
        <EditRowStyle BackColor="#7C6F57" />
        <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="ID_" HeaderText="ID_" ReadOnly="True" 
                SortExpression="ID_" InsertVisible="False" />

            
                 <asp:TemplateField HeaderText="Наименование" SortExpression="NAME">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertNAME" runat="server" Text='<%# Bind("NAME") %>' Height="50px" TextMode="MultiLine"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="InsertNAME"
                ErrorMessage="Введите наименование(пустое поле) "  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="InsertNAME"
                ErrorMessage="Наименование средства защиты(недопустимый символ)" ValidationExpression='^[0-9а-яА-Яa-zA-Z№_"-."\s]{1,60}$' ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
          
                 <asp:TemplateField HeaderText="Производитель" SortExpression="FABRIC">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertFABRIC" runat="server" Text='<%# Bind("FABRIC") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="InsertFABRIC"
                ErrorMessage="Введите наименование группы (пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="InsertFABRIC"
                ErrorMessage="Наименование группы (недопустимый символ)" ValidationExpression='^[0-9а-яА-Яa-zA-Z№_"-."\s]{1,40}$' ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
           <asp:TemplateField HeaderText="Группа СЗ" SortExpression="K_GROUP">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertK_GROUP" runat="server" Text='<%# Bind("K_GROUP") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="InsertK_GROUP"
                ErrorMessage="Введите номер группы(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="InsertK_GROUP"
                ErrorMessage="Номер группы(недопустимый символ)" ValidationExpression="^\d{1,2}$"  ValidationGroup="Gr1" ForeColor="Red" >!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Признак" SortExpression="HIDDEN">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertHIDDEN" runat="server" Text='<%# Bind("HIDDEN") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="InsertHIDDEN"
                ErrorMessage="ПРИЗНАК(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="InsertHIDDEN"
                ErrorMessage="ПРИЗНАК(недопустимый символ)" ValidationExpression="^\d{1,2}$"  ValidationGroup="Gr1" ForeColor="Red" >!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                ShowEditButton="True" ShowInsertButton="True" />
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

