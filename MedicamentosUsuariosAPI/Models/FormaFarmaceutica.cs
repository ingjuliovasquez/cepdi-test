using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentosUsuariosAPI.Models
{
    public class FormaFarmaceutica
    {
        [Key]
        [Column("idformafarmaceutica")]
        public int IdFormaFarmaceutica { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("habilitado")]
        public int? Habilitado { get; set; }
    }
}
