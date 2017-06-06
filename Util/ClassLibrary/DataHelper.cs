using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Util
{
    class DataHelper
    {
        #region 执行sql语句返回datatable

        public DataTable GetDataTable(Database database, string sql)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = database.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            conn.Close();//连接需要关闭
            conn.Dispose();
            return table;
        }

        public DataTable GetDataTable(Database database, string sql, DbParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = database.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        #endregion
    }
}