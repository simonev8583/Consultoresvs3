using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Consultoresvs3.Models
{
    public class UsuarioProyecto
    {
        [Key]
        public int Id { get; set; }
        // Relacion con proyectoProyecto
        [Display(Name = "Proyecto")]
        public int IdProyecto { get; set; }

        [ForeignKey("IdProyecto")]
        public virtual Proyecto Proyecto { get; set; }



        // Relacion con usuario
        [Display(Name = "Usuario")]
        public string IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual ApplicationUser Usuario { get; set; }
    }
}