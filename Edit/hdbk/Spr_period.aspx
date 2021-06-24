<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Spr_period.aspx.cs" Inherits="Spr_period" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>" 
        SelectCommand="SELECT ID_, NAME_GROUP, NUM_PROTCL, PERIOD, TARIFF,EDIZM FROM IZP_SPR_PERIOD ORDER BY NUM_PROTCL"
        UpdateCommand="UPDATE IZP_SPR_PERIOD SET  PERIOD=:PERIOD, TARIFF=:TARIFF,EDIZM=:EDIZM WHERE (ID_=:ID_)"
        DeleteCommand = "DELETE FROM IZP_SPR_PERIOD WHERE (ID_= :ID_)"
        InsertCommand = "INSERT INTO IZP_SPR_PERIOD (ID_,NAME_GROUP,NUM_PROTCL,PERIOD,TARIFF,EDIZM) VALUES (IZP_SPR_PERIOD_SEQ.NEXTVAL,:NAME_GROUP,:NUM_PROTCL,:PERIOD,:TARIFF,:EDIZM)">
        <UpdateParameters>
            <asp:FormParameter FormField="ID_" Name="ID_" />
            <asp:FormParameter FormField="PERIOD" Name="PERIOD" />
            <asp:FormParameter FormField="TARIFF" Name="TARIFF" />
            <asp:FormParameter FormField="EDIZM" Name="EDIZM" />
        </UpdateParameters>
        <DeleteParameters>
        <asp:FormParameter FormField="ID_" Name="ID_" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <table style="width:100%">
    <tr>
    <td style=" width:800px">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID_" DataSourceID="SqlDataSource1" 
        ondatabound="GridView1_DataBound" Width="100%" CellPadding="4" 
            ForeColor="#333333" GridLines="None" HorizontalAlign="Left" > 
          
        <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
        <Columns>
            <asp:BoundField DataField="NUM_PROTCL" HeaderText="№ прот-ла" SortExpression="NUM_PROTCL"   ReadOnly="true" ItemStyle-Width="15px" >
                <ItemStyle Width="15px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ID_" HeaderText="ID_" ReadOnly="True" Visible="false" 
                SortExpression="ID_" />
             <asp:BoundField DataField="NAME_GROUP" HeaderText="Группа средств защиты" 
                SortExpression="NAME_GROUP" ReadOnly="true" ItemStyle-Width="350px"   />
              <asp:TemplateField HeaderText="Периодичн. испыт. (мес.)" SortExpression="PERIOD" >
                <EditItemTemplate>
                    <asp:TextBox ID="PERIOD" runat="server" Text='<%# Bind("PERIOD") %>' Width="25px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" ErrorMessage="Пустое поле(период)"
                            ControlToValidate="PERIOD" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PERIOD"
                ErrorMessage="Недопустимый символ(период)" ValidationExpression="^\d{1,3}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="PERIOD" runat="server" Text='<%# Bind("PERIOD") %>' Width="25px" ></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>    
            <asp:TemplateField HeaderText="Тариф без НДС, руб." SortExpression="TARIFF" >
                <EditItemTemplate>
                    <asp:TextBox ID="TARIFF" runat="server" Text='<%# Bind("TARIFF") %>' Width="40px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" ErrorMessage="Пустое поле(тариф)"
                            ControlToValidate="TARIFF" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TARIFF"
                ErrorMessage="Недопустимый символ(тариф)" ValidationExpression="^\d{1,10},\d{1,2}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="TARIFF" runat="server" Text='<%# Bind("TARIFF") %>' Width="40px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>    
             <asp:TemplateField HeaderText="Единица измерения" SortExpression="EDIZM" >
                <EditItemTemplate>
                    <asp:TextBox ID="EDIZM" runat="server" Text='<%# Bind("EDIZM") %>' Width="40px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" ErrorMessage="Пустое поле (единицу измерения)"
                            ControlToValidate="EDIZM" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="EDIZM"
                ErrorMessage="Недопустимый символ(ед. изм.)" ValidationExpression="^[а-яА-Я''-.'\s]{1,10}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="EDIZM" runat="server" Text='<%# Bind("EDIZM") %>' Width="40px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>         
        
            <asp:CommandField ButtonType="Image" HeaderText="Править" ShowEditButton="True" ShowHeader="True" 
                CancelImageUrl="~/Pictures/Cancel.gif" EditImageUrl="~/Pictures/edit.gif" 
                UpdateImageUrl="~/Pictures/Update.gif"  ValidationGroup="Group1"  CancelText="Отмена" EditText="Править" UpdateText="Обновить"  >
               <ControlStyle Width="15px" />
            <ItemStyle Width="15px" />
            </asp:CommandField>
               <asp:TemplateField>
               <ItemTemplate>
               <asp:ImageButton ImageUrl="~/Pictures/delete.gif" CommandName="Delete"
  ID="DelImageBtn"  runat="server" OnClientClick="return confirm('Вы хотите удалить эту запись','Внимание');" Visible="false" />  
               </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" Height="30px" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" VerticalAlign="Middle" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>    
    </td>
    <td style="text-align: left; vertical-align:top">
    <asp:Button ID="Button1" runat="server" Text="Добавить новую запись" 
        onclick="Button1_Click"  Visible="false"/>
    <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
        AutoGenerateRows="False" CellPadding="4" DataKeyNames="ID_" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" 
        Height="50px" Width="125px" Visible="False" DefaultMode="Insert" 
        onitemcommand="DetailsView1_ItemCommand">
        <AlternatingRowStyle BackColor="White" />
        <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
        <EditRowStyle BackColor="#7C6F57" />
        <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="ID_" HeaderText="ID_" ReadOnly="True" 
                SortExpression="ID_" InsertVisible="False" />
                 <asp:TemplateField HeaderText="Группа средств защиты" SortExpression="NAME_GROUP">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertNAME_GROUP" runat="server" Text='<%# Bind("NAME_GROUP") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="InsertNAME_GROUP"
                ErrorMessage="Наименование группы(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="InsertNAME_GROUP"
                ErrorMessage="Недопустимый символ(название группы)" ValidationExpression='^[а-яА-Яa-zA-ZЁё0-9№,_"-.\s]{1,60}$'  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
           <asp:TemplateField HeaderText="Номер протокола" SortExpression="NUM_PROTCL">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertNUM_PROTCL" runat="server" Text='<%# Bind("NUM_PROTCL") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="InsertNUM_PROTCL"
                ErrorMessage="Номер протокола(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="InsertNUM_PROTCL"
                ErrorMessage="Недопустимый символ(номер протокола)" ValidationExpression="^\d{1,3}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
           <asp:TemplateField HeaderText="Период испытания" SortExpression="PERIOD">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertPERIOD" runat="server" Text='<%# Bind("PERIOD") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="InsertPERIOD"
                ErrorMessage="Период(пустое поле))"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="InsertPERIOD"
                ErrorMessage="Период(недопустимый символ)" ValidationExpression="^\d{1,2}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField> 
     <asp:TemplateField HeaderText="Тариф без НДС, руб." SortExpression="TARIFF">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertTARIF" runat="server" Text='<%# Bind("TARIFF") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="InsertTARIF"
                ErrorMessage="пустое поле(Тариф без НДС, руб.)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="InsertTARIF"
                ErrorMessage="Неправильный символ(Тариф без НДС, руб.)" ValidationExpression="^\d{1,6}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField> 
      <asp:TemplateField HeaderText="Единица измерения" SortExpression="EDIZM">
         <InsertItemTemplate>
              <asp:TextBox ID="EDIZM" runat="server" Text='<%# Bind("EDIZM") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="EDIZM"
                ErrorMessage="Пустое поле (единицу измерения)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="EDIZM"
                ErrorMessage="Недопустимый символ(единицы измерения)" ValidationExpression="^[а-яА-Я''-.'\s]{1,10}$"  ValidationGroup="Gr1" ForeColor="Red">!!
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

