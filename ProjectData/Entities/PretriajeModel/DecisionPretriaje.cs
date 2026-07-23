using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities.PretriajeModel
{
    [Table("DecisionesPretriaje")]
    public class DecisionPretriaje
    {
        [Key]
        public int IdDecision { get; set; }


        public int IdPregunta { get; set; }

        [ForeignKey(nameof(IdPregunta))]
        public virtual PreguntaPretriaje Pregunta { get; set; }


        public int IdOpcionEsperada { get; set; }

        [ForeignKey(nameof(IdOpcionEsperada))]
        public virtual OpcionPreguntaPretriaje OpcionEsperada { get; set; }


        public bool FinalizaFlujo { get; set; }


        public int? IdPreguntaSiguiente { get; set; }

        [ForeignKey(nameof(IdPreguntaSiguiente))]
        public virtual PreguntaPretriaje PreguntaSiguiente { get; set; }


        public int? IdPrioridad { get; set; }

        [ForeignKey(nameof(IdPrioridad))]
        public virtual PrioridadPretriaje Prioridad { get; set; }


        public bool EsActivo { get; set; }
    }
}
