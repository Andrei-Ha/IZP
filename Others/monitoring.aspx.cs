using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using System.Data.OleDb;

public partial class Others_monitoring : System.Web.UI.Page
{
    string st_filter_org = "", st_org_val = "", st_filter_group = "", st_group_val = "", st_filter_date = "", st_date_val = "";
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    int i_period, i_count, i_count_d = 0;
    string st_fac_num;
    DateTime date_Today = DateTime.Today;
    DateTime date2099 = Convert.ToDateTime("01.01.2100");
    DateTime date_prot = DateTime.Today;
    string st_k_group = "-1", st_k_organ = "-1", st_k_gr = "-1", st_k_org = "-1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //SqlDataSource2.SelectCommand = "SELECT K_ORGAN,NAME_ORG,K_GROUP,NAME_GROUP,K_NAME,NAME,COUNT(FAC_NUM) AS QUANTITY FROM (SELECT K_ORGAN,NAME_ORG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM, MAX(NVL(DATE_PROT,'01.01.2100')) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE K_ORGAN=CODE AND IZP_BOOK.K_GROUP=NUM_PROTCL AND K_NAME=IZP_SPR_PROTECT.ID_ GROUP BY K_ORGAN,NAME_ORG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM) GROUP BY K_ORGAN,NAME_ORG,K_GROUP,NAME_GROUP,K_NAME,NAME";
            //RadGrid2.DataBind();
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
            DropDownListOrg.Items.Insert(0, new ListItem("все организации", "0"));
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
            DropDownListCondition.Items.Insert(0, new ListItem("все СЗ", "0"));
            DropDownListCondition.Items.Insert(1, new ListItem("в работе", "1"));
            DropDownListCondition.Items.Insert(2, new ListItem("осталось 10 дней", "2"));
            DropDownListCondition.Items.Insert(3, new ListItem("подлежат испытанию", "3"));
            DropDownListCondition.Items.Insert(4, new ListItem("на испытании", "4"));
        }
        ColorDropDownLists();
    }
    protected void ColorDropDownLists()
    {
        DropDownListGroup.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListOrg.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListCondition.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListCondition.Items[2].Attributes.Add("style", "color:Orange");
        DropDownListCondition.Items[3].Attributes.Add("style", "color:Red");
        DropDownListCondition.Items[4].Attributes.Add("style", "color:Green");
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label Label_Name_Group = e.Item.FindControl("Label_Name_Group") as Label;
            st_k_gr = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_GROUP"));
            st_k_org = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_ORGAN"));
            if (st_k_group != st_k_gr || st_k_organ != st_k_org)
            {
                Label_Name_Group.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "NAME_GROUP"));
            }
            else
            {
                Label_Name_Group.Text = "";
            }
            st_k_group = st_k_gr;
            st_k_organ = st_k_org;
            Label Label_Detail = e.Item.FindControl("Label_Detail") as Label;
            Label_Detail.Text = "";
            FUNC_DATE_PROT("WHERE");
            string commandString = "SELECT FAC_NUM,PERIOD,DATE_PROT FROM (SELECT FAC_NUM,PERIOD,MAX(ADD_MONTHS(NVL(DATE_PROT,'01.01.2100'),PERIOD)) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=" + st_k_org + " AND  K_GROUP=" + st_k_gr + " AND K_NAME=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_NAME")) + " GROUP BY FAC_NUM,PERIOD)"+st_filter_date;
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader Reader = myCommand.ExecuteReader();
            i_count_d = 1;
            
            while (Reader.Read())
            {
                st_fac_num = Convert.ToString(Reader["FAC_NUM"]);
                date_prot = Convert.ToDateTime(Reader["DATE_PROT"]);
                i_period = Convert.ToInt16(Reader["PERIOD"]);
                if (date_prot >= date_Today && date_prot <= date_Today.AddDays(10))
                {
                    st_fac_num = "<span class=\"styleOr\">" + st_fac_num + "</span>";
                }
                if (date_prot < date_Today)
                {
                    st_fac_num = "<span class=\"styleR\">" + st_fac_num + "</span>";
                }
                if (date_prot > date2099)
                {
                    st_fac_num = "<span class=\"styleG\">" + st_fac_num + "</span>";
                }
                else
                {
                    st_fac_num += "<span class=\"styleGr\">(" + date_prot.ToString("dd.MM.yy") + ")</span>";
                }
                Label_Detail.Text += st_fac_num + ", ";
                if (i_count_d % 5 == 0 && i_count_d != 0) 
                {
                    Label_Detail.Text += "<br/>";
                }
                i_count_d++;
            }
            if ((i_count_d - 1) % 5 == 0) // удаляем последнюю запятую или <br/>
            {
                Label_Detail.Text = Label_Detail.Text.Substring(0, Label_Detail.Text.Length - 7);
            }
            else
            {
                Label_Detail.Text = Label_Detail.Text.Substring(0, Label_Detail.Text.Length - 2);
            }
            Reader.Close();
            myConnection.Close();
            i_count += Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "QUANTITY"));
        }
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = (GridFooterItem)e.Item;
            Label Label_SumQuantity = footerItem.FindControl("Label_SumQuantity") as Label;
            Label_SumQuantity.Text = i_count.ToString();
        }
    }
    protected void ButtonFilter_Click(object sender, EventArgs e)
    {
        st_org_val = DropDownListOrg.SelectedValue;
        st_group_val = DropDownListGroup.SelectedValue;
        if (st_org_val != "0")
        {
            st_filter_org = " AND K_ORGAN =" + st_org_val + " ";
        }
        else { st_filter_org = ""; }
        if (st_group_val != "0")
        {
            st_filter_group = " AND IZP_BOOK.K_GROUP =" + st_group_val + " ";
        }
        else { st_filter_group = ""; }
        FUNC_DATE_PROT("WHERE");
        SqlDataSource2.SelectCommand = "SELECT K_ORGAN,NAME_ORG,K_GROUP,NAME_GROUP,K_NAME,NAME,COUNT(FAC_NUM) AS QUANTITY FROM (SELECT K_ORGAN,NAME_ORG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM, MAX(ADD_MONTHS(NVL(DATE_PROT,'01.01.2100'),PERIOD)) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE K_ORGAN=CODE " + st_filter_org + st_filter_group + " AND IZP_BOOK.K_GROUP=NUM_PROTCL AND K_NAME=IZP_SPR_PROTECT.ID_ GROUP BY K_ORGAN,NAME_ORG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM)" + st_filter_date + " GROUP BY K_ORGAN,NAME_ORG,K_GROUP,NAME_GROUP,K_NAME,NAME";
        RadGrid2.DataBind();
    }
    protected void FUNC_DATE_PROT(string str)
    {
        st_date_val = DropDownListCondition.SelectedValue;
        switch (st_date_val)
        {
            case "0":
                st_filter_date = "";
                break;
            case "1":
                st_filter_date = " " + str + " DATE_PROT>TRUNC(SYSDATE+10) AND DATE_PROT<'01.01.2100'";
                break;
            case "2":
                st_filter_date = " " + str + " DATE_PROT>=TRUNC(SYSDATE) AND DATE_PROT<=TRUNC(SYSDATE+10)";
                break;
            case "3":
                st_filter_date = " " + str + " DATE_PROT<TRUNC(SYSDATE)";
                break;
            case "4":
                st_filter_date = " " + str + " DATE_PROT>'01.01.2100'";
                break;

        }
    }
}