namespace Infrastructure.Security.Migrations
{
    using global::System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Security.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Infrastructure.Security.ApplicationDbContext";
        }


        protected override void Seed(Infrastructure.Security.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
