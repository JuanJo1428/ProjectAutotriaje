namespace ProjectDto.Dtos.PretriajeDtos
{
    public class ResultadoPretriajeDto
    {
        public bool Finalizado { get; set; }

        public int? Prioridad { get; set; }

        public string Mensaje { get; set; }

        public PreguntaPretriajeDto SiguientePregunta { get; set; }
    }
}
