using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.BD;

namespace WebApiPeliculas
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Pelicula> Alumnos { get; set; }
    }
}
