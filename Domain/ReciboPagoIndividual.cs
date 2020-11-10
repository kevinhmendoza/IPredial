using System;

namespace Domain
{
    public class ReciboPagoIndividual : ReciboPago
    {
        public int VigenciaPagar { get; set; }
        protected override string GetPeriodos()
        {
            return VigenciaPagar.ToString();
        }
    }
}
