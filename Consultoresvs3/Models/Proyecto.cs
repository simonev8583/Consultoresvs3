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

        [Display(Name = "Honorario del Proyecto")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        [Display(Name = "Tiempo Estimado (Horas)")]
        public decimal TiempoEstipulado { get; set; }
        //Horas totales trabajadas
        [Display(Name = "Horas Trabajadas")]
        public decimal HorasTrabajdas { get; set; }
        //Empresa
        [Display(Name = "Empresa")]
        public int IdEmpresa { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Fecha Estimada de Finalización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Estado Proyecto")]
        public int IdEstado { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [ForeignKey("IdEstado")]
        public virtual EstadoProyecto Estado { get; set; }
    }
}