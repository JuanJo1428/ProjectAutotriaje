using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("RegistrosAtencion")]
    public class Atencion
    {
        [Key]
        public int IdAtencion { get; set; }

        public int IdPaciente { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int EdadPaciente { get; set; }

        public string MotivoConsulta { get; set; }

        public bool CondicionMatenidad { get; set; }

        public bool CondicionMental { get; set; }

        public bool CondicionOncologica { get; set; }
    }
}