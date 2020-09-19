namespace Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Data.CleanArchitectureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Infrastructure.Data.IPredialContext";
        }
        protected override void Seed(Infrastructure.Data.CleanArchitectureContext context)
        {
            //Seeders
        }
    }
    
}
