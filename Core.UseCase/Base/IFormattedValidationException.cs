using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.UseCase.Base
{
    public interface IFormattedValidationException
    {
        string Message { get; }
    }
}
