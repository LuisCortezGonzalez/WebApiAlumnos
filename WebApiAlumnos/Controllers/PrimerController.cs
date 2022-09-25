using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Entidades;

namespace WebApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PrimerController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PrimerController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            return await dbContext.Peliculas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Pelicula pelicula)
        {
            dbContext.Add(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(Pelicula pelicula, int id)
        {
            if(pelicula.Id != id)
            {
                return BadRequest("El id del alumno no coincide");
            }
            dbContext.Update(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Pelicula()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
