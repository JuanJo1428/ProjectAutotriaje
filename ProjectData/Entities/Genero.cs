using Project.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectData.Entities
{
    [Table("OpcionesGenero")]
     public class Genero
    {
        [Key]
        public int IdGenero { get; set; }


        [Required]
        public string Descripcion { get; set; } = string.Empty;
        

        public bool Activo { get; set; }


        //Relacion varios pacientes pueden estar en alguno de los diferentes generos
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
