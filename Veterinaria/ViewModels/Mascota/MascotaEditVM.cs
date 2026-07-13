using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Veterinaria.ViewModels.Mascota
{
    public class MascotaEditVM : MascotaCreateVM
    {
        [Required]
        public int IdMascota { get; set; }

        public string? FotoUrl { get; set; }
    }
}
