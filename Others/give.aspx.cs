using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using System.Data.OleDb;

public partial class give : System.Web.UI.Page
{
    string st_filter_org = "", st_org_val = "", st_filter_brig = "", st_brig_val = "", st_filter_group = "", st_group_val = "";
    string st_fac_num, st_ES;
    string st_k_group = "-1", st_k_organ = "-1", st_k_brig = "-1", st_k_gr = "-1", st_k_org = "-1", st_k_br = "-1", st_k_name = "-1";
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
        RadGrid2.DataBind();
        ButtonSave.Visible = false;
        Button_CheckAll.Visible = false;
        //SqlDataSource2.SelectCommand
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink hyperLink_cell = e.Item.FindControl("HyperLink_CELL") as HyperLink;
            hyperLink_cell.Attributes["href"] = "#";
            hyperLink_cell.Attributes["onclick"] = string.Format("return ShowCabinets('{0}','{1}');", Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CELL")), e.Item.ItemIndex);
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
                Label_Name_Group.Text = "-//-";
            }
            st_k_group = st_k_gr;
            st_k_organ = st_k_org;
            CheckBox checkbox_ES = e.Item.FindControl("check_Exec_Sign") as CheckBox;
            st_ES = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "EXEC_SIGN"));
            if (st_ES == "1")
            {
                checkbox_ES.Checked = true;
            }
            else
            {
                checkbox_ES.Checked = false;
            }
            Label Label_Fac_Num = e.Item.FindControl("Label_Fac_Num") as Label;
            Label_Fac_Num.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FAC_NUM")) + "<span class=\"styleGr\"> (" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DATE_PROT")).Substring(0, 10) + ")</span>";
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        foreach (GridItem item in RadGrid2.MasterTableView.Items)
        {
            GridDataItem dataitem = (GridDataItem)item;
            TableCell cell = dataitem["EXEC_SIGN"];
            CheckBox checkBox = (CheckBox)cell.FindControl("check_Exec_Sign");
            if (checkBox.Checked)
            {
                st_ES = "1"; 
                string commandString = "UPDATE IZP_BOOK SET CELL=0, EXEC_SIGN=" + st_ES + " WHERE ID_=" + dataitem.GetDataKeyValue("ID").ToString();
                OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
                object update_exec_sign = myCommand.ExecuteNonQuery();
            }
            else { st_ES = "0"; }
        }
        myConnection.Close();
        SqlDataSource2.SelectCommand = HiddenFieldSDS2.Value;
        RadGrid2.DataBind();
    }
    protected void ButtonFilter_Click(object sender, EventArgs e)
    {
        st_org_val = DropDownListOrg.SelectedValue;
        st_brig_val = DropDownListBrig.SelectedValue;
        st_group_val = DropDownListGroup.SelectedValue;
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
            st_filter_group = " AND IZP_BOOK.K_GROUP =" + st_group_val + " ";
        }
        else { st_filter_group = ""; }

        SqlDataSource2.SelectCommand = "SELECT IZP_BOOK.ID_ AS ID,K_ORGAN,K_BRIG,NAME_ORG,NAME_BRIG,IZP_BOOK.K_GROUP,NAME_GROUP,K_NAME,NAME,FAC_NUM,CELL,DATE_PROT,EXEC_SIGN FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE AND IZP_BOOK.K_GROUP=NUM_PROTCL AND K_NAME=IZP_SPR_PROTECT.ID_ AND DATE_PROT IS NOT NULL AND CELL<>0 AND EXEC_SIGN=0 " + st_filter_org + st_filter_brig + st_filter_group + "ORDER BY IZP_BOOK.K_GROUP";
        HiddenFieldSDS2.Value = SqlDataSource2.SelectCommand;

    }
    protected void RadGrid2_DataBound(object sender, EventArgs e)
    {
        FuncRadGridCount();
    }
    protected void FuncRadGridCount()
    {
        if (RadGrid2.MasterTableView.Items.Count > 0)
        {
            ButtonSave.Visible = true;
            Button_CheckAll.Visible = true;
        }
        else
        {
            ButtonSave.Visible = false;
            Button_CheckAll.Visible = false;
        }
    }
}