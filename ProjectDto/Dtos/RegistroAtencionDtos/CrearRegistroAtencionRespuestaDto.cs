namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class CrearRegistroAtencionRespuestaDto
    {
        public RegistroAtencionDto RegistroAtencion { get; set; }

        public bool Priorizado { get; set; }

        public string Observacion { get; set; } = string.Empty;
    }
}
