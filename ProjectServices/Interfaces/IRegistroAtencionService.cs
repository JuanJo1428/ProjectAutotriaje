using ProjectData.Entities;
using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using System.Collections.Generic;

namespace ProjectServices.Interfaces
{
    public interface IRegistroAtencionService
    {
        CrearRegistroAtencionRespuestaDto CrearRegistroAtencion(CrearRegistroAtencionDto datosRegistro);

        ConsultarRegistroPendienteRespuestaDto ConsultarRegistroPendiente(PacienteProcesadoRespuestaDto pacienteProcesado);

        RegistroAtencionDto ActualizarRegistro(ActualizarRegistroAtencionDto datosRegistro);

        List<RegistroAtencion> ObtenerPacientesSalaEspera();
    }
}
