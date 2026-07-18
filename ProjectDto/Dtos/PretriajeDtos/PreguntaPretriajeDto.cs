using ProjectCommon.Constants;
using System.Collections.Generic;

namespace ProjectDto.Dtos.PretriajeDtos
{
    public class PreguntaPretriajeDto
    {
        public PreguntaPretriajeDto()
        {
            Opciones = new List<OpcionPreguntaDto>();
        }

        public int IdPregunta { get; set; }

        public string TextoPregunta { get; set; }

        public TipoRespuesta TipoRespuesta { get; set; }

        public bool Obligatoria { get; set; }

        public List<OpcionPreguntaDto> Opciones { get; set; }
    }
}
