using ProjectDto.Dtos.EscanerDtos;
using ProjectServices.Interfaces;
using ProjectServices.Parser;

namespace ProjectServices.Implementations
{
    public class EscanerService: IEscanerService
    {
        private readonly IEscanParser _parser = new CedulaParser();
        public PacienteEscaneadoDto ProcesarLectura(string lectura)
        {

            return _parser.Parse(lectura);
        }
    }
}
