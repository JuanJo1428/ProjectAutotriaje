using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;

namespace ProjectServices.Interfaces
{
    public interface IRegistroAtencionService
    {
        CrearRegistroAtencionRespuestaDto CrearRegistroAtencion(CrearRegistroAtencionDto datosRegistro);

        ConsultarRegistroPendienteRespuestaDto ConsultarRegistroPendiente(PacienteProcesadoRespuestaDto pacienteProcesado);

        RegistroAtencionDto ActualizarRegistro(ActualizarRegistroAtencionDto datosRegistro);
    }
}
