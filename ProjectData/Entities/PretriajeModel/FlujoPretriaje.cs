using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities.PretriajeModel
{
    [Table("FlujosPretriaje")]
    public class FlujoPretriaje
    {
        public FlujoPretriaje()
        {
            Preguntas = new List<PreguntaPretriaje>();
        }

        [Key]
        public int IdFlujo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Codigo { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<PreguntaPretriaje> Preguntas { get; set; }
    }
}
