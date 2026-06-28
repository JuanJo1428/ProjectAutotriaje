namespace ProjectDto.Dtos
{
    public class CrearPacienteRespuestaDto
    {
        public PacienteDto Paciente { get; set; }

        public bool Creado { get; set; }

        public string Observacion { get; set; } = string.Empty;
    }
}
