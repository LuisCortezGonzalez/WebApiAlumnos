using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiPeliculas.Validaciones;

namespace WebApiPeliculas.Entidades
{
    public class Descripcion
    {
        public int Id { get; set; }

        [PrimeraLetraMayuscula]
        public string Genero { get; set; }

        [Range(1900,2022,ErrorMessage = "El campo no se encuentra en el rango de años")]
        [NotMapped]
        public int year { get; set; }

        [NotMapped]
        public int Duracion { get; set; }

        [NotMapped]
        public int PeliculaId { get; set; }

        [NotMapped]
        public Pelicula Pelicula { get; set; }

        public List<PeliculaDescripcion> peliculaDescripcions { get; set; }
    }
}
