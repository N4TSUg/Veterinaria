using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string Apellido { get; set; } = null!;

        [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener 8 dígitos")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El DNI solo debe contener números")]
        public string? DNI { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo debe contener números")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(150)]
        public string Direccion { get; set; } = null!;

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Debe ser un formato de correo electrónico válido")]
        public string? Correo { get; set; }

        // Relación 1:N con Mascota
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
