using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Consultoresvs3.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        [Display(Name = "Tiempo estipulado")]
        public int TiempoEstipulado { get; set; }
        //Empresa
        public int IdEmpresa { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Estado Proyecto")]
        public int IdEstado { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [ForeignKey("IdEstado")]
        public virtual EstadoProyecto Estado { get; set; }
    }
}