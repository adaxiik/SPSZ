using System.Collections.Generic;
using System.Data;
using System;
using SPSZDataLayer.GlobalConfig;
using System.IO;

namespace SPSZDataLayer.TableGateway.Csv
{
    public class CsvUtils
    {
        public static DataTable LoadTable(string tableName)
        {
            string filename = tableName + ".csv";
            string path = Path.Combine(Config.CsvDBFolder, filename);
            DataTable table = new DataTable(tableName);
            if (!File.Exists(path))
            {
                throw new Exception("File not found: " + path);
            }
            string[] lines = File.ReadAllLines(path);
            string[] headers = lines[0].Split(Config.CsvDelimiter);

            foreach (string header in headers)
                table.Columns.Add(header);
            
            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(Config.CsvDelimiter);
                DataRow row = table.NewRow();
                for (int j = 0; j < fields.Length; j++)
                    row[j] = fields[j];
                table.Rows.Add(row);
            }

            return table;

        }

        public static void SaveTable(DataTable table)
        {
            string filename = table.TableName + ".csv";
            string path = Path.Combine(Config.CsvDBFolder, filename);

            string[] lines = new string[table.Rows.Count + 1];
            string[] headers = new string[table.Columns.Count];

            for (int i = 0; i < table.Columns.Count; i++)
                headers[i] = table.Columns[i].ColumnName;

            lines[0] = string.Join(Config.CsvDelimiter, headers);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string[] fields = new string[table.Columns.Count];

                for (int j = 0; j < table.Columns.Count; j++)
                    fields[j] = table.Rows[i][j].ToString();

                lines[i + 1] = string.Join(Config.CsvDelimiter, fields);
            }
            File.WriteAllLines(path, lines);
        }

        public static int LastRowId(DataTable table)
        {
            if (table.Rows.Count == 0)
                return 0;
            return Convert.ToInt32(table.Rows[table.Rows.Count - 1]["id"].ToString());
        }
        public static int NextRowId(DataTable table) => LastRowId(table) + 1;

        public static List<DataRow> GetAll(string tableName)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                rows.Add(row);
            return rows;
        }

        public static List<DataRow> GetAllByType(string tableName, string type)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                if (row["type"].ToString() == type)
                    rows.Add(row);
            return rows;
        }


        public static DataRow GetById(int id, string tableName)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString())
                    return row;
            return null;
        }

        public static DataRow GetByIdAndType(int id, string tableName, string type)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString() && row["type"].ToString() == type)
                    return row;
            return null;
        }

        public static int Insert(DataRow row, string tableName)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            DataTable temp = row.Table;     
            
            if (!temp.Columns.Contains("id"))
                throw new Exception("Column name id does not exist");

            int id = CsvUtils.NextRowId(table);
            var newr = table.NewRow();

            foreach (DataColumn column in table.Columns)
                if(temp.Columns.Contains(column.ColumnName))
                    newr[column.ColumnName] = row[column.ColumnName];
            
            newr["id"] = id;
            table.Rows.Add(newr);
            CsvUtils.SaveTable(table);
            return id;
        }

        public static int DeleteById(int id, string tableName)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString())
                {
                    table.Rows.Remove(row);
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public static int DeleteByIdAndType(int id, string tableName, string type)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            foreach (DataRow row in table.Rows)
                if (row["id"].ToString() == id.ToString() && row["type"].ToString() == type)
                {
                    table.Rows.Remove(row);
                    CsvUtils.SaveTable(table);
                    return 1;
                }
            return 0;
        }

        public static void DeleteAll(string tableName)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            table.Rows.Clear();
            CsvUtils.SaveTable(table);
        }

        public static void DeleteAllByType(string tableName, string type)
        {
            DataTable table = CsvUtils.LoadTable(tableName);
            foreach (DataRow row in table.Rows)
                if (row["type"].ToString() == type)
                    table.Rows.Remove(row);
            CsvUtils.SaveTable(table);
        }

    }
}