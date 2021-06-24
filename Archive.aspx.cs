using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using Telerik.WebControls;

public partial class Archive : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    int i_count_str=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime date_po = DateTime.Today;
            DateTime date_s = Convert.ToDateTime("01.01." + date_po.Year);
            RadDatePicker_date_po.SelectedDate = date_po;
            RadDatePicker_date_s.SelectedDate = date_s;
            string commandStringYear = "SELECT DISTINCT TO_CHAR(DATE_IN,'yyyy') as DATE_IN FROM IZP_BOOK ORDER BY DATE_IN DESC";
            OleDbCommand myCommandYear = new OleDbCommand(commandStringYear, myConnection);
            myConnection.Open();
            OleDbDataReader ReaderYear = myCommandYear.ExecuteReader();
            while (ReaderYear.Read())
            {
                DropDownList_Year.Items.Add(new ListItem(Convert.ToString(ReaderYear["DATE_IN"]), Convert.ToString(ReaderYear["DATE_IN"])));
                DropDownList_Year.Items[0].Selected = true;
            }
            ReaderYear.Close();
            myConnection.Close();
            string commandString = "SELECT CODE,NAME_ORG FROM IZP_SPR_ORG WHERE HIDDEN=0 ORDER BY NAME_ORG";
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader reader1 = myCommand.ExecuteReader();
            while (reader1.Read())
            {
                DropDownListOrg.Items.Add(new ListItem(Convert.ToString(reader1["NAME_ORG"]), Convert.ToString(reader1["CODE"])));
            }
            reader1.Close();
            myConnection.Close();
            DropDownListOrg.Items.Insert(0, new ListItem("все подразделения", "0"));
            DropDownListBrig.Items.Insert(0, new ListItem("все бригады", "0"));
            string commandStringGroup = "SELECT NAME_GROUP,NUM_PROTCL FROM IZP_SPR_PERIOD WHERE NUM_PROTCL<>17 ORDER BY NUM_PROTCL";
            OleDbCommand myCommandGroup = new OleDbCommand(commandStringGroup, myConnection);
            myConnection.Open();
            OleDbDataReader readerGroup = myCommandGroup.ExecuteReader();
            while (readerGroup.Read())
            {
                DropDownListGroup.Items.Add(new ListItem(Convert.ToString(readerGroup["NAME_GROUP"]), Convert.ToString(readerGroup["NUM_PROTCL"])));
            }
            readerGroup.Close();
            myConnection.Close();
            DropDownListGroup.Items.Insert(0, new ListItem("все группы", "0"));
        }
        ColorDropDownLists();
    }
    protected void ColorDropDownLists()
    {
        DropDownListGroup.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListOrg.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListBrig.Items[0].Attributes.Add("style", "color:Blue");
    }
    protected void DropDownListOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListBrig.Items.Clear();
        string commandString = "SELECT CODE,NAME_BRIG FROM IZP_SPR_BRIG WHERE HIDDEN=0 AND CODE_ORG<>0 AND CODE_ORG=" + DropDownListOrg.SelectedValue + " ORDER BY NAME_BRIG";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            DropDownListBrig.Items.Add(new ListItem(Convert.ToString(reader1["NAME_BRIG"]), Convert.ToString(reader1["CODE"])));
        }
        reader1.Close();
        myConnection.Close();
        DropDownListBrig.Items.Insert(0, new ListItem("все бригады", "0"));
        DropDownListBrig.Items[0].Attributes.Add("style", "color:Blue");
        Func_NachParam();
    }
    protected void DropDownListBrig_SelectedIndexChanged(object sender, EventArgs e)
    {
        Func_NachParam();
    }
    protected void DropDownListGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Func_NachParam();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        i_count_str = 0;
        HiddenFieldCountString.Value = i_count_str.ToString();
        Func_Build_Grid1();
        GridView1.PageIndex = 0;
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        Func_Build_Grid1();
    }
    protected void Func_Build_Grid1()
    {
        string str_dop = "";
        string str_term = "";
        string str_podr = "для всех сторонних организаций";
        if (RadioButton1.Checked)
        {
            string str_year = DropDownList_Year.SelectedValue;
            str_dop += " TO_CHAR(DATE_PROT,'yyyy')=" + str_year;
            str_term = "за " + str_year + " год";
        }
        else
        {
            string str_dat1 = RadDatePicker_date_s.SelectedDate.ToString().Substring(0, 10);
            string str_dat2 = RadDatePicker_date_po.SelectedDate.ToString().Substring(0, 10);
            str_dop += " DATE_PROT >= '" + str_dat1 + "' AND DATE_PROT<='" + str_dat2 + "'";
            str_term = "за период с " + str_dat1 + " по " + str_dat2;
        }
        if (DropDownListOrg.SelectedValue != "0")
        {
            str_dop += " AND K_ORGAN = " + DropDownListOrg.SelectedValue;
            str_podr = "для организации " + DropDownListOrg.SelectedItem.Text;
        }
        if (DropDownListBrig.SelectedValue != "0")
        {
            str_dop += " AND K_BRIG = " + DropDownListBrig.SelectedValue;
            str_podr += "(" + DropDownListBrig.SelectedItem.Text + ")";
        }
        if (DropDownListGroup.SelectedValue != "0")
        {
            str_dop += " AND IZP_BOOK.K_GROUP = " + DropDownListGroup.SelectedValue;
        }
        string commandString = "SELECT count(FAC_NUM) FROM IZP_BOOK WHERE" + str_dop;
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        string commandStringBrak = "SELECT count(FAC_NUM) FROM IZP_BOOK WHERE" + str_dop + " AND RESULT_EXP=0";
        OleDbCommand myCommandBrak = new OleDbCommand(commandStringBrak, myConnection);
        myConnection.Open();
        object o_count = myCommand.ExecuteScalar();
        object o_countBrak = myCommandBrak.ExecuteScalar();
        myConnection.Close();
        Label_Stat.Text = "Всего испытано:" + "<span class=\"styleG\">" + o_count.ToString() + "</span>   из них забраковано:<span class=\"styleR\">" + o_countBrak.ToString() + "</span>";
        str_dop = " AND" + str_dop;
        SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,NAME_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,C_RIGHT,C_LEFT,C_POINTER,T_WATER,T_AIR,HUMIDITY,PRESSURE,Replace(REPLACE(RESULT_EXP,'0','негодно'),'1','годно') as RESULT_EXP, TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_PROT,NUM_ACT,NOTE FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE IZP_BOOK.K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE ANd IZP_SPR_PROTECT.ID_=K_NAME" + str_dop + " order by NUM_P ";
        SqlDataSource2.SelectCommand = "Select K_GROUP,NAME_GROUP, COUNT(FAC_NUM) as ALL_COUNT, (COUNT(FAC_NUM)-SUM(RESULT_EXP)) as BRAK_COUNT from IZP_book,IZP_SPR_PERIOD WHERE IZP_BOOK.K_GROUP=NUM_PROTCL AND DATE_PROT IS NOT NULL" + str_dop + " Group by k_group,NAME_GROUP ORDER BY K_GROUP" ;
        Button2.Visible = true;
        ButtonPDF.Visible = true;
        ButtonPDF.Style["display"] = "none";

        LabelQstring.Value = "?term=" + str_term + "&podr=" + str_podr + "&brak=";
    }
    protected void RadDatePicker_date_po_SelectedDateChanged(object sender, Telerik.WebControls.SelectedDateChangedEventArgs e)
    {
        Func_NachParam();
    }
    protected void RadDatePicker_date_s_SelectedDateChanged(object sender, Telerik.WebControls.SelectedDateChangedEventArgs e)
    {
        Func_NachParam();
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        Func_RadioBut();
        Func_NachParam();
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        Func_RadioBut();
        Func_NachParam();
    }
    protected void Func_RadioBut()
    {
        if (!RadioButton1.Checked)
        {
            DropDownList_Year.Enabled = false;
            Panel2.Style["display"] = "inline";
        }
        else
        {
            DropDownList_Year.Enabled = true;
            Panel2.Style["display"] = "none";
        }
    }
    protected void DropDownList_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        Func_NachParam();
    }
    protected void Func_NachParam()
    {
        GridView1.DataBind();
        Label_Stat.Text = "";
        Button2.Visible = false;
        ButtonPDF.Visible = false;
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            LabelQstring.Value += DataBinder.Eval(e.Item.DataItem, "K_GROUP").ToString() + "," + DataBinder.Eval(e.Item.DataItem, "ALL_COUNT").ToString() + "," + DataBinder.Eval(e.Item.DataItem, "BRAK_COUNT").ToString() + ";";
            i_count_str++;
            HiddenFieldCountString.Value = i_count_str.ToString();
        }
    }
}