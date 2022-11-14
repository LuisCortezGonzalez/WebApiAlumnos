using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Entidades;
using WebApiPeliculas.Filtros;
using WebApiPeliculas.Services;
using WebApiPeliculas.DTOs;

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
        private readonly IMapper mapper;

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
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetPeliculaDTO>>> Get()
        {
            var alumnos = await dbContext.Peliculas.ToListAsync();
            return mapper.Map<List<GetPeliculaDTO>>(alumnos);
        }

        [HttpGet("{id:int}",Name ="obtenerpelicula")]
        //[Authorize]
        public async Task<ActionResult<DescripcionDTOConPeliculas>> Get(int id)
        {
            var pelicula = await dbContext.Peliculas
                .Include(peliculaDB => peliculaDB.peliculaDescripcions)
                .ThenInclude(peliculaDescripcionDB => peliculaDescripcionDB.Descripcion)
                .FirstOrDefaultAsync(peliculaBD => peliculaBD.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return mapper.Map<DescripcionDTOConPeliculas>(pelicula);
        }

        [HttpGet("{nombre}")]

        public async Task<ActionResult<List<GetPeliculaDTO>>> Get([FromRoute] string nombre)
        {
            var peliculas = await dbContext.Peliculas.Where(peliculaBD => peliculaBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetPeliculaDTO>>(peliculas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PeliculaDTO peliculaDto)
        {
            var existePeliculaMismoNombre = await dbContext.Peliculas.AnyAsync(x => x.Nombre == peliculaDto.Nombre);

            if (existePeliculaMismoNombre)
            {
                return BadRequest("Ya existe una pelicula con el nombre {peliculaDto.Nombre}");
            }
            var pelicula = mapper.Map<Pelicula>(peliculaDto);

            dbContext.Add(pelicula);
            await dbContext.SaveChangesAsync();

            var alumnoDTO = mapper.Map<GetPeliculaDTO>(pelicula);

            return CreatedAtRoute("obtenerpelicula", new { id = pelicula.Id }, alumnoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(PeliculaDTO peliculaCreacionDTO, int id)
        {
            var exist = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            pelicula.Id = id;

            dbContext.Update(pelicula);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
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
