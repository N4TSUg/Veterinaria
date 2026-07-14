using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class VacunaAplicada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVacunaAplicada { get; set; }

        [Required(ErrorMessage = "La mascota es obligatoria")]
        public int IdMascota { get; set; }

        [ForeignKey(nameof(IdMascota))]
        public Mascota Mascota { get; set; } = null!;

        [Required(ErrorMessage = "La vacuna es obligatoria")]
        public int IdVacuna { get; set; }

        [ForeignKey(nameof(IdVacuna))]
        public Vacuna Vacuna { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de aplicación es obligatoria")]
        public DateTime FechaAplicacion { get; set; } = DateTime.UtcNow.AddHours(-5);

        public DateTime? FechaProximaDosis { get; set; }
    }
}
