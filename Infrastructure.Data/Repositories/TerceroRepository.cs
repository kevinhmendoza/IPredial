using Core.Entities.General;
using Core.UseCase;
using Infrastructure.Data.Base;

namespace infrastructure.Data.Repositories
{
    public class TerceroRepository : GenericRepository<Tercero>, ITerceroRepository
    {
        public TerceroRepository(IDbContext context) : base(context)
        {
        }
    }
}
