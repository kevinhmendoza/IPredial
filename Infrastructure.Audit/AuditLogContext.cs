using Infrastructure.Audit.Base;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infrastructure.Audit
{
    public class AuditLogContext : DbContextBaseAudit
    {

        public AuditLogContext()
            : base("Name=CleanArchitectureContextAuditLog")
        {
            
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
