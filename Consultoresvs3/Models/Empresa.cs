using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consultoresvs3.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "N.I.T")]
        public int NIT { get; set; }
        [Display(Name = "Nombre Empresa")]
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        // validar que sea correo
        public string CorreoEmpresa { get; set; }
        // deben ser 4 digitos
        public int ActividadEconomica { get; set; }
        public int Telefono { get; set; }
        public string NombreRepLegal { get; set; }
        public int IdentificacionRepLegal { get; set; }
        public string NombreRepSuplente { get; set; }
        public int IdentificacionRepSuplente { get; set; }
        [Display(Name = "Miembro Junta Directiva Principal")]
        public string NombreJuntaDir1 { get; set; }
        [Display(Name = "Identificación Principal")]
        public int IdentificacionJuntaDir1 { get; set; }

        [Display(Name = "Miembro Junta Directiva Principal")]
        public string NombreJuntaDir2 { get; set; }
        [Display(Name = "Identificación Principal")]
        public int IdentificacionJuntaDir2 { get; set; }

        [Display(Name = "Miembro Junta Directiva Principal")]
        public string NombreJuntaDir3 { get; set; }
        [Display(Name = "Identificación Principal")]
        public int IdentificacionJuntaDir3 { get; set; }

        [Display(Name = "Miembro Junta Directiva Principal")]
        public string NombreJuntaDir4 { get; set; }
        [Display(Name = "Identificación Principal")]
        public int IdentificacionJuntaDir4 { get; set; }

        [Display(Name = "Miembro Junta Directiva Principal")]
        public string NombreJuntaDir5 { get; set; }
        [Display(Name = "Identificación Principal")]
        public int IdentificacionJuntaDir5 { get; set; }

        [Display(Name = "Miembro Junta Directiva Suplente")]
        public string NombreJuntaDir6 { get; set; }
        [Display(Name = "Identificación Suplente")]
        public int IdentificacionJuntaDir6 { get; set; }

        [Display(Name = "Miembro Junta Directiva Suplente")]
        public string NombreJuntaDir7 { get; set; }
        [Display(Name = "Identificación Suplente")]
        public int IdentificacionJuntaDir7 { get; set; }

        [Display(Name = "Miembro Junta Directiva Suplente")]
        public string NombreJuntaDir8 { get; set; }
        [Display(Name = "Identificación Suplente")]
        public int IdentificacionJuntaDir8 { get; set; }

        [Display(Name = "Miembro Junta Directiva Suplente")]
        public string NombreJuntaDir9 { get; set; }
        [Display(Name = "Identificación Suplente")]
        public int IdentificacionJuntaDir9 { get; set; }

        [Display(Name = "Miembro Junta Directiva Suplente")]
        public string NombreJuntaDir10 { get; set; }
        [Display(Name = "Identificación Suplente")]
        public int IdentificacionJuntaDir10 { get; set; }

    }
}