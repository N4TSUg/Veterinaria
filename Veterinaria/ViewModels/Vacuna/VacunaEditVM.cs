using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Vacuna
{
    public class VacunaEditVM
    {
        public int IdVacuna { get; set; }

        [Required(ErrorMessage = "El nombre de la vacuna es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de vacuna es obligatorio")]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;
    }
}
