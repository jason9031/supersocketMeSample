using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.SocketClientEngine.Info
{
    public class CsvFileHelper
    {
        public static DataTable LoadCsvFile(string filePath, string newFeild = "")
        {
            DataTable table = new DataTable();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    bool flag = true;
                    string empty = string.Empty;
                    while ((empty = streamReader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(newFeild) && !flag)
                        {
                            empty += ",";
                        }
                        string[] array = empty.Split(',');
                        if (flag)
                        {
                            string[] array2 = array;
                            foreach (string st in array2)
                            {
                                CreateColumn(ref table, st);
                            }
                            if (!string.IsNullOrEmpty(newFeild))
                            {
                                CreateColumn(ref table, newFeild);
                            }
                            flag = false;
                        }
                        else
                        {
                            DataRow dataRow = table.NewRow();
                            for (int j = 0; j < array.Length; j++)
                            {
                                dataRow[j] = array[j];
                            }
                            table.Rows.Add(dataRow);
                        }
                    }
                    streamReader.Close();
                }
                fileStream.Close();
            }
            return table;
        }

        public static DataTable LoadCsvFile(string filePath)
        {
            DataTable table = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    bool firstLine = true;
                    string line = string.Empty;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var items = line.Split(',');

                        if (firstLine == true)
                        {
                            foreach (string st in items)
                            {
                                DataColumn column = new DataColumn(st);
                                table.Columns.Add(column);
                            }
                            firstLine = false;
                        }
                        else
                        {
                            DataRow row = table.NewRow();
                            for (int i = 0; i < items.Length; i++)
                            {
                                row[i] = items[i];
                            }

                            table.Rows.Add(row);
                        }
                    }
                    reader.Close();
                }
                fs.Close();
            }
            return table;
        }

        public static bool SaveToCsvFile(string filePath, DataTable table)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }
            if (table == null)
            {
                return false;
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    string text = string.Empty;
                    foreach (DataColumn column in table.Columns)
                    {
                        text = text + column.Caption + ",";
                    }
                    streamWriter.WriteLine(text.TrimEnd(','));
                    foreach (DataRow row in table.Rows)
                    {
                        text = string.Empty;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            text = text + row[i] + ",";
                        }
                        streamWriter.WriteLine(text.Remove(text.LastIndexOf(',')));
                    }
                    streamWriter.Close();
                }
                fileStream.Close();
            }
            return true;
        }

        public static void CreateBlankCsv(string path, string content)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(content);
                    streamWriter.Close();
                }
                fileStream.Close();
            }
        }

        private static void CreateColumn(ref DataTable table, string st)
        {
            DataColumn column = new DataColumn(st);
            table.Columns.Add(column);
        }

        public static int GetCsvVersion(string filePath)
        {
            int result = -1;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string text = streamReader.ReadLine();
                    if (!string.IsNullOrEmpty(text))
                    {
                        result = text.Split(',').Length;
                    }
                    streamReader.Close();
                }
                fileStream.Close();
            }
            return result;
        }
    }
}
