using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Util;
using System.Configuration;

namespace Data.Repository
{
    public class BaseRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param">构造参数，可以为数据库连接字符串或者DbContext</param>
        public BaseRepository(Object param,string dbType)
        {
            BuildParam = param;
            _dbType = DbFactory.GetDbType(dbType);
            _db = DbFactory.GetDbContext(BuildParam);
            IsDisposed = false;
        }

        #endregion

        #region 拥有属性

        /// <summary>
        /// 数据库类型
        /// </summary>
        private string _dbType { get; set; }

        /// <summary>
        /// 连接上下文DbContext
        /// </summary>
        private DbContext _db { get; set; }

        /// <summary>
        /// 建造DbConText所需参数
        /// </summary>
        private Object BuildParam { get; set; }

        /// <summary>
        /// 标记是否已经释放
        /// </summary>
        private bool IsDisposed { get; set; }

        /// <summary>
        /// 判断是否开始事物
        /// </summary>
        private DbContextTransaction Transaction { get; set; }

        protected DbContext db
        {
            get
            {
                if (IsDisposed)
                {
                    _db = DbFactory.GetDbContext(BuildParam);
                    IsDisposed = false;
                }

                return _db;
            }
            set
            {
                _db = value;
            }
        }


        #endregion

        #region 提交及事物 
        /// <summary>
        /// 提交到数据库
        /// </summary>
        protected void Submit()
        {
            db.SaveChanges();
            if(Transaction==null)
            {
                db.Dispose();
                IsDisposed = true;
            }
        }

        /// <summary>
        /// 开始事物提交
        /// </summary>
        public void BeginTransaction()
        {
            Transaction = db.Database.BeginTransaction();
        }

        /// <summary>
        /// 结束事物提交
        /// </summary>
        public void EndTransaction()
        {
            Transaction.Commit();
            db.Dispose();
            IsDisposed = true;
        }

        #endregion

        #region 数据库连接相关方法

        /// <summary>
        /// 获取DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return db;
        }

        #endregion

        #region 增加数据

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Insert<T>(T entity) where T : class, new()
        {
            db.Set<T>().Add(entity);
            Submit();
        }

        /// <summary>
        /// 插入数据列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        public void Insert<T>(List<T> entities) where T : class, new()
        {
            db.Set<T>().AddRange(entities);
            Submit();
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Delete<T>(T entity) where T : class, new()
        {
            db.Set<T>().Attach(entity);
            db.Set<T>().Remove(entity);
            Submit();
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Delete<T>(List<T> entities) where T : class, new()
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Attach(entity);
                db.Set<T>().Remove(entity);
            }
            Submit();
        }

        /// <summary>
        /// 通过条件删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">条件</param>
        public void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            List<T> entities = db.Set<T>().Where(condition).ToList();
            if (entities.Count != 0)
                Delete<T>(entities);
        }
        #endregion

        #region 更新数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Update<T>(T entity) where T : class, new()
        {
            db.Set<T>().Attach(entity);
            foreach (var propery in typeof(T).GetProperties())
            {
                var value = db.Entry(entity).Property(propery.Name).CurrentValue;
                if (value != null)
                    db.Entry(entity).Property(propery.Name).IsModified = true;
            }

            Submit();
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Update<T>(List<T> entities) where T : class, new()
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Attach(entity);
                foreach (var propery in typeof(T).GetProperties())
                {
                    var value = db.Entry(entity).Property(propery.Name).CurrentValue;
                    if (value != null)
                        db.Entry(entity).Property(propery.Name).IsModified = true;
                }
            }

            Submit();
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetEntity<T>(string keyValue) where T : class, new()
        {
            return db.Set<T>().Find(keyValue);
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public List<T> GetList<T>() where T : class, new()
        {
            return db.Set<T>().ToList();
        }

        /// <summary>
        /// 获取实体对应的表，延迟加载，主要用于支持Linq查询操作
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public IQueryable<T> GetTable<T>() where T : class, new()
        {
            return db.Set<T>();
        }

        /// <summary>
        /// 通过Sql语句获取DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql)
        {
            DbConnection conn = DbFactory.GetDbConnection(_dbType);
            conn.ConnectionString = db.Database.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            DbCommand cmd = DbFactory.GetDbCommand(_dbType);
            cmd.Connection = conn;
            cmd.CommandText = sql;

            DataAdapter adapter = DbFactory.GetDataAdapter(_dbType, cmd);
            DataSet table = new DataSet();
            adapter.Fill(table);

            conn.Close();//连接需要关闭
            conn.Dispose();
            return table.Tables[0];
        }

        /// <summary>
        /// 通过Sql参数查询返回DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            DbConnection conn = DbFactory.GetDbConnection(_dbType);
            conn.ConnectionString = db.Database.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DbCommand cmd = DbFactory.GetDbCommand(_dbType);
            cmd.Connection = conn;
            cmd.CommandText = sql;

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }

            DataAdapter adapter = DbFactory.GetDataAdapter(_dbType, cmd);
            DataSet table = new DataSet();
            adapter.Fill(table);
            cmd.Parameters.Clear();
            conn.Close();//连接需要关闭
            conn.Dispose();
            return table.Tables[0];
        }

        /// <summary>
        /// 通过sql返回List
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public List<T> GetListBySql<T>(string sqlStr)
        {
            return GetDataTableWithSql(sqlStr).DataTableToList<T>();
        }

        /// <summary>
        /// 通过sql返回list
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<T> GetListBySql<T>(string sqlStr, List<DbParameter> param)
        {
            return GetDataTableWithSql(sqlStr, param).DataTableToList<T>();
        }


        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExcuteBySql(string sql)
        {
            db.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 通过参数执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExcuteBySql(string sql, List<DbParameter> spList)
        {
            db.Database.ExecuteSqlCommand(sql, spList);
        }

        #endregion

    }
}
