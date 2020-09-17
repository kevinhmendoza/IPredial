using Infrastructure.Data.Base;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Infrastructure.Data.Map;
using Core.Entities.General;

namespace Infrastructure.Data
{
    public class CleanArchitectureContext : DbContextBase
    {
      
        public CleanArchitectureContext()
            : base("Name=CleanArchitectureContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CleanArchitectureContext, Migrations.Configuration>("CleanArchitectureContext"));
        }
        
        public virtual DbSet<Tercero> Terceros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            MaperEntityToDataBase.MapearEntityToDataBase(modelBuilder);
        }

    }
}
