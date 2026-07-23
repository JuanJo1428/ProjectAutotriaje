using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RespuestaPretriajeDtos;

namespace ProjectServices.Interfaces
{
    public interface IRespuestaPreguntaPretriajeService
    {
        RespuestaPreguntaPretriajeDto RegistrarRespuesta(RegistrarRespuestaPreguntaDto respuesta);

        ConsultarRespuestasRegistroDto ObtenerRespuestasRegistro(int idRegistro);
    }
}
