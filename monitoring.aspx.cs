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
    string st_filter_org = "", st_org_val = "", st_filter_brig = "", st_brig_val = "", st_filter_group = "", st_group_val = "", st_filter_condition = "", st_condition_val = "", st_filter_date = "", st_date_val = "";
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    int i_period, i_count, i_count_d = 0;
    string st_fac_num, st_spisan = "false", str_edit_link = "";
    DateTime date_Today = DateTime.Today;
    DateTime date2099 = Convert.ToDateTime("01.01.2100");
    DateTime date_prot = DateTime.Today;
    string st_k_group = "-1", st_k_organ = "-1", st_k_brig = "-1", st_k_gr = "-1", st_k_org = "-1", st_k_br = "-1", st_k_name = "-1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //SqlDataSource2.SelectCommand = "SELECT K_ORGAN,NAME_ORG,K_GROUP,NAME_GROUP,K_NAME,NAME,COUNT(FAC_NUM) AS QUANTITY FROM (SELECT K_ORGAN,NAME_ORG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM, MAX(NVL(DATE_PROT,'01.01.2100')) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE K_ORGAN=CODE AND IZP_BOOK.K_GROUP=NUM_PROTCL AND K_NAME=IZP_SPR_PROTECT.ID_ GROUP BY K_ORGAN,NAME_ORG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM) GROUP BY K_ORGAN,NAME_ORG,K_GROUP,NAME_GROUP,K_NAME,NAME";
            //RadGrid2.DataBind();
            string commandString = "SELECT CODE,NAME_ORG FROM PES_IZP_SPR_ORG WHERE HIDDEN=0 ORDER BY NAME_ORG";
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
            DropDownListCondition.Items.Insert(0, new ListItem("все СЗ", "0"));
            DropDownListCondition.Items.Insert(1, new ListItem("в работе", "1"));
            DropDownListCondition.Items.Insert(2, new ListItem("осталось 10 дней", "2"));
            DropDownListCondition.Items.Insert(3, new ListItem("подлежат испытанию", "3"));
            DropDownListCondition.Items.Insert(4, new ListItem("на испытании", "4"));
            DropDownListCondition.Items.Insert(5, new ListItem("списанные", "5"));
        }
        ColorDropDownLists();

    }
    protected void ColorDropDownLists()
    {
        DropDownListGroup.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListOrg.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListBrig.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListCondition.Items[0].Attributes.Add("style", "color:Blue");
        DropDownListCondition.Items[2].Attributes.Add("style", "color:Orange");
        DropDownListCondition.Items[3].Attributes.Add("style", "color:Red");
        DropDownListCondition.Items[4].Attributes.Add("style", "color:Green");
        DropDownListCondition.Items[5].Attributes.Add("style", "background-color:Red");
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {

            Label Label_Name_Group = e.Item.FindControl("Label_Name_Group") as Label;
            st_k_gr = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_GROUP"));
            st_k_org = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_ORGAN"));
            st_k_br = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_BRIG"));
            st_k_name = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_NAME"));
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
            string str_Label_Detail = "", st_link = "";
            FUNC_DATE_PROT("WHERE");
           // string commandStri45 = "SELECT FAC_NUM,PERIOD,DATE_PROT FROM (SELECT FAC_NUM,PERIOD,MAX(ADD_MONTHS(NVL(DATE_PROT,'01.01.2100'),PERIOD)) AS DATE_PROT FROM PES_IZP_BOOK,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=" + st_k_org + " AND  K_GROUP=" + st_k_gr + " AND K_BRIG=" + st_k_br + " AND K_NAME=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_NAME")) + " AND " + st_filter_condition + " GROUP BY FAC_NUM,PERIOD)"+st_filter_date;
            string commandString = "SELECT FAC_NUM,PERIOD,DATE_PROT FROM (SELECT FAC_NUM,PERIOD,MAX(DATE_PROT) AS DATE_PROT FROM (SELECT FAC_NUM,PERIOD,(ADD_MONTHS(NVL(DATE_PROT,'01.01.2100'),PERIOD)+EXEC_SIGN/24/60/60) as DATE_PROT FROM PES_IZP_BOOK,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=" + st_k_org + " AND  K_GROUP=" + st_k_gr + " AND K_BRIG=" + st_k_br + " AND K_NAME=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "K_NAME")) + " AND " + st_filter_condition + ") GROUP BY FAC_NUM,PERIOD)" + st_filter_date;
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader Reader = myCommand.ExecuteReader();
            i_count_d = 1;
            string st_style_inpes1 = "";
            while (Reader.Read())
            {
                if (Session["SIZP_User"] == null)// доработать!!! переместить в hiddenfield, чтобы не запрашивало каждый раз при перестроении radGrid
                {
                    str_edit_link = "";
                }
                else
                {
                    if (Session["SIZP_User"].ToString() != "-")
                    {
                        str_edit_link = "разрешить";
                    }
                }
                st_fac_num = Convert.ToString(Reader["FAC_NUM"]);
                date_prot = Convert.ToDateTime(Reader["DATE_PROT"]);
                if (date_prot.ToString("ss") == "01") { date_prot = date_prot.AddSeconds(-1); st_style_inpes1 = ""; } else { st_style_inpes1 = "border-color:Lime; border-style:solid; border-width:thin;"; }
                i_period = Convert.ToInt16(Reader["PERIOD"]);
                DateTime date_prot_spisan = date_prot.AddMonths(-i_period);
                
                if (str_edit_link == "разрешить") { str_edit_link = "cursor:pointer\" onclick=\"return ShowInsertValForm('Pes/sz.aspx?k_group=" + st_k_gr + "&k_name=" + st_k_name + "&fac_num=" + st_fac_num + "&spisan=" + st_spisan + "');"; }
                st_link = "style=\"" + st_style_inpes1 + str_edit_link + "\"";

                st_fac_num = "<span " + st_link + ">&nbsp" + st_fac_num + "&nbsp</span>";

                if (st_spisan != "true")
                {
                    if (date_prot >= date_Today && date_prot <= date_Today.AddDays(10))
                    {
                        st_fac_num = "<span " + " class=\"styleOr\" " + ">" + st_fac_num + "</span>";
                    }
                    if (date_prot < date_Today)
                    {
                        st_fac_num = "<span " + " class=\"styleR\" " + ">" + st_fac_num + "</span>";
                    }
                    if (date_prot > date2099)
                    {
                        st_fac_num = "<span " + " class=\"styleG\" " + st_fac_num + "</span>";
                    }
                    else
                    {
                        st_fac_num += "<span class=\"styleGr\">(" + date_prot.ToString("dd.MM.yy") + ")</span>";
                    }
                    //st_fac_num += "<span style=\" border-color:Lime; border-style:solid; border-width:thin\" class=\"styleGr\">(" + date_prot.ToString() + st_inpes+ ")</span>";
                }
                else
                {
                    //st_fac_num = "<span " + " class=\"styleRBack\" " + ">&nbsp;" + st_fac_num + "&nbsp;</span>";
                    st_fac_num = st_fac_num.Replace("style=\"", "style=\" background-color:Red;color:White;");
                    st_fac_num += "<span class=\"styleGr\">(спис. " + date_prot_spisan.ToString("dd.MM.yy") + ")</span>";
                }
                str_Label_Detail += st_fac_num + ", ";
                if (i_count_d % 4 == 0 && i_count_d != 0) 
                {
                    str_Label_Detail += "<br/>";
                }
                i_count_d++;
            }
            if ((i_count_d - 1) % 4 == 0) // удаляем последнюю запятую или <br/>
            {
                str_Label_Detail = str_Label_Detail.Substring(0, str_Label_Detail.Length - 7);
            }
            else
            {
                str_Label_Detail = str_Label_Detail.Substring(0, str_Label_Detail.Length - 2);
            }
            Label_Detail.Text = "<p>" + str_Label_Detail + "</p>";
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
        st_brig_val = DropDownListBrig.SelectedValue;
        st_group_val = DropDownListGroup.SelectedValue;
        st_condition_val = DropDownListCondition.SelectedValue;
        if (st_org_val != "0")
        {
            st_filter_org = " AND K_ORGAN =" + st_org_val + " ";
        }
        else { st_filter_org = ""; }
        if (st_brig_val != "0")
        {
            st_filter_brig = " AND K_BRIG =" + st_brig_val + " ";
        }
        else { st_filter_brig = ""; }
        if (st_group_val != "0")
        {
            st_filter_group = " AND PES_IZP_BOOK.K_GROUP =" + st_group_val + " ";
        }
        else { st_filter_group = ""; }
        FUNC_DATE_PROT("WHERE");
        /*if (st_condition_val == "5")
        {
            st_filter_condition = "DATE_SPISAN IS NOT NULL";
        }
        else
        {
            st_filter_condition = "DATE_SPISAN IS NULL";
        }*/
        SqlDataSource2.SelectCommand = "SELECT K_ORGAN,K_BRIG,NAME_ORG,NAME_BRIG,K_GROUP,NAME_GROUP,K_NAME,NAME,COUNT(FAC_NUM) AS QUANTITY FROM (SELECT K_ORGAN,K_BRIG,NAME_ORG,NAME_BRIG,PES_IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM, MAX(ADD_MONTHS(NVL(DATE_PROT,'01.01.2100'),PERIOD)) AS DATE_PROT FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE " + st_filter_condition + " " + st_filter_org + st_filter_brig + st_filter_group + " AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE AND PES_IZP_BOOK.K_GROUP=NUM_PROTCL AND K_NAME=IZP_SPR_PROTECT.ID_ GROUP BY K_ORGAN,K_BRIG,NAME_ORG,NAME_BRIG,PES_IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM)" + st_filter_date + " GROUP BY K_ORGAN,K_BRIG,NAME_ORG,NAME_BRIG,K_GROUP,NAME_GROUP,K_NAME,NAME";
        HiddenFieldSDS2.Value = SqlDataSource2.SelectCommand;
        //Label_Temp.Text = SqlDataSource2.SelectCommand;
        //RadGrid2.DataBind();
    }
    protected void FUNC_DATE_PROT(string str)
    {
        st_date_val = DropDownListCondition.SelectedValue;
        st_filter_condition = "DATE_SPISAN IS NULL";
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
            case "5":
                st_filter_date = "";
                st_filter_condition = "DATE_SPISAN IS NOT NULL";
                st_spisan = "true";
                break;
        }
    }
    protected void DropDownListOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListBrig.Items.Clear();
        string commandString = "SELECT CODE,NAME_BRIG FROM PES_IZP_SPR_BRIG WHERE HIDDEN=0 AND CODE_ORG<>0 AND CODE_ORG=" + DropDownListOrg.SelectedValue + " ORDER BY NAME_BRIG";
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
        RadGrid2.DataBind();
    }
    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
        base.RaisePostBackEvent(sourceControl, eventArgument);

        if (sourceControl is RadGrid)
        {
            SqlDataSource2.SelectCommand = HiddenFieldSDS2.Value;
            RadGrid2.DataBind();
        }

    }
}