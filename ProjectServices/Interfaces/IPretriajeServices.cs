using ProjectData.Entities.PretriajeModel;
using ProjectDto.Dtos.PretriajeDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectServices.Interfaces
{
    public interface IPretriajeService
    {
        Task<FlujoPretriajeDto> DeterminarFlujoAsync(SolicitudPretriajeDto solicitud);

        List<PreguntaPretriaje> ObtenerPreguntasFlujo(int idFlujo);

        PreguntaPretriajeDto ObtenerPrimeraPregunta(int idFlujo);

        ResultadoPretriajeDto ProcesarRespuesta(RespuestaPreguntaDto respuesta);
    }
}