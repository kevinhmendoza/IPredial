
using Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Application.WebApi.HelperClasses.Filters
{
    public class ByAFormattedExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ByAFormattedException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "Error Persitiendo los Datos"
                };
                throw new HttpResponseException(resp);
            }
            else 
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "Error no esperado"
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}