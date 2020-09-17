using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using SirccELC.Core.UseCase;

namespace Core.UseCase.Security.Auditoria
{
    public class ConfiguracionAuditoriaInteractor : IRequestHandler<ConfiguracionAuditoriaRequest, ConfiguracionAuditoriaResponse>
    {
        private readonly IAuditLogRepository _repositorio;

        public ConfiguracionAuditoriaInteractor(IAuditLogRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<ConfiguracionAuditoriaResponse> Handle(ConfiguracionAuditoriaRequest request, CancellationToken cancellationToken)
        {
            List<string> tableName = _repositorio.GetNameTable();
            List<string> interactors= _repositorio.GetNameInteractor();
            List<string> modules = _repositorio.GetNameModules();
            return Task.FromResult(new ConfiguracionAuditoriaResponse(tableName,interactors,modules));
        }
    }
    public class ConfiguracionAuditoriaRequest : IRequest<ConfiguracionAuditoriaResponse>
    {

    }
    public class ConfiguracionAuditoriaResponse
    {
        public List<string> TableName { get; }
        public List<string> Modulos { get; }
        public List<string> Acciones { get; }

        public ConfiguracionAuditoriaResponse(List<string> _tableName, List<string> _interactors, List<string> _modules)
        {
            Acciones = _interactors;
            Modulos = _modules;
            TableName = _tableName;
        }
    }
}
