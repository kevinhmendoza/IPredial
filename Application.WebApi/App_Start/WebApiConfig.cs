using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Application.WebApi.HelperClasses.Handlers;

namespace Application.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
