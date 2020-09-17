using Autofac;
using Core.UseCase.Base;
using Infrastructure.Audit;
using Infrastructure.Audit.Base;
using Infrastructure.Data;
using Infrastructure.Data.Base;

namespace Application.WebApi.Modules
{
    public class EfModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType(typeof(CleanArchitectureContext)).As(typeof(IDbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(AuditLogContext)).As(typeof(IDbContextAudit)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(GenericRepositoryAuditLog<>)).As(typeof(IGenericRepositoryQuery<>)).InstancePerRequest();
        }
    }
}