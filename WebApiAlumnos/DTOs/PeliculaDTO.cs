using System.ComponentModel.DataAnnotations;
using WebApiPeliculas.Validaciones;

namespace WebApiPeliculas.DTOs
{
    public class PeliculaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
