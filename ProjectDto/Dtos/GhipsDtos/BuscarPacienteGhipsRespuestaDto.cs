using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDto.Dtos.GhipsDtos
{
    public class BuscarPacienteGhipsRespuestaDto
    {
        public bool Encontrado { get; set; }

        public PacienteDto Paciente { get; set; }

        public string Observacion { get; set; } = string.Empty;
    }
}
