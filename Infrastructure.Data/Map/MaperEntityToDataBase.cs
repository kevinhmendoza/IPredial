using System.Data.Entity;

namespace Infrastructure.Data.Map
{
    class MaperEntityToDataBase
    {
        protected MaperEntityToDataBase()
        {

        }
        internal static void MapearEntityToDataBase(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TerceroMap());
        }
    }
}
