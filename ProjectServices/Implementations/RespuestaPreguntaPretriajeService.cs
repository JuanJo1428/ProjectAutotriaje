using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Implementations.PretriajeImplementations;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RespuestaPretriajeDtos;
using ProjectServices.Interfaces;
using System.Collections.Generic;

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
                    IdRegistro = respuesta.IdRegistro,

                    IdPregunta = respuesta.IdPregunta,

                    IdOpcionSeleccionada = respuesta.IdOpcionSeleccionada,

                    Activo = true
                };

            respuestaAñadida = _respuestaRepository.CrearRespuesta(respuestaAñadida);

            return ConvertirRespuestaDto(respuestaAñadida);
        }

        public ConsultarRespuestasRegistroDto ObtenerRespuestasRegistro(int idRegistro)
        {
            List<RespuestaPreguntaPretriaje> respuestas = _respuestaRepository.ObtenerPorRegistro(idRegistro);

            ConsultarRespuestasRegistroDto consultaDto = new ConsultarRespuestasRegistroDto();

            foreach (RespuestaPreguntaPretriaje respuesta in respuestas)
            {
                consultaDto.Respuestas.Add(ConvertirRespuestaDto(respuesta));
            }

            return consultaDto;
        }

        //Metodo de Mapeo
        private RespuestaPreguntaPretriajeDto ConvertirRespuestaDto(RespuestaPreguntaPretriaje respuesta)
        {
            return new RespuestaPreguntaPretriajeDto
            {
                IdRespuesta = respuesta.IdRespuesta,

                IdRegistro = respuesta.IdRegistro,

                IdPregunta = respuesta.IdPregunta,

                IdOpcionSeleccionada = respuesta.IdOpcionSeleccionada
            };
        }
    }
}