using ProjectCommon.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities.PretriajeModel
{
    [Table("PreguntasPretriaje")]
    public class PreguntaPretriaje
    {
        public PreguntaPretriaje()
        {
            Opciones = new List<OpcionPreguntaPretriaje>();
            Decisiones = new List<DecisionPretriaje>();
        }


        [Key]
        public int IdPregunta { get; set; }


        public int IdFlujo { get; set; }

        [ForeignKey(nameof(IdFlujo))]
        public virtual FlujoPretriaje Flujo { get; set; }


        [Required]
        public string TextoPregunta { get; set; }


        public TipoRespuesta TipoRespuesta { get; set; }


        public bool Activo { get; set; }


        public virtual ICollection<OpcionPreguntaPretriaje> Opciones { get; set; }


        //  Le indicamos a EF que esta lista corresponde a la propiedad "Pregunta" en DecisionPretriaje
        [InverseProperty(nameof(DecisionPretriaje.Pregunta))]
        public virtual ICollection<DecisionPretriaje> Decisiones { get; set; }
    }
}
