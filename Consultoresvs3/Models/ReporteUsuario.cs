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
        [DataType(DataType.DateTime)]
        public DateTime FechaReporte { get; set; }
        [Display(Name = "Horas Trabajadas")]
        public int HTrabajadas { get; set; }
        public int IdServicio { get; set; }
        public int IdUsuarioProyecto { get; set; }

        [ForeignKey("IdServicio")]
        public Servicio Servicio { get; set; }

        [ForeignKey("IdUsuarioProyecto")]
        public virtual UsuarioProyecto Usuarioproyecto { get; set; }
    }
}