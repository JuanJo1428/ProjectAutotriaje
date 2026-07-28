using System;

namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    [Serializable]
    public class SalaEsperaDto
    {
        public int IdAtencion { get; set; }


        public int IdPaciente { get; set; }


        public DateTime FechaRegistro { get; set; } = DateTime.Now;


        public int EdadPaciente { get; set; }


        public bool CondicionMaternidad { get; set; }


        public bool CondicionMental { get; set; }


        public bool CondicionOncologica { get; set; }


        public bool AutotriajeIniciado { get; set; }


        public string MotivoConsulta { get; set; }


        public int? NivelPrioridad { get; set; }


        public string FlujoClinico { get; set; }


        public DateTime? FechaActualizacion { get; set; }


        public string NombreCompleto { get; set; }


        public string NumeroDocumento { get; set; }


        public string TipoDocumento { get; set; }
    }
}
