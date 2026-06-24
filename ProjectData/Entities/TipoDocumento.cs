using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities
{
    [Table("TiposDocumento")]
    public class TipoDocumento
    {
        [Key]
        public int IdTipoDocumento { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public int Activo { get; set; }
    }
}
