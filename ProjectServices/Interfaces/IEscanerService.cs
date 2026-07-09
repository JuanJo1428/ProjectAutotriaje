using ProjectDto.Dtos.EscanerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Interfaces
{
    public interface IEscanerService
    {
        PacienteEscaneadoDto ProcesarLectura(string lectura);
    }
}
