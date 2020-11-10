using System.Collections.Generic;

namespace Domain
{
    public class ReciboPagoMultiple : ReciboPago
    {
        public List<int> VigenciasPagar { get; set; }
        protected override string GetPeriodos()
        {
            return string.Join(",", VigenciasPagar);
        }
    }
}
