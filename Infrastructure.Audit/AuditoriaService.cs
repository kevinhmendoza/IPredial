using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Audit
{
    public class AuditoriaService
    {
        private readonly AuditLogContext _ctx;
        public AuditoriaService()
        {
            _ctx = new AuditLogContext();
        }
        public void GuardarAuditorias(List<AuditLog> Auditorias)
        {
            _ctx.AuditLogs.AddRange(Auditorias);
            _ctx.SaveChanges();
        }
    }
}
