<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Spr_equip.aspx.cs" Inherits="Spr_equip"MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Connection_to_Pinsk %>" 
        ProviderName="<%$ ConnectionStrings:Connection_to_Pinsk.ProviderName %>" 
        SelectCommand="SELECT ID_, NAME, MODEL, FAB_NUM, TO_CHAR(DATE_POV,'DD.MM.YYYY') as DATE_POV, VAL, UNIT FROM IZP_SPR_EQUIP"
        UpdateCommand="UPDATE IZP_SPR_EQUIP SET NAME=:NAME, MODEL=:MODEL, FAB_NUM=:FAB_NUM, DATE_POV=:DATE_POV, VAL=:VAL, UNIT=:UNIT WHERE (ID_=:ID_)"
        DeleteCommand = "DELETE FROM IZP_SPR_EQUIP WHERE (ID_= :ID_)"
        InsertCommand = "INSERT INTO IZP_SPR_EQUIP (ID_,NAME,MODEL,FAB_NUM,DATE_POV,VAL,UNIT) VALUES (IZP_SPR_EQUIP_SEQ.NEXTVAL,:NAME,:MODEL,:FAB_NUM,:DATE_POV,:VAL,:UNIT)">
        <UpdateParameters>
        <asp:FormParameter FormField="ID_" Name="ID_" />
        </UpdateParameters>
        <DeleteParameters>
        <asp:FormParameter FormField="ID_" Name="ID_" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <table style="width:100%">
    <tr>
    <td style="width:800px; vertical-align:top">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID_" DataSourceID="SqlDataSource1" 
        ondatabound="GridView1_DataBound" Width="100%" CellPadding="4" 
            ForeColor="#333333" GridLines="None" style="text-align: left">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="№ п/п">
                    <HeaderStyle />
                    <ItemTemplate>    <%# Convert.ToString( Container.DataItemIndex + 1 ) %>  </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID_" HeaderText="ID_" ReadOnly="True" Visible="false" 
                SortExpression="ID_" />
           
             <asp:TemplateField HeaderText="Наименование" SortExpression="NAME" >
                <EditItemTemplate>
                    <asp:TextBox ID="NAME" runat="server" Text='<%# Bind("NAME") %>' Width="200px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" ErrorMessage="Введите наименование(пустое поле)"
                            ControlToValidate="NAME" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NAME"
                ErrorMessage="Наименование (недопустимый символ, длина строки)" ValidationExpression="^[а-яА-Яфa-zA-Z0-9№''-.'\s]{1,60}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="NAME" runat="server" Text='<%# Bind("NAME") %>' Width="200px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Модель" SortExpression="MODEL" >
                <EditItemTemplate>
                    <asp:TextBox ID="MODEL" runat="server" Text='<%# Bind("MODEL") %>' Width="60px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" ErrorMessage="Введите название модели(пустое поле)"
                            ControlToValidate="MODEL" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="MODEL"
                ErrorMessage="Название модели(недопустимый символ,длина строки)" ValidationExpression="^[а-яА-Яфa-zA-Z0-9№''-.'\s]{1,20}$" ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="MODEL" runat="server" Text='<%# Bind("MODEL") %>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Заводской номер" SortExpression="FAB_NUM" >
                <EditItemTemplate>
                    <asp:TextBox ID="FAB_NUM" runat="server" Text='<%# Bind("FAB_NUM") %>' Width="60px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" ErrorMessage="Введите заводской номер(пустое поле)"
                            ControlToValidate="FAB_NUM" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FAB_NUM"
                ErrorMessage=" Заводской номер(недопустимый символ, длина строки)" ValidationExpression="^\d{1,8}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="FAB_NUM" runat="server" Text='<%# Bind("FAB_NUM") %>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>   
            
             <asp:TemplateField HeaderText="Дата поверки, колибровки" SortExpression="DATE_POV" >
                <EditItemTemplate>
                    <asp:TextBox ID="DATE_POV" runat="server" Text='<%# Bind("DATE_POV") %>' Width="70px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Runat="server" ErrorMessage="Введите дату "
                            ControlToValidate="DATE_POV" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="DATE_POV"
                ErrorMessage="Введите дд/мм/гггг" ValidationExpression="^([0-9]+).([0-9]+).([0-9][0-9][0-9][0-9])?$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="DATE_POV" runat="server" Text='<%# Bind("DATE_POV") %>' Width="70px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>   
           
             <asp:TemplateField HeaderText="Значение" SortExpression="VAL" >
                <EditItemTemplate>
                    <asp:TextBox ID="VAL" runat="server" Text='<%# Bind("VAL") %>' Width="40px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Runat="server" ErrorMessage="Значение(пустое поле)"
                            ControlToValidate="VAL" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="VAL"
                ErrorMessage="Введите значение(недопустимый символ, длина строки)" ValidationExpression="^\d{1,4}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="VAL" runat="server" Text='<%# Bind("VAL") %>' Width="40px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>  
           
             <asp:TemplateField HeaderText="Ед.изм." SortExpression="UNIT" >
                <EditItemTemplate>
                    <asp:TextBox ID="UNIT" runat="server" Text='<%# Bind("UNIT") %>' Width="30px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Runat="server" ErrorMessage="Название ед. изм.(пустое поле)"
                            ControlToValidate="UNIT" ForeColor="Red" ValidationGroup="Group1" > !!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="UNIT"
                ErrorMessage="Введите Ед.изм.(недопустимый символ,длина строки)" ValidationExpression="^[а-яА-Я''-.'\s]{1,10}$"  ValidationGroup="Group1" ForeColor="Red">*
              </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="UNIT" runat="server" Text='<%# Bind("UNIT") %>' Width="30px"></asp:Label>
                </ItemTemplate>
                <ControlStyle />
            </asp:TemplateField>  
            <asp:CommandField ButtonType="Image" HeaderText="Править" ShowEditButton="True" ShowHeader="True" 
                CancelImageUrl="~/Pictures/Cancel.gif" DeleteImageUrl="~/Pictures/delete.gif" 
                EditImageUrl="~/Pictures/edit.gif" 
                UpdateImageUrl="~/Pictures/Update.gif"  ValidationGroup="Group1" />
              <asp:TemplateField>
               <ItemTemplate>
               <asp:ImageButton ImageUrl="~/Pictures/delete.gif" CommandName="Delete"
  ID="DelImageBtn"  runat="server" OnClientClick="return confirm('Вы хотите удалить эту запись','Внимание');" Visible="true" />  
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
        AutoGenerateRows="False" CellPadding="4" DataKeyNames="ID_" 
        DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" 
        Height="50px" Width="125px" Visible="False" DefaultMode="Insert" 
        onitemcommand="DetailsView1_ItemCommand" >
        <AlternatingRowStyle BackColor="White" />
        <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
        <EditRowStyle BackColor="#7C6F57" />
        <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="ID_" HeaderText="ID_" ReadOnly="True" 
                SortExpression="ID_" InsertVisible="False" />
           
                  <asp:TemplateField HeaderText="Наименование" SortExpression="NAME"  >
         <InsertItemTemplate>
              <asp:TextBox ID="InsertNAME" runat="server" Text='<%# Bind("NAME") %>'  Width="200px" TextMode="MultiLine" Height="50px" ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="InsertNAME"
                ErrorMessage="Введите наименование(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red" >*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="InsertNAME"
                ErrorMessage="Наименование(недопустимый символ)" ValidationExpression="^[0-9а-яА-Яa-zA-Z№_''-.'\s]{1,60}$" ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
    </asp:TemplateField>
            
                  <asp:TemplateField HeaderText="Модель" SortExpression="MODEL">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertMODEL" runat="server" Text='<%# Bind("MODEL")  %>' ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="InsertMODEL"
                ErrorMessage="Введите наименование модели(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="InsertMODEL"
                ErrorMessage="Название модели(недопустимый символ)" ValidationExpression="^[0-9а-яА-Яa-zA-Z№_''-.'\s]{1,60}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Заводской номер" SortExpression="FAB_NUM">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertFAB_NUM" runat="server" Text='<%# Bind("FAB_NUM") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="InsertFAB_NUM"
                ErrorMessage="Введите заводской номер(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="InsertFAB_NUM"
                ErrorMessage="Введите заводской номер(недопустимый символ)" ValidationExpression="^\d{1,6}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField> 
           
                 <asp:TemplateField HeaderText="Дата поверки, калибровки" SortExpression="DATE_POV">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertDATE_POV" runat="server" Text='<%# Bind("DATE_POV") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="InsertDATE_POV"
                ErrorMessage="Введите дату(пустое плое)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="InsertDATE_POV"
                ErrorMessage="дата колибровки(неправильный формат)" ValidationExpression="^([0-9]+)/([0-9]+)/([0-9][0-9][0-9][0-9])?$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField> 
          
                 <asp:TemplateField HeaderText="Значение" SortExpression="VAL">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertVAL" runat="server" Text='<%# Bind("VAL") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="InsertVAL"
                ErrorMessage="Введите Значение(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="InsertVAL"
                ErrorMessage="Введите значение(недопустимый символ)" ValidationExpression="^\d{1,6}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField> 
           
 <asp:TemplateField HeaderText="Ед. изм." SortExpression="UNIT">
         <InsertItemTemplate>
              <asp:TextBox ID="InsertUNIT" runat="server" Text='<%# Bind("UNIT") %>'></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="InsertUNIT"
                ErrorMessage="Введите единицу измерения(пустое поле)"  ValidationGroup="Gr1" ForeColor="Red">*
              </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="InsertUNIT"
                ErrorMessage="Единица измерения(недлпустимый символ)" ValidationExpression="^[0-9а-яА-Яa-zA-Z№_''-.'\s]{1,10}$"  ValidationGroup="Gr1" ForeColor="Red">!!
              </asp:RegularExpressionValidator>
         </InsertItemTemplate>
    </asp:TemplateField> 
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                ShowEditButton="True" ShowInsertButton="True"  ValidationGroup="Gr1" />
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

