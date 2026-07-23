using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities.PretriajeModel
{
    [Table("RespuestasPreguntasPretriaje")]
    public class RespuestaPreguntaPretriaje
    {
        [Key]
        public int IdRespuesta { get; set; }


        public int IdRegistro { get; set; }

        [ForeignKey(nameof(IdRegistro))]
        public virtual RegistroAtencion Registro { get; set; }


        public int IdPregunta { get; set; }

        [ForeignKey(nameof(IdPregunta))]
        public virtual PreguntaPretriaje Pregunta { get; set; }


        public int IdOpcionSeleccionada { get; set; }

        [ForeignKey(nameof(IdOpcionSeleccionada))]
        public virtual OpcionPreguntaPretriaje OpcionSeleccionada { get; set; }


        public bool Activo { get; set; } = true;
    }
}
