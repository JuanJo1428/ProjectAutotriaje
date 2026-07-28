using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RespuestaPretriajeDtos;
using System.Collections.Generic;

namespace ProjectServices.Interfaces
{
    public interface IRespuestaPreguntaPretriajeService
    {
        RespuestaPreguntaPretriajeDto RegistrarRespuesta(RegistrarRespuestaPreguntaDto respuesta);

        List<RespuestaModalSalaEsperaDto> ObtenerRespuestasRegistro(int idRegistro);
    }
}
