namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class ActualizarRegistroAtencionDto
    {
        public int IdAtencion { get; set; }

        public string MotivoConsulta { get; set; }

        public bool AutotriajeIniciado { get; set; }

        public int IdPrioridad { get; set; }

        public int IdFlujoClinico { get; set; }

        public bool Atendido { get; set; }
    }
}
