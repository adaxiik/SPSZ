using System;
using System.Collections.Generic;
using System.Data;
using SPSZDataLayer.TableGateway.Interface;

namespace SPSZDataLayer.TableGateway.Stub
{
    public class SubjectStubTG : ISubjectTG
    {
        public string TableName = "Subject";

        public List<DataRow> GetAll()
        {
            var t = new DataTable();
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("name", typeof(string));
            t.Columns.Add("description", typeof(string));
            t.Columns.Add("label", typeof(string));
            t.Rows.Add(1, "Matematyka", "Matematyka", "Matematyka");
            t.Rows.Add(2, "Meth", "Meth", "Meth");
            
            var list = new List<DataRow>();
            foreach (DataRow r in t.Rows)
                list.Add(r);

            return list;
        }

        public DataRow GetById(int id) 
        {
            var t = new DataTable();
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("name", typeof(string));
            t.Columns.Add("description", typeof(string));
            t.Columns.Add("label", typeof(string));
            t.Rows.Add(1, "Matematyka", "Matematyka", "Matematyka");
            t.Rows.Add(2, "Meth", "Meth", "Meth");
            
            foreach (DataRow r in t.Rows)
                if (r["id"].ToString() == id.ToString())
                    return r;

            return null;
        }

        public int Insert(DataRow row) 
        {
            return 1;
        }

        public int Update(DataRow row)
        {
            return 1;
        }

        public int DeleteById(int id)
        {
            return 1;
        }

        public void DeleteAll() {}
        
    }
}