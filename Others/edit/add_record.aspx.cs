using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using Telerik.WebControls;

public partial class add_record : System.Web.UI.Page
{
    int i_count = 0;
    string st_grid2;
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string commandStringParam = "SELECT TO_CHAR(DATE_,'dd,MM,yyyy') as DATE_ FROM IZP_PARAM";
            OleDbCommand myCommandParam = new OleDbCommand(commandStringParam, myConnection);
            myConnection.Open();
            object o_date = myCommandParam.ExecuteScalar();
            myConnection.Close();
            DateTime date_in = Convert.ToDateTime(o_date);
            RadDatePicker_date_in.SelectedDate = date_in;
            FillDropDownListOrg();
            //FillDropDownListBrig();
            //FillDropDownListProt();
        }
        else { InjectScript_Temp.Text = ""; }
}
    protected int FNUM_P(out string s_max_num, out string s_next_num)
    {
        string commandString = "SELECT NVL(MAX(NUM_P),0) FROM IZP_BOOK WHERE K_ORGAN=" + DropDownListOrg.SelectedValue + " AND K_BRIG=" + DropDownListBrig.SelectedValue + " AND K_GROUP=" + DropDownListGroup.SelectedValue + " AND TO_CHAR(DATE_IN,'YYYY')=" + RadDatePicker_date_in.SelectedDate.ToString().Substring(6, 4) + " AND DATE_PROT IS NULL AND NUM_ACT IS NULL";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        string commandString2 = "SELECT NVL(MAX(NUM_P)+1,1) FROM IZP_BOOK WHERE TO_CHAR(DATE_IN,'YYYY')=" + RadDatePicker_date_in.SelectedDate.ToString().Substring(6, 4) + " AND NUM_P>0";
        OleDbCommand myCommand2 = new OleDbCommand(commandString2, myConnection);
        myConnection.Open();
        object max_num = myCommand.ExecuteScalar();
        s_max_num = max_num.ToString();
        object next_num = myCommand2.ExecuteScalar();
        s_next_num = next_num.ToString();
        string commandString3 = "SELECT COUNT(NUM_P) FROM IZP_BOOK WHERE K_ORGAN=" + DropDownListOrg.SelectedValue + " AND K_BRIG=" + DropDownListBrig.SelectedValue + " AND K_GROUP=" + DropDownListGroup.SelectedValue + " AND TO_CHAR(DATE_IN,'YYYY')=" + RadDatePicker_date_in.SelectedDate.ToString().Substring(6, 4) + " AND DATE_PROT IS NULL AND NUM_ACT IS NULL AND NUM_P=" + s_max_num;
        OleDbCommand myCommand3 = new OleDbCommand(commandString3, myConnection);
        object prot_count = myCommand3.ExecuteScalar();
        myConnection.Close();
        return(Convert.ToInt16(prot_count));
    }
    protected void InsertFun()
    {
        DateTime data = DateTime.Now;
        data = Convert.ToDateTime(RadDatePicker_date_in.SelectedDate);
        string num_p,max_num,next_num;
        int prot_count,ix=0,i_del=0;
        prot_count = FNUM_P(out max_num,out next_num);
        int i_check_count = CheckBoxListFacNum.Items.Count;
        int i_list_count = (i_check_count / 10)+2;
        LinkedList<string> list = new LinkedList<string>();
        LinkedList<string> list_to_del = new LinkedList<string>();
        string commandStringAVAIL = "SELECT NUMB FROM IZP_AVAILABLE WHERE TYPE_NUMB=2 AND YEAR_NUMB=" + data.ToString("yyyy").Substring(2) + " ORDER BY NUMB";
        OleDbCommand myCommandAVAIL = new OleDbCommand(commandStringAVAIL, myConnection);
        myConnection.Open();
        OleDbDataReader MyReader = myCommandAVAIL.ExecuteReader();
        while (MyReader.Read())
        {
            list.AddLast(Convert.ToString(MyReader["NUMB"]));
            list_to_del.AddLast(Convert.ToString(MyReader["NUMB"]));
            ix++;
        }
        MyReader.Close();
        myConnection.Close();
        int y = Convert.ToInt16(next_num), y_list;
        if (list.Count > 0)
        {
            y_list = Convert.ToInt16(list.Last.Value) + 1;
            if (y < y_list) { y = y_list; }
        }
        for (int i = ix; i < i_list_count; i++)
        {
            list.AddLast(y.ToString());
            y++;
        }
        next_num = list.First.Value;
        list.RemoveFirst();
        foreach (ListItem li in CheckBoxListFacNum.Items)
        {
            if (li.Selected)
            {// выбор номера протокола
                if (max_num == "0")
                {
                    num_p = next_num;
                    max_num = next_num;
                    prot_count = 1;
                    i_del++;
                    next_num = list.First.Value;
                    list.RemoveFirst();
                }
                else
                {
                    if (prot_count < 10)
                    {
                        num_p = max_num;
                        prot_count++;
                    }
                    else
                    {
                        num_p = next_num;
                        max_num = next_num;
                        prot_count = 1;
                        i_del++;
                        next_num = list.First.Value;
                        list.RemoveFirst();
                    }
                }
                // проверка групп 1-4
                string commandStringTEC = "INSERT into IZP_BOOK(ID_,NUM,NUM_P,K_ORGAN,K_BRIG,K_GROUP,K_NAME,FAC_NUM,DATE_IN,DATE_LOG,YEAR) VALUES(IZP_BOOK_SEQ.NEXTVAL,(SELECT NVL(MAX(NUM)+1,1) FROM IZP_BOOK WHERE TO_CHAR(DATE_IN,'YYYY')=" + RadDatePicker_date_in.SelectedDate.ToString().Substring(6, 4) + ")," + num_p + "," + DropDownListOrg.SelectedValue + "," + DropDownListBrig.SelectedValue + "," + DropDownListGroup.SelectedValue + "," + DropDownListProt.SelectedValue + ",'" + li.Value + "','" + RadDatePicker_date_in.SelectedDate.ToString().Substring(0, 10) + "',sysdate," + (Convert.ToInt32(RadDatePicker_date_in.SelectedDate.ToString().Substring(8, 2) + "00000") + Convert.ToInt16(num_p)) + ")";
                OleDbCommand myCommandTEC = new OleDbCommand(commandStringTEC, myConnection);
                myConnection.Open();
                object num_rowTEC = myCommandTEC.ExecuteNonQuery();
                int i_group = Convert.ToInt16(DropDownListGroup.SelectedValue);
                if (Array.IndexOf(new int[] { 1, 2, 3, 4 },i_group)>=0)
                {
                    string st_facn = "FAC" + DropDownListGroup.SelectedValue;
                    string st_fac_org = "FAC_ORG" + DropDownListGroup.SelectedValue;
                    string st_fac_brig = "FAC_BRIG" + DropDownListGroup.SelectedValue;
                    string commandString = "UPDATE IZP_FAC9999 SET " + st_facn + "=(" + st_facn + "+1)," + st_fac_org + "=" + DropDownListOrg.SelectedValue + "," + st_fac_brig + "=" + DropDownListBrig.SelectedValue + " WHERE FAC_PROT=" + li.Value;
                    OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
                    object to_izp_fac9999 = myCommand.ExecuteNonQuery();
                }
                myConnection.Close();
            }
        }

        LinkedListNode<string> node;
        myConnection.Open();
        for (node = list_to_del.First; node != null; node = node.Next)
        {
            if (i_del > 0)
            {
                string commandStringDEL = "DELETE FROM IZP_AVAILABLE WHERE NUMB=" + node.Value + " AND TYPE_NUMB=2 AND YEAR_NUMB=" + data.ToString("yyyy").Substring(2);
                OleDbCommand myCommandDEL = new OleDbCommand(commandStringDEL, myConnection);
                object sql_to_del = myCommandDEL.ExecuteNonQuery();
                i_del--;
            }
        }
        myConnection.Close();
    }
    protected void ColorDropDownListOrg() {
        DropDownListOrg.Items[0].Attributes.Add("style", "background-color:Red");
        DropDownListOrg.Items[1].Attributes.Add("style", "color:DarkOrange");     
    }
    protected void ColorDropDownListBrig()
    {
        //DropDownListBrig.Items[0].Attributes.Add("style", "background-color:Red");
        DropDownListBrig.Items[1].Attributes.Add("style", "color:DarkOrange");
    }
    protected void ColorDropDownListGroup()
    {
        DropDownListGroup.Items[0].Attributes.Add("style", "background-color:Red");
        //DropDownListGroup.Items[1].Attributes.Add("style", "color:DarkOrange");
    }
    protected void ColorDropDownListProt()
    {
        DropDownListProt.Items[0].Attributes.Add("style", "background-color:Red");
        DropDownListProt.Items[1].Attributes.Add("style", "color:DarkOrange");
    }
    protected void FillDropDownListOrg() {
        DropDownListOrg.Items.Clear();
        CheckBoxListFacNum.Items.Clear();
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
        DropDownListOrg.Items.Insert(0, new ListItem("---", "0"));
        DropDownListOrg.Items.Insert(1, new ListItem("добавить новую организацию", "1"));
        ColorDropDownListOrg();
    }
    protected void FillDropDownListBrig() {
        DropDownListBrig.Items.Clear();
        CheckBoxListFacNum.Items.Clear();
        string commandString = "SELECT CODE,NAME_BRIG,CODE_ORG FROM IZP_SPR_BRIG WHERE HIDDEN=0 AND CODE_ORG=" + DropDownListOrg.SelectedValue + " ORDER BY NAME_BRIG";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            DropDownListBrig.Items.Add(new ListItem(Convert.ToString(reader1["NAME_BRIG"]), Convert.ToString(reader1["CODE"])));
        }
        reader1.Close();
        myConnection.Close();
        DropDownListBrig.Items.Insert(0, new ListItem("-", "0"));
        DropDownListBrig.Items.Insert(1, new ListItem("добавить новую бригаду", "1"));
        ColorDropDownListBrig();    
    }
    protected void FillDropDownListGroup()
    {
        DropDownListGroup.Items.Clear();
        CheckBoxListFacNum.Items.Clear();
        string commandString = "SELECT NAME_GROUP,NUM_PROTCL FROM IZP_SPR_PERIOD WHERE NUM_PROTCL<>17 ORDER BY NUM_PROTCL";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            DropDownListGroup.Items.Add(new ListItem(Convert.ToString(reader1["NAME_GROUP"]), Convert.ToString(reader1["NUM_PROTCL"])));
        }
        reader1.Close();
        myConnection.Close();
        DropDownListGroup.Items.Insert(0, new ListItem("---", "0"));
        //ColorDropDownListProt();
    }
    protected void FillDropDownListProt() {
        DropDownListProt.Items.Clear();
        CheckBoxListFacNum.Items.Clear();
        string commandString = "SELECT ID_,NAME,FABRIC,K_GROUP FROM IZP_SPR_PROTECT WHERE HIDDEN=0 AND K_GROUP=" + DropDownListGroup.SelectedValue + " ORDER BY NAME";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            DropDownListProt.Items.Add(new ListItem(Convert.ToString(reader1["NAME"]), Convert.ToString(reader1["ID_"])));
        }
        reader1.Close();
        myConnection.Close();
        DropDownListProt.Items.Insert(0, new ListItem("---", "0"));
        DropDownListProt.Items.Insert(1, new ListItem("добавить средство защиты", "1"));
        ColorDropDownListProt();
    }
    protected void FillCheckBoxListFacNum() {
        if (DropDownListOrg.SelectedValue != "0" && DropDownListOrg.SelectedValue != "1" && DropDownListBrig.SelectedValue != "1" && DropDownListGroup.SelectedValue != "0")
        {
            CheckBoxListFacNum.Items.Clear();
            string commandString = "SELECT FAC_NUM, TO_CHAR(MAX(DATE_PROT),'DD.MM.YYYY') as DATE_PROT FROM IZP_BOOK WHERE K_ORGAN=" + DropDownListOrg.SelectedValue + " AND K_BRIG=" + DropDownListBrig.SelectedValue + " AND K_GROUP = " + DropDownListGroup.SelectedValue + " AND DATE_SPISAN IS NULL AND DATE_PROT IS NOT NULL GROUP BY FAC_NUM ORDER BY FAC_NUM";
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                ListItem item = new ListItem(Convert.ToString(reader["FAC_NUM"]) + " (" + Convert.ToString(reader["DATE_PROT"]) + ")", Convert.ToString(reader["FAC_NUM"]));
                CheckBoxListFacNum.Items.Add(item);
            }
            reader.Close();
            myConnection.Close();
        }    
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        string st_text = TextBoxNew.Text.Trim().Replace("\n", " ").Replace("\r", "");
        if (st_text != "")
        {
            string commandStringTEC = "INSERT into IZP_SPR_ORG(CODE,NAME_ORG) VALUES (IZP_SPR_ORG_SEQ.NEXTVAL,'" + st_text + "')";
            OleDbCommand myCommandTEC = new OleDbCommand(commandStringTEC, myConnection);
            myConnection.Open();
            object num_rowTEC = myCommandTEC.ExecuteNonQuery();
            myConnection.Close();
            FillDropDownListOrg();
            TextBoxNew.Visible = false;
            ButtonNew.Visible = false;
            foreach (ListItem item in DropDownListOrg.Items)
            {
                if (item.Text == st_text) { item.Selected = true; }
            }
            TextBoxNew.Text = "";
            FillDropDownListBrig();
            FillDropDownListGroup();
            FillDropDownListProt();
            FillCheckBoxListFacNum();
            ColorDropDownListOrg();
            ColorDropDownListBrig();
            ColorDropDownListGroup();
            FuncRadGrid2Fill();
        }
        ColorDropDownListOrg();
    }
    protected void ButtonNewBrig_Click(object sender, EventArgs e)
    {
        string st_text = TextBoxNewBrig.Text.Trim().Replace("\n", " ").Replace("\r", "");
        if (st_text != "")
        {
            string commandStringTEC = "INSERT into IZP_SPR_BRIG(CODE,NAME_BRIG,CODE_ORG) VALUES (IZP_SPR_BRIG_SEQ.NEXTVAL,'" + st_text + "'," + DropDownListOrg.SelectedValue + ")";
            OleDbCommand myCommandTEC = new OleDbCommand(commandStringTEC, myConnection);
            myConnection.Open();
            object num_rowTEC = myCommandTEC.ExecuteNonQuery();
            myConnection.Close();
            FillDropDownListBrig();
            TextBoxNewBrig.Visible = false;
            ButtonNewBrig.Visible = false;
            foreach (ListItem item in DropDownListBrig.Items)
            {
                if (item.Text == st_text) { item.Selected = true; }
            }
            TextBoxNewBrig.Text = "";
            FuncRadGrid2Fill();
        }
        ColorDropDownListOrg();
        ColorDropDownListBrig();
        ColorDropDownListGroup();
        ColorDropDownListProt();
    }
    protected void ButtonNewProt_Click(object sender, EventArgs e)
    {
        string st_text = TextBoxNewProt.Text.Trim().Replace("\n", " ").Replace("\r", "");
        if (st_text != "")
        {
            string commandStringTEC = "INSERT into IZP_SPR_PROTECT(ID_,NAME,K_GROUP) VALUES (IZP_SPR_PROTECT_SEQ.NEXTVAL,'" + st_text + "'," + DropDownListGroup.SelectedValue + ")";
            OleDbCommand myCommandTEC = new OleDbCommand(commandStringTEC, myConnection);
            myConnection.Open();
            object num_rowTEC = myCommandTEC.ExecuteNonQuery();
            myConnection.Close();
            FillDropDownListProt();
            TextBoxNewProt.Visible = false;
            ButtonNewProt.Visible = false;
            foreach (ListItem item in DropDownListProt.Items)
            {
                if (item.Text == st_text) { item.Selected = true; }
            }
            TextBoxNewProt.Text = "";
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //SqlDataSource1.SelectCommand = "SELECT CODE, NAME_ORG, HIDDEN FROM IZP_SPR_ORG WHERE NAME_ORG LIKE '%"+TextBox1.Text+"%'";
    }
    protected void FuncRadGrid2Fill()
    {
        i_count = 0;
        string str_brig = "1";
        if (DropDownListBrig.Items.Count > 0)
        {
            str_brig = " AND K_BRIG=" + DropDownListBrig.SelectedValue;
        }
        SqlDataSource2.SelectCommand = "SELECT IZP_BOOK.K_GROUP AS K_GROUP,NAME_GROUP,NUM_P,EDIZM,K_NAME,IZP_SPR_PROTECT.NAME AS NAME,COUNT(K_NAME) as QUANTITY FROM IZP_BOOK,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE IZP_BOOK.K_GROUP=IZP_SPR_PERIOD.NUM_PROTCL AND IZP_SPR_PROTECT.ID_=K_NAME AND K_ORGAN=" + DropDownListOrg.SelectedItem.Value + str_brig + " AND TO_CHAR(DATE_IN,'yyyy')='" + RadDatePicker_date_in.SelectedDate.ToString().Substring(6, 4) + "' AND NUM_ACT IS NULL GROUP BY IZP_BOOK.K_GROUP,NAME_GROUP,NUM_P,EDIZM,K_NAME,NAME ORDER BY IZP_BOOK.K_GROUP";
        RadGrid2.DataBind();
    }
    protected void DropDownListOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListOrg.SelectedItem.Value == "1")
        {
            ColorDropDownListOrg();
            TextBoxNew.Visible = true;
            ButtonNew.Visible = true;
            TextBoxNew.Focus();
        }
        else
        {
            TextBoxNew.Visible = false;
            ButtonNew.Visible = false;
            FillDropDownListBrig();
            FillDropDownListGroup();
            FillDropDownListProt();
            FillCheckBoxListFacNum();
            ColorDropDownListOrg();
            ColorDropDownListBrig();
            ColorDropDownListGroup();
            ColorDropDownListProt();
        }
        FuncRadGrid2Fill();
    }
    protected void DropDownListBrig_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListBrig.SelectedItem.Value == "1")
        {
            TextBoxNewBrig.Visible = true;
            ButtonNewBrig.Visible = true;
            TextBoxNewBrig.Focus();
        }
        else
        {
            TextBoxNewBrig.Visible = false;
            ButtonNewBrig.Visible = false;
            FillCheckBoxListFacNum();
        }
        ColorDropDownListOrg();
        ColorDropDownListBrig();
        ColorDropDownListGroup();
        ColorDropDownListProt();
        FuncRadGrid2Fill();
    }
    protected void DropDownListGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDownListProt();
        FillCheckBoxListFacNum();
        ColorDropDownListOrg();
        ColorDropDownListBrig();
        ColorDropDownListGroup();
        ColorDropDownListProt();
        string ddlsival = DropDownListGroup.SelectedValue;
        ButtonFacNum.Visible = true;
        if (ddlsival == "1" || ddlsival == "2" || ddlsival == "3" || ddlsival == "4")
        {
            if (ddlsival == "4")
            {
                DropDownListProt.Items[0].Selected = true;
            }
            else
            {
                DropDownListProt.Items[2].Selected = true;
            }
            LabelFacNum.Text = "наберите кол-во новых пар или штук";
        }
        else
        {
            LabelFacNum.Text = "введите заводской номер";
        }
    }
    protected void DropDownListProt_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColorDropDownListOrg();
        ColorDropDownListBrig();
        ColorDropDownListGroup();
        ColorDropDownListProt();
        if (DropDownListProt.SelectedItem.Value == "1")
        {
            TextBoxNewProt.Visible = true;
            ButtonNewProt.Visible = true;
            TextBoxNewProt.Focus();
        }
        else
        {
            FillCheckBoxListFacNum();
            TextBoxNewProt.Visible = false;
            ButtonNewProt.Visible = false;
        }
    }
    protected void ButtonFacNum_Click(object sender, EventArgs e)
    {
        if (TextBoxFacNum.Text != "" && TextBoxFacNum.Text != "0")
        {// ф-я заполнения checkboxlist для групп средств защиты=[1,2,3]. Сначала берутся свободные значения из таблицы IZP_SPISAN, а затем - следующий по порядку до 9999
            string ddlsival = DropDownListGroup.SelectedValue;
            if (ddlsival == "1" || ddlsival == "2" || ddlsival == "3" || ddlsival == "4")
            {
                //LabelFacNum.Text = ddlsival;
                   int n;
                   if (Int32.TryParse(TextBoxFacNum.Text, out n))
                   {
                       string commandString = "SELECT FAC_PROT FROM IZP_FAC9999 WHERE FAC" + ddlsival + "=0 AND ROWNUM<" + (n+1).ToString() + " ORDER BY FAC_PROT";
                       OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
                       myConnection.Open();
                       OleDbDataReader reader = myCommand.ExecuteReader();
                       while (reader.Read())
                       {
                           string fac_num = Convert.ToString(reader["FAC_PROT"]); 
                           ListItem item = new ListItem(fac_num + " (нов.)", fac_num);
                           CheckBoxListFacNum.Items.Add(item);
                           item.Selected = true;
                       }
                       reader.Close();
                       myConnection.Close();
                       ButtonFacNum.Visible = false;
                   }
            }
            else
            {
                ListItem item = new ListItem(TextBoxFacNum.Text + " (нов.)", TextBoxFacNum.Text);
                if (CheckBoxListFacNum.Items.FindByValue(TextBoxFacNum.Text) == null)
                {
                    CheckBoxListFacNum.Items.Insert(0, item);
                    item.Selected = true;
                }
                else
                {
                    CheckBoxListFacNum.Items.FindByValue(TextBoxFacNum.Text).Selected = true;
                }
                TextBoxFacNum.Focus();
            }
            TextBoxFacNum.Text = "";
        }
        ColorDropDownListOrg();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FuncExecuteIn())
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;
            Image1.Visible = true;
            InsertFun();
            double IEx = Convert.ToDouble(Request.Browser.Version.Replace(".", ","));
            if (IEx >= 7)
            { InjectScript.Text = "<script type=\"text/javascript\">CloseAndRebind(true)</script>"; }
            else
            { InjectScript.Text = "<script type=\"text/javascript\">CancelEdit()</script>"; }
        }
        else
        {
            InjectScript_Temp.Text = "<script type=\"text/javascript\">fnHide('Ошибка ввода!')</script>";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (FuncExecuteIn())
        {
            InsertFun();
            InjectScript_Temp.Text = "<script type=\"text/javascript\">fnHide('запись успешно сохранена!')</script>";
            if (DropDownListGroup.SelectedValue == "1" || DropDownListGroup.SelectedValue == "2" || DropDownListGroup.SelectedValue == "3")
            {
                DropDownListGroup.SelectedIndex = 0;
                ColorDropDownListGroup();
            }
            ButtonFacNum.Visible = true;
            DropDownListProt.SelectedIndex = 0;
            ColorDropDownListProt();
            TextBoxFacNum.Text = "";
            CheckBoxListFacNum.Items.Clear();
            FuncRadGrid2Fill();
        }
        else
        {
            InjectScript_Temp.Text = "<script type=\"text/javascript\">fnHide('Ошибка ввода!')</script>";
        }
    }
    protected bool FuncExecuteIn()
    {
        bool count_checked= false;
        foreach (ListItem li in CheckBoxListFacNum.Items)
        {
            if (li.Selected) { count_checked = true; }
        }
        if (DropDownListOrg.SelectedValue != "0" && DropDownListOrg.SelectedValue != "1" && DropDownListBrig.SelectedValue != "1" && DropDownListGroup.SelectedValue != "0" && DropDownListProt.SelectedValue != "1" && DropDownListProt.SelectedValue != "0" && count_checked)
        { return true; } else { return false; }
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            string st_r;
           i_count += Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "QUANTITY"));
           Label Label_Num_p = e.Item.FindControl("Label_Num_p") as Label;
           st_r = (DataBinder.Eval(e.Item.DataItem, "NUM_P")).ToString();
           if (st_r != st_grid2) { Label_Num_p.Text = st_r; } else { Label_Num_p.Text = "-//-"; }
           st_grid2 = st_r;

        }
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = (GridFooterItem)e.Item;
            Label Label_SumQuantity = footerItem.FindControl("Label_SumQuantity") as Label;
            Label_SumQuantity.Text = i_count.ToString();
        }
    }
    protected void ButtonSQL_Click(object sender, EventArgs e)
    {
        /*string commandString2 = "SELECT * FROM IZP_BOOK WHERE K_GROUP=4";
        OleDbCommand myCommand2 = new OleDbCommand(commandString2, myConnection);
        myConnection.Open();
        OleDbDataReader reader = myCommand2.ExecuteReader();
        while (reader.Read())
        {
            Convert.ToString(reader["FAC_NUM"]);
            string commandString3 = "UPDATE IZP_FAC9999 SET FAC4=1,FAC_ORG4=" + Convert.ToString(reader["K_ORGAN"]) + ",FAC_BRIG4=" + Convert.ToString(reader["K_BRIG"]) + "  WHERE FAC_PROT=" + Convert.ToString(reader["FAC_NUM"]);
            OleDbCommand myCommand3 = new OleDbCommand(commandString3, myConnection);
            object del = myCommand3.ExecuteNonQuery();
        }
        reader.Close();
        myConnection.Close();*/
    }
}