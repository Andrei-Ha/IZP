using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;

public partial class move : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string st_k_group, st_k_name, st_fac_num;
    protected void Page_Load(object sender, EventArgs e)
    {
        st_k_group = Request.QueryString["k_group"];
        st_k_name = Request.QueryString["k_name"];
        st_fac_num = Request.QueryString["fac_num"];
        if (!Page.IsPostBack)
        {
            //string commandString = "SELECT NUM_P,NAME_ORG,NAME_BRIG,NAME_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,C_RIGHT,C_LEFT,C_POINTER,T_WATER,T_AIR,HUMIDITY,PRESSURE,RESULT_EXP, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_PROT,NUM_ACT,NOTE FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE IZP_BOOK.K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE ANd IZP_SPR_PROTECT.ID_=K_NAME order by NUM_P ";
            string commandString = "SELECT * FROM(SELECT NAME_ORG,NAME_BRIG,NAME_GROUP,YEAR,IZP_SPR_PROTECT.NAME AS NAME,FAC_NUM, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE (PES_IZP_BOOK.K_GROUP=" + st_k_group + " AND PES_IZP_BOOK.K_GROUP=NUM_PROTCL) AND (K_NAME=" + st_k_name + " AND IZP_SPR_PROTECT.ID_=K_NAME) AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE AND FAC_NUM='" + st_fac_num + "' ORDER BY DATE_PROT DESC) WHERE ROWNUM=1";
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader Reader = myCommand.ExecuteReader();
            while (Reader.Read())
            {
                LabelQS.Text = Reader["NAME_ORG"] + "(" + Reader["NAME_BRIG"] + ")" + "</br>" + Reader["NAME_GROUP"] + "</br>" + Reader["NAME"] + ", заводской номер:" + Reader["FAC_NUM"];
                /*if (File.Exists(Server.MapPath("~/pdf/pes/" + Reader["YEAR"] + ".pdf")))
                {
                    HyperLinkProtcl.Text = "протокол(" + Reader["DATE_EXP"] + ")";
                    HyperLinkProtcl.NavigateUrl = "../pdf/pes/" + Reader["YEAR"] + ".pdf";
                }*/
            }
            Reader.Close();
            string commandStringOrg = "SELECT CODE,NAME_ORG FROM PES_IZP_SPR_ORG WHERE HIDDEN=0 ORDER BY NAME_ORG";
            OleDbCommand myCommandOrg = new OleDbCommand(commandStringOrg, myConnection);
            OleDbDataReader reader1 = myCommandOrg.ExecuteReader();
            while (reader1.Read())
            {
                DropDownListOrg.Items.Add(new ListItem(Convert.ToString(reader1["NAME_ORG"]), Convert.ToString(reader1["CODE"])));
            }
            reader1.Close();
            DropDownListOrg.Items.Insert(0, new ListItem("выберите организацию", "0"));
            myConnection.Close();
            DropDownListBrig.Items.Insert(0, new ListItem("-", "0"));
        }
        ColorDDLOrg();
    }
    protected void DropDownListOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListBrig.Items.Clear();
        string commandString = "SELECT CODE,NAME_BRIG FROM PES_IZP_SPR_BRIG WHERE HIDDEN=0 AND CODE_ORG=" + DropDownListOrg.SelectedValue + " ORDER BY NAME_BRIG";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            DropDownListBrig.Items.Add(new ListItem(Convert.ToString(reader1["NAME_BRIG"]), Convert.ToString(reader1["CODE"])));
        }
        reader1.Close();
        myConnection.Close();
        if (DropDownListOrg.SelectedValue != "0") { DropDownListBrig.Items.Insert(0, new ListItem("-", "0")); }
    }
    protected void ColorDDLOrg()
    {
        if (DropDownListOrg.Items.Count > 0) { DropDownListOrg.Items[0].Attributes.Add("style", "background-color:Red"); }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownListOrg.SelectedValue != "0")
        {
            string commandString = "UPDATE PES_IZP_BOOK SET K_ORGAN=" + DropDownListOrg.SelectedValue + ", K_BRIG=" + DropDownListBrig.SelectedValue + " WHERE DATE_SPISAN IS NULL AND K_GROUP=" + st_k_group + " AND K_NAME=" + st_k_name + " AND FAC_NUM='" + st_fac_num + "'";
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            object execute = myCommand.ExecuteNonQuery();
            myConnection.Close();
            //Response.Redirect("../sz.aspx?"+Request.QueryString.ToString(), true);
            InjectScript.Text = "<script type=\"text/javascript\">Close(true);</script>";
            //Response.Write("<script type=\"text/javascript\">this.window.opener = null;window.open('','_self'); window.close();   </script>");
        }
    }
}