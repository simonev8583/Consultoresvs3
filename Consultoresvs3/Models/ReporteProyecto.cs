using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultoresvs3.Models
{
    public class ReporteProyecto
    {
        [Key]
        public int Id { get; set; }

        // Relacion con proyectoProyecto
        [Display(Name = "Proyecto")]
        public int IdProyecto { get; set; }
        [ForeignKey("IdProyecto")]
        public virtual Proyecto Proyecto { get; set; }
        //Horas Gastadas 
        [Display(Name = "Horas Invertidas")]
        public int HorasInvertidas { get; set; }
        //utilidad
        public double Utilidad { get; set; }
    }
}