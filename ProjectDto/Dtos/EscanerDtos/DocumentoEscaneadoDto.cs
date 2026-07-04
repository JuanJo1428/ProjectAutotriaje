using System;

namespace ProjectDto.Dtos.EscanerDtos
{
    public class DocumentoEscaneadoDto
    {
        public string NumeroDocumento { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string TextoExtraido { get; set; }
    }
}