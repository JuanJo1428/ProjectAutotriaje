using ProjectData.Entities.PretriajeModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectData.Entities
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


        public bool CondicionMaternidad { get; set; }


        public bool CondicionMental { get; set; }


        public bool CondicionOncologica { get; set; }


        public bool AutotriajeIniciado { get; set; }


        public string MotivoConsulta { get; set; }


        public int? IdPrioridad { get; set; }

        [ForeignKey(nameof(IdPrioridad))]
        public virtual PrioridadPretriaje Prioridad { get; set; }


        public int? IdFlujoClinico { get; set; }

        [ForeignKey(nameof(IdFlujoClinico))]
        public virtual FlujoPretriaje FlujoClinico { get; set; }


        public bool Atendido { get; set; }


        public bool Activo {  get; set; }


        public DateTime? FechaActualizacion { get; set; }
    }
}