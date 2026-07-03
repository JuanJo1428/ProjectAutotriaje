using ProjectData.Repositories.Interfaces;
using System.Linq;
using System.Web.Http;

namespace AppAutotriajeProject.Controllers
{
    [RoutePrefix("api/tipodocumento")]
    public class TipoDocumentoController : ApiController
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public TipoDocumentoController(ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult ObtenerTodos()
        {
            var tiposDocumento = _tipoDocumentoRepository
                .ObtenerTodos()
                .Select(td => new
                {
                    td.IdTipoDocumento,
                    td.Nombre,
                    td.Descripcion
                })
                .ToList();

            return Ok(tiposDocumento);
        }
    }
}