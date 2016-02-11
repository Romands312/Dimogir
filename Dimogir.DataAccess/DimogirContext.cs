using System.Data.Entity;
using Dimogir.DomainModel;

namespace Dimogir.DataAccess
{
    public class DimogirContext : DbContext
    {
        public DimogirContext() : base("DBConnection")
        {
            Database.SetInitializer<DimogirContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
        }
    }
}
