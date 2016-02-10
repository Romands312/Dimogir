using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Dimogir.DataAccess;
using Dimogir.DomainModel;

namespace Dimogir.Services
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : Entity<TKey>
    {
        TEntity[] GetAll();
        TEntity Load(TKey key);

        void Create(TEntity entity);
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

        public void Create(TEntity entity)
        {
            Entities.Add(entity);
            DbContext.SaveChanges();
        }
    }
}
