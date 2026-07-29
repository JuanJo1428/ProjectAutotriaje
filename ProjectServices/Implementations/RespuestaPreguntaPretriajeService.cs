using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Implementations.PretriajeImplementations;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RespuestaPretriajeDtos;
using ProjectServices.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectServices.Implementations
{
    public class RespuestaPreguntaPretriajeService : IRespuestaPreguntaPretriajeService
    {
        private readonly IRespuestaPreguntaPretriajeRepository _respuestaRepository;

        public RespuestaPreguntaPretriajeService(
            IRespuestaPreguntaPretriajeRepository respuestaRepository)
        {
            _respuestaRepository = respuestaRepository;
        }

        public RespuestaPreguntaPretriajeService()
        {
            _respuestaRepository = new RespuestaPreguntaPretriajeRepository();
        }

        public RespuestaPreguntaPretriajeDto RegistrarRespuesta(RegistrarRespuestaPreguntaDto respuesta)
        {
            RespuestaPreguntaPretriaje respuestaAñadida =
                new RespuestaPreguntaPretriaje
                {
                    IdAtencion = respuesta.IdRegistro,

                    IdPregunta = respuesta.IdPregunta,

                    IdOpcionSeleccionada = respuesta.IdOpcionSeleccionada,

                    Activo = true
                };

            respuestaAñadida = _respuestaRepository.CrearRespuesta(respuestaAñadida);

            return ConvertirRespuestaDto(respuestaAñadida);
        }

        public List<RespuestaModalSalaEsperaDto> ObtenerRespuestasRegistro(int idRegistro)
        {
            List<RespuestaPreguntaPretriaje> respuestas = _respuestaRepository.ObtenerPorRegistro(idRegistro);

            return respuestas.Select(r => new RespuestaModalSalaEsperaDto
            {
                Pregunta = r.Pregunta.TextoPregunta,
                Respuesta = r.OpcionSeleccionada.Texto,
            })
            .ToList();
        }

        //Metodo de Mapeo
        private RespuestaPreguntaPretriajeDto ConvertirRespuestaDto(RespuestaPreguntaPretriaje respuesta)
        {
            return new RespuestaPreguntaPretriajeDto
            {
                IdRespuesta = respuesta.IdRespuesta,

                IdRegistro = respuesta.IdAtencion,

                IdPregunta = respuesta.IdPregunta,

                IdOpcionSeleccionada = respuesta.IdOpcionSeleccionada
            };
        }
    }
}