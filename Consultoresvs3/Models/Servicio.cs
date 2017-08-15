using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consultoresvs3.Models
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tipo de Servicio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}