using System.Data.Entity;
using ProjectData.Entities;

namespace ProjectData.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<RegistroAtencion> RegistrosAtencion { get; set; }

        public DbSet<Genero> TiposGenero { get; set; }

        public DbSet<TipoDocumento> TiposDocumento { get; set; }
    }
}