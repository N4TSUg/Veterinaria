using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class ControlAntiparasitario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdControlAntiparasitario { get; set; }

        [Required(ErrorMessage = "La mascota es obligatoria")]
        public int IdMascota { get; set; }

        [ForeignKey(nameof(IdMascota))]
        public Mascota Mascota { get; set; } = null!;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow.AddHours(-5);

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.1, 150.0, ErrorMessage = "El peso debe ser mayor a 0")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "El medicamento es obligatorio")]
        [StringLength(100)]
        public string Medicamento { get; set; } = null!;

        [Required(ErrorMessage = "La dosis es obligatoria")]
        [StringLength(100)]
        public string Dosis { get; set; } = null!;
    }
}
