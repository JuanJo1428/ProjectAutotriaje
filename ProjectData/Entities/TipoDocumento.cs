using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Entities
{
    [Table("TiposDocumento")]
    public class TipoDocumento
    {
        [Key]
        public int IdTipoDocumento { get; set; }


        public int Codigo { get; set; }


        [Required]
        [MaxLength(3)]
        public string Nombre { get; set; }


        [Required]
        public string Descripcion { get; set; } = string.Empty;


        public bool Activo { get; set; }


        // Relacion varios pacientes por cada tipo de documento
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
