namespace ProjectDto.Dtos
{
    public class BuscarPacienteRespuestaDto
    {
        public bool EncontradoAutotriaje { get; set; }

        public bool EncontradoGhips { get; set; }

        public bool Existe { get; set; }

        public PacienteDto PacienteAutotriaje { get; set; }

        public PacienteDto PacienteGhips { get; set; }

        public PacienteDto PacientePrincipal { get; set; }

        public string Observacion { get; set; }
    }
}
