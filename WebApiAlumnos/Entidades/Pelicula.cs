using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiPeliculas.Validaciones;

namespace WebApiPeliculas.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        [StringLength(maximumLength:15,ErrorMessage = "Solo pueden ser 15 caracteres.")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public List<PeliculaDescripcion> peliculaDescripcions { get; set; }
    }
}
