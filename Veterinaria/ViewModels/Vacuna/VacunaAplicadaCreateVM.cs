using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Vacuna
{
    public class VacunaAplicadaCreateVM
    {
        [Required]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "La vacuna es obligatoria")]
        public int IdVacuna { get; set; }

        [Required(ErrorMessage = "La fecha de aplicación es obligatoria")]
        public DateTime FechaAplicacion { get; set; } = DateTime.UtcNow.AddHours(-5);

        public DateTime? FechaProximaDosis { get; set; }

        // Para llenar el Dropdown en la vista
        public IEnumerable<SelectListItem>? VacunasDisponibles { get; set; }
    }
}
