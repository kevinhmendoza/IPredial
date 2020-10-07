using Core.Entities.Enumerations.General;
using Core.Entities.General;
using Core.UseCase;
using Infrastructure.Data.Base;
using System.Collections.Generic;
using System.Linq;

namespace infrastructure.Data.Repositories
{
    public class TerceroRepository : GenericRepository<Tercero>, ITerceroRepository
    {
        public TerceroRepository(IDbContext context) : base(context)
        {
        }

        public List<Tercero> GetTercerosJuridicos()
        {
            return _dbset.Where(t => t.TipoPersona == TipoPersonaEnumeration.Juridica.Value).ToList();
        }
    }
}
