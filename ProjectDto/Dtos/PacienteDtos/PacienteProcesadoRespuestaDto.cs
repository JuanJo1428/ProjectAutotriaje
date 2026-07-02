namespace ProjectDto.Dtos
{
    public class PacienteProcesadoRespuestaDto
    {
        public PacienteDto Paciente { get; set; }

        public string AccionRealizada { get; set; } = string.Empty;

        public bool NotificarGhips { get; set; }

        public string ObservacionGhips { get; set; } = string.Empty;

        public string ObservacionAutotriaje { get; set; } = string.Empty;
    }
}

