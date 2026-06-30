namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class ConsultarRegistroPendienteRespuestaDto
    {
        public bool TieneRegistroPendiente { get; set; }

        public bool MostrarOpcionContinuarAutotriaje { get; set; }

        public RegistroAtencionDto RegistroAtencion { get; set; }
    }
}
