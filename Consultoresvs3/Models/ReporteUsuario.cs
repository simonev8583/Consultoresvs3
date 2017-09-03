using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Consultoresvs3.Models
{
    public class ReporteUsuario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode =true)]
        public DateTime FechaReporte { get; set; }
        [Range(1, 10, ErrorMessage = "{0} las horas deben estar entre {1} y {2} .")]
        [Display(Name = "Horas Trabajadas")]
        public int HTrabajadas { get; set; }
        public int IdServicio { get; set; }

        [ForeignKey("IdServicio")]
        public virtual Servicio Servicio { get; set; }
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