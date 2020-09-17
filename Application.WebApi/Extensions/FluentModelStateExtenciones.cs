using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.ModelBinding;
namespace Application.WebApi.Extensions
{
    public static class FluentModelStateExtenciones
    {
        /// <summary>
        /// Concatena cada resultado en una Retorna una cadena de carácteres
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToText(this ModelStateDictionary modelState)
        {
            switch (modelState.IsValid)
            {
                case true:
                    return "Ok";
                case false:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Error:<br/>");

                    List<ModelErrorCollection> errors = modelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();

                    foreach (ModelErrorCollection error in errors)
                    {
                        List<string> erroresCampo = error.Select(t => t.ErrorMessage).ToList();
                        erroresCampo.ForEach(t => sb.Append(t + "<br/>"));
                    }

                    string Error = sb.ToString();
                    return Error;
            }
            return "";
        }
    }
}