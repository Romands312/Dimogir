using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Dimogir.DataAccess;

namespace Dimogir.Services
{
    public interface IRepository<out TEntity>
    {
        TEntity[] GetAll();
    }

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> Entities { get; set; }

        protected DimogirContext DbContext;

        public Repository()
        {
            DbContext = new DimogirContext();
        }

        public TEntity[] GetAll()
        {
            return Entities.ToArray();
        }
    }
}
