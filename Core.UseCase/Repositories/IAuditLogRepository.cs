using Core.UseCase.Base;
using System.Collections.Generic;
using Core.UseCase.Contracts;
using Infrastructure.Audit;

namespace SirccELC.Core.UseCase
{
    public interface IAuditLogRepository : IGenericRepositoryQuery<AuditLog>
    {
        List<AuditLog> FindBy(IParamAuditLog param);
        List<string> GetNameTable();
        List<string> GetNameInteractor();
        List<string> GetNameModules();
    }
}