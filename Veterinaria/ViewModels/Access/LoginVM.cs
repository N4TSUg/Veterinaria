using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Access
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ser un formato de correo electrónico válido")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
