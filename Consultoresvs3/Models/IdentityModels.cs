using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Consultoresvs3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Identificacion { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public string FechaIngresoEmpresa { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }
        [Required]
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public decimal ValorHoraPrestacionesSociales { get; set; }
        public decimal ValorHoraNoPrestacionSociales { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EstadoProyecto> EstadoProyectos { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<ReporteProyecto> ReporteProyectos { get; set; }
        public DbSet<ReporteUsuario> ReporteUsuarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}