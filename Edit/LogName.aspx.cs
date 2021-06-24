using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Edit_LogName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string st_redirectUrl = Request.QueryString["ReturnUrl"];//.Replace("/test","~");
        if (st_redirectUrl == "") { st_redirectUrl = "~/Edit/Default.aspx"; };
        switch (HttpContext.Current.User.Identity.Name.ToString())
        {
            case "egik":
                Session["SIZP_User"] = "Горегляд А.С.";

                break;
            case "yura":
                Session["SIZP_User"] = "Приловский Ю.Д.";
                st_redirectUrl = "~/Edit/Default.aspx";

                break;
            case "viktor":
                Session["SIZP_User"] = "Кузьминчук В.И.";
                st_redirectUrl = "~/Edit/Default.aspx";

                break;
            case "sizp":
                Session["SIZP_User"] = "Пригодич Н.П.";

                break;
            case "natasha":
                Session["SIZP_User"] = "Бухтик Н.В.";
                st_redirectUrl = "~/Default.aspx";
                break;
            default:
                Session["SIZP_User"] = "-";

                break;
        }
        Response.Redirect(st_redirectUrl);
    }
}