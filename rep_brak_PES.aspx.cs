using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NativeExcel;
using GemBox.Spreadsheet;
using System.Data.OleDb;

public partial class rep_brak_PES : System.Web.UI.Page
{
    static System.Configuration.ConnectionStringSettings constring = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_to_Pinsk"];
    static string connectionString = "Provider=MSDAORA; " + constring.ConnectionString;
    OleDbConnection myConnection = new OleDbConnection(connectionString);
    string[] arrExcl = new string[16];
    string[] arrSmall = new string[3];
    string[] separators = { ";" };
    string[] separators2 = { "," };
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelQstring.Text = Request.QueryString.ToString();
        string path = Server.MapPath("~/Edit/reports/brak_pes.xls");
        IWorkbook book = NativeExcel.Factory.OpenWorkbook(path);
        if (book != null)
        {
            IWorksheet sheet = book.Worksheets[1];
            sheet.Cells["A4"].Value = Request.QueryString["term"];
            sheet.Cells["A6"].Value = Request.QueryString["podr"];
            string commandString = "SELECT NUM_PROTCL,NAME_GROUP FROM IZP_SPR_PERIOD WHERE ID_<>99 ORDER BY NUM_PROTCL";
            OleDbCommand myCommand = new OleDbCommand(commandString, myConnection);
            myConnection.Open();
            OleDbDataReader reader1 = myCommand.ExecuteReader();
            int i = 9;
            while (reader1.Read())
            {
                sheet.Cells["A" + i.ToString()].Value = Convert.ToString(reader1["NAME_GROUP"]);
                sheet.Cells["B" + i.ToString()].Value = 0;
                sheet.Cells["C" + i.ToString()].Value = 0;
                sheet.Range["A9:C24"].Rows[i-8].Hidden = true;
                i++;
            }
            reader1.Close();
            myConnection.Close();
            int i_count_str = Convert.ToInt16(Request.QueryString["count_str"]);
            string index = "";
            arrExcl = Request.QueryString["brak"].Split(separators, i_count_str+1, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < i_count_str; j++)
            {
                arrSmall = arrExcl[j].Split(separators2, 3, StringSplitOptions.None);
                index = (8 + Convert.ToInt16(arrSmall[0])).ToString();
                sheet.Cells["B" + index].Value = Convert.ToInt16(arrSmall[1]);
                sheet.Cells["C" + index].Value = Convert.ToInt16(arrSmall[2]);
                sheet.Range["A9:C24"].Rows[Convert.ToInt16(arrSmall[0])].Hidden = false;
            }
            book.SaveAs(path);
            string path_pdf = Server.MapPath("~/pdf/reports/brak_pes.pdf");
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile ExcToPdf = ExcelFile.Load(path);
            ExcelWorksheet ws = ExcToPdf.Worksheets[0];
            //ws.PrintOptions.BottomMargin = 0;
            ws.PrintOptions.LeftMargin = 0.1;
            ws.PrintOptions.RightMargin = 0.1;
            ws.PrintOptions.TopMargin = 0.1;
            ws.PrintOptions.BottomMargin = 0.1;
            ws.PrintOptions.Portrait = true;
            ws.PrintOptions.HorizontalCentered = true;
            ExcToPdf.Save(path_pdf);
            if (HttpContext.Current != null)
                HttpContext.Current.Response.Redirect("pdf/reports/brak_pes.pdf");
        }
    }
}