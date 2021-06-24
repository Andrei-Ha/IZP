using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using System.Data.OleDb;
using NativeExcel;
using GemBox.Spreadsheet;

public partial class output_acts : System.Web.UI.Page
{

    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    int total_price;
    int row_count;
    string st_org,st_num_act;
    string excl_organ;
    //string excl_brig;
    string[] arrExcl = new string[40];
    string[] separators = { ";", ":" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //InjectScript.Text = "";
        }
        else
        {
            SqlDataSource1.SelectCommand = "SELECT NAME_ORG, K_ORGAN, NUM_ACT, DATE_IN, COUNT(K_BRIG) AS QUANTITY FROM (SELECT DISTINCT NAME_ORG,K_ORGAN,K_BRIG,NUM_ACT,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN FROM IZP_BOOK,IZP_SPR_ORG WHERE K_ORGAN=IZP_SPR_ORG.CODE AND NUM_ACT IS NULL) GROUP BY NAME_ORG, K_ORGAN, NUM_ACT, DATE_IN ORDER BY DATE_IN DESC";
            DateTime DateToday = DateTime.Today;
            string s_year = DateToday.ToString("yyyy");
            Label_Year.Text = s_year + " год";
        }
        InjectScript.Text = "";
    }
    protected string FuncTakeNumAct()
    {
        string st_num_act;
        DateTime data = DateTime.Now;
        string commandString, commandStringAct;
        OleDbCommand myCommand, myCommandAct;
        myConnection.Open();
        if (CheckBox_Act.Checked == true)
        {
            commandStringAct = "SELECT MAX(NUMB) FROM IZP_AVAILABLE WHERE NUMB<1 AND TYPE_NUMB=1 AND YEAR_NUMB=" + data.ToString("yyyy").Substring(2);
            myCommandAct = new OleDbCommand(commandStringAct, myConnection);
            object num_act_av = myCommandAct.ExecuteScalar();
            st_num_act = num_act_av.ToString();
            if (st_num_act == "")
            {
                commandString = "SELECT NVL(MIN(NUM_ACT)-1,0) FROM IZP_BOOK WHERE TO_CHAR(DATE_IN,'YYYY')=" + data.ToString("yyyy") + " AND NUM_ACT<1";
                myCommand = new OleDbCommand(commandString, myConnection);
                object num_act = myCommand.ExecuteScalar();
                st_num_act = num_act.ToString();
            }
        }
        else
        {
            commandStringAct = "SELECT MIN(NUMB) FROM IZP_AVAILABLE WHERE NUMB>0 AND TYPE_NUMB=1 AND YEAR_NUMB=" + data.ToString("yyyy").Substring(2);
            myCommandAct = new OleDbCommand(commandStringAct, myConnection);
            object num_act_av = myCommandAct.ExecuteScalar();
            st_num_act = num_act_av.ToString();
            if (st_num_act == "")
            {
                commandString = "SELECT NVL(MAX(NUM_ACT)+1,1) FROM IZP_BOOK WHERE TO_CHAR(DATE_IN,'YYYY')=" + data.ToString("yyyy") + " AND NUM_ACT>0";
                myCommand = new OleDbCommand(commandString, myConnection);
                object num_act = myCommand.ExecuteScalar();
                st_num_act = num_act.ToString();
            }

        }
        myConnection.Close();
        return st_num_act;
        //Button1.OnClientClick = "return confirm('Вы уверены, что хотите удалить все входящие в этот акт средства защиты?');";
    }
    protected void RadGrid1_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Params_to_Zero();
            Label Label_Org = e.Item.FindControl("Label_Org") as Label;
            Label_Org.Text = DataBinder.Eval(e.Item.DataItem, "NAME_ORG").ToString();
            string st_q = DataBinder.Eval(e.Item.DataItem, "QUANTITY").ToString();
            Label Label_Quantity = e.Item.FindControl("Label_Quantity") as Label;
            if (st_q != "1")
            {
                Label_Quantity.Text = "(" + st_q + ")";
            }
        }
    }
    protected void RadGrid1_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            //Button1.Text = FuncTakeNumAct();
            InjectScript.Text = "";
            total_price = 0;
            row_count = 0;
            for (int i = 0; i < 40; i++)
            {
                arrExcl[i] = "0";
            }
            st_org = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["K_ORGAN"]).ToString();
            arrExcl[19] = st_org;
            //st_brig = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["K_BRIG"]).ToString();
            //arrExcl[20] = st_brig;
            st_num_act = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUM_ACT"]).ToString();
            if (st_num_act != "") { arrExcl[20] = st_num_act; }
            excl_organ = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NAME_ORG"]).ToString();
            //excl_brig = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NAME_BRIG"]).ToString();
            //if (excl_brig == "-") { excl_brig = ""; }
            HiddenField_ORG.Value = excl_organ /*+ " " + excl_brig*/;
            if (RadioButton1.Checked == true)
            {
                SqlDataSource2.SelectCommand = "SELECT K_GROUP,NAME_GROUP,EDIZM,COUNT(K_GROUP) as QUANTITY,TARIFF,(TARIFF*COUNT(K_GROUP)) AS PRICE FROM IZP_BOOK,IZP_SPR_PERIOD WHERE IZP_BOOK.K_GROUP=IZP_SPR_PERIOD.NUM_PROTCL AND K_ORGAN=" + st_org + " AND NUM_ACT IS NULL GROUP BY K_GROUP,NAME_GROUP,EDIZM,TARIFF";
                CheckBox_Act.Visible = true;
                Label_Act.Visible = true;
                HiddenField_NUM_ACT.Value = FuncTakeNumAct();
                Button1.Text = "задать акту номер:" + HiddenField_NUM_ACT.Value;
            }
            else
            {
                SqlDataSource2.SelectCommand = "SELECT K_GROUP,NAME_GROUP,EDIZM,COUNT(K_GROUP) as QUANTITY,TARIFF,(TARIFF*COUNT(K_GROUP)) AS PRICE FROM IZP_BOOK,IZP_SPR_PERIOD WHERE IZP_BOOK.K_GROUP=IZP_SPR_PERIOD.NUM_PROTCL AND NUM_ACT=" + st_num_act + " AND TO_CHAR(DATE_IN,'YYYY')='" + DateTime.Now.ToString("yyyy") + "' GROUP BY K_GROUP,NAME_GROUP,EDIZM,TARIFF";
                CheckBox_Act.Visible = false;
                Label_Act.Visible = false;
                Button1.Text = "акт";
            }
            RadGrid2.DataBind();
            e.Item.Selected = true;
            Button1.Visible = true;
            Button_DelAct.Visible = true;
        }
    }
    protected void Params_to_Zero()
    {
        Button1.Visible = false;
        Button_DelAct.Visible = false;
        CheckBox_Act.Visible = false;
        Label_Act.Visible = false;
        SqlDataSource2.SelectCommand = "";
        RadGrid2.DataBind();
    }
    protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            FuncSqlRebindGrid1(false);
        }
        else
        {
            FuncSqlRebindGrid1(true);
        }
        InjectScript.Text = "";
        Params_to_Zero();
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {

            arrExcl[Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "K_GROUP"))] = DataBinder.Eval(e.Item.DataItem, "QUANTITY").ToString();
            arrExcl[20+Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "K_GROUP"))] = DataBinder.Eval(e.Item.DataItem, "TARIFF").ToString();
            total_price += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "PRICE"));
            row_count++;
        }
        if (e.Item is GridFooterItem)
        {
            int tariff_protcl=0;
            GridFooterItem footerItem = (GridFooterItem)e.Item;
            Button ButFoot = footerItem.FindControl("ButtonFooter") as Button;
            HyperLink HyperLink_Protcl = footerItem.FindControl("HyperLink_Protcl") as HyperLink;
            HyperLink_Protcl.Text = (row_count+1).ToString()+".";
            string commandString = "SELECT NAME_GROUP,EDIZM,TARIFF FROM IZP_SPR_PERIOD WHERE NUM_PROTCL=17";
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader reader1 = myCommand.ExecuteReader();
            while (reader1.Read())
            {
                Label Label_ProtcltName_Group = footerItem.FindControl("Label_ProtcltName_Group") as Label;
                Label_ProtcltName_Group.Text = Convert.ToString(reader1["NAME_GROUP"]);
                Label Label_ProtclEdizm = footerItem.FindControl("Label_ProtclEdizm") as Label;
                Label_ProtclEdizm.Text = Convert.ToString(reader1["EDIZM"]);
                Label Label_ProtclTariff = footerItem.FindControl("Label_ProtclTariff") as Label;
                Label_ProtclTariff.Text = Convert.ToString(reader1["TARIFF"]);
                tariff_protcl = Convert.ToInt32(reader1["TARIFF"]);
                arrExcl[20 + 17] = tariff_protcl.ToString();
            }
            reader1.Close();
            if (RadioButton1.Checked == true)
            {
                commandString = "SELECT COUNT(DISTINCT NUM_P) FROM IZP_BOOK WHERE K_ORGAN=" + st_org + " AND NUM_ACT IS NULL";
            }
            else
            {
                commandString = "SELECT COUNT(DISTINCT NUM_P) FROM IZP_BOOK WHERE NUM_ACT=" + st_num_act + " AND TO_CHAR(DATE_IN,'YYYY')='" + DateTime.Now.ToString("yyyy") + "'";
            }
            myCommand = new OleDbCommand(commandString, myConnection);
            object quant_protcl = myCommand.ExecuteScalar();
            myConnection.Close();
            Label Label_ProtclQuantity = footerItem.FindControl("Label_ProtclQuantity") as Label;
            arrExcl[17] = quant_protcl.ToString();
            Label_ProtclQuantity.Text = arrExcl[17];
            Label Label_ProtclPrice = footerItem.FindControl("Label_ProtclPrice") as Label;
            int price_protcl = (Convert.ToInt32(quant_protcl) * tariff_protcl);
            Label_ProtclPrice.Text = price_protcl.ToString();
            total_price += price_protcl;
            //
            Label Label_PriceNoNDS = footerItem.FindControl("Label_PriceNoNDS") as Label;
            Label_PriceNoNDS.Text = total_price.ToString();
            Label Label_NDS = footerItem.FindControl("Label_NDS") as Label;
            Label_NDS.Text = Math.Round(total_price * 0.2).ToString();
            Label Label_PriceYesNDS = footerItem.FindControl("Label_PriceYesNDS") as Label;
            Label_PriceYesNDS.Text = Math.Round(total_price * 1.2).ToString();            
        }
        string str_array = "";
        foreach (string str in arrExcl)
        {
            str_array += str + ";";
        }
        Label_Array.Text = str_array;
    }
    protected void Button_DelAct_Click(object sender, EventArgs e)
    {
        Button_DelAct.Visible = false;
        arrExcl = Label_Array.Text.Split(separators, 40, StringSplitOptions.RemoveEmptyEntries);
        string commandString, commandStringFAC, commandStringPROT, commandStringDEL;
        OleDbCommand myCommand, myCommandFAC, myCommandPROT, myCommandDEL;
        OleDbDataReader MyReader, MyReaderProt;
        myConnection.Open();
        if (RadioButton1.Checked == true)
        {
            string str = "NUM_ACT IS NULL AND K_ORGAN=" + arrExcl[19];
            commandStringFAC = "SELECT FAC_NUM,K_ORGAN,K_BRIG,K_GROUP FROM IZP_BOOK WHERE " + str + " AND (K_GROUP=1 OR K_GROUP=2 OR K_GROUP=3 OR K_GROUP=4)";
            commandStringPROT = "SELECT DISTINCT YEAR FROM IZP_BOOK WHERE " + str;
            commandStringDEL = "DELETE FROM IZP_BOOK WHERE " + str;
        }
        else
        {
            string str2 = "NUM_ACT=" + arrExcl[20] + "  AND TO_CHAR(DATE_IN,'YYYY')='" + DateTime.Now.ToString("yyyy") + "'";
            commandStringFAC = "SELECT FAC_NUM,K_ORGAN,K_BRIG,K_GROUP FROM IZP_BOOK WHERE " + str2 + " AND (K_GROUP=1 OR K_GROUP=2 OR K_GROUP=3 OR K_GROUP=4)";
            commandStringPROT = "SELECT DISTINCT YEAR FROM IZP_BOOK WHERE " + str2;
            commandStringDEL = "DELETE FROM IZP_BOOK WHERE " + str2;
            commandString = "INSERT INTO IZP_AVAILABLE(NUMB,TYPE_NUMB,YEAR_NUMB) VALUES(" + arrExcl[20] + ",1," + DateTime.Now.ToString("yyyy").Substring(2) + ")";
            myCommand = new OleDbCommand(commandString, myConnection);
            object ins_available1 = myCommand.ExecuteNonQuery();
        }
        myCommandFAC = new OleDbCommand(commandStringFAC, myConnection);
        MyReader = myCommandFAC.ExecuteReader();
        while (MyReader.Read())
        {
            string st_facn = "FAC" + Convert.ToString(MyReader["K_GROUP"]);
            commandString = "UPDATE IZP_FAC9999 SET " + st_facn + "=(" + st_facn + "-1) WHERE FAC_PROT=" + Convert.ToString(MyReader["FAC_NUM"]);
            myCommand = new OleDbCommand(commandString, myConnection);
            object upd_act9999 = myCommand.ExecuteNonQuery();
        } 
        myCommandPROT = new OleDbCommand(commandStringPROT, myConnection);
        MyReaderProt = myCommandPROT.ExecuteReader();
        while (MyReaderProt.Read())
        {
            string year_numb = Convert.ToString(MyReaderProt["YEAR"]).Substring(0, 2);
            string numb = Convert.ToString(MyReaderProt["YEAR"]).Substring(2);
            commandString = "INSERT INTO IZP_AVAILABLE(NUMB,TYPE_NUMB,YEAR_NUMB) VALUES(" + numb + ",2," + year_numb + ")";
            myCommand = new OleDbCommand(commandString, myConnection);
            object upd_available2 = myCommand.ExecuteNonQuery();
        }
        myCommandDEL = new OleDbCommand(commandStringDEL, myConnection);
        object delete_act = myCommandDEL.ExecuteNonQuery();
        myConnection.Close();
        if (RadioButton1.Checked == true) { FuncSqlRebindGrid1(false); } else { FuncSqlRebindGrid1(true); }
        InjectScript.Text = "";
        Params_to_Zero();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CheckBox_Act.Checked = false;
        string path = Server.MapPath("~/Others/edit/acts/pattern_act.xls");
        string st_num = HiddenField_NUM_ACT.Value;
        IWorkbook book = NativeExcel.Factory.OpenWorkbook(path);

        if (book != null)
        {
            arrExcl = Label_Array.Text.Split(separators, 40, StringSplitOptions.RemoveEmptyEntries);
            //sheet one  
            IWorksheet sheet = book.Worksheets[1];
            //sheet.PageSetup
            //Change a value for cell A1
            sheet.Cells["G3"].Value = HiddenField_ORG.Value;

            for (int j = 1; j < 18; j++)
            {
                sheet.Cells["H"+(17+j).ToString()].Value = Convert.ToInt16(arrExcl[j]);
                int i_tariff = Convert.ToInt32(arrExcl[20+j]);
                sheet.Cells["I" + (17 + j).ToString()].Value = i_tariff;
            }
            DateTime data = DateTime.Now;
            sheet.Cells["F9"].Value = "\""+data.ToString("dd")+"\"";
            string smm="";
            switch (data.ToString("MM"))
            {
                case "01":
                    smm = "января";
                    break;
                case "02":
                    smm = "февраля";
                    break;
                case "03":
                    smm = "марта";
                    break;
                case "04":
                    smm = "апреля";
                    break;
                case "05":
                    smm = "мая";
                    break;
                case "06":
                    smm = "июня";
                    break;
                case "07":
                    smm = "июля";
                    break;
                case "08":
                    smm = "августа";
                    break;
                case "09":
                    smm = "сентября";
                    break;
                case "10":
                    smm = "октября";
                    break;
                case "11":
                    smm = "ноября";
                    break;
                case "12":
                    smm = "декабря";
                    break;
            }
            sheet.Cells["G9"].Value = smm;
            sheet.Cells["I9"].Value = data.ToString("yyyy");
            // Номер акта
            if (RadioButton1.Checked == true)// проверка. новый акт или уже выданный
            {

                
                string commandString, commandStringDel;
                OleDbCommand myCommand, myCommandDel;
                commandString = "UPDATE IZP_BOOK SET NUM_ACT=" + st_num + " WHERE NUM_ACT IS NULL AND K_ORGAN=" + arrExcl[19];
                myCommand = new OleDbCommand(commandString, myConnection);
                commandStringDel = "DELETE FROM IZP_AVAILABLE WHERE NUMB=" + st_num + " AND TYPE_NUMB=1 AND YEAR_NUMB=" + data.ToString("yyyy").Substring(2);
                myCommandDel = new OleDbCommand(commandStringDel, myConnection);
                myConnection.Open();
                Object execute_upd = myCommand.ExecuteNonQuery();
                Object execute_del = myCommandDel.ExecuteNonQuery();
                myConnection.Close();
                sheet.Cells["E8"].Value = st_num;

                FuncSqlRebindGrid1(false);
                Params_to_Zero();
            }
            else
            {
                st_num = arrExcl[20];
                sheet.Cells["E8"].Value = st_num;
            }
            //Save workbook
            book.SaveAs(path);
            string path_pdf = Server.MapPath("~/pdf/acts/" + st_num + ".pdf");
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile ExcToPdf = ExcelFile.Load(path);
            ExcelWorksheet ws = ExcToPdf.Worksheets[0];
            //ws.PrintOptions.BottomMargin = 0;
            ws.PrintOptions.LeftMargin = 0.1;
            ws.PrintOptions.RightMargin = 0.1;
            ws.PrintOptions.TopMargin = 0.1;
            ws.PrintOptions.BottomMargin = 0.1;
            ws.PrintOptions.Portrait = false;
            ws.PrintOptions.HorizontalCentered = true;
            ws.PrintOptions.VerticalCentered = true;
            ExcToPdf.Save(path_pdf);
        }
        string s_openURL = "../pdf/acts/" + st_num + ".pdf";
        InjectScript.Text = "<script type=\"text/javascript\">OpenPrint('" + s_openURL + "')</script>";
    }
    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
        base.RaisePostBackEvent(sourceControl, eventArgument);
        if (sourceControl is RadGrid)
        {
            if (eventArgument == "Rebind1")
            {
                FuncSqlRebindGrid1(false);
            }
            if (eventArgument == "Rebind2")
            {
                SqlDataSource2.SelectCommand = "";
                RadGrid2.DataBind();
            }
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            FuncSqlRebindGrid1(false);
            Params_to_Zero();
            Button_Add.Enabled = true;
        }
        else
        {
            FuncSqlRebindGrid1(true);
            Params_to_Zero();
            Button_Add.Enabled = false;
        }
    }
    protected void FuncSqlRebindGrid1(bool act)
    {
        if (act)
        {
            SqlDataSource1.SelectCommand = "SELECT NAME_ORG, K_ORGAN, NUM_ACT, DATE_IN, COUNT(K_BRIG) AS QUANTITY FROM (SELECT DISTINCT NAME_ORG,K_ORGAN,K_BRIG,NUM_ACT,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN FROM IZP_BOOK,IZP_SPR_ORG WHERE K_ORGAN=IZP_SPR_ORG.CODE AND NUM_ACT IS NOT NULL AND TO_CHAR(DATE_IN,'YYYY')='" + DateTime.Now.ToString("yyyy") + "') GROUP BY NAME_ORG, K_ORGAN, NUM_ACT, DATE_IN ORDER BY NUM_ACT DESC";
        }
        else
        {
            SqlDataSource1.SelectCommand = "SELECT NAME_ORG, K_ORGAN, NUM_ACT, DATE_IN, COUNT(K_BRIG) AS QUANTITY FROM (SELECT DISTINCT NAME_ORG,K_ORGAN,K_BRIG,NUM_ACT,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN FROM IZP_BOOK,IZP_SPR_ORG WHERE K_ORGAN=IZP_SPR_ORG.CODE AND NUM_ACT IS NULL) GROUP BY NAME_ORG, K_ORGAN, NUM_ACT, DATE_IN ORDER BY DATE_IN DESC";            
        }
        RadGrid1.DataBind();
    }
    protected void CheckBox_Act_CheckedChanged(object sender, EventArgs e)
    {
        HiddenField_NUM_ACT.Value = FuncTakeNumAct();
        Button1.Text = "задать акту номер:" + HiddenField_NUM_ACT.Value;
    }
}