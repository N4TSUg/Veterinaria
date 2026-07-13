using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(20)]
        public string NameRol { get; set; } = null!;

        // Relación 1:N con Usuario
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
