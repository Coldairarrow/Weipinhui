using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository
    {
        #region 数据库连接相关方法
        DbContext GetDbContext();

        #endregion

        #region 事物提交

        void BeginTransaction();
        void EndTransaction();

        #endregion

        #region 增加数据

        void Insert<T>(T entity) where T : class, new();

        void Insert<T>(List<T> entities) where T : class, new();

        #endregion

        #region 删除数据

        void Delete<T>(T entity) where T : class, new();

        void Delete<T>(List<T> entities) where T : class, new();

        /// <summary>
        /// 通过条件删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">条件</param>
        void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new();

        #endregion

        #region 更新数据

        void Update<T>(T entity) where T : class, new();

        void Update<T>(List<T> entities) where T : class, new();

        #endregion

        #region 查询数据

        T GetEntity<T>(string keyValue) where T : class, new();

        List<T> GetList<T>() where T : class, new();

        IQueryable<T> GetTable<T>() where T : class, new();

        DataTable GetDataTableWithSql(string sql);

        DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters);

        List<T> GetListBySql<T>(string sqlStr);

        List<T> GetListBySql<T>(string sqlStr, List<DbParameter> param);
        #endregion

        #region 执行Sql语句

        void ExcuteBySql(string sql);

        void ExcuteBySql(string sql, List<DbParameter> spList);

        #endregion
    }
}
