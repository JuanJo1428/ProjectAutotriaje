using ProjectDto.Dtos.GhipsDtos;

namespace ProjectServices.Interfaces
{
    public interface IGhipsService
    {
        BuscarPacienteGhipsRespuestaDto BuscarPaciente(BuscarPacienteGhipsDto paciente);
    }
}
