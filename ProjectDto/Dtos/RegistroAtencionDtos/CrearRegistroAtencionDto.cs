namespace ProjectDto.Dtos.RegistroAtencionDtos
{
    public class CrearRegistroAtencionDto
    {
        public int IdPaciente { get; set; }

        public string MotivoConsulta { get; set; }

        public bool CondicionMaternidad { get; set; }

        public bool CondicionMental { get; set; }

        public bool CondicionOncologica { get; set; }

        public bool AutotriajeIniciado { get; set; }
    }
}
