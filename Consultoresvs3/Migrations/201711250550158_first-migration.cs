namespace Consultoresvs3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NIT = c.Int(nullable: false),
                        NombreEmpresa = c.String(),
                        Direccion = c.String(),
                        CorreoEmpresa = c.String(),
                        ActividadEconomica = c.Int(nullable: false),
                        Telefono = c.Int(nullable: false),
                        NombreRepLegal = c.String(),
                        IdentificacionRepLegal = c.Int(nullable: false),
                        NombreRepSuplente = c.String(),
                        IdentificacionRepSuplente = c.Int(nullable: false),
                        NombreJuntaDir1 = c.String(),
                        IdentificacionJuntaDir1 = c.Int(nullable: false),
                        NombreJuntaDir2 = c.String(),
                        IdentificacionJuntaDir2 = c.Int(nullable: false),
                        NombreJuntaDir3 = c.String(),
                        IdentificacionJuntaDir3 = c.Int(nullable: false),
                        NombreJuntaDir4 = c.String(),
                        IdentificacionJuntaDir4 = c.Int(nullable: false),
                        NombreJuntaDir5 = c.String(),
                        IdentificacionJuntaDir5 = c.Int(nullable: false),
                        NombreJuntaDir6 = c.String(),
                        IdentificacionJuntaDir6 = c.Int(nullable: false),
                        NombreJuntaDir7 = c.String(),
                        IdentificacionJuntaDir7 = c.Int(nullable: false),
                        NombreJuntaDir8 = c.String(),
                        IdentificacionJuntaDir8 = c.Int(nullable: false),
                        NombreJuntaDir9 = c.String(),
                        IdentificacionJuntaDir9 = c.Int(nullable: false),
                        NombreJuntaDir10 = c.String(),
                        IdentificacionJuntaDir10 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadoProyectoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Proyectoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TiempoEstipulado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HorasTrabajdas = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdEmpresa = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        IdEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.IdEmpresa, cascadeDelete: true)
                .ForeignKey("dbo.EstadoProyectoes", t => t.IdEstado, cascadeDelete: true)
                .Index(t => t.IdEmpresa)
                .Index(t => t.IdEstado);
            
            CreateTable(
                "dbo.ReporteProyectoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdProyecto = c.Int(nullable: false),
                        HorasInvertidas = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Utilidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proyectoes", t => t.IdProyecto, cascadeDelete: true)
                .Index(t => t.IdProyecto);
            
            CreateTable(
                "dbo.ReporteUsuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaReporte = c.DateTime(nullable: false),
                        HTrabajadas = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdServicio = c.Int(nullable: false),
                        IdProyecto = c.Int(nullable: false),
                        IdUsuario = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proyectoes", t => t.IdProyecto, cascadeDelete: true)
                .ForeignKey("dbo.Servicios", t => t.IdServicio, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUsuario)
                .Index(t => t.IdServicio)
                .Index(t => t.IdProyecto)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Servicios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Identificacion = c.Int(nullable: false),
                        FechaIngresoEmpresa = c.String(),
                        FechaNacimiento = c.String(nullable: false),
                        Cargo = c.String(nullable: false),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorHoraPrestacionesSociales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorHoraNoPrestacionSociales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReporteUsuarios", "IdUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReporteUsuarios", "IdServicio", "dbo.Servicios");
            DropForeignKey("dbo.ReporteUsuarios", "IdProyecto", "dbo.Proyectoes");
            DropForeignKey("dbo.ReporteProyectoes", "IdProyecto", "dbo.Proyectoes");
            DropForeignKey("dbo.Proyectoes", "IdEstado", "dbo.EstadoProyectoes");
            DropForeignKey("dbo.Proyectoes", "IdEmpresa", "dbo.Empresas");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ReporteUsuarios", new[] { "IdUsuario" });
            DropIndex("dbo.ReporteUsuarios", new[] { "IdProyecto" });
            DropIndex("dbo.ReporteUsuarios", new[] { "IdServicio" });
            DropIndex("dbo.ReporteProyectoes", new[] { "IdProyecto" });
            DropIndex("dbo.Proyectoes", new[] { "IdEstado" });
            DropIndex("dbo.Proyectoes", new[] { "IdEmpresa" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Servicios");
            DropTable("dbo.ReporteUsuarios");
            DropTable("dbo.ReporteProyectoes");
            DropTable("dbo.Proyectoes");
            DropTable("dbo.EstadoProyectoes");
            DropTable("dbo.Empresas");
        }
    }
}
