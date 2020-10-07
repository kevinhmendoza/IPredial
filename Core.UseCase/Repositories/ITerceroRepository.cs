using Core.Entities.General;
using Core.UseCase.Base;
using System.Collections.Generic;

namespace Core.UseCase
{
    public interface ITerceroRepository : IGenericRepository<Tercero>
    {
        List<Tercero> GetTercerosJuridicos();
    }
}