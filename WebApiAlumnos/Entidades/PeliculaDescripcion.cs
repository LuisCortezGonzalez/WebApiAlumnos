using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebApiPeliculas.Entidades
{
    public class PeliculaDescripcion
    {
        public int PeliculaId { get; set; }

        public int DescriptionId { get; set; }
        public int Orden { get; set; }

        public Pelicula Pelicula { get; set; }
        public Descripcion Descripcion { get; set; }
    }
}
