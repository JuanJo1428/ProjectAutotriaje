using ProjectDto.Dtos.EscanerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
