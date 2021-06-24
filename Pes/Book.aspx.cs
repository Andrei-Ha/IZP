using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using NativeExcel;
using System.Data.OleDb;
using GemBox.Spreadsheet;
using System.IO;
//using LinqBridge;

public partial class Book : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    //static string connectionString = "Provider=System.Data.OracleClient;Data Source=pirr1;Persist Security Info=True;Password=pes;User ID=pinsk";
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string s_n_organ;
    string s_n_brig;
    string s_n_p;
    string s_year, s_period;
    string s_k_group;
    string s_printprotcl = "visible";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Label1.Text = connectionString;
        if (Page.IsPostBack)
        {
            //LabelOrg.Text = "rebind номер протокола: " + HiddenFieldNP.Value;
        }
        else 
        {
            SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,QUANTITY,TO_CHAR(DATE_PROT,'dd.MM.yyyy') AS DATE_PROT FROM (SELECT  NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,count(PES_IZP_BOOK.ID_) AS QUANTITY,MAX(NVL(DATE_PROT,'11.11.2100')) AS DATE_PROT FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE GROUP BY NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD) WHERE DATE_PROT='11.11.2100' ORDER BY TO_NUMBER(YEAR) DESC";
        }
        InjectScript.Text = "";
        //RadGrid1.Rebind();
        Label_Warning.Visible = false;
    }
    protected void RadGrid1_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label Label_Num_p = e.Item.FindControl("Label_Num_p") as Label;
            Label_Num_p.Text = DataBinder.Eval(e.Item.DataItem, "NUM_P").ToString()+".";
            Label Label_Org = e.Item.FindControl("Label_Org") as Label;
            Label Label_Group = e.Item.FindControl("Label_Group") as Label;
            string s_organ = DataBinder.Eval(e.Item.DataItem, "NAME_ORG").ToString();
            string s_brig = DataBinder.Eval(e.Item.DataItem, "NAME_BRIG").ToString();
            string s_group = DataBinder.Eval(e.Item.DataItem, "NAME_GROUP").ToString();
            string s_year = DataBinder.Eval(e.Item.DataItem, "YEAR").ToString();
            if (s_brig == "-") { s_brig = ""; } else { s_brig = " (" + s_brig + ") "; }
            Label_Org.Text = s_organ + s_brig;
            Label_Group.Text = s_group;
            Label Label_Quantity = e.Item.FindControl("Label_Quantity") as Label;
            Label_Quantity.Text = DataBinder.Eval(e.Item.DataItem, "QUANTITY").ToString();
            Label Label_Date_prot = e.Item.FindControl("Label_Date_prot") as Label;
            string st_date_prot = DataBinder.Eval(e.Item.DataItem, "DATE_PROT").ToString();
            if (st_date_prot != "11.11.2100")
            {
                Label_Date_prot.Text = "("+st_date_prot+")";
            }
            
            /*string path_pdf = Server.MapPath("~/pdf/pes/" + s_year + ".pdf");
            if (File.Exists(path_pdf))
            {
                string s_openURL = "../pdf/pes/" + s_year + ".pdf";
                HyperLink HyperLinkProtcl = e.Item.FindControl("HyperLinkProtcl") as HyperLink;
                HyperLinkProtcl.Text = "протокол";
                HyperLinkProtcl.Attributes["href"] = "#";
                HyperLinkProtcl.Attributes["onclick"] = "return OpenPrint('" + s_openURL + "')";
            }*/
        }
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int ix = 0;
            string formin="";
            int i_group = Convert.ToInt16(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["K_GROUP"]);
            s_n_p = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUM_P"]).ToString();
            s_year = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["YEAR"]).ToString();
            s_period = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PERIOD"]).ToString();
            s_k_group = i_group.ToString();
            if (Array.IndexOf(new int[] { 1, 2, 3 },i_group)>-1) { formin = "edit/valin/valin1.aspx?id_="; }
            else
            {
                if (Array.IndexOf(new int[] { 4, 7, 8, 9, 10, 11, 12, 14, 15 },i_group)>=0) { formin = "edit/valin/valin2.aspx?id_="; }
                else
                {
                    if (i_group == 5) { formin = "edit/valin/valin3.aspx?id_="; }
                    else
                    {
                        if (i_group == 6) { formin = "edit/valin/valin4.aspx?id_=";}
                        else
                        {
                            if (i_group == 13) { formin = "edit/valin/valin5.aspx?id_=";}
                            else
                            {
                                if (i_group == 16) { formin = "edit/valin/valin6.aspx?id_="; }
                            }
                        }
                    }
                }
            }
            HyperLink ValLink = e.Item.FindControl("ValLink") as HyperLink;
            //picsLink.Text = "редактировать";
            if (RadioButton1.Checked)
            {
                ValLink.Attributes["href"] = "#";
                ValLink.Attributes["onclick"] = string.Format("return ShowInsertValForm('{0}','{1}','{2}','{3}');", formin, e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_"], i_group, e.Item.ItemIndex);
            }
            Label Values = e.Item.FindControl("Values") as Label;
            string v_prot = (DataBinder.Eval(e.Item.DataItem, "V_PROT").ToString());
            string v_isol = (DataBinder.Eval(e.Item.DataItem, "V_ISOL").ToString());
            string v_work = (DataBinder.Eval(e.Item.DataItem, "V_WORK").ToString());
            string v_wire = (DataBinder.Eval(e.Item.DataItem, "V_WIRE").ToString());
            string v_indic = (DataBinder.Eval(e.Item.DataItem, "V_INDIC").ToString());
            string v_indic2 = (DataBinder.Eval(e.Item.DataItem, "V_INDIC2").ToString());
            string c_left = (DataBinder.Eval(e.Item.DataItem, "C_LEFT").ToString());
            string c_right = (DataBinder.Eval(e.Item.DataItem, "C_RIGHT").ToString());
            string c_pointer = (DataBinder.Eval(e.Item.DataItem, "C_POINTER").ToString());
            Values.Text = "";
            Values.Text = Values.Text + ((v_prot == "") ? "" : "U_(ср.защ.)=" + v_prot + "кВ,<br/>");
            Values.Text = Values.Text + ((v_isol == "") ? "" : "U_(изол.ч.)=" + v_isol + "кВ,<br/>");
            Values.Text = Values.Text + ((v_work == "") ? "" : "U_(раб.ч.)=" + v_work + "кВ,<br/>");
            Values.Text = Values.Text + ((v_wire == "") ? "" : "U_(провод)=" + v_wire + "кВ,<br/>");
            Values.Text = Values.Text + ((v_indic == "") ? "" : "U_(индикац.)=" + v_indic + "кВ,<br/>");
            Values.Text = Values.Text + ((v_indic2 == "") ? "" : "U_(индикац.2)=" + v_indic2 + "кВ,<br/>");
            if (i_group == 16)
            {
                Values.Text = Values.Text + ((c_right == "") ? "" : "I_(издел.)=" + c_right + "мА,<br/>");
            }
            else
            {
                Values.Text = Values.Text + ((c_right == "") ? "" : "I_(прав.изд.)=" + c_right + "мА,<br/>");
            }
            Values.Text = Values.Text + ((c_left == "") ? "" : "I_(лев.изд.)=" + c_left + "мА,<br/>");
            Values.Text = Values.Text + ((c_pointer == "") ? "" : "I_(указат.)=" + c_pointer + "mA,<br/>");
            if (Values.Text.Length > 6) { Values.Text = Values.Text.Substring(0, Values.Text.Length - 6); }
            Label Params = e.Item.FindControl("Params") as Label;
            if ((DataBinder.Eval(e.Item.DataItem, "DATE_EXP").ToString()) != "")
            {
                string t_air = (DataBinder.Eval(e.Item.DataItem, "T_AIR").ToString());
                string t_water = (DataBinder.Eval(e.Item.DataItem, "T_WATER").ToString());
                string humidity = (DataBinder.Eval(e.Item.DataItem, "HUMIDITY").ToString());
                //string pressure = (DataBinder.Eval(e.Item.DataItem, "PRESSURE").ToString());
                string fio_exec = (DataBinder.Eval(e.Item.DataItem, "FIO_EXEC").ToString());
                //Params.Text = "Т воды:" + t_water + "°С<br/>Т воздуха:" + t_air + "°С<br/>Влажность:" + humidity + "%<br>Давление:" + pressure + "кПа";
                Params.Text = "Т_вод.=" + t_water + " °С, Т_возд.=" + t_air + " °С, φ=" + humidity + " %"/*, P=" + pressure + " кПа"*/;
            }
            else { Params.Text = " "; }
            Label Resul = e.Item.FindControl("Resul") as Label;
            string result_exp = DataBinder.Eval(e.Item.DataItem, "RESULT_EXP").ToString();
            if (result_exp != "")
            {
                if (result_exp == "1")
                {
                    Resul.Text = "соответствует";
                    //Button_Spisan.Visible = false;
                }
                else
                {
                    ix++;
                    Resul.Text = "<span class=\"styleR\">не соответствует</span>";
                    if (ix == 1 && RadioButton2.Checked == true)
                    {
                        if (File.Exists(Server.MapPath("~/pdf/pes/spisan/s" + s_year + ".pdf")))
                        {
                            Button_Spisan.Visible = true;
                        }
                    }
                }
            }
            else
            {
                Resul.Text = " ";
            }
            Label_Protcl.Text = s_k_group + ";" + s_n_p + ";" + s_year + ";" + s_period;
        }
        /*if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = (GridFooterItem)e.Item;
            Button ButFoot = footerItem.FindControl("ButtonFooter") as Button;
            ButFoot.Text = "ПечПрот";
            string s_openURL = "edit/protocols/protcl1.xls";
            string s_openURL ="edit/protocols/protocol1.aspx?k_group="+s_k_group+"&num_p="+s_n_p+"&year="+s_year;
            ButFoot.OnClientClick = "return OpenPrint('" + s_openURL + "')";
        }*/
    }
    protected void RadGrid1_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            s_n_organ = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NAME_ORG"].ToString();
            s_n_brig = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NAME_BRIG"].ToString();
            s_n_p = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUM_P"].ToString();
            HiddenFieldNUM_P.Value = s_n_p;
            s_period = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PERIOD"].ToString();
            HiddenFieldNP.Value = e.Item.DataSetIndex.ToString();
            s_year = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["YEAR"].ToString();
            HiddenFieldYEAR.Value = s_year;
            RadGrid1.Controls.Add(new LiteralControl(" выбран протокол №: " + s_n_p));
            LabelOrg.Text = "организация: "+ s_n_organ + " (" + s_n_brig + ") Номер протокола: " + s_n_p;
            Button_DelProtcl.Text = "удалить протокол №" + s_n_p;
            if (RadioButton1.Checked == true)
            { Button_Protcl.Text = "выдать протокол №" + s_n_p; } else { Button_Protcl.Text = "открыть протокол №" + s_n_p; }
            Button_Protcl.Visible = true;
            Button_DelProtcl.Visible = true;
            Button_Spisan.Visible = false;
            SqlDataSource2.SelectCommand = "SELECT PES_IZP_BOOK.ID_ as ID_,NUM,NUM_P,YEAR,PERIOD,NAME_ORG,K_BRIG,NAME_BRIG,PES_IZP_BOOK.K_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,V_INDIC2,C_LEFT,C_RIGHT,C_POINTER,T_WATER,T_AIR,HUMIDITY,"/*PRESSURE,*/+ "RESULT_EXP,FIO_EXEC,EXEC_SIGN,DATE_PROT,NUM_ACT,NOTE FROM IZP_SPR_PERIOD,PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PROTECT WHERE IZP_SPR_PERIOD.NUM_PROTCL=PES_IZP_BOOK.K_GROUP AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE AND K_NAME=IZP_SPR_PROTECT.ID_ AND YEAR=" + s_year + " ORDER BY NUM_P,FAC_NUM";
            RadGrid2.DataBind();
        }
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
        Button_Protcl.Visible = false;
        Params_to_Zero();
    }
    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
        base.RaisePostBackEvent(sourceControl, eventArgument);

        if (sourceControl is RadGrid)
        {
            if (eventArgument == "RebindAndNavigate2")
            {
                int irow = Convert.ToInt16(HiddenFieldNP.Value) % 10;
                //LabelOrg.Text = "Row(Ajax)" + irow.ToString();
                SqlDataSource2.SelectCommand = "SELECT PES_IZP_BOOK.ID_ as ID_,NUM,NUM_P,YEAR,PERIOD,NAME_ORG,K_BRIG,NAME_BRIG,PES_IZP_BOOK.K_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,V_INDIC2,C_LEFT,C_RIGHT,C_POINTER,T_WATER,T_AIR,HUMIDITY,"/*PRESSURE,*/+ "RESULT_EXP,FIO_EXEC,EXEC_SIGN,DATE_PROT,NUM_ACT,NOTE FROM IZP_SPR_PERIOD,PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PROTECT WHERE IZP_SPR_PERIOD.NUM_PROTCL=PES_IZP_BOOK.K_GROUP AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE AND K_NAME=IZP_SPR_PROTECT.ID_ AND YEAR=" + HiddenFieldYEAR.Value + " ORDER BY NUM_P,FAC_NUM";
                RadGrid2.DataBind();
                RadGrid1.Items[irow].Selected = true;
            }
            else
            {
                if (eventArgument == "Rebind1")
                {
                    FuncSqlRebindGrid1(false);
                }
                if (eventArgument == "Rebind2")
                {
                    SqlDataSource2.SelectCommand = "";
                    RadGrid2.Rebind();
                }
            }
        }
    }
    protected void Button_Protcl_Click(object sender, EventArgs e)
    {
        string s_spisan = "0";
        InjectScript.Text = "";
        string[] arrStr = new string[4];
        string[] separators = { ";", ":" };
        arrStr = Label_Protcl.Text.Split(separators, 4, StringSplitOptions.None);
        if (RadioButton1.Checked == true)
        {
            string commandStringCheck = "SELECT COUNT(ID_) FROM PES_IZP_BOOK WHERE YEAR='" + arrStr[2] + "' AND RESULT_EXP IS NULL";
            OleDbCommand myCommandCheck = new OleDbCommand(commandStringCheck, myConnection);
            myConnection.Open();
            object o_check = myCommandCheck.ExecuteScalar();
            myConnection.Close();
            if (Convert.ToString(o_check) != "0")
            {
                Label_Warning.Text = "Не все результаты измерений внесены в протокол №" + arrStr[1];
                Label_Warning.Visible = true;
            }
            else
            {
                DateTime data = DateTime.Today;
                DateTime data_next = data;
                bool b_date_next = false;
                string path = Server.MapPath("~/Edit/protocols/pattern" + arrStr[0] + ".xls");
                string path_pdf = Server.MapPath("~/pdf/pes/" + arrStr[2] + ".pdf");
                int i_group = Convert.ToInt16(arrStr[0]);
                IWorkbook book = NativeExcel.Factory.OpenWorkbook(path);
                //Label1.Text = Label_Protcl.Text;
                if (book != null)
                {
                    IWorksheet sheet = book.Worksheets[1];
                    sheet.Cells["A12"].Value = "ПРОТОКОЛ № 3/" + arrStr[1];
                    string commandStringParam = "SELECT FIO_EXEC,FIO_CHIEF FROM IZP_PARAM";
                    OleDbCommand myCommandParam = new OleDbCommand(commandStringParam, myConnection);
                    string commandStringEquip = "SELECT NAME,MODEL,FAB_NUM,TO_CHAR(DATE_POV,'dd.MM.yyyy') as DATE_POV FROM IZP_SPR_EQUIP";
                    OleDbCommand myCommandEquip = new OleDbCommand(commandStringEquip, myConnection);
                    string commandString = "SELECT PES_IZP_BOOK.ID_ as ID_,NUM,NUM_P,NAME_ORG,K_BRIG,NAME_BRIG,PES_IZP_BOOK.K_GROUP AS K_GROUP,PES_IZP_BOOK.K_NAME AS K_NAME,IZP_SPR_PROTECT.NAME AS NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,V_INDIC2,C_LEFT,C_RIGHT,C_POINTER,T_WATER,T_AIR,HUMIDITY,"/*PRESSURE,*/+ "RESULT_EXP,FIO_EXEC,EXEC_SIGN,DATE_PROT,NUM_ACT,NOTE FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PROTECT WHERE K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE AND K_NAME=IZP_SPR_PROTECT.ID_ AND YEAR='" + arrStr[2] + "' ORDER BY RESULT_EXP,FAC_NUM";
                    OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
                    string commandStringSpis = "";
                    OleDbCommand myCommandSpis;
                    myConnection.Open();
                    OleDbDataReader ReaderParam = myCommandParam.ExecuteReader();
                    while (ReaderParam.Read())
                    {
                        //sheet.Cells["G49"].Value = "("+Convert.ToString(ReaderParam["FIO_EXEC"])+")";
                        sheet.Cells["J51"].Value = Convert.ToString(ReaderParam["FIO_CHIEF"]);
                    }
                    ReaderParam.Close();
                    OleDbDataReader ReaderEquip = myCommandEquip.ExecuteReader();
                    int i = 0;
                    while (ReaderEquip.Read() && i < 8)
                    {
                        switch (i)
                        {
                            case 0:
                                if (Array.IndexOf(new int[] { 1, 3, 4, 5, 7, 9, 11, 16 },i_group)>=0)
                                {
                                    sheet.Cells["D20"].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                    sheet.Cells["H20"].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                    sheet.Cells["K20"].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                    sheet.Cells["J" + (20 + i).ToString()].Font.Size = 9;
                                    sheet.Cells["J" + (20 + i).ToString()].Value = "Поверен";
                                }
                                break;
                            case 5:
                                if (i_group == 13)
                                {
                                    sheet.Cells["D20"].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                    sheet.Cells["H20"].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                    sheet.Cells["K20"].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                    sheet.Cells["J" + (20 + i).ToString()].Font.Size = 9;
                                    sheet.Cells["J" + (20 + i).ToString()].Value = "Поверен";
                                }
                                break;
                            case 6:
                                if (Array.IndexOf(new int[] { 2, 6, 8, 10, 12, 14, 15 },i_group)>=0)
                                {
                                    sheet.Cells["D20"].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                    sheet.Cells["H20"].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                    sheet.Cells["K20"].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                    sheet.Cells["J" + (20 + i).ToString()].Font.Size = 9;
                                    sheet.Cells["J" + (20 + i).ToString()].Value = "Поверен";
                                }
                                break;
                            case 7:
                                break;
                            default:
                                sheet.Cells["D" + (20 + i).ToString()].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                sheet.Cells["H" + (20 + i).ToString()].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                sheet.Cells["K" + (20 + i).ToString()].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                sheet.Cells["J" + (20 + i).ToString()].Font.Size = 9;
                                sheet.Cells["J" + (20 + i).ToString()].Value = "Поверен";
                                break;
                        }
                        i++;
                    }
                    sheet.Cells["J20"].Value = "Калиброван";
                    ReaderEquip.Close();
                    OleDbDataReader Reader = myCommand.ExecuteReader();
                    i = 0;
                    int ix = 0;
                    string st_num, st_result, st_not_for_use = "-";
                    while (Reader.Read())
                    {
                        st_num = (i + 1).ToString();
                        if (i == 0)
                        {
                            sheet.Cells["J49"].Value = Session["SIZP_User"].ToString() ; // "(" + Convert.ToString(Reader["FIO_EXEC"]) + ")";
                            sheet.Cells["I8"].Value = Convert.ToString(Reader["NAME_ORG"]) + " " + (Convert.ToString(Reader["NAME_BRIG"]) == "-" ? "" : Convert.ToString(Reader["NAME_BRIG"]));
                            sheet.Cells["C16"].Value = Convert.ToString(Reader["DATE_EXP"]);
                            sheet.Cells["K16"].Value = Convert.ToString(Reader["DATE_EXP"]);
                            //sheet.Cells["C54"].Value = Convert.ToString(Reader["NAME_ORG"]);
                            sheet.Cells["A18"].Value = "ТНПА на метод испытаний  МВИ БР. 370-2019";
                            sheet.Cells["I25"].Value = Convert.ToString(Reader["T_WATER"]) + " °С";
                            sheet.Cells["A26"].Value = "Температура окружающей среды " + Convert.ToString(Reader["T_AIR"]) + " °С        ";
                            sheet.Cells["A26"].Value += "Относительная влажность " + Convert.ToString(Reader["HUMIDITY"]) + " %        ";
                            //sheet.Cells["A26"].Value += "Давление " + Convert.ToString(Reader["PRESSURE"]) + " кПа";
                            data_next = Convert.ToDateTime(Reader["DATE_EXP"]).AddMonths(Convert.ToInt16(arrStr[3]));
                            sheet.Cells["E48"].Value = data_next.ToString("dd.MM.yyyy");
                            string smm = "";
                            data = Convert.ToDateTime(Reader["DATE_EXP"]);
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
                            sheet.Cells["A13"].Value = "от " + "\"" + data.ToString("dd") + "\"" + "   " + smm + "   " + data.ToString("yyyy");
                        }
                        if (Convert.ToString(Reader["RESULT_EXP"]) != "1")
                        {
                            commandStringSpis = "UPDATE PES_IZP_BOOK SET DATE_SPISAN='" + data.ToString("dd.MM.yyyy") + "' WHERE K_GROUP=" + Convert.ToString(Reader["K_GROUP"]) + " AND K_NAME=" + Convert.ToString(Reader["K_NAME"]) + " AND FAC_NUM='" + Convert.ToString(Reader["FAC_NUM"]) + "' AND DATE_SPISAN IS NULL";
                            myCommandSpis = new OleDbCommand(commandStringSpis, myConnection);
                            object executeSpisn = myCommandSpis.ExecuteNonQuery();
                            st_result = "не соответствует";
                            ix++;
                            if (ix > 1) { st_not_for_use += "," + st_num; } else { st_not_for_use = st_num; }
                        }
                        else { st_result = "соответствует"; b_date_next = true; }
                        sheet.Cells["A" + (34 + i).ToString()].Value = (i + 1).ToString();
                        if (Array.IndexOf(new int[] { 1, 2, 3 },i_group)>=0)
                        {
                            sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                            string s_right = Convert.ToString(Reader["C_RIGHT"]);
                            if (s_right == "") { s_right = "пробой"; }
                            sheet.Cells["C" + (34 + i).ToString()].Value = s_right;
                            sheet.Cells["F" + (34 + i).ToString()].Value = st_result;
                            string s_left = Convert.ToString(Reader["C_LEFT"]);
                            if (s_left == "") { s_left = "пробой"; }
                            sheet.Cells["I" + (34 + i).ToString()].Value = s_left;
                        }
                        else
                        {
                            if (Array.IndexOf(new int[] { 4, 7, 8, 9, 10, 11, 12, 14, 15 },i_group)>=0)
                            {
                                sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["NAME"]);
                                sheet.Cells["C" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                                sheet.Cells["F" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_PROT"]);
                                if (Array.IndexOf(new int[] { 4, 14, 16 },i_group)>=0)
                                {
                                    sheet.Cells["I" + (34 + i).ToString()].Value = "1";
                                }
                                else
                                {
                                    sheet.Cells["I" + (34 + i).ToString()].Value = "5";
                                }
                            }
                            else
                            {
                                if (i_group == 5)
                                {
                                    sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["NAME"]);
                                    sheet.Cells["C" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                                    if (Convert.ToString(Reader["V_ISOL"]) != "")
                                    {
                                        sheet.Cells["E" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_ISOL"]);
                                        sheet.Cells["I" + (34 + i).ToString()].Value = "0,6";
                                    }
                                    else
                                    {
                                        sheet.Cells["E" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_WIRE"]);
                                        sheet.Cells["I" + (34 + i).ToString()].Value = "10";
                                    }
                                    sheet.Cells["G" + (34 + i).ToString()].Value = "50";
                                    sheet.Cells["H" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_INDIC"]);
                                    sheet.Cells["J" + (34 + i).ToString()].Value = Convert.ToString(Reader["C_POINTER"]);
                                }
                                else
                                {
                                    if (i_group == 6)
                                    {
                                        sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["NAME"]);
                                        sheet.Cells["C" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                                        sheet.Cells["E" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_ISOL"]);
                                        sheet.Cells["G" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_WORK"]);
                                        sheet.Cells["I" + (34 + i).ToString()].Value = "2,5";
                                        sheet.Cells["J" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_INDIC"]);
                                    }
                                    else
                                    {
                                        if (i_group == 16)
                                        {
                                            sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["NAME"]);
                                            sheet.Cells["C" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                                            sheet.Cells["F" + (34 + i).ToString()].Value = Convert.ToString(Reader["C_RIGHT"]);
                                            sheet.Cells["I" + (34 + i).ToString()].Value = "1";
                                        }
                                        else
                                        {
                                            sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["NAME"]);
                                            sheet.Cells["C" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                                            sheet.Cells["E" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_ISOL"]);
                                            sheet.Cells["G" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_WORK"]);
                                            sheet.Cells["H" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_WIRE"]);
                                            sheet.Cells["I" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_INDIC"]);
                                            sheet.Cells["J" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_INDIC2"]);
                                        }
                                    }
                                }
                            }
                        }
                        sheet.Cells["K" + (34 + i).ToString()].Value = st_result;
                        i++;
                    }
                    Reader.Close();
                    myConnection.Close();
                    //Label_Protcl.Text = i.ToString();
                    for (int y = i; y < 10; y++)
                    {
                        sheet.Cells["A" + (34 + y).ToString()].Value = "-----";
                        sheet.Cells["B" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["C" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["D" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["E" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["F" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["G" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["H" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["I" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["J" + (34 + y).ToString()].Value = "-------";
                        sheet.Cells["K" + (34 + y).ToString()].Value = "-------";
                    }
                    if (b_date_next)
                    {
                        sheet.Cells["E48"].Value = data_next.ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        sheet.Cells["E48"].Value = "-";
                    }
                    sheet.Range["A46:L46"].Merge();
                    sheet.Cells["A46"].Value = "Заключение: Средства защиты за исключением позиций №  " + st_not_for_use + "  испытание";
                    //Save workbook
                    book.SaveAs(path);
                    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                    ExcelFile ExcToPdf = ExcelFile.Load(path);
                    ExcelWorksheet ws = ExcToPdf.Worksheets[0];
                    ws.PrintOptions.LeftMargin = 0.1;
                    ws.PrintOptions.RightMargin = 0.1;
                    ws.PrintOptions.TopMargin = 0.1;
                    ws.PrintOptions.BottomMargin = 0.1;
                    ws.PrintOptions.HorizontalCentered = true;
                    ws.PrintOptions.VerticalCentered = true;
                    ExcToPdf.Save(path_pdf);
                    if (ix > 0)
                    {
                        for (int y = ix; y < 10; y++)
                        {
                            sheet.Cells["A" + (34 + y).ToString()].Value = "-----";
                            sheet.Cells["B" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["C" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["D" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["E" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["F" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["G" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["H" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["I" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["J" + (34 + y).ToString()].Value = "-------";
                            sheet.Cells["K" + (34 + y).ToString()].Value = "-------";
                        }
                        sheet.Cells["E48"].Value = "-";
                        string path_spisan = Server.MapPath("~/Edit/spisan_protcls/pattern" + arrStr[0] + ".xls");
                        book.SaveAs(path_spisan);
                        ExcelFile ExcToPdf2 = ExcelFile.Load(path_spisan);
                        ExcelWorksheet ws2 = ExcToPdf2.Worksheets[0];
                        ws2.PrintOptions.LeftMargin = 0.1;
                        ws2.PrintOptions.RightMargin = 0.1;
                        ws2.PrintOptions.TopMargin = 0.1;
                        ws2.PrintOptions.BottomMargin = 0.1;
                        ws2.PrintOptions.HorizontalCentered = true;
                        ws2.PrintOptions.VerticalCentered = true;
                        s_spisan = "../pdf/pes/spisan/s" + arrStr[2] + ".pdf";
                        ExcToPdf2.Save(Server.MapPath("~/pdf/pes/spisan/s" + arrStr[2] + ".pdf"));
                    }
                }
                string commandStringUPD = "UPDATE PES_IZP_BOOK SET DATE_PROT='" + data.ToString("dd.MM.yyyy") + "' WHERE YEAR=" + arrStr[2];
                OleDbCommand myCommandUPD = new OleDbCommand(commandStringUPD, myConnection);
                //Label_Protcl.Text = commandStringUPD;
                myConnection.Open();
                object update = myCommandUPD.ExecuteNonQuery();
                myConnection.Close();
                Button_Protcl.Visible = false;
                string s_openURL = "../pdf/pes/" + arrStr[2] + ".pdf";
                InjectScript.Text = "<script type=\"text/javascript\">OpenPrint('" + s_openURL + "','" + s_spisan +"')</script>";
            }
            FuncSqlRebindGrid1(false);
            Params_to_Zero();
        }
        else
        {
            string s_openURL = "../pdf/pes/" + arrStr[2] + ".pdf";
            InjectScript.Text = "<script type=\"text/javascript\">OpenPrint('" + s_openURL + "','" + s_spisan + "')</script>";
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        Button_Protcl.Visible = false;
        if (RadioButton1.Checked == true)
        {
            FuncSqlRebindGrid1(false);
            Params_to_Zero();
            Button_Add.Enabled = true;
            Button_Protcl.Text = "выдать протокол №";
            Button_Spisan.Visible = false;
        }
        else
        {
            FuncSqlRebindGrid1(true);
            Params_to_Zero();
            Button_Add.Enabled = false;
            Button_Protcl.Text = "открыть протокол №";
        }
    }
    protected void FuncSqlRebindGrid1(bool act)
    {
        if (!act)
        {
            SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,QUANTITY,TO_CHAR(DATE_PROT,'dd.MM.yyyy') AS DATE_PROT FROM (SELECT  NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,count(PES_IZP_BOOK.ID_) AS QUANTITY,MAX(NVL(DATE_PROT,'11.11.2100')) AS DATE_PROT FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE GROUP BY NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD) WHERE DATE_PROT='11.11.2100' ORDER BY TO_NUMBER(YEAR) DESC";
        }
        else
        {
            SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,QUANTITY,TO_CHAR(DATE_PROT,'dd.MM.yyyy') AS DATE_PROT FROM (SELECT  NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,count(PES_IZP_BOOK.ID_) AS QUANTITY,MAX(NVL(DATE_PROT,'11.11.2100')) AS DATE_PROT FROM PES_IZP_BOOK,PES_IZP_SPR_ORG,PES_IZP_SPR_BRIG,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=PES_IZP_SPR_ORG.CODE AND K_BRIG=PES_IZP_SPR_BRIG.CODE GROUP BY NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD) WHERE DATE_PROT<>'11.11.2100' ORDER BY TO_NUMBER(YEAR) DESC";
        }
        RadGrid1.DataBind();
    }
    protected void Params_to_Zero()
    {
        Label_DelProtcl.Visible = false;
        Button_Spisan.Visible = false;
        Button_DelProtcl.Visible = false;
        LabelOrg.Text = "";
        SqlDataSource2.SelectCommand = "";
        RadGrid2.DataBind();
    }
    protected void Button_DelProtcl_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {

        }
        else
        {

        }
        string commandStringDel = "DELETE FROM PES_IZP_BOOK WHERE YEAR=" + HiddenFieldYEAR.Value;
        OleDbCommand myCommandDel = new OleDbCommand(commandStringDel, myConnection);
        string commandString = "INSERT INTO PES_IZP_AVAILABLE(NUMB,TYPE_NUMB,YEAR_NUMB) VALUES(" + HiddenFieldNUM_P.Value + ",2," + HiddenFieldYEAR.Value.Substring(0,2) + ")";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        object o_delete = myCommandDel.ExecuteNonQuery();
        object o_insert = myCommand.ExecuteNonQuery();
        myConnection.Close();

        if (RadioButton1.Checked == true) { FuncSqlRebindGrid1(false); } else { FuncSqlRebindGrid1(true); }
        InjectScript.Text = "";
        Params_to_Zero();
    }
    protected void Button_Spisan_Click(object sender, EventArgs e)
    {
        string[] arrStr = new string[4];
        string[] separators = { ";", ":" };
        arrStr = Label_Protcl.Text.Split(separators, 4, StringSplitOptions.None);
        string s_spisan = "../pdf/pes/spisan/s" + arrStr[2] + ".pdf";
        InjectScript.Text = "<script type=\"text/javascript\">OpenPrint('0','" + s_spisan + "')</script>";
    }
}