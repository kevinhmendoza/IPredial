using Autofac;
using Core.Entities.Contracts;
using Core.UseCase.Contracts;
using Infrastructure.Security;
using Infrastructure.System;
using Infrastructure.System.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.WebApi.Modules
{
    public class ContractsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(Logger)).As(typeof(ILogger)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(ByAMapper)).As(typeof(IMapper)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(Sistema)).As(typeof(ISistema)).InstancePerLifetimeScope();
        }
    }
}