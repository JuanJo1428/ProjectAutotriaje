using ProjectData.Repositories.Interfaces;
using System.Linq;
using System.Web.Http;

namespace AppAutotriajeProject.Controllers
{
    [RoutePrefix("api/genero")]
    public class GeneroController : ApiController
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult ObtenerGeneros()
        {
            var respuesta = _generoRepository.ObtenerTodos().Select(g => new
            {
                g.IdGenero,
                g.Descripcion

            }).ToList();

            return Ok(respuesta);
        }
    }
}