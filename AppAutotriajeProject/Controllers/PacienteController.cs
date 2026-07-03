using ProjectDto.Dtos;
using ProjectServices.Interfaces;
using System.Web.Http;

namespace AppAutotriajeProject.Controllers
{
    [RoutePrefix("api/paciente")]
    public class PacienteController : ApiController
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpPost]
        [Route("buscar")]
        public IHttpActionResult BuscarPaciente(BuscarPacienteDto datosBusqueda)
        {
            BuscarPacienteRespuestaDto respuesta =
                _pacienteService.BuscarPaciente(datosBusqueda);

            return Ok(respuesta);
        }

        
        [HttpPost]
        [Route("procesar")]
        public IHttpActionResult ProcesarPaciente(PacienteValidadoDto paciente)
        {
            PacienteProcesadoRespuestaDto respuesta =
                _pacienteService.ProcesarPaciente(paciente);

            return Ok(respuesta);
        }
    }

}