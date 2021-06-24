using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;


public partial class Default : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    protected void FuncParam()
    {
        string commandString = "SELECT TO_CHAR(DATE_,'DD.MM.YYYY') as DATE_,T_WATER,T_AIR,HUMIDITY,PRESSURE,FIO_EXEC,FIO_CHIEF,DATE_INS, DATE_PRICE FROM IZP_PARAM";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader reader = myCommand.ExecuteReader();
        reader.Read();
        Literal1.Text =
            "Дата: " + Convert.ToString(reader["DATE_"]) + "<br />" +
            "Температура воды: " + Convert.ToString(reader["T_WATER"]) + "°С" + "<br />" +
            "Температура воздуха: " + Convert.ToString(reader["T_AIR"]) + "°С" + "<br />" +
            "Относительная влажность: " + Convert.ToString(reader["HUMIDITY"]) + "%" + "<br />" +
            "<br />" +
            "Ф.И.О. исполнителя: " + Convert.ToString(reader["FIO_EXEC"]) + "<br />" +
            "Ф.И.О. начальника СИЗП: " + Convert.ToString(reader["FIO_CHIEF"]);
        Label1.Text = "<p style='color:#0000ff'> Данные внесены: " + Convert.ToString(reader["DATE_INS"]) + "</p>";
        reader.Close();
        myConnection.Close();    
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FuncParam();
            Label2.Visible = false;
            string commandStringParam = "SELECT TO_CHAR(DATE_,'dd,MM,yyyy') as DATE_,T_WATER,T_AIR,HUMIDITY,PRESSURE,FIO_EXEC,FIO_CHIEF, TO_CHAR(DATE_PRICE,'dd,MM,yyyy') as DATE_PRICE FROM IZP_PARAM";
            OleDbCommand myCommandParam = new OleDbCommand(commandStringParam, myConnection);
            myConnection.Open();
            OleDbDataReader Reader = myCommandParam.ExecuteReader();
            while (Reader.Read())
            {
                DateTime date_in = Convert.ToDateTime(Reader["DATE_"]);
                DateTime date_price = Convert.ToDateTime(Reader["DATE_PRICE"]);
                RadDatePicker_Price.SelectedDate = date_price;
                T_WATER.Text = Convert.ToString(Reader["T_WATER"]);
                T_AIR.Text = Convert.ToString(Reader["T_AIR"]);
                HUMIDITY.Text = Convert.ToString(Reader["HUMIDITY"]);
                //PRESSURE.Text = Convert.ToString(Reader["PRESSURE"]);
                //FIO_EXEC.Text = Convert.ToString(Reader["FIO_EXEC"]);
                FIO_CHIEF.Text = Convert.ToString(Reader["FIO_CHIEF"]);
            }
            Reader.Close();
            myConnection.Close();
            if (Session["SIZP_User"] != null)
            {
                FIO_EXEC.Text = Session["SIZP_User"].ToString();
            }        
        }
        
        RadDatePicker_date_dok.SelectedDate = DateTime.Now;
    }
    protected void Button_Click(object sender, EventArgs e)
    {
       
        //Label2.Visible = true;
        //Label2.Text = "Валидация пройдена";
        string commandString = "UPDATE IZP_PARAM SET DATE_='" + RadDatePicker_date_dok.SelectedDate.ToString().Substring(0, 10) + "',T_WATER='"+ T_WATER.Text +"',T_AIR='" + T_AIR.Text + "',HUMIDITY='" + HUMIDITY.Text + "',FIO_EXEC='" + FIO_EXEC.Text + "',FIO_CHIEF='" + FIO_CHIEF.Text + "',DATE_INS=sysdate, DATE_PRICE='" + RadDatePicker_Price.SelectedDate.ToString().Substring(0, 10) + "'";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        //Label1.Text = commandStringTEC;
        myConnection.Open();
        object num_row = myCommand.ExecuteNonQuery();
        myConnection.Close();
        FuncParam();
    }
}