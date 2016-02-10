using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Dimogir.DataAccess;
using Dimogir.DomainModel;

namespace Dimogir.Services
{
    public interface IRepository<out TEntity, in TKey>
    {
        TEntity[] GetAll();
        TEntity Load(TKey key);
    }

    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : Entity<TKey>
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

        public TEntity Load(TKey key)
        {
            return Entities.Find(key);
        }
    }
}
