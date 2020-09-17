namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tercero",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoIdentificacion = c.String(),
                        Identificacion = c.String(nullable: false, maxLength: 20),
                        Nombres = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false),
                        Sexo = c.String(),
                        Telefono = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        CorreoElectronico = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IPAddress = c.String(maxLength: 20),
                        State = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Identificacion, t.State }, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tercero", new[] { "Identificacion", "State" });
            DropTable("dbo.Tercero");
        }
    }
}
