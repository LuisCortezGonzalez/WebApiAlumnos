namespace WebApiPeliculas.Entidades
{
    public class Descripcion
    {
        public int Id { get; set; }

        public string Genero { get; set; }

        public int year { get; set; }

        public int Duracion { get; set; }

        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }
    }
}
