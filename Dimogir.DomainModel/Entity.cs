using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimogir.DomainModel
{
    public class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
