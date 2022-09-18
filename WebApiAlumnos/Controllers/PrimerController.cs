using Microsoft.AspNetCore.Mvc;
using WebApiAlumnos.BD;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/alumnos")]
    public class PrimerController : ControllerBase
    {
        [HttpGet]

        public ActionResult<List<Alumno>> Get()
        {
            return new List<Alumno>()
            {
                new Alumno() {Id = 1,Nombre = "Luis" },
                new Alumno() {Id = 2,Nombre = "Yoriichi"}
                
            };
        }
    }
}
