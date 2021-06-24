using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // определение имени пользователя происходит в файле  Edit/LogName.aspx.cs 
        if (Session["SIZP_User"] != null)
        {
            if (Session["SIZP_User"].ToString() == "-")
            {
                ButtonExit.Visible = false;
                ButtonEnter.Visible = true;
                InjectScript.Text = Session["SIZP_User"].ToString();
            }
            else
            {
                InjectScript.Text = Session["SIZP_User"].ToString();
                ButtonExit.Visible = true;
                ButtonEnter.Visible = false;
            }
        }
        else
        {
            FormsAuthentication.SignOut();
            Session["SIZP_User"] = "-";
            InjectScript.Text = "-";
            ButtonExit.Visible = false;
            ButtonEnter.Visible = true;
        }
            
    }
    protected void ButtonExit_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session["SIZP_User"] = "-";
        FormsAuthentication.RedirectToLoginPage("as=1");
    }
    protected void ButtonEnter_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/login.aspx");
    }
}
