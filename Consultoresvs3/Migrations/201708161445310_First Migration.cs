namespace Consultoresvs3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Apellido", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Identificacion", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FechaIngresoEmpresa", c => c.String());
            AddColumn("dbo.AspNetUsers", "FechaNacimiento", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Cargo", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Salario", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "ValorHoraPrestacionesSociales", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "ValorHoraNoPrestacionSociales", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ValorHoraNoPrestacionSociales");
            DropColumn("dbo.AspNetUsers", "ValorHoraPrestacionesSociales");
            DropColumn("dbo.AspNetUsers", "Salario");
            DropColumn("dbo.AspNetUsers", "Cargo");
            DropColumn("dbo.AspNetUsers", "FechaNacimiento");
            DropColumn("dbo.AspNetUsers", "FechaIngresoEmpresa");
            DropColumn("dbo.AspNetUsers", "Identificacion");
            DropColumn("dbo.AspNetUsers", "Apellido");
            DropColumn("dbo.AspNetUsers", "Nombre");
        }
    }
}
