namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarRestriccionesTercero : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tercero", "Nombres", c => c.String());
            AlterColumn("dbo.Tercero", "Apellidos", c => c.String());
            AlterColumn("dbo.Tercero", "Telefono", c => c.String());
            AlterColumn("dbo.Tercero", "Direccion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tercero", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Tercero", "Telefono", c => c.String(nullable: false));
            AlterColumn("dbo.Tercero", "Apellidos", c => c.String(nullable: false));
            AlterColumn("dbo.Tercero", "Nombres", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
