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

    public static class EntityUtilities
    {
        public static TEntity Find<TEntity, TKey>(this IEnumerable<TEntity> table, TKey id) where TEntity : Entity<TKey>
        {
            return table.FirstOrDefault(e => e.Id.Equals(id));
        }

        public static TEntity Find<TEntity>(this IQueryable<TEntity> table, int id) where TEntity : Entity<int>
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static TEntity Find<TEntity>(this IQueryable<TEntity> table, string id) where TEntity : Entity<string>
        {
            return table.FirstOrDefault(e => e.Id == id);
        }
    }
}
