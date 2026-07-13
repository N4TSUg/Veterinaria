using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Mascota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "El cliente (dueño) es obligatorio")]
        public int IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Cliente Cliente { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La especie es obligatoria (Ej: Perro, Gato)")]
        [StringLength(30)]
        public string Especie { get; set; } = null!;

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(50)]
        public string Raza { get; set; } = null!;

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(10)]
        public string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "La edad es obligatoria")]
        [Range(0, 50, ErrorMessage = "La edad debe ser entre 0 y 50 años")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.1, 150.0, ErrorMessage = "El peso debe ser mayor a 0")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Peso { get; set; }

        [StringLength(255)]
        public string? FotoUrl { get; set; }

        // Relaciones 1:N
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
        public ICollection<VacunaAplicada> VacunasAplicadas { get; set; } = new List<VacunaAplicada>();
    }
}
