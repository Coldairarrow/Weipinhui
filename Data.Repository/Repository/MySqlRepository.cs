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
using MySql.Data.MySqlClient;
using Util;

namespace Data.Repository
{
    public class MySqlRepository : BaseRepository,IRepository
    {
        #region 构造

        public MySqlRepository(Object buildParam,string dbType)
            :base(buildParam,dbType)
        {
        }

        #endregion
    }
}
