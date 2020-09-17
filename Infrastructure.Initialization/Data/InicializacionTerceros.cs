using Core.Entities.General;
using Infrastructure.Data;
using Infrastructure.System;
using Infrastructure.System.Logging;
using System.Collections.Generic;

namespace Infrastructure.Initialization.Data
{
    public class InicializacionTerceros
    {
        private Logger _logger;
        public void Seed(CleanArchitectureContext ctx , Logger logger)
        {
            _logger = logger;
            _logger.Info("**************************************Inicializando Terceros**************************************");
            List<Tercero> _terceros = new List<Tercero>()
            {
                UsuarioAdmin()
            };
            ctx.Terceros.AddRange(_terceros);
        }

        private Tercero UsuarioAdmin()
        {
            _logger.Info("Se guardo el tercero del usuario admin");
            return new Tercero()
            {
                Nombres = "Anibal Jose",
                Apellidos = "Guerra Zapata",
                CreatedBy = "admin",
                CreatedDate = ByADateTime.Now,
                Identificacion = "admin",
                Direccion = "Carrera 13 # 36 - 111 12 Octubre",
                Sexo = "M",
                Telefono = "3186875411",
                TipoIdentificacion = "admin",
                UpdatedBy = "admin",
                UpdatedDate = ByADateTime.Now,
                IPAddress = "admin"
            };
        }
    }
}
