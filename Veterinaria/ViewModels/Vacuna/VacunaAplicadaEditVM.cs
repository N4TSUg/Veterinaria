using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Vacuna
{
    public class VacunaAplicadaEditVM : VacunaAplicadaCreateVM
    {
        [Required]
        public int IdVacunaAplicada { get; set; }
    }
}
