using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class ConsultarRegistroPendienteRespuestaDto
    {
        public bool TieneRegistroPendiente { get; set; }

        public bool MostrarOpcionContinuarAutotriaje { get; set; }

        public RegistroAtencionDto RegistroAtencion { get; set; }
    }
}
