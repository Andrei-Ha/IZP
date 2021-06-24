using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class Cabinets : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string sub_str="", name="", cell="0", cell_temp="0";
        int kolvo = 0, sum_cell = 0;
        if (Request.QueryString["param"] != null)
        {
            string str_cell = Request.QueryString["param"].ToString();
            
            HiddenField_Cell.Value = str_cell;
        }
        string commandString = "SELECT NAME_GROUP, CELL, num_protcl, count(EXEC_SIGN) as KOLVO FROM PES_IZP_BOOK, IZP_SPR_PERIOD WHERE PES_IZP_BOOK.K_GROUP = NUM_PROTCL AND EXEC_SIGN = 0 AND CELL> 0 group by NAME_GROUP,CELL,num_protcl ORDER BY CELL, num_protcl";
        OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
        string commandString2 = "SELECT NAME_GROUP, CELL, num_protcl, count(EXEC_SIGN) as KOLVO FROM IZP_BOOK, IZP_SPR_PERIOD WHERE IZP_BOOK.K_GROUP = NUM_PROTCL AND EXEC_SIGN = 0 AND CELL> 0 group by NAME_GROUP,CELL,num_protcl ORDER BY CELL, num_protcl";
        OleDbCommand myCommand2 = new OleDbCommand(commandString2, myConnection);
        myConnection.Open();
        OleDbDataReader reader1 = myCommand.ExecuteReader();
        while (reader1.Read())
        {
            cell= Convert.ToString(reader1["CELL"]);
            name = Convert.ToString(reader1["NAME_GROUP"]);
            kolvo = Convert.ToInt16(reader1["KOLVO"]);
            if (cell_temp != cell)
            {
                if (cell_temp != "0")
                    sub_str += "<a class=\"cl_red\">Пинских ЭС - " + sum_cell.ToString() + "</a>:[" + sum_cell.ToString() + "];";
                sub_str += cell + ":";
                sum_cell = 0;
                cell_temp = cell;
            }
            sum_cell += kolvo;
            sub_str += name + " " + kolvo + "<br/>";
        }
        if(cell_temp!="0")
            sub_str += "<a class=\"cl_red\">Пинских ЭС - " + sum_cell.ToString() + "</a>:[" + sum_cell.ToString() + "];";
        reader1.Close();
        ///////////////////////////////////////
        cell_temp = "0";
        reader1 = myCommand2.ExecuteReader();
        while (reader1.Read())
        {
            cell = Convert.ToString(reader1["CELL"]);
            name = Convert.ToString(reader1["NAME_GROUP"]);
            kolvo = Convert.ToInt16(reader1["KOLVO"]);
            if (cell_temp != cell)
            {
                if (cell_temp != "0")
                    sub_str += "<a class=\"cl_green\">сторонних орг. - " + sum_cell.ToString() + "</a>:(" + sum_cell.ToString() + ");";
                sub_str += cell + ":";
                sum_cell = 0;
                cell_temp = cell;
            }
            sum_cell += kolvo;
            sub_str += name + " " + kolvo + "<br/>";
        }
        if (cell_temp != "0")
            sub_str += "<a class=\"cl_green\">сторонних орг. - " + sum_cell.ToString() + "</a>:(" + sum_cell.ToString() + ");";
        reader1.Close();
        myConnection.Close();
        //Label_Cell.Text = sub_str;
        HiddenField_Content.Value = sub_str;
    }
    /*SELECT NAME_GROUP, CELL, num_protcl, count(EXEC_SIGN)
FROM PES_IZP_BOOK, IZP_SPR_PERIOD
WHERE PES_IZP_BOOK.K_GROUP = NUM_PROTCL
AND EXEC_SIGN = 0 AND CELL> 0 group by NAME_GROUP,CELL,num_protcl ORDER BY CELL, num_protcl*/
    //DropDownListOrg.Items.Add(new ListItem(Convert.ToString(reader1["NAME_ORG"]), Convert.ToString(reader1["CODE"])));
    /*select NAME_ORG, NAME_BRIG, NAME_GROUP, CELL, COUNT(EXEC_SIGN) as KOLVO from(SELECT NAME_ORG, NAME_BRIG, NAME_GROUP, CELL, EXEC_SIGN
FROM PES_IZP_BOOK, PES_IZP_SPR_ORG, PES_IZP_SPR_BRIG, IZP_SPR_PERIOD
WHERE K_ORGAN = PES_IZP_SPR_ORG.CODE AND K_BRIG = PES_IZP_SPR_BRIG.CODE AND PES_IZP_BOOK.K_GROUP = NUM_PROTCL
AND EXEC_SIGN = 0 AND CELL > 0 ORDER BY CELL, PES_IZP_BOOK.K_GROUP, PES_IZP_BOOK.K_BRIG)  GROUP BY NAME_ORG,NAME_BRIG,NAME_GROUP,CELL ORDER BY CELL*/
}