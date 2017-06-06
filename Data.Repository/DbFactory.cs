using System;
using System.Configuration;
using System.Data.Entity;
using Entity;
using System.Data.SqlClient;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Data.Repository
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 根据配置文件获取数据库类型，并返回对应的工厂接口
        /// </summary>
        /// <param name="obj">初始化参数，可为连接字符串或者DbContext</param>
        /// <returns></returns>
        public static IRepository GetRepository(Object obj,string dbType)
        {
            string _dbType = GetDbType(dbType);
            return (IRepository)Activator.CreateInstance(Type.GetType("Data.Repository." + _dbType + "Repository"),obj,dbType);
        }

        /// <summary>
        /// 获取DbType
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static string GetDbType(string dbType)
        {
            string _dbType = null;
            if (dbType == null)
            {
                _dbType = ConfigurationManager.AppSettings["DbType"] == null ? "SqlServer" : ConfigurationManager.AppSettings["DbType"];
            }
            else
            {
                _dbType = dbType;
            }

            return _dbType;
        }

        /// <summary>
        /// 根据参数获取数据库的DbContext
        /// </summary>
        /// <param name="obj">初始化参数，可为连接字符串或者DbContext</param>
        /// <returns></returns>
        public static DbContext GetDbContext(Object obj)
        {
            if(obj==null)
            {
                return new WeipinhuiEntities();
            }
            else
            {
                Type type = obj.GetType();
                if (type.Name == "String")
                    return new BaseDbContext((string)obj);
                else if (type.BaseType.Name == "DbContext")
                    return (DbContext)obj;
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取DbConnection
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbConnection GetDbConnection(string dbType)
        {
            DbConnection dbconnection = null;
            switch (dbType)
            {
                case "SqlServer":dbconnection = (DbConnection)Activator.CreateInstance(typeof(SqlConnection)); break;
                case "MySql":dbconnection = (DbConnection)Activator.CreateInstance(typeof(MySqlConnection)); break;
                default: break;
            }

            return dbconnection;
        }

        /// <summary>
        /// 获取DbCommand
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbCommand GetDbCommand(string dbType)
        {
            DbCommand dbcommand = null;
            switch (dbType)
            {
                case "SqlServer": dbcommand = (DbCommand)Activator.CreateInstance(typeof(SqlCommand)); break;
                case "MySql": dbcommand = (DbCommand)Activator.CreateInstance(typeof(MySqlCommand)); break;
                default: break;
            }
            return dbcommand;
        }

        /// <summary>
        /// 获取DataAdapter
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DataAdapter GetDataAdapter(string dbType,DbCommand cmd)
        {
            DataAdapter dataadapter = null;
            switch (dbType)
            {
                case "SqlServer": dataadapter = (DataAdapter)Activator.CreateInstance(typeof(SqlDataAdapter),cmd); break;
                case "MySql": dataadapter = (DataAdapter)Activator.CreateInstance(typeof(MySqlDataAdapter),cmd); break;
                default: break;
            }
            return dataadapter;
        }
    }
}
