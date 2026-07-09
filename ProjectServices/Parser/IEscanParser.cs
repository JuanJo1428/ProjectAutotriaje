using ProjectDto.Dtos.EscanerDtos;

namespace ProjectServices.Parser
{
    public interface IEscanParser
    {
        PacienteEscaneadoDto Parse(string lectura);
    }
}