using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimogir.DataAccess;
using Dimogir.DomainModel;

namespace Dimogir.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;

        void Commit();
    }

    public class UnitOfWork : DimogirContext, IUnitOfWork
    {
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void Commit()
        {
            SaveChanges();
        }

        void IDisposable.Dispose()
        {
            Commit();
        }
    }
}
