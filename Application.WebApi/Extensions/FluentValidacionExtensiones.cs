using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Application.WebApi.Extensions
{
    public static class FluentValidacionExtensiones
    {
        /// <summary>
        /// Concatena cada resultado en una Retorna una cadena de carácteres
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToText(this ValidationResult validationResult)
        {
            switch (validationResult.IsValid)
            {
                case true:
                    return "Ok";
                case false:
                    StringBuilder sb = new StringBuilder();
                    //sb.Append("Error:\n");
                    foreach (var error in validationResult.Errors)
                    {
                        sb.Append($"{error.ErrorMessage}\n");
                    }
                    string Error = sb.ToString();
                    return Error;
            }
            return "";


        }

        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}