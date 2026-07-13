using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string NameUser { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Debe ser un formato de correo electrónico válido")]
        public string EmailUser { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255)] // Largo de 255 para almacenar el Hash correctamente
        public string PasswordUser { get; set; } = null!;

        // Relación con Rol
        [Required(ErrorMessage = "El rol es obligatorio")]
        public int IdRol { get; set; }
        
        [ForeignKey(nameof(IdRol))]
        public Rol Rol { get; set; } = null!;
    }
}
