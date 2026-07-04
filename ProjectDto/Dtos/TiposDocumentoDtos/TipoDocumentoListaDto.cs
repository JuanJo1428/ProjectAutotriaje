namespace ProjectDto.Dtos
{
    public class TipoDocumentoListaDto
    {
        public int IdTipoDocumento { get; set; }

        public string Descripcion { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }
    }
}
