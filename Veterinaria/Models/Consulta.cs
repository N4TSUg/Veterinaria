using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConsulta { get; set; }

        [Required(ErrorMessage = "La mascota es obligatoria")]
        public int IdMascota { get; set; }

        [ForeignKey(nameof(IdMascota))]
        public Mascota Mascota { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de la consulta es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow.AddHours(-5);

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio")]
        [StringLength(200)]
        public string Motivo { get; set; } = null!;

        [Required(ErrorMessage = "Los síntomas son obligatorios")]
        [StringLength(500)]
        public string Sintomas { get; set; } = null!;

        [Required(ErrorMessage = "El diagnóstico es obligatorio")]
        [StringLength(500)]
        public string Diagnostico { get; set; } = null!;

        [Required(ErrorMessage = "El tratamiento es obligatorio")]
        [StringLength(500)]
        public string Tratamiento { get; set; } = null!;

        public DateTime? FechaProximaCita { get; set; }

        public List<string>? AdjuntosUrls { get; set; }
    }
}
