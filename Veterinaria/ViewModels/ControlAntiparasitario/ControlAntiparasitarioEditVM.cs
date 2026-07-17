using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.ControlAntiparasitario
{
    public class ControlAntiparasitarioEditVM
    {
        [Required]
        public int IdControlAntiparasitario { get; set; }

        [Required]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.1, 150.0, ErrorMessage = "El peso debe ser mayor a 0")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "El medicamento es obligatorio")]
        [StringLength(100)]
        public string Medicamento { get; set; } = null!;

        [Required(ErrorMessage = "La dosis es obligatoria")]
        [StringLength(100)]
        public string Dosis { get; set; } = null!;
    }
}
