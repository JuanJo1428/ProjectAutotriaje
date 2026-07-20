using ProjectDto.Dtos.PretriajeDtos;
using System.Threading.Tasks;

namespace ProjectServices.Interfaces
{
    public interface IClasificadorMotivoConsultaService
    {
        Task<string> ClasificarMotivoConsultaAsync(SolicitudPretriajeDto solicitud);
    }
}
