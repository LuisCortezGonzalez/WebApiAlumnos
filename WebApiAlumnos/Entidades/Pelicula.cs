namespace WebApiPeliculas.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Descripcion> descripciones { get; set; }
    }
}
