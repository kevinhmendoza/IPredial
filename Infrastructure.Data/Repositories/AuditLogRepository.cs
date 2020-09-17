using Core.UseCase.Contracts;
using Infrastructure.Audit;
using Infrastructure.Audit.Base;
using Infrastructure.Data.Base;
using SirccELC.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace infrastructure.Data.Repositories
{
    public class AuditLogRepository : GenericRepositoryAuditLog<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(IDbContextAudit context)
              : base(context)
        {

        }

        public List<AuditLog> FindBy(IParamAuditLog param)
        {
            var query = this.AsQueryable();
            if (!string.IsNullOrEmpty(param.Interactor))
            {
                query = query.Where(x => x.Interactor == param.Interactor);
            }
            if (!string.IsNullOrEmpty(param.Modulo))
            {
                query = query.Where(x => x.Module == param.Modulo);
            }
            if (!string.IsNullOrEmpty(param.Tabla))
            {
                query = query.Where(x => x.TableName == param.Tabla);
            }
            if (!string.IsNullOrEmpty(param.Id))
            {
                query = query.Where(x => x.RecordID == param.Id);
            }
            if (!string.IsNullOrEmpty(param.Usuario))
            {
                query = query.Where(x => x.UserID.ToUpper().Contains(param.Usuario.ToUpper()));
            }
            if (param.FechaFinal.HasValue && param.FechaInicial.HasValue)
            {
                DateTime FechaFinal = new DateTime(param.FechaFinal.Value.Year, param.FechaFinal.Value.Month, param.FechaFinal.Value.Day, 23, 59, 59);
                DateTime FechaInicial = new DateTime(param.FechaInicial.Value.Year, param.FechaInicial.Value.Month, param.FechaInicial.Value.Day, 0, 0, 0);
                if(FechaFinal.Year > 1 && FechaInicial.Year > 1)
                {
                    query = query.Where(x => x.EventDateLocalTime >= FechaInicial && x.EventDateLocalTime <= FechaFinal);
                }
            }
            if (param.HoraInicial.HasValue && param.HoraFinal.HasValue)
            {
                TimeSpan HoraFinal = new TimeSpan(param.HoraFinal.Value.Hour, param.HoraFinal.Value.Minute, 0);
                TimeSpan HoraInicial = new TimeSpan(param.HoraInicial.Value.Hour, param.HoraInicial.Value.Minute, 59);
                query = query.Where(x => DbFunctions.CreateTime(x.EventDateLocalTime.Hour, x.EventDateLocalTime.Minute, x.EventDateLocalTime.Second) >= HoraInicial && DbFunctions.CreateTime(x.EventDateLocalTime.Hour, x.EventDateLocalTime.Minute, x.EventDateLocalTime.Second) <= HoraFinal);
            }
            return query.ToList();
        }

        public List<string> GetNameInteractor()
        {
            return this.AsQueryable().GroupBy(x => x.Interactor).Select(x => x.Key).ToList();
        }

        public List<string> GetNameModules()
        {
            return this.AsQueryable().GroupBy(x => x.Module).Select(x => x.Key).ToList();
        }

        public List<string> GetNameTable()
        {
            return this.AsQueryable().GroupBy(x => x.TableName).Select(x => x.Key).ToList();
        }
    }
}
