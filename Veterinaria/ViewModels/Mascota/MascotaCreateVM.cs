using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Mascota
{
    public class MascotaCreateVM
    {
        [Required(ErrorMessage = "El cliente (dueño) es obligatorio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La especie es obligatoria (Ej: Perro, Gato)")]
        [StringLength(30)]
        public string Especie { get; set; } = null!;

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(50)]
        public string Raza { get; set; } = null!;

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(30)]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(10)]
        public string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; } = DateTime.UtcNow.AddHours(-5).AddYears(-1);

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow.AddHours(-5);

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.1, 150.0, ErrorMessage = "El peso debe ser mayor a 0")]
        public decimal Peso { get; set; }

        public IFormFile? Foto { get; set; }

        // Para llenar el Dropdown en la vista
        public IEnumerable<SelectListItem>? Clientes { get; set; }
    }
}
