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
    public interface ICategoryService : IRepository<Category, string>
    {
        
    }

    public class CategoryService : Repository<Category, string>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
