using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities.PretriajeModel
{
    [Table("OpcionesPreguntaPretriaje")]
    public class OpcionPreguntaPretriaje
    {
        [Key]
        public int IdOpcion { get; set; }

       
        public int? IdPregunta { get; set; }

        [ForeignKey(nameof(IdPregunta))]
        public virtual PreguntaPretriaje Pregunta { get; set; }


        [Required]
        [MaxLength(100)]
        public string Texto { get; set; }


        public bool EsGlobal { get; set; }


        public bool Activo { get; set; }
    }
}
