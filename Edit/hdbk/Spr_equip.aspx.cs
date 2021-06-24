using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class Spr_equip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DetailsView1.Visible = true;
        Button1.Visible = false;
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        DetailsView1.Visible = false;
        //Button1.Visible = true;
        DetailsView1.DataBind();
    }
    protected void DetailsView1_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        DetailsView1.Visible = false;
        Button1.Visible = true;
    }
   
}