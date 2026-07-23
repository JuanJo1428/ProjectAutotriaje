using ProjectData.Entities.PretriajeModel;
using System.Collections.Generic;

namespace ProjectData.Repositories.Interfaces.PretriajeInterfaces
{
    public interface IRespuestaPreguntaPretriajeRepository
    {
        RespuestaPreguntaPretriaje CrearRespuesta(RespuestaPreguntaPretriaje respuesta);

        List<RespuestaPreguntaPretriaje> ObtenerPorRegistro(int idRegistro);

        bool InactivarRespuesta(int idRespuesta);
    }
}