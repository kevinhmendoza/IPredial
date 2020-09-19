namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCamposTercero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tercero", "RazonSocial", c => c.String());
            AddColumn("dbo.Tercero", "NombreCompleto", c => c.String());
            AddColumn("dbo.Tercero", "TipoPersona", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tercero", "TipoPersona");
            DropColumn("dbo.Tercero", "NombreCompleto");
            DropColumn("dbo.Tercero", "RazonSocial");
        }
    }
}
