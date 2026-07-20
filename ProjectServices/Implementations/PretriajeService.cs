using ProjectCommon.Constants;
using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Interfaces;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using ProjectDto.Dtos.PretriajeDtos;
using ProjectServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectServices.Implementations
{
    public class PretriajeService : IPretriajeService
    {
        private readonly IFlujoPretriajeRepository _flujoRepository;
        private readonly IPreguntaPretriajeRepository _preguntaRepository;
        private readonly IDecisionPretriajeRepository _decisionRepository;
        private readonly IPrioridadPretriajeRepository _prioridadRepository;
        private readonly IClasificadorMotivoConsultaService _clasificador;

        public PretriajeService(
            IFlujoPretriajeRepository flujoRepository,
            IPreguntaPretriajeRepository preguntaRepository,
            IDecisionPretriajeRepository decisionRepository,
             IPrioridadPretriajeRepository prioridadRepository,
             IClasificadorMotivoConsultaService clasificador)
        {
            _flujoRepository = flujoRepository;
            _preguntaRepository = preguntaRepository;
            _decisionRepository = decisionRepository;
            _prioridadRepository = prioridadRepository;
            _clasificador = clasificador;
        }

        public async Task<FlujoPretriajeDto> DeterminarFlujoAsync(SolicitudPretriajeDto solicitud)
        {
            // Validar la solicitud
            if (solicitud == null || string.IsNullOrWhiteSpace(solicitud.MotivoConsulta))
                return null;


            // Obtener el código del flujo mediante la IA
            string codigoFlujo = await _clasificador.ClasificarMotivoConsultaAsync(solicitud);

            if (string.IsNullOrWhiteSpace(codigoFlujo))
                return null;


            // Buscar el flujo parametrizado en la base de datos
            FlujoPretriaje flujo = _flujoRepository.ObtenerPorCodigo(codigoFlujo);

            if (flujo == null)
                return null;


            return ConvertirFlujoDto(flujo);
        }

        public List<PreguntaPretriaje> ObtenerPreguntasFlujo(int idFlujo)
        {
            return _preguntaRepository.ObtenerPreguntasPorFlujo(idFlujo);
        }


        public PreguntaPretriajeDto ObtenerPrimeraPregunta(int idFlujo)
        {
            PreguntaPretriaje pregunta =  _preguntaRepository.ObtenerPrimeraPregunta(idFlujo);

            if (pregunta == null)
                return null;

            return ConvertirPreguntaDto(pregunta);
        }

        public ResultadoPretriajeDto ProcesarRespuesta(RespuestaPreguntaDto respuesta)
        {
            // 1. Obtener la pregunta actual
            PreguntaPretriaje pregunta = _preguntaRepository.ObtenerPorId(respuesta.IdPregunta);

            if (pregunta == null)
            {
                return new ResultadoPretriajeDto
                {
                    Finalizado = true,
                    Mensaje = "La pregunta no existe."
                };
            }


            // 2. Obtener la decisión correspondiente
            DecisionPretriaje decision = ObtenerDecision(pregunta, respuesta);

            if (decision == null)
            {
                return new ResultadoPretriajeDto
                {
                    Finalizado = true,
                    Mensaje = "No existe una decisión configurada para la respuesta."
                };
            }


            // 3. ¿El flujo termina?
            if (decision.FinalizaFlujo)
            {
                return ConstruirResultadoFinal(decision);
            }


            // 4. Cargar la siguiente pregunta
            return ConstruirResultadoPregunta(decision);
        }


        //Metodos Privado de Mapeo
        private PreguntaPretriajeDto ConvertirPreguntaDto(PreguntaPretriaje pregunta)
        {
            return new PreguntaPretriajeDto
            {
                IdPregunta = pregunta.IdPregunta,

                TextoPregunta = pregunta.TextoPregunta,

                TipoRespuesta = pregunta.TipoRespuesta,

                Obligatoria = pregunta.Obligatoria,

                Opciones = pregunta.Opciones?
                    .Where(o => o.Activo)
                    .Select(o => new OpcionPreguntaDto
                    {
                        IdOpcion = o.IdOpcion,
                        Texto = o.Texto
                    })
                    .ToList()
            };
        }

        private PrioridadPretriajeDto ConvertirPrioridadDto(PrioridadPretriaje prioridad)
        {
            return new PrioridadPretriajeDto
            {
                IdPrioridad = prioridad.IdPrioridad,

                NivelPrioridad = prioridad.NivelPrioridad,

                Color = prioridad.Color,

                Codigo = prioridad.Codigo
            };
        }

        private FlujoPretriajeDto ConvertirFlujoDto(FlujoPretriaje flujo)
        {
            return new FlujoPretriajeDto
            {
                IdFlujo = flujo.IdFlujo,
                Nombre = flujo.Nombre,
                Descripcion = flujo.Descripcion,
                Codigo = flujo.Codigo
            };
        }

        private DecisionPretriaje ObtenerDecision(PreguntaPretriaje pregunta, RespuestaPreguntaDto respuesta)
        {
            switch (pregunta.TipoRespuesta)
            {
                case TipoRespuesta.SiNo:

                    return _decisionRepository.ObtenerDecisionSiNo(
                        pregunta.IdPregunta,
                        respuesta.RespuestaSiNo.Value);

                case TipoRespuesta.Lista:

                    return _decisionRepository.ObtenerDecisionLista(
                        pregunta.IdPregunta,
                        respuesta.IdOpcionSeleccionada.Value);

                default:

                    return null;
            }
        }

        private ResultadoPretriajeDto ConstruirResultadoFinal(DecisionPretriaje decision)
        {
            PrioridadPretriaje prioridad = _prioridadRepository.ObtenerPorId(decision.IdPrioridad.Value);

                return new ResultadoPretriajeDto
                {
                    Finalizado = true,

                    Prioridad = ConvertirPrioridadDto(prioridad),

                    Mensaje = "El paciente ha sido clasificado."
                };

        }

        private ResultadoPretriajeDto ConstruirResultadoPregunta(DecisionPretriaje decision)
        {
            PreguntaPretriaje siguientePregunta = _preguntaRepository.ObtenerPorId(decision.IdPreguntaSiguiente.Value);

            return new ResultadoPretriajeDto
            {
                Finalizado = false,

                SiguientePregunta = ConvertirPreguntaDto(siguientePregunta)
            };
        }
    }

}