using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Entidades;
using WebApiPeliculas.Filtros;
using WebApiPeliculas.Services;

namespace WebApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/pelicula")]  //ruta del controlador
    public class PrimerController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<PrimerController> logger;
        private readonly IWebHostEnvironment env;

        public PrimerController(ApplicationDbContext dbContext, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<PrimerController> logger,
            IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecucion");
            return Ok(new
            {
                AlumnosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                AlumnosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                AlumnosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Pelicula>> Get(int id)
        {
            var pelicula =  await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        [HttpGet("{nombre}")]

        public async Task<ActionResult<Pelicula>> Get(string nombre)
        {
            var pelicula = await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Pelicula pelicula)
        {
            var existeAlumnoMismoNombre = await dbContext.Peliculas.AnyAsync(x => x.Nombre == pelicula.Nombre);

            if (existeAlumnoMismoNombre)
            {
                return BadRequest("Ya existe una pelicula con ese nombre");
            }
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
