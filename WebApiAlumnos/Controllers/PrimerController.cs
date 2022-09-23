using Microsoft.AspNetCore.Mvc;
using WebApiPeliculas.BD;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PrimerController : ControllerBase
    {
        [HttpGet]

        public ActionResult<List<Pelicula>> Get()
        {
            return new List<Pelicula>()
            {
                new Pelicula() {Id = 1,Nombre = "Spider-Man" },
                new Pelicula() {Id = 2,Nombre = "Iron Man"}
                
            };
        }
    }
}
