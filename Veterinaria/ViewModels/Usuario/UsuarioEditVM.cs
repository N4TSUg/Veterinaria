using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Usuario
{
    public class UsuarioEditVM
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string NameUser { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string EmailUser { get; set; } = null!;

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mínimo 6 caracteres")]
        public string? PasswordUser { get; set; } // Opcional en edición

        [Required(ErrorMessage = "El rol es obligatorio")]
        public int IdRol { get; set; }
    }
}
