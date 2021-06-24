using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using NativeExcel;
using System.Data.OleDb;
using GemBox.Spreadsheet;

public partial class Book : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string s_n_organ;
    string s_n_brig;
    string s_n_p;
    string s_year, s_period;
    string s_k_group;
    string s_printprotcl = "visible";
   /*string s_organ="xxx";
   string s_organ_sel;
   string s_brig = "xxx";
   string s_brig_sel;
   string s_num_p = "xxx";
   string s_num_p_sel;
   string s_name_brig;
    int ix = 1;*/
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //LabelOrg.Text = "rebind номер протокола: " + HiddenFieldNP.Value;
        }
        else
        {
            SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,QUANTITY,TO_CHAR(DATE_PROT,'dd.MM.yyyy') AS DATE_PROT FROM (SELECT  NUM_P,NUM_ACT,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,count(IZP_BOOK.ID_) AS QUANTITY,MAX(NVL(DATE_PROT,'11.11.2100')) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE GROUP BY NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD) WHERE DATE_PROT='11.11.2100' AND NUM_ACT IS NOT NULL ORDER BY TO_NUMBER(YEAR) DESC";
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
            Label_Num_p.Text = DataBinder.Eval(e.Item.DataItem, "NUM_P").ToString() + ".";
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
                Label_Date_prot.Text = "(" + st_date_prot + ")";
            }
        }
    }
    protected void RadGrid2_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {   string formin="";
            int i_group = Convert.ToInt16(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["K_GROUP"]);
            s_n_p = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUM_P"]).ToString();
            s_year = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["YEAR"]).ToString();
            s_period = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PERIOD"]).ToString();
            s_k_group = i_group.ToString();
            if ((new[] { 1, 2, 3 }).Contains(i_group)) { formin = "edit/valin/valin1.aspx?id_="; }
            else
            {
                if ((new[] { 4, 7, 8, 9, 10, 11, 12, 14, 15 }).Contains(i_group)) { formin = "edit/valin/valin2.aspx?id_="; }
                else
                {
                    if (i_group == 5) { formin = "edit/valin/valin3.aspx?id_="; }
                    else
                    {
                        if (i_group == 6) { formin = "edit/valin/valin4.aspx?id_=";}
                        else
                        {
                            if (i_group == 13) { formin = "edit/valin/valin5.aspx?id_="; }
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
            if (RadioButton1.Checked || Session["SIZP_User"].ToString() == "Горегляд А.С.")
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
            Values.Text = Values.Text + ((v_prot == "") ? "" : "U_(ср.защ.)=" + v_prot + " кВ,<br/>");
            Values.Text = Values.Text + ((v_isol == "") ? "" : "U_(изол.ч.)=" + v_isol + " кВ,<br/>");
            Values.Text = Values.Text + ((v_work == "") ? "" : "U_(раб.ч.)=" + v_work + " кВ,<br/>");
            Values.Text = Values.Text + ((v_wire == "") ? "" : "U_(провод)=" + v_wire + " кВ,<br/>");
            Values.Text = Values.Text + ((v_indic == "") ? "" : "U_(индикац.)=" + v_indic + " В,<br/>");
            Values.Text = Values.Text + ((v_indic2 == "") ? "" : "U_(индикац.2)=" + v_indic2 + " В,<br/>");
            if (i_group == 16)
            {
                Values.Text = Values.Text + ((c_right == "") ? "" : "I_(издел.)=" + c_right + " мА,<br/>");
            }
            else
            {
                Values.Text = Values.Text + ((c_right == "") ? "" : "I_(прав.изд.)=" + c_right + " мА,<br/>");
            }
            Values.Text = Values.Text + ((c_left == "") ? "" : "I_(лев.изд.)=" + c_left + " мА,<br/>");
            Values.Text = Values.Text + ((c_pointer == "") ? "" : "I_(указат.)=" + c_pointer + " mA,<br/>");
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
                Params.Text = "Т_вод.=" + t_water + " °С, Т_возд.=" + t_air + " °С, φ=" + humidity + " %" /*+", P=" + pressure + " кПа"*/;
            }
            else { Params.Text = " "; }
            Label Resul = e.Item.FindControl("Resul") as Label;
            string result_exp = DataBinder.Eval(e.Item.DataItem, "RESULT_EXP").ToString();
            if (result_exp != "")
            {
                Resul.Text = (result_exp == "1") ? "годно" : "<span class=\"styleR\">негодно</span>";
                //s_printprotcl = "visible";
                //Button_Protcl.Visible = true;
            }
            else
            {
                Resul.Text = " ";
                //s_printprotcl = "hidden";
                //Button_Protcl.Visible = false;
            }
            Label_Protcl.Text = s_k_group + ";" + s_n_p + ";" + s_year + ";" + s_period;
        }
    }
    protected void RadGrid1_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            s_n_organ = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NAME_ORG"].ToString();
            s_n_brig = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NAME_BRIG"].ToString();
            s_n_p = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUM_P"].ToString();
            s_period = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PERIOD"].ToString();
            HiddenFieldNP.Value = e.Item.DataSetIndex.ToString();
            s_year = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["YEAR"].ToString();
            HiddenFieldYEAR.Value = s_year;
            RadGrid1.Controls.Add(new LiteralControl(" выбран протокол №: " + s_n_p));
            LabelOrg.Text = "организация: " + s_n_organ + " (" + s_n_brig + ") Номер протокола: " + s_n_p;
            Button_Protcl.Visible = true;
            if ((Session["SIZP_User"].ToString() == "Горегляд А.С.") && (RadioButton2.Checked == true))
            {
                Button_Admin.Visible = true;
            }
            SqlDataSource2.SelectCommand = "SELECT IZP_BOOK.ID_ as ID_,NUM,NUM_P,YEAR,PERIOD,NAME_ORG,K_BRIG,NAME_BRIG,IZP_BOOK.K_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,V_INDIC2,C_LEFT,C_RIGHT,C_POINTER,T_WATER,T_AIR,HUMIDITY,"+/*PRESSURE,*/"RESULT_EXP,FIO_EXEC,EXEC_SIGN,DATE_PROT,NUM_ACT,NOTE FROM IZP_SPR_PERIOD,IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PROTECT WHERE IZP_SPR_PERIOD.NUM_PROTCL=IZP_BOOK.K_GROUP AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE AND K_NAME=IZP_SPR_PROTECT.ID_ AND YEAR=" + s_year + " ORDER BY NUM_P,FAC_NUM";
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
        Button_Admin.Visible = false;
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
                SqlDataSource2.SelectCommand = "SELECT IZP_BOOK.ID_ as ID_,NUM,NUM_P,YEAR,PERIOD,NAME_ORG,K_BRIG,NAME_BRIG,IZP_BOOK.K_GROUP,IZP_SPR_PROTECT.NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,V_INDIC2,C_LEFT,C_RIGHT,C_POINTER,T_WATER,T_AIR,HUMIDITY,"/*PRESSURE,*/+"RESULT_EXP,FIO_EXEC,EXEC_SIGN,DATE_PROT,NUM_ACT,NOTE FROM IZP_SPR_PERIOD,IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PROTECT WHERE IZP_SPR_PERIOD.NUM_PROTCL=IZP_BOOK.K_GROUP AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE AND K_NAME=IZP_SPR_PROTECT.ID_ AND YEAR=" + HiddenFieldYEAR.Value + " ORDER BY NUM_P,FAC_NUM";
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
        InjectScript.Text = "";
        string[] arrStr = new string[4];
        bool b_Admin_Rebuild_Protcl = false;
        string[] separators = { ";", ":" };
        arrStr = Label_Protcl.Text.Split(separators, 4, StringSplitOptions.None);
        Button mybutton=(sender) as Button;
        b_Admin_Rebuild_Protcl = mybutton.Text == "<-! сформировать заново !->"?true:false;// верно, если протокол пересоздает Администратор
        //mybutton.Text == "<-! сформировать заново !->"
        //Label2.Text = mybutton.Text;
        if ((RadioButton1.Checked == true) || b_Admin_Rebuild_Protcl)
        {
            string commandStringCheck = "SELECT COUNT(ID_) FROM IZP_BOOK WHERE YEAR='" + arrStr[2] + "' AND RESULT_EXP IS NULL";
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
                string path_pdf = Server.MapPath("~/pdf/protocols/" + arrStr[2] + ".pdf");
                int i_group = Convert.ToInt16(arrStr[0]);
                IWorkbook book = NativeExcel.Factory.OpenWorkbook(path);
                //Label1.Text = Label_Protcl.Text;
                if (book != null)
                {
                    IWorksheet sheet = book.Worksheets[1];
                    sheet.Cells["A12"].Value = "ПРОТОКОЛ № 3/" + arrStr[1];
                    string commandStringParam = "SELECT FIO_EXEC,FIO_CHIEF FROM IZP_PARAM";
                    OleDbCommand myCommandParam = new OleDbCommand(commandStringParam, myConnection);
                    string commandStringEquip = "SELECT ID_,NAME,MODEL,FAB_NUM,TO_CHAR(DATE_POV,'dd.MM.yyyy') as DATE_POV FROM IZP_SPR_EQUIP ORDER BY ID_";
                    OleDbCommand myCommandEquip = new OleDbCommand(commandStringEquip, myConnection);
                    string commandString = "SELECT IZP_BOOK.ID_ as ID_,NUM,NUM_P,NAME_ORG,K_BRIG,NAME_BRIG,IZP_BOOK.K_GROUP,IZP_SPR_PROTECT.NAME AS NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,V_PROT,V_ISOL,V_WORK,V_WIRE,V_INDIC,V_INDIC2,C_LEFT,C_RIGHT,C_POINTER,T_WATER,T_AIR,HUMIDITY,"/*PRESSURE,*/+ "RESULT_EXP,FIO_EXEC,EXEC_SIGN,DATE_PROT,NUM_ACT,NOTE FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PROTECT WHERE K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE AND K_NAME=IZP_SPR_PROTECT.ID_ AND YEAR='" + arrStr[2] + "' ORDER BY NUM_P,FAC_NUM";
                    OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
                    myConnection.Open();
                    OleDbDataReader ReaderParam = myCommandParam.ExecuteReader();
                    while (ReaderParam.Read())
                    {
                        //sheet.Cells["G49"].Value = "("+Convert.ToString(ReaderParam["FIO_EXEC"])+")";
                        sheet.Cells["J51"].Value = "(" + Convert.ToString(ReaderParam["FIO_CHIEF"]) + ")";
                    }
                    ReaderParam.Close();
                    OleDbDataReader ReaderEquip = myCommandEquip.ExecuteReader();
                    int i = 0;
                    while (ReaderEquip.Read() && i < 8)
                    {
                        switch (i)
                        {
                            case 0:
                                if ((new[] { 1,3,4,5,7,9,11,16 }).Contains(i_group))
                                {
                                    sheet.Cells["D20"].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                    sheet.Cells["H20"].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                    sheet.Cells["K20"].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                }
                                break;
                            case 5:
                                if (i_group==13)
                                {
                                    sheet.Cells["D20"].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                    sheet.Cells["H20"].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                    sheet.Cells["K20"].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                }
                                break;
                            case 6:
                                if ((new[] { 2,6,8,10,12,14,15 }).Contains(i_group))
                                {
                                    sheet.Cells["D20"].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                    sheet.Cells["H20"].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                    sheet.Cells["K20"].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                }
                                break;
                            case 7:
                                break;
                            default:
                                sheet.Cells["D" + (20 + i).ToString()].Value = Convert.ToString(ReaderEquip["MODEL"]);
                                sheet.Cells["H" + (20 + i).ToString()].Value = Convert.ToString(ReaderEquip["FAB_NUM"]);
                                sheet.Cells["K" + (20 + i).ToString()].Value = Convert.ToString(ReaderEquip["DATE_POV"]);
                                break;
                        }
                        i++;
                    }
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
                            if (!b_Admin_Rebuild_Protcl)    // Не менять имя исполнителя, если правит Админ 
                            { 
                                sheet.Cells["J49"].Value = "(" + Session["SIZP_User"].ToString() + ")"; 
                            }
                            else 
                            {
                                sheet.Cells["J49"].Value = "(" + Convert.ToString(Reader["FIO_EXEC"]) + ")"; 
                            }
                            sheet.Cells["I8"].Value = Convert.ToString(Reader["NAME_ORG"]) + " " + (Convert.ToString(Reader["NAME_BRIG"]) == "-" ? "" : Convert.ToString(Reader["NAME_BRIG"]));
                            sheet.Cells["C16"].Value = Convert.ToString(Reader["DATE_EXP"]);
                            sheet.Cells["K16"].Value = Convert.ToString(Reader["DATE_EXP"]);
                            //sheet.Cells["C54"].Value = Convert.ToString(Reader["NAME_ORG"]);
                            sheet.Cells["I25"].Value = Convert.ToString(Reader["T_WATER"]) + " °С";
                            sheet.Cells["A26"].Value = "Температура окружающей среды " + Convert.ToString(Reader["T_AIR"]) + " °С        ";
                            sheet.Cells["A26"].Value += "Относительная влажность " + Convert.ToString(Reader["HUMIDITY"]) + " %        ";
                            //sheet.Cells["A26"].Value += "Давление " + Convert.ToString(Reader["PRESSURE"]) + " кПа";
                            data_next = Convert.ToDateTime(Reader["DATE_EXP"]).AddMonths(Convert.ToInt16(arrStr[3]));
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
                            st_result = "негодно";
                            ix++;
                            if (ix > 1) { st_not_for_use += "," + st_num; } else { st_not_for_use = st_num; }
                        }
                        else { st_result = "годно"; b_date_next = true; }
                        sheet.Cells["A" + (34 + i).ToString()].Value = (i + 1).ToString();
                        if ((new[] { 1, 2, 3 }).Contains(i_group))
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
                            if ((new[] { 4, 7, 8, 9, 10, 11, 12, 14, 15 }).Contains(i_group))
                            {
                                sheet.Cells["B" + (34 + i).ToString()].Value = Convert.ToString(Reader["NAME"]);
                                sheet.Cells["C" + (34 + i).ToString()].Value = Convert.ToString(Reader["FAC_NUM"]);
                                sheet.Cells["F" + (34 + i).ToString()].Value = Convert.ToString(Reader["V_PROT"]);
                                if ((new[] { 4, 14, 16 }).Contains(i_group))
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
                    sheet.Cells["H46"].Value = st_not_for_use + "   испытание";
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
                }
                string commandStringUPD = "UPDATE IZP_BOOK SET DATE_PROT='" + data.ToString("dd.MM.yyyy") + "' WHERE YEAR=" + arrStr[2];
                OleDbCommand myCommandUPD = new OleDbCommand(commandStringUPD, myConnection);
                //Label_Protcl.Text = commandStringUPD;
                myConnection.Open();
                object update = myCommandUPD.ExecuteNonQuery();
                myConnection.Close();
                Button_Protcl.Visible = false;
                Button_Admin.Visible = false;
                string s_openURL = "../pdf/protocols/" + arrStr[2] + ".pdf";
                InjectScript.Text = "<script type=\"text/javascript\">OpenPrint('" + s_openURL + "')</script>";
            }
            if (b_Admin_Rebuild_Protcl) { FuncSqlRebindGrid1(true); } else { FuncSqlRebindGrid1(false); } // Не переходить ввод данных, если правит Админ
            Params_to_Zero();
        }
        else
        {
            string s_openURL = "../pdf/protocols/" + arrStr[2] + ".pdf";
            InjectScript.Text = "<script type=\"text/javascript\">OpenPrint('" + s_openURL + "')</script>";
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        Button_Protcl.Visible = false;
        Button_Admin.Visible = false;
        if (RadioButton1.Checked == true)
        {
            FuncSqlRebindGrid1(false);
            Params_to_Zero();
            Button_Protcl.Text = "выдать протокол";
        }
        else
        {
            FuncSqlRebindGrid1(true);
            Params_to_Zero();
            Button_Protcl.Text = "открыть протокол";
        }
    }
    protected void FuncSqlRebindGrid1(bool act)
    {
        //  act = true для выданных протоколов и false для невыданных
        if (!act)
        {
            SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,QUANTITY,TO_CHAR(DATE_PROT,'dd.MM.yyyy') AS DATE_PROT FROM (SELECT  NUM_P,NUM_ACT,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,count(IZP_BOOK.ID_) AS QUANTITY,MAX(NVL(DATE_PROT,'11.11.2100')) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE GROUP BY NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD) WHERE DATE_PROT='11.11.2100' AND NUM_ACT IS NOT NULL ORDER BY TO_NUMBER(YEAR) DESC";
        }
        else
        {
            SqlDataSource1.SelectCommand = "SELECT NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,QUANTITY,TO_CHAR(DATE_PROT,'dd.MM.yyyy') AS DATE_PROT FROM (SELECT  NUM_P,NUM_ACT,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD,count(IZP_BOOK.ID_) AS QUANTITY,MAX(NVL(DATE_PROT,'11.11.2100')) AS DATE_PROT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD WHERE K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE GROUP BY NUM_P,NAME_ORG,NAME_BRIG,YEAR,K_ORGAN,K_BRIG,NAME_GROUP,PERIOD) WHERE DATE_PROT<>'11.11.2100' AND NUM_ACT IS NOT NULL ORDER BY TO_NUMBER(YEAR) DESC";
        }
        RadGrid1.DataBind();
    }
    protected void Params_to_Zero()
    {
        LabelOrg.Text = "";
        SqlDataSource2.SelectCommand = "";
        RadGrid2.DataBind();
    }
}