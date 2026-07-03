using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Interfaces;
using System.Web.Http;
using System.Web.Routing;

namespace AppAutotriajeProject.Controllers
{
    [RoutePrefix("api/registroatencion")]
    public class RegistroAtencionController : ApiController
    {
        private readonly IRegistroAtencionService _registroAtencionService;

        public RegistroAtencionController(IRegistroAtencionService registroAtencionService)
        {
            _registroAtencionService = registroAtencionService;
        }

        [HttpPost]
        [Route("consultarpendiente")]
        public IHttpActionResult ConsultarRegistroPendiente(PacienteProcesadoRespuestaDto pacienteProcesado)
        {
            ConsultarRegistroPendienteRespuestaDto respuesta = _registroAtencionService.ConsultarRegistroPendiente(pacienteProcesado);

            return Ok(respuesta);
        }


        [HttpPost]
        [Route("crear")]
        public IHttpActionResult CrearRegistroAtencion(CrearRegistroAtencionDto datosRegistro)
        {
            CrearRegistroAtencionRespuestaDto respuesta = _registroAtencionService.CrearRegistroAtencion(datosRegistro);

            return Ok(respuesta);
        }


        [HttpPost]
        [Route("actualizar")]
        public IHttpActionResult ActualizarRegistro(ActualizarRegistroAtencionDto datosRegistro)
        {
            RegistroAtencionDto respuesta = _registroAtencionService.ActualizarRegistro(datosRegistro);

            return Ok(respuesta);
        }
    }
}