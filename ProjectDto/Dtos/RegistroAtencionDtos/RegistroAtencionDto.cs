using System;

namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class RegistroAtencionDto
    {
        public int IdAtencion { get; set; }

        public int IdPaciente { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int EdadPaciente { get; set; }

        public bool CondicionMaternidad { get; set; }

        public bool CondicionMental { get; set; }

        public bool CondicionOncologica { get; set; }

        public bool AutotriajeIniciado { get; set; }

        public string MotivoConsulta { get; set; }

        public bool Atendido { get; set; }

        public bool Activo { get; set; }
    }
}
