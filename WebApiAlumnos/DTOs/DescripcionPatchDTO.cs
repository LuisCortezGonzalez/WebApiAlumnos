﻿using System.ComponentModel.DataAnnotations;
using WebApiPeliculas.Validaciones;

namespace WebApiPeliculas.DTOs
{
    public class DescripcionPatchDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
