using ProjectDto.Dtos.EscanerDtos;

namespace ProjectServices.Interfaces
{
    public interface IEscanerService
    {
        PacienteEscaneadoDto ProcesarLectura(string lectura);
    }
}
