using ProjectDto.Dtos;
using ProjectDto.Dtos.GhipsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Interfaces
{
    public interface IGhipsService
    {
        BuscarPacienteGhipsRespuestaDto BuscarPaciente(BuscarPacienteGhipsDto paciente);
    }
}
