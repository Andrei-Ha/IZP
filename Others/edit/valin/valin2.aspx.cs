﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class valin2 : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string t_water;
    string t_air;
    string humidity;
    string pressure;
    string fio_exec;
    string id_;
    string k_group;
    double i_maxcurr;
    protected void TakeParam()
    {
        string commandStringInfo = "SELECT NUM_P,NAME_ORG,NAME_BRIG,NAME_GROUP,IZP_SPR_PROTECT.NAME AS NAME,FAC_NUM,TO_CHAR(DATE_IN,'DD.MM.YYYY') as DATE_IN,TO_CHAR(DATE_EXP,'DD.MM.YYYY') as DATE_EXP,NUM_ACT FROM IZP_BOOK,IZP_SPR_ORG,IZP_SPR_BRIG,IZP_SPR_PERIOD,IZP_SPR_PROTECT WHERE IZP_BOOK.K_GROUP=NUM_PROTCL AND K_ORGAN=IZP_SPR_ORG.CODE AND K_BRIG=IZP_SPR_BRIG.CODE AND IZP_SPR_PROTECT.ID_=K_NAME AND IZP_BOOK.ID_=" + id_;
        OleDbCommand myCommandInfo = new OleDbCommand(commandStringInfo, myConnection);
        string commandString = "SELECT T_WATER,T_AIR,HUMIDITY,PRESSURE,FIO_EXEC FROM IZP_PARAM";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        myConnection.Open();
        OleDbDataReader readerInfo = myCommandInfo.ExecuteReader();
        while (readerInfo.Read())
        {
            Label_Name.Text = "<strong>" + Convert.ToString(readerInfo["NAME"]) + "</strong>  заводск.номер:<strong>" + Convert.ToString(readerInfo["FAC_NUM"]) + "</strong>";
            Label_Organ.Text = Convert.ToString(readerInfo["NAME_ORG"]) + " (" + Convert.ToString(readerInfo["NAME_BRIG"]) + ")";
            Label_Info.Text = "протокол №" + Convert.ToString(readerInfo["NUM_P"]);
        }
        readerInfo.Close();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            t_water = Convert.ToString(reader1["T_WATER"]);
            t_air = Convert.ToString(reader1["T_AIR"]);
            humidity = Convert.ToString(reader1["HUMIDITY"]);
            pressure = Convert.ToString(reader1["PRESSURE"]);
            fio_exec = Convert.ToString(reader1["FIO_EXEC"]);
        }
        reader1.Close();
        myConnection.Close();
    }
    protected void Zapoln(string x)
    {
        RadioButton1.Text = x+"кВ";
        HiddenField_v_prot.Value = x;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RadDatePicker_date_exp.SelectedDate = DateTime.Now;
            lab.Text = Request.QueryString["param"];
        }
        id_ = Request.QueryString["id_"];
        k_group = Request.QueryString["k_group"];
        lab.Text = "id="+id_+", k_group="+k_group;
        TakeParam();
        switch (k_group)
        {
            case "4": //Электроизолирующий инструмент
                Zapoln("2");
                break;
            case "7": //Электроизолирующие клещи до 1000В
                Zapoln("2");
                break;
            case "8": //Электроизолирующие клещи выше 1000В
                Zapoln("40");
                break;
            case "9": //Электроизмерительные клещи до 1000В
                Zapoln("2");
                break;
            case "10": //Электроизмерительные клещи выше 1000В
                Zapoln("40");
                break;
            case "11": //Электроизолирующие штанги до 1000В
                Zapoln("2");
                break;
            case "12": //Электроизолирующие штанги выше 1000В
                Zapoln("40");
                break;
            case "14": //Электроизолирующие лестницы
                Zapoln("40");
                break;
            case "15": //Устройство для прокола кабеля
                Zapoln("40");
                break;
            case "16": //Колпаки
                Zapoln("10");
                break;
        }
        Save.Focus();
        //int i_group = Convert.ToInt16(k_group);
    }
    protected void UpdateFun()
    {
        string sv_prot = "";    //1
        string sv_isol = "";    //2
        string sv_work = "";    //3
        string sv_wire = "";    //4
        string sv_indic = "";   //5
        string sc_left = "";    //6
        string sc_right = "";   //7
        string sc_pointer = ""; //8
        string sresult_exp = "0";
        if (RadioButton1.Checked == true)
        {
            sv_prot = ",V_PROT=" + HiddenField_v_prot.Value;
            sresult_exp = "1";
        }
        else
        {
            sv_prot = ",V_PROT=0";
        }
        Save.Enabled = false;
        Cancel.Enabled = false;
        string commandStringTEC = "UPDATE IZP_BOOK SET DATE_EXP='" + RadDatePicker_date_exp.SelectedDate.ToString().Substring(0, 10) + "'" + sv_prot + sv_isol + sv_work + sv_wire + sv_indic + sc_left + sc_right + sc_pointer + ",RESULT_EXP=" + sresult_exp +
            ",T_WATER=" + t_water + ",T_AIR=" + t_air + ",HUMIDITY=" + humidity + ",PRESSURE='" + pressure + "',FIO_EXEC='" + fio_exec + "' WHERE ID_=" + id_;
        OleDbCommand myCommandTEC = new OleDbCommand(commandStringTEC, myConnection);
        lab.Text = lab.Text + " string=" + commandStringTEC;
        myConnection.Open();
        object num_rowTEC = myCommandTEC.ExecuteNonQuery();
        myConnection.Close();    
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        UpdateFun();
        double IEx = Convert.ToDouble(Request.Browser.Version.Replace(".", ","));
        if (IEx >= 7)
        { InjectScript.Text = "<script type=\"text/javascript\">CloseAndRebind(true)</script>"; }
        else
        { InjectScript.Text = "<script type=\"text/javascript\">CancelEdit()</script>"; }
    }
}