namespace ProjectDto.Dtos
{
    public class PacienteProcesadoRespuestaDto
    {
        public PacienteDto Paciente { get; set; }

        public string AccionRealizada { get; set; } = string.Empty;

        public bool NotificarGhips { get; set; }

        public string Observacion { get; set; } = string.Empty;
    }
}

