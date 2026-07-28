using System;

namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class CrearRegistroAtencionDto
    {
        public int IdPaciente { get; set; }

        public bool CondicionMaternidad { get; set; }

        public bool CondicionMental { get; set; }

        public bool CondicionOncologica { get; set; }

        public bool AutotriajeIniciado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
