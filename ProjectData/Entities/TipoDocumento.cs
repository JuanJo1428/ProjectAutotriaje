using Project.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectData.Entities
{
    [Table("TiposDocumento")]
    public class TipoDocumento
    {
        [Key]
        public int IdTipoDocumento { get; set; }


        public int Codigo { get; set; }


        [Required]
        [MaxLength(5)]
        public string Nombre { get; set; }


        [Required]
        [MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;


        public bool Activo { get; set; }


        // Relacion varios pacientes por cada tipo de documento
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
