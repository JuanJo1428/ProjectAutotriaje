using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Interfaces
{
    public interface IRegistroAtencionService
    {
        CrearRegistroAtencionRespuestaDto CrearRegistroAtencion(CrearRegistroAtencionDto datosRegistro);

        ConsultarRegistroPendienteRespuestaDto ConsultarRegistroPendiente(PacienteProcesadoRespuestaDto pacienteProcesado);

        RegistroAtencionDto ActualizarRegistro(ActualizarRegistroAtencionDto datosRegistro);
    }
}
