using Infrastructure.Security.Entities;
using Infrastructure.Security.Maps;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infrastructure.Security
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public virtual DbSet<InitialPermissionUser> PermisosInicialesUsuarios{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new InitialPermissionUserMap());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
