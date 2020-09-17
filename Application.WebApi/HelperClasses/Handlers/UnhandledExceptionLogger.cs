using Infrastructure.System.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Application.WebApi.HelperClasses.Handlers
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var log = context.Exception.ToString();
            var logger = new Logger();
            logger.Error(log,context.Exception);
        }
    }
}