using Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Application.WebApi.HelperClasses.Handlers
{
    //https://www.exceptionnotfound.net/the-asp-net-web-api-exception-handling-pipeline-a-guided-tour/
    
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ByAFormattedException)
            {
                var result = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    
                    Content = new StringContent("Error de Base de Datos: "+ context.Exception.Message+ "Oops! Sorry! Something went wrong." +
                                                "Please contact anibaljoseguerraz@gmail.com so we can try to fix it."
                                                ),
                    ReasonPhrase = "Se ha presentando un inconveniente al acceder a la base de datos"
                };

                context.Result = new ByAFormattedResult(result);
            }
            else
            {
                // Handle other exceptions, do other things
                var result = new HttpResponseMessage()
                {
                    StatusCode = context.Exception is SecurityException ? HttpStatusCode.Unauthorized : HttpStatusCode.InternalServerError,
                    Content = new StringContent("Error de Aplicación: " + context.Exception.Message),
                    ReasonPhrase = "Se ha presentado un inconveniente al procesar la solicitud"
                };
                context.Result = new ByAFormattedResult(result);
            }
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        public class ByAFormattedResult : IHttpActionResult
        {

            private readonly HttpResponseMessage _httpResponseMessage;


            public ByAFormattedResult(HttpResponseMessage httpResponseMessage)
            {
                _httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_httpResponseMessage);
            }
        }
    }
}