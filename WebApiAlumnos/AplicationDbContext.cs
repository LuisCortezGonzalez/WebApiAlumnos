using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.BD;

namespace WebApiAlumnos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Alumno> Alumnos { get; set; }
    }
}
