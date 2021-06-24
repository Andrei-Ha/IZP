<%@ Page Title="страница регистрации" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
	        function GetRadWindow()
	        {
		        var oWindow = null;
		        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
		        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)
    				
		        return oWindow;
	        }
	        function CancelEdit()
	        {
	            GetRadWindow().Close();
	            return false;		
	        }
</script>
    <div style="text-align: center">
    <p style="text-align: center; font-style:italic"> Ввод пароля </p>
    		<table style="border-right: gray thin groove; border-top: gray thin groove; border-left: gray thin groove; border-bottom: gray thin groove">
		<tr>
			<td><b>Имя пользователя</b></td>
			<td><asp:textbox runat="server" text="" id="userName" /></td></tr>
		<tr>
			<td><b>пароль</b></td>
			<td><asp:textbox runat="server" text="" id="passWord" textmode="password" /></td></tr>
		<tr>
		    <td colspan="2">
		        <asp:button ID="Button1" runat="server" text="ok" onclick="LogonUser" Width="100px" />
		    </td>
		</tr>
		</table>
		<br />
		<asp:label runat="server" id="errorMsg" Font-Names="Verdana" Font-Size="Small" Font-Bold="True" ForeColor="Red"/>
    </div><br />
                <div style="text-align:center"><asp:button ID="Button2" runat="server" text="на главную" OnClientClick="JavaScript: window.location='Default.aspx'; return false;" /></div>
    </asp:Content>
