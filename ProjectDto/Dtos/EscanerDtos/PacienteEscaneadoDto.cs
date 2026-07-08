using System;

namespace ProjectDto.Dtos.EscanerDtos
{
    public class PacienteEscaneadoDto
    {
        public int IdTipoDocumento { get; set; }

        public string DescripcionTipoDocumento { get; set; }

        public string NroDocumento { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public int IdGenero { get; set; }

        public DateTime FechaNacimiento { get; set; }

    }
}