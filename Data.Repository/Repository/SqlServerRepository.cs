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
using System.Reflection;
using Util;

namespace Data.Repository
{
    public class SqlServerRepository : BaseRepository, IRepository
    {
        #region 构造
        public SqlServerRepository(Object buildParam,string dbType)
            : base(buildParam,dbType)
        {
        }
        #endregion
    }
}
