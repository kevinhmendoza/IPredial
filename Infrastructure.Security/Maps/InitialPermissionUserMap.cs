using Infrastructure.Security.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Security.Maps
{
    public class InitialPermissionUserMap:EntityTypeConfiguration<InitialPermissionUser>
    {
        public InitialPermissionUserMap()
        {
            HasKey(k => k.Id);
            HasRequired(u => u.User).WithMany(y => y.PermisosInitiales).HasForeignKey(y=>y.UserId);
        }
    }
}
