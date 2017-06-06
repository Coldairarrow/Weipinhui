using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BaseDbContext:DbContext
    {
        public BaseDbContext(string nameOfDb)
            :base(nameOfDb)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var assembly = this.GetType().Assembly;
            var entityTypes = from type in assembly.GetTypes()
                              where type.GetCustomAttribute<TableAttribute>() != null
                              select type;

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            foreach (var type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, null);
            }
        }
    }
}
