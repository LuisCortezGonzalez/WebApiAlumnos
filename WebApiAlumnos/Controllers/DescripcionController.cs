using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Entidades;

namespace WebApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/descripciones")]
    public class DescripcionController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DescripcionController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Descripcion>>> GetAll()
        {
            return await dbContext.descripciones.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Descripcion>> GetById(int id)
        {
            return await dbContext.descripciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]

        public async Task<ActionResult> Post(Descripcion descripcion)
        {
            var existePelicula = await dbContext.Peliculas.AnyAsync(x => x.Id == descripcion.PeliculaId);
            if (!existePelicula)
            {
                return BadRequest($"No existe la Pelicula con el ID: {descripcion.PeliculaId}");
            }
            dbContext.Add(descripcion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Descripcion descripcion, int id)
        {
            var exist = await dbContext.descripciones.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("La Descripcion no existe.");
            }
            if(descripcion.Id != id)
            {
                return BadRequest("El id de la Descripcion no coincide.");
            }
            dbContext.Update(descripcion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.descripciones.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("La Descripcion no fue encontrada.");
            }
            dbContext.Remove(new Descripcion { Id = id});
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
