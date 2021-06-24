using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        userName.Focus();
    }

    protected void LogonUser(object sender, EventArgs e)
    {
        string user = userName.Text;
        string pswd = passWord.Text;

        // Custom authentication
        bool bAuthenticated = AuthenticateUser(user, pswd);
        if (bAuthenticated)
        {
            FormsAuthentication.RedirectFromLoginPage(user, false);
            //errorMsg.Text = Request.QueryString["ReturnUrl"];
            Response.Redirect("Edit/LogName.aspx?ReturnUrl=" + Request.QueryString["ReturnUrl"]);
        }
        else
            errorMsg.Text = "Неверное имя пользователя или пароль";
    }

    private bool AuthenticateUser(string username, string pswd)
    {
        int found = 0;
        if (FormsAuthentication.Authenticate(username, pswd))
        { found = 1; }
        else { found = -1; }

        return (found > 0);
    }
}
