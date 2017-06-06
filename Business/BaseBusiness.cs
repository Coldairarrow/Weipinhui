using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
namespace Business
{
    public class BaseBusiness<T>:IRepository<T> where T:class,new()
    {
        public IRepository Service { get; set; }

        #region 构造函数
        public BaseBusiness()
        {
            Service = DbFactory.GetRepository(null,null);
        }
        
        public BaseBusiness(Object obj)
        {
            Service = DbFactory.GetRepository(obj,null);
        }

        public BaseBusiness(Object obj,string dbType)
        {
            Service = DbFactory.GetRepository(obj, dbType);
        }

        #endregion

        #region 事物提交

        /// <summary>
        /// 开始事物提交
        /// </summary>
        public void BeginTransaction()
        {
            Service.BeginTransaction();
        }

        /// <summary>
        /// 结束事物提交
        /// </summary>
        public void EndTransaction()
        {
            Service.EndTransaction();
        }


        #endregion

        #region 增加数据

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Insert(T entity)
        {
            Service.Insert<T>(entity);
        }

        /// <summary>
        /// 插入数据列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        public void Insert(List<T> entities)
        {
            Service.Insert(entities);
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Delete(T entity)
        {
            Service.Delete(entity);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Delete(List<T> entities)
        {
            Service.Delete(entities);
        }

        /// <summary>
        /// 通过条件删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">条件</param>
        public void Delete(Expression<Func<T, bool>> condition)
        {
            Service.Delete(condition);
        }
        #endregion

        #region 更新数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Update(T entity)
        {
            Service.Update(entity);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Update(List<T> entities)
        {
            Service.Update(entities);
        }
        

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetEntity(string keyValue)
        {
            return Service.GetEntity<T>(keyValue);
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public List<T> GetList()
        {
            return Service.GetList<T>();
        }

        /// <summary>
        /// 获取实体对应的表，延迟加载，主要用于支持Linq查询操作
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public IQueryable<T> GetTable()
        {
            return Service.GetTable<T>();
        }

        /// <summary>
        /// 通过Sql查询返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql)
        {
            return Service.GetDataTableWithSql(sql);
        }

        /// <summary>
        /// 通过Sql参数查询返回DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            return Service.GetDataTableWithSql(sql, parameters);
        }


        /// <summary>
        /// 通过sql返回List
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public List<U> GetListBySql<U>(string sqlStr)
        {
            return Service.GetListBySql<U>(sqlStr);
        }

        /// <summary>
        /// 通过sql返回list
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<U> GetListBySql<U>(string sqlStr, List<DbParameter> param)
        {
            return Service.GetListBySql<U>(sqlStr, param);
        }

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExcuteBySql(string sql)
        {
            Service.ExcuteBySql(sql);
        }

        /// <summary>
        /// 通过参数执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExcuteBySql(string sql, List<DbParameter> parameters)
        {
            Service.ExcuteBySql(sql, parameters);
        }

        #endregion

    }
}
