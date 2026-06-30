using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDto.Dtos.GhipsDtos
{
    public class PacienteGhipsDto
    {
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string Genero { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}
