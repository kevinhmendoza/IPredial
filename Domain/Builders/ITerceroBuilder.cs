namespace Domain.Builders
{
    public interface ITerceroBuilder
    {
        Tercero Build();
        TerceroBuilder SetApellidos(string Apellidos);
        TerceroBuilder SetCorreoElectronico(string CorreoElectronico);
        TerceroBuilder SetDireccion(string Direccion);
        TerceroBuilder SetIdentificacion(string Identificacion);
        TerceroBuilder SetNombres(string Nombres);
        TerceroBuilder SetRazonSocial(string RazonSocial);
        TerceroBuilder SetSexo(string Sexo);
        TerceroBuilder SetTelefono(string Telefono);
        TerceroBuilder SetTipoIdentificacion(string TipoIdentificacion);
    }
}