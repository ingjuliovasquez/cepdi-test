using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentosUsuariosAPI.Models
{
    public class Usuario
    {
        [Key]
        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("fechacreacion")]
        public DateTime? FechaCreacion { get; set; }

        [Column("usuario")]
        public string NombreUsuario { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("estatus")]
        public int? Estatus { get; set; }
    }
}
