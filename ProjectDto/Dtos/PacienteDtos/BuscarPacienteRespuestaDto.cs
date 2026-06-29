namespace ProjectDto.Dtos
{
    public class BuscarPacienteRespuestaDto
    {
        public PacienteDto Paciente { get; set; }

        public bool Encontrado { get; set; }

        public string Origen { get; set; } = string.Empty;

        public string Observacion { get; set; } = string.Empty;
    }
}
