using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimogir.DataAccess
{
    public class DimogirContextFactory : IDbContextFactory<DimogirContext>
    {
        public DimogirContext Create()
        {
            return new DimogirContext();
        }
    }
}
