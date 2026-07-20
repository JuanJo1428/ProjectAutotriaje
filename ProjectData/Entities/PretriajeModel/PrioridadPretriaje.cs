using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities.PretriajeModel
{
    [Table("PrioridadesPretriaje")]
    public class PrioridadPretriaje
    {
        [Key]
        public int IdPrioridad { get; set; }


        public int NivelPrioridad { get; set; }


        [Required]
        public string Color { get; set; }


        [Required]
        public string Codigo { get; set; }


        public int? TiempoMaximoAtencion { get; set; }


        public bool Activo { get; set; }
    }
}