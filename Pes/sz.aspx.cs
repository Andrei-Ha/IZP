using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;

public partial class sz : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string st_k_group, st_k_name, st_fac_num, st_spisan, st_filter_condition;
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (!Page.IsPostBack)
        {
        }*/
        st_k_group = Request.QueryString["k_group"];
        st_k_name = Request.QueryString["k_name"];
        st_fac_num = Request.QueryString["fac_num"];
        st_spisan = Request.QueryString["spisan"];
        if (st_spisan != "true") // добавлено 06,06,2017
        {
            st_filter_condition = " AND DATE_SPISAN IS NULL";
        }
        else
        {
            st_filter_condition = " AND DATE_SPISAN IS NOT NULL";
        }
        //string commandString = "SELECT NUM_P,NAME_ORG,NAME_BRIG,NAME_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,C_RIGHT,C_LEFT,C_POINTER,T_WATER,T_AIR,HUMIDITY,PRESSURE,RESULT_EXP, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_PROT,NUM_ACT,NOTE FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE IZP_BOOK.K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE ANd IZP_SPR_PROTECT.ID_=K_NAME order by NUM_P ";
        string commandString = "SELECT * FROM(SELECT K_ORGAN,NAME_ORG,NAME_BRIG,NAME_GROUP,YEAR,IZP_SPR_PROTECT.NAME AS NAME,FAC_NUM, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE (PES_IZP_BOOK.K_GROUP=" + st_k_group + " AND PES_IZP_BOOK.K_GROUP=NUM_PROTCL) AND (K_NAME=" + st_k_name + " AND IZP_SPR_PROTECT.ID_=K_NAME) AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE AND FAC_NUM='" + st_fac_num + "'" + st_filter_condition + " ORDER BY DATE_PROT DESC) WHERE ROWNUM=1";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader Reader = myCommand.ExecuteReader();
        while (Reader.Read())
        {
            if (Reader["K_ORGAN"].ToString() == "1005")
            {
                Button1.Enabled = true;
                Button1.Visible = true;
            }
            LabelQS.Text = Reader["NAME_ORG"] + "(" + Reader["NAME_BRIG"] + ")" + "</br>" + Reader["NAME_GROUP"] + "</br>" + Reader["NAME"] + ", заводской номер:" + Reader["FAC_NUM"];
            if (File.Exists(Server.MapPath("~/pdf/pes/" + Reader["YEAR"] + ".pdf")))
            {
                HyperLinkProtcl.Text = "протокол(" + Reader["DATE_EXP"] + ")";
                HyperLinkProtcl.NavigateUrl = "../pdf/pes/" + Reader["YEAR"] + ".pdf";
            }
            if (st_spisan != "true")
            {
                HyperLinkMove.Text = "переместить это СЗ";
                HyperLinkMove.NavigateUrl = "edit/move.aspx?" + Request.QueryString.ToString();
            }
            else
            {
                HyperLinkMove.Visible = false;
                if (File.Exists(Server.MapPath("~/pdf/pes/spisan/s" + Reader["YEAR"] + ".pdf")))
                {
                    HyperLinkProtclSpisan.Text = "протокол списания(" + Reader["DATE_EXP"] + ")";
                    HyperLinkProtclSpisan.NavigateUrl = "../pdf/pes/spisan/s" + Reader["YEAR"] + ".pdf";
                }
            }
        }
        Reader.Close();
        myConnection.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
        string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
        OleDbConnection myConnection = new OleDbConnection(connectionString);
        string commandString = "DELETE FROM PES_IZP_BOOK WHERE K_GROUP=" + st_k_group + " AND K_NAME=" + st_k_name + " AND FAC_NUM='" + st_fac_num + "'";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        object lolo = myCommand.ExecuteNonQuery();
        myConnection.Close();

         InjectScript.Text = "<script type=\"text/javascript\">Close(true);</script>"; 
    }
}