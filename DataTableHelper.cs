using System;
using System.Data;

namespace Frikz.Web.Core
{
    public static class DataTableHelper
    {
        public static DataTable AddPrimaryKey(DataTable dt, int pk)
        {
            DataColumn PrimaryKeyColumn = new DataColumn("TEMP_ID", typeof(Int32));
            dt.Columns.Add(PrimaryKeyColumn);
            dt.Columns["TEMP_ID"].SetOrdinal(0);
            dt.Rows[0]["TEMP_ID"] = pk;
            dt.PrimaryKey = new DataColumn[] { PrimaryKeyColumn };
            dt.AcceptChanges();
            return dt;
        }
    }
}