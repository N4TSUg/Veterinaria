using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Vacuna
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVacuna { get; set; }

        [Required(ErrorMessage = "El nombre de la vacuna es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de la vacuna es obligatorio")]
        [StringLength(50)]
        public string Tipo { get; set; } = null!;

        // Relación 1:N
        public ICollection<VacunaAplicada> VacunasAplicadas { get; set; } = new List<VacunaAplicada>();
    }
}
