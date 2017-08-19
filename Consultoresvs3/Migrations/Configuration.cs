namespace Consultoresvs3.Migrations
{
    using Consultoresvs3.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Consultoresvs3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Consultoresvs3.Models.ApplicationDbContext";
        }

        protected override void Seed(Consultoresvs3.Models.ApplicationDbContext context)
        {
            context.Servicios.AddOrUpdate(
                a => a.Id,
                new Servicio { Id = 1, Nombre = "Ayuda", Descripcion = "ayuda al colaborador con todo" },
                new Servicio { Id = 2, Nombre = "Colabora", Descripcion = "realiza trabajo" },
                new Servicio { Id = 3, Nombre = "No se sabe", Descripcion = "no se sabe" }
            );
            context.EstadoProyectos.AddOrUpdate(
                a => a.Id,
                new EstadoProyecto { Id = 1, Nombre = "Ejecucion" },
                new EstadoProyecto { Id = 2, Nombre = "Pausado" },
                new EstadoProyecto { Id = 3, Nombre = "Finalizado" }

            );

            context.Empresas.AddOrUpdate(
                a => a.Id,
                new Empresa
                {
                    Id = 1,
                    NIT = 1234,
                    NombreEmpresa = "Exito",
                    Direccion = "Medellin",
                    CorreoEmpresa = "Exito123@gmail.com",
                    ActividadEconomica = 1000,
                    Telefono = 5550022,
                    NombreRepLegal = "Pepito",
                    IdentificacionRepLegal = 1020,
                    NombreRepSuplente = "Juan",
                    IdentificacionRepSuplente = 1110,
                    NombreJuntaDir1 = "junta 1",
                    IdentificacionJuntaDir1 = 1,
                    NombreJuntaDir2 = "Junta 2",
                    IdentificacionJuntaDir2 = 2,
                    NombreJuntaDir3 = "Junta 3",
                    IdentificacionJuntaDir3 = 3
                },
                new Empresa
                {
                    Id = 2,
                    NIT = 1234,
                    NombreEmpresa = "Flamingo",
                    Direccion = "Medellin-Rionegro",
                    CorreoEmpresa = "Flamingo123@gmail.com",
                    ActividadEconomica = 10000,
                    Telefono = 5550012,
                    NombreRepLegal = "Pepita",
                    IdentificacionRepLegal = 1120,
                    NombreRepSuplente = "Juana",
                    IdentificacionRepSuplente = 1111,
                    NombreJuntaDir1 = "junta 11",
                    IdentificacionJuntaDir1 = 11,
                    NombreJuntaDir2 = "Junta 22",
                    IdentificacionJuntaDir2 = 22,
                    NombreJuntaDir3 = "Junta 33",
                    IdentificacionJuntaDir3 = 33
                }

            );
            context.Proyectos.AddOrUpdate(
                a => a.Id,
                new Proyecto { Id = 1, Nombre = "Ballen", Precio = 1500000, TiempoEstipulado = 200, IdEmpresa = 1, Fecha = new DateTime(1996, 05, 22, 12, 00, 00), IdEstado = 1 },
                new Proyecto { Id = 2, Nombre = "Marqueting", Precio = 21500000, TiempoEstipulado = 400, IdEmpresa = 2, Fecha = new DateTime(1996, 12, 12, 12, 00, 00), IdEstado = 2 },
                new Proyecto { Id = 3, Nombre = "Casanova", Precio = 12000000, TiempoEstipulado = 100, IdEmpresa = 1, Fecha = new DateTime(1996, 8, 5, 12, 00, 00), IdEstado = 1 }
            );
            // CREAR ADMINISTRADOR
            var store = new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(
                new ApplicationDbContext()
            );
            var manager = new UserManager<ApplicationUser>(store);

            // Crear rol
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            string roleName = "ADMIN";
            IdentityResult roleResult;
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            // Crear usuario
            var user = new ApplicationUser
            {
                UserName = "admin@consultores.com",
                Nombre = "Cristian",
                Apellido = "Castañeda",
                Identificacion = 0011223344,
                FechaIngresoEmpresa = "12/12/2013",
                FechaNacimiento = "12/12/2013",
                Cargo = "Gerente"
            };
            var user1 = new ApplicationUser
            {
                UserName = "daniel@consultores.com",
                Nombre = "Cristian",
                Apellido = "Castañeda",
                Identificacion = 0021223344,
                FechaIngresoEmpresa = "12/12/2013",
                FechaNacimiento = "12/12/2013",
                Cargo = "Administrador"
            };
            if (manager.FindByName("admin@consultores.com") == null)
            {
                manager.Create(user, "consultores");
            }
            if (manager.FindByName("daniel@consultores.com") == null)
            {
                manager.Create(user1, "consultores");
            }

            var userdb = manager.FindByName(user.UserName);
            if (!manager.IsInRole(userdb.Id, roleName))
            {
                manager.AddToRole
                (
                   userdb.Id,
                    roleName
                );
            }
            context.ReporteUsuarios.AddOrUpdate(
                a => a.Id,
                new ReporteUsuario { Id = 1, FechaReporte = new DateTime(1996, 05, 22, 12, 00, 00),HTrabajadas=50,IdServicio= 1, IdProyecto = 1, IdUsuario = "c566ab0a-8f56-43cb-9341-25d0979da63c" },
                new ReporteUsuario { Id = 2, FechaReporte = new DateTime(1996, 05, 22, 12, 00, 00), HTrabajadas =  100, IdServicio = 1, IdProyecto = 2, IdUsuario = "c566ab0a-8f56-43cb-9341-25d0979da63c" }
            );
        }
    }
}
