using System.Data.Entity;
using ProjectData.Entities;
using ProjectData.Entities.PretriajeModel;

namespace ProjectData.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<RegistroAtencion> RegistrosAtencion { get; set; }

        public DbSet<Genero> OpcionesGenero { get; set; }

        public DbSet<TipoDocumento> TiposDocumento { get; set; }

        public DbSet<FlujoPretriaje> FlujosPretriaje { get; set; }

        public DbSet<PreguntaPretriaje> PreguntasPretriaje { get; set; }

        public DbSet<DecisionPretriaje> DecisionesPretriaje { get; set; }

        public DbSet<OpcionPreguntaPretriaje> OpcionesPreguntaPretriaje { get; set; }

        public DbSet<PrioridadPretriaje> PrioridadesPretriaje { get; set; }
    }
}