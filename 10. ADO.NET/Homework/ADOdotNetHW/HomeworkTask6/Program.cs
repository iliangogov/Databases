using System;
using System.Data;
using System.Data.OleDb;

namespace HomeworkTask6
{
    class Program
    {
        //Create an Excel file with 2 columns: name and score:

        public static void Main()
        {
            ReadExcelData();
        }

        private static void ReadExcelData()
        {
            using (var excelConnection = new OleDbConnection(Properties.Settings.Default.excelConnection))
            {
                excelConnection.Open();
                string sheetName = GetSheetName(excelConnection);
                OleDbCommand excelCommand = GetOleDbCommand(sheetName, excelConnection);

                using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(excelCommand))
                {
                    DataSet dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet);

                    using (DataTableReader reader = dataSet.CreateDataReader())
                    {
                        while (reader.Read())
                        {
                            var fullName = reader["Name"];
                            var score = reader["Score"];

                            Console.WriteLine(fullName + " -> " + score);
                        }
                    }
                }
            }
        }

        private static string GetSheetName(OleDbConnection oleDbConnection)
        {
            DataTable excelSchema = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();
            return sheetName;
        }

        private static OleDbCommand GetOleDbCommand(string sheetName, OleDbConnection excelConnection)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(@"SELECT *
                                                           FROM [" + sheetName + "]", excelConnection);
            return oleDbCommand;
        }
    }
}

