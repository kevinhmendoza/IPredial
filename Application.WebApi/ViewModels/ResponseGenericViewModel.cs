namespace Application.WebApi.ViewModels
{
    public class ResponseGenericViewModel
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }
}