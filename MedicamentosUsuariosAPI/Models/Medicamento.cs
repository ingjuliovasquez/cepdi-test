using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentosUsuariosAPI.Models
{
    public class Medicamento
    {
        [Key]
        public int IdMedicamento { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("concentracion")]
        public string Concentracion { get; set; }

        [ForeignKey("formafarmaceutica")]
        public int? IdFormaFarmaceutica { get; set; }

        public FormaFarmaceutica FormaFarmaceutica { get; set; }

        [Column("precio")]
        public decimal? Precio { get; set; }

        [Column("stock")]
        public int? Stock { get; set; }

        [Column("presentacion")]
        public string Presentacion { get; set; }

        [Column("bhabilitado")]
        public int? Habilitado { get; set; }
    }
}
