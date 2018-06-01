namespace Consultoresvs3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaseProyectoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreFase = c.String(),
                        DescripcionFase = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ReporteUsuarios", "IdFase", c => c.Int(nullable: false));
            CreateIndex("dbo.ReporteUsuarios", "IdFase");
            AddForeignKey("dbo.ReporteUsuarios", "IdFase", "dbo.FaseProyectoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReporteUsuarios", "IdFase", "dbo.FaseProyectoes");
            DropIndex("dbo.ReporteUsuarios", new[] { "IdFase" });
            DropColumn("dbo.ReporteUsuarios", "IdFase");
            DropTable("dbo.FaseProyectoes");
        }
    }
}
