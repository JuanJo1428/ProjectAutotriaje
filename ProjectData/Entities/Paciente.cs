using ProjectData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    [Table("PacientesGhips")]
    public class Paciente

    {
        [Key]
        public int IdPaciente { get; set; }


        // ForeingKey hacia TipoDocumento
        public int IdTipoDocumento { get; set; }

        [ForeignKey(nameof(IdTipoDocumento))]
        public virtual TipoDocumento TipoDocumento { get; set; }

        //Relacion muchos registros para un solo paciente
        public virtual ICollection<RegistroAtencion> Atenciones { get; set; }


        [Required]
        [MaxLength(20)]
        public string NroDocumento { get; set; } = string.Empty;


        [Required]
        public string PrimerNombre { get; set; } = string.Empty;


        public string SegundoNombre { get; set; }


        [Required]
        public string PrimerApellido { get; set; } = string.Empty;


        public string SegundoApellido { get; set; }


        [Required]
        public string Sexo { get; set; } = string.Empty;


        public DateTime FechaNacimiento { get; set; }


        public bool Activo { get; set; }

    }
}