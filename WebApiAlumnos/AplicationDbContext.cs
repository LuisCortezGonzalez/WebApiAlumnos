using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Entidades;

namespace WebApiPeliculas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
