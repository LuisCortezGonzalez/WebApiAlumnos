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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Genero))
            {
                var primeraLetra = Genero[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] { nameof(Genero) });
                }
            }
            var yearcap = 2022;
            if (year > yearcap)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el año 2022",
                    new String[] { nameof(year) });
            }
        }
    }
}
