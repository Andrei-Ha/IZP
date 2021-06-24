using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class DelArch_PES : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string st_confirm = "0";
    System.Web.HttpContext context = System.Web.HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (HttpContext.Current.User.Identity.Name.ToString())
        {
            case "sizp":
                st_confirm = "CONFIRM1";
                break;
            case "egik":
                st_confirm = "CONFIRM2";
                break;
            default:
                st_confirm = "0";
                break;
        }
        DateTime date_ = new DateTime();
        date_ = DateTime.Today.AddYears(-1);
        RadDatePicker_date.MaxDate = date_;
        if (!Page.IsPostBack)
        {
            RadDatePicker_date.SelectedDate = date_;
            FuncIsDel();
        }
        SqlDataSource2.SelectCommand = "SELECT ID,TO_CHAR(MIN_DATE,'DD.MM.YYYY') as MIN_DATE,TO_CHAR(MAX_DATE,'DD.MM.YYYY') as MAX_DATE,PROT_COUNT,REC_COUNT,REPLACE(EXEC_INFO,';',';<br />') as EXEC_INFO,REPLACE(Replace(CONFIRM1,'0','запрещено'),'1','СОГЛАСОВАНО') as CONFIRM1,REPLACE(CONFIRM1_INFO,';',';<br />') as CONFIRM1_INFO,REPLACE(Replace(CONFIRM2,'0','запрещено'),'1','СОГЛАСОВАНО') as CONFIRM2,REPLACE(CONFIRM2_INFO,';',';<br />') as CONFIRM2_INFO,REPLACE(Replace(DEL,'0','согласование'),'1','УДАЛЕН') as DEL,REPLACE(DEL_INFO,';',';<br />') as DEL_INFO FROM PES_IZP_DEL_ARCH ORDER BY ID DESC";
    }
    protected void FuncIsDel()
    {
        string commandString = "SELECT ID,TO_CHAR(MIN_DATE,'DD.MM.YYYY') as MIN_DATE,TO_CHAR(MAX_DATE,'DD.MM.YYYY') as MAX_DATE,PROT_COUNT,REC_COUNT, EXEC_INFO,CONFIRM1,CONFIRM1_INFO,CONFIRM2,CONFIRM2_INFO,DEL,DEL_INFO FROM PES_IZP_DEL_ARCH WHERE DEL=0";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader Reader = myCommand.ExecuteReader();
        if (st_confirm == "0")
        {
            PanelConfirm1.Visible = false;
            if (Reader.HasRows)
            {

                PanelFilter.Visible = false;
                PanelCancel.Visible = true;
                Reader.Read();
                if (Reader["CONFIRM1"].ToString() == "1" && Reader["CONFIRM2"].ToString() == "1")
                {
                    ButtonDelete.Visible = true;
                }
                else { ButtonDelete.Visible = false; }
                Reader.Close();
            }
            else
            {
                PanelFilter.Visible = true;
                PanelCancel.Visible = false;
            }
        }
        else
        {
            PanelFilter.Visible = false;
            PanelCancel.Visible = false;
            if (Reader.HasRows)
            {
                PanelConfirm1.Visible = true;
                Reader.Close();
                LabelConfirm.Text = " Уважаемый " + Session["SIZP_User"] + ", т.к. поступил запрос на удаление архива, а вы являетесь лицом принимающим решение(ЛПР"+st_confirm.Substring(7)+"), сделайте свой выбор:";
            }
            else
            {
                PanelConfirm1.Visible = false;
                LabelConfirm.Text = "";
            }
        }

        myConnection.Close();
    }
    protected void ButtonForm_Click(object sender, EventArgs e)
    {
        string s_date = RadDatePicker_date.SelectedDate.ToString().Substring(0, 10);
        string commandString = "SELECT MIN(DATE_IN) FROM PES_IZP_BOOK WHERE DATE_IN<='" + s_date + "'";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        string commandString2 = "SELECT MAX(DATE_IN) FROM PES_IZP_BOOK WHERE DATE_IN<='" + s_date + "'";
        OleDbCommand myCommand2 = new OleDbCommand(commandString2, myConnection);
        string commandString3 = "SELECT COUNT(*) FROM PES_IZP_BOOK WHERE DATE_IN<='" + s_date + "'";
        OleDbCommand myCommand3 = new OleDbCommand(commandString3, myConnection);
        string commandString4 = "SELECT count(*) FROM (SELECT Distinct NUM_P, TO_CHAR(DATE_IN,'yyyy') FROM PES_IZP_BOOK WHERE DATE_IN<='" + s_date + "')";
        OleDbCommand myCommand4 = new OleDbCommand(commandString4, myConnection);
        string commandStringYear = "SELECT DISTINCT TO_CHAR(DATE_IN,'yyyy') as DATE_IN FROM PES_IZP_BOOK WHERE DATE_IN<='" + s_date + "' ORDER BY DATE_IN";
        OleDbCommand myCommandYear = new OleDbCommand(commandStringYear, myConnection);
        myConnection.Open();
        string min_date = myCommand.ExecuteScalar().ToString();
        string max_date = myCommand2.ExecuteScalar().ToString();
        string rec_count = myCommand3.ExecuteScalar().ToString();
        string prot_count = myCommand4.ExecuteScalar().ToString();
        myConnection.Close();
        if (min_date != "")
        {
            min_date = min_date.Substring(0, 10);
            max_date = max_date.Substring(0, 10);
            LabelContent.Text = "период с" + min_date + " по " + max_date + "<br/>кол-во протоколов:" + prot_count + "<br/>кол-во записей:" + rec_count + "<hr>";
            string commandStringIf = "SELECT count(ID) as ID_COUNT FROM PES_IZP_DEL_ARCH WHERE DEL=0";
            OleDbCommand myCommandIf = new OleDbCommand(commandStringIf, myConnection);
            myConnection.Open();
            object o_id_count = myCommandIf.ExecuteScalar();
            if (o_id_count.ToString() == "0")
            {
                string commandStringIn = "INSERT INTO PES_IZP_DEL_ARCH(ID,MIN_DATE,MAX_DATE,PROT_COUNT,REC_COUNT,EXEC_INFO) VALUES((SELECT NVL(MAX(ID)+1,1) FROM PES_IZP_DEL_ARCH),'" + min_date + "','" + max_date + "'," + prot_count + "," + rec_count + ",TO_CHAR(sysdate,'DD.MM.YYYY')||';" + Session["SIZP_User"].ToString() + "'||';" + context.Request.ServerVariables["REMOTE_ADDR"] + "') ";
                OleDbCommand myCommandIn = new OleDbCommand(commandStringIn, myConnection);
                object o_insert = myCommandIn.ExecuteNonQuery();
                OleDbDataReader ReaderYear = myCommandYear.ExecuteReader();
                while (ReaderYear.Read())
                {
                    string commandStringYearDetail = "SELECT MIN(DATE_IN) as MIN_DATE,MAX(DATE_IN) as MAX_DATE,MIN(NUM_P) as MIN_PROT,MAX(NUM_P) as MAX_PROT,count(*) as REC_COUNT,count(distinct num_p) as PROT_COUNT FROM PES_IZP_BOOK WHERE TO_CHAR(DATE_IN,'yyyy') = '" + Convert.ToString(ReaderYear["DATE_IN"]) + "' AND DATE_IN<='" + s_date + "'";
                    OleDbCommand myCommandYearDetail = new OleDbCommand(commandStringYearDetail, myConnection);
                    OleDbDataReader ReaderYearDetail = myCommandYearDetail.ExecuteReader();
                    while (ReaderYearDetail.Read())
                    {
                        LabelContent.Text += "<br/>" + "c " + Convert.ToString(ReaderYearDetail["MIN_DATE"]).Substring(0, 10) + " по " + Convert.ToString(ReaderYearDetail["MAX_DATE"]).Substring(0, 10) + " протоколы:" + Convert.ToString(ReaderYearDetail["MIN_PROT"]) + "-" + Convert.ToString(ReaderYearDetail["MAX_PROT"]) + "  кол-во протоколов:" + Convert.ToString(ReaderYearDetail["PROT_COUNT"]) + "  кол-во записей:" + Convert.ToString(ReaderYearDetail["REC_COUNT"]);
                    }
                    ReaderYearDetail.Close();
                }
                ReaderYear.Close();
            }
            myConnection.Close();
            RadGrid2.DataBind();
            FuncIsDel();
        }else
        {
            LabelContent.Text = "нет записей до выбранной даты!";
        }        

    }
    protected void ButtonCansel_Click(object sender, EventArgs e)
    {
        string commandString = "DELETE FROM PES_IZP_DEL_ARCH WHERE DEL=0";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        object o_cancel = myCommand.ExecuteNonQuery();
        myConnection.Close();
        RadGrid2.DataBind();
        FuncIsDel();
        LabelContent.Text = "";
    }
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        string commandString = "UPDATE PES_IZP_DEL_ARCH SET DEL=1,DEL_INFO=TO_CHAR(sysdate,'DD.MM.YYYY')||';" + Session["SIZP_User"].ToString() + "'||';" + context.Request.ServerVariables["REMOTE_ADDR"] + "' WHERE DEL=0";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        object o_cancel = myCommand.ExecuteNonQuery();
        myConnection.Close();
        RadGrid2.DataBind();
        FuncIsDel();
        LabelContent.Text = "";
    }
    protected void ButtonConfirmYes_Click(object sender, EventArgs e)
    {
        FuncUpdate("1");
    }
    protected void ButtonConfirmNo_Click(object sender, EventArgs e)
    {
        FuncUpdate("0");
    }
    protected void FuncUpdate(string st_yes)
    {
        string commandString = "UPDATE PES_IZP_DEL_ARCH SET " + st_confirm + "=" + st_yes + "," + st_confirm + "_INFO=TO_CHAR(sysdate,'DD.MM.YYYY')||';" + Session["SIZP_User"].ToString() + "'||';" + context.Request.ServerVariables["REMOTE_ADDR"] + "' WHERE DEL=0";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        object o_cancel = myCommand.ExecuteNonQuery();
        myConnection.Close();
        RadGrid2.DataBind();
        FuncIsDel();
        LabelContent.Text = "";
    }
}