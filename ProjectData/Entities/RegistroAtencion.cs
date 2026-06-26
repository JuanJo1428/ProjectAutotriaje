using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("RegistrosAtencion")]
    public class RegistroAtencion
    {
        [Key]
        public int IdAtencion { get; set; }


        //ForeingKey hacia Paciente
        public int IdPaciente { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        public virtual Paciente Paciente { get; set; }


        public DateTime FechaRegistro { get; set; } = DateTime.Now;


        public int EdadPaciente { get; set; }


        public string MotivoConsulta { get; set; }


        public bool CondicionMaternidad { get; set; }


        public bool CondicionMental { get; set; }


        public bool CondicionOncologica { get; set; }


        public bool Activo {  get; set; }
    }
}