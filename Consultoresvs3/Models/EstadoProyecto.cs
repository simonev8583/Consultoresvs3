﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consultoresvs3.Models
{
    public class EstadoProyecto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}