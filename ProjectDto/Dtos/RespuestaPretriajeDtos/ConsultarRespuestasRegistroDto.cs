using System.Collections.Generic;

namespace ProjectDto.Dtos.RespuestaPretriajeDtos
{
    public class ConsultarRespuestasRegistroDto
    {
        public List<RespuestaPreguntaPretriajeDto> Respuestas { get; set; }

        public ConsultarRespuestasRegistroDto()
        {
            Respuestas = new List<RespuestaPreguntaPretriajeDto>();
        }
    }
}
