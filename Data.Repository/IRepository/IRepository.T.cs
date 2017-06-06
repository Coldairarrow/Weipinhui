using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository<T> where T:class,new()
    {
        #region 事物提交

        void BeginTransaction();
        void EndTransaction();

        #endregion

        #region 增加数据

        void Insert(T entity);

        void Insert(List<T> entities);

        #endregion

        #region 删除数据

        void Delete(T entity);

        void Delete(List<T> entities);

        void Delete(Expression<Func<T, bool>> condition);

        #endregion

        #region 更新数据

        void Update(T entity);

        void Update(List<T> entities);

        #endregion

        #region 查询数据

        T GetEntity(string keyValue);

        List<T> GetList();

        IQueryable<T> GetTable();

        DataTable GetDataTableWithSql(string sql);

        DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters);

        List<U> GetListBySql<U>(string sqlStr);

        List<U> GetListBySql<U>(string sqlStr, List<DbParameter> param);

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        void ExcuteBySql(string sql);

        /// <summary>
        /// 通过参数执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        void ExcuteBySql(string sql, List<DbParameter> spList);

        #endregion

    }
}
