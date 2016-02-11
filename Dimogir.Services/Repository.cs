using System;
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

        IDbSet<TEntity> Get();
    }

    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable
        where TEntity : Entity<TKey>
    {

        protected DimogirContext DbContext;
        private IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TEntity[] GetAll()
        {
            return Get().ToArray();
        }

        public TEntity Load(TKey key)
        {
            return Get().Find(key);
        }

        public void Create(TEntity entity)
        {
            _unitOfWork.Set<TEntity>().Add(entity);
        }

        public IDbSet<TEntity> Get()
        {
            return _unitOfWork.Set<TEntity>();
        }

        public void Dispose()
        {
            var a = 0;
        }
    }
}
