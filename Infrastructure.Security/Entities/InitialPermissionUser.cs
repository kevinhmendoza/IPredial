using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Infrastructure.Security.Entities
{
    public class InitialPermissionUser
    {
        public int Id { get; set; }
        public string Permission { get; set; }
        public string UserId { get; set; }
        public string State { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
